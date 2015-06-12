// Bradley Eliott and David Brad
// Game.cs
// Main logic for our game.
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using System.IO;

namespace MastermindLibrary
{
    public class Game : MarshalByRefObject
    {
        // Static members
        public static int MAX_GUESSES = 10;
        public static int NUM_PEGS = 4;
        public static int NUM_COLOURS = 7;

        // Private members
        private StateInfo info = new StateInfo();
        private Random rnd = new Random();
        private MMRow soln = new MMRow();

        private Dictionary<string, ICallBack> clientCallbacks = new Dictionary<string, ICallBack>();

        // Constructor
        public Game()
        {
            Console.WriteLine("Constructing a new Game object.");
            info = new StateInfo();

            Next();
        }

        // Generate a new sequence of pegs to be guessed at by players
        public void Next()
        {
            soln.pegs.Clear();

            Console.WriteLine("Selecting a new Row...");
            for (int i = 0; i < NUM_PEGS; ++i)
            {
                soln.pegs.Add(soln.pegs.Count, rnd.Next(NUM_COLOURS));
            }

            // clear old game state data
            info.guesses = new Hashtable();
            info.status = StateInfo.StatusType.Playing;

            updateAllClients();
        }

        // Join a new player to the game
        public StateInfo Join(string name, ICallBack callback)
        {
            try
            {
                clientCallbacks.Add(name, callback);
                Console.WriteLine("  -- Player [" + name + "] Connected!");
                if (info.playerTurn == "")
                    info.playerTurn = name;
                return info;
            }
            catch
            {
                return null;
            }
        }

        // Remove a player from the game
        public void Leave(string name)
        {
            try
            {
                List<string> players = clientCallbacks.Keys.ToList();

                clientCallbacks.Remove(name);

                // Update the current player if the person leaving is the current player
                //      or the only player
                if (info.playerTurn == name && clientCallbacks.Count == 0)
                    info.playerTurn = "";
                else if (info.playerTurn == name)
                    info.playerTurn = players[ (players.IndexOf(name) + 1) % players.Count];
               
                Console.WriteLine("  -- Player [" + name + "] Disconnected!");
                updateAllClients();
            }
            catch (Exception ex)
            {
                Console.WriteLine("  -- Unknown Player Disconnected (" + ex.Message + ")");
            }
        }

        // Update all clients with the current game state
        private void updateAllClients()
        {
            Console.Write("Updating Clients...");
            foreach (ICallBack callback in clientCallbacks.Values)
            {
                callback.GameState(info);
            }
            Console.Write("Clients Updated!\n");
        }

        // A player submits a guess
        public void submitGuess(MMRow guess)
        {
            // list of current players, used to determine the next player
            List<string> players = clientCallbacks.Keys.ToList();

            Console.WriteLine("  -- Guess Submited...");
            
            // Determine the perfect pegs
            List<int> perfect = new List<int>();
            for (int i = 0; i < soln.pegs.Count; ++i)
            {
                if ((int)guess.pegs[i] == (int)soln.pegs[i])
                {
                    // a peg is perfect
                    guess.results.Add(guess.results.Count, MMRow.marks.Perfect);
                    perfect.Add(i);
                }
                if (perfect.Count == soln.pegs.Count)
                {
                    // all pegs are perfect, game is over
                    Console.WriteLine("!!Victory!!");
                    info.status = StateInfo.StatusType.Won;
                    info.guesses.Add(info.guesses.Count, guess);
                    updateAllClients();
                    Next();
                    return;                    
                }
            }

            // Determine all pegs that are the right colour, but in the wrong place
            List<int> marked = new List<int>();
            for (int i = 0; i < guess.pegs.Count; ++i)
            {
                if (perfect.Contains(i))
                    continue;
                for (int j = 0; j < soln.pegs.Count; ++j)
                {
                    if (!perfect.Contains(j) && !marked.Contains(j) && (int)guess.pegs[i] == (int)soln.pegs[j])
                    {
                        guess.results.Add(guess.results.Count, MMRow.marks.Right_Colour);
                        marked.Add(j);
                        break;
                    }
                }
            }

            // add this guess to the hash of guesses
            info.guesses.Add(info.guesses.Count, guess);

            if (info.guesses.Count >= MAX_GUESSES)
            {
                // the players have exceeded the maximum number of guesses
                Console.WriteLine("!!Defeat!!");
                info.status = StateInfo.StatusType.Lost;
                updateAllClients();
                Next();
                return;
            }

            // determine the next player
            info.playerTurn = players[(players.IndexOf(info.playerTurn) + 1) % players.Count];

            updateAllClients();
        }

        // A function just to use the library
        public bool Ping()
        {
            return true;
        }
    }
}