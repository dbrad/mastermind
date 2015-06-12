// Bradley Elliott and David Brad
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

using MastermindLibrary;

namespace MastermindClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Create a reference to a Masterind Game object
        private Game game;
        private int guessNumber = 0;

        // ctor
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                //Load the remoting configuration file
                RemotingConfiguration.Configure("MastermindClient.exe.config", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // the delegate for updating
        private delegate void ClientUpdateDelegate(StateInfo info);

        public void UpdateClientWindow(StateInfo info)
        {
            this.Dispatcher.BeginInvoke(new ClientUpdateDelegate(updateClientWindow), info);
        }

        // being updated with passed in game state information
        private void updateClientWindow(StateInfo info)
        {
            // clear any old state data
            Reset();

            // update current player label
            lblPlayer.Content = info.playerTurn;

            // only allow current player to submit
            btnSubmit.IsEnabled = (info.playerTurn == txtName.Text);

            // reflect game state onto GUI board
            for (int u = 0; u < info.guesses.Count; ++u)
            {
                if (((MMRow)info.guesses[u]).results.Count > 0)
                {
                    for (int m = 0; m < ((MMRow)info.guesses[u]).results.Count; ++m)
                    {
                        if ((MMRow.marks)((MMRow)info.guesses[u]).results[m] == MMRow.marks.Perfect)
                            ((PegRow)gridPegs.Children[u]).getResultPegs()[m].SetPerfect();
                        else if ((MMRow.marks)((MMRow)info.guesses[u]).results[m] == MMRow.marks.Right_Colour)
                            ((PegRow)gridPegs.Children[u]).getResultPegs()[m].SetCorrectColour();
                    }
                }

                for (int p = 0; p < ((MMRow)info.guesses[u]).pegs.Count; ++p)
                {
                    ((PegRow)gridPegs.Children[u]).getPegs()[p].setColour((int)((MMRow)info.guesses[u]).pegs[p]);
                }
            }

            ((PegRow)gridPegs.Children[guessNumber]).isLocked = true;
            guessNumber = info.guesses.Count;

            // determine if game is over and if not, unlock next row
            if (info.status == StateInfo.StatusType.Won)
            {
                lblResult.Content = "VICTORY!!";
                guessNumber = 0;
            }
            else if (info.status == StateInfo.StatusType.Lost)
            {
                lblResult.Content = "DEFEAT!";
                guessNumber = 0;
            }
            else if (btnSubmit.IsEnabled)
                ((PegRow)gridPegs.Children[guessNumber]).isLocked = false;
        }

        // server button clicked
        private void btnServer_Click(object sender, RoutedEventArgs e)
        {
            // attempt to connect to the given server (from the text box)
            try
            {
                game = (Game)Activator.GetObject(typeof(Game), "http://" + txtServer.Text + ":42000/game.soap");

                // if the game is valid, ask for the player's identity
                if (game.Ping())
                {
                    lblServer.Visibility = System.Windows.Visibility.Hidden;
                    txtServer.Visibility = System.Windows.Visibility.Hidden;
                    btnServer.Visibility = System.Windows.Visibility.Hidden;

                    lblName.Visibility = System.Windows.Visibility.Visible;
                    txtName.Visibility = System.Windows.Visibility.Visible;
                    btnJoin.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // join button clicked
        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            // if the player entered an ID
            if (txtName.Text.Length > 0)
            {
                try
                {
                    // attempt to join the game with the ID
                    StateInfo info = game.Join(txtName.Text, new CallBack(this));

                    // if successful
                    if (info != null)
                    {
                        // create board
                        for (int i = 0; i < Game.MAX_GUESSES; ++i)
                        {
                            RowDefinition rowDef = new RowDefinition();
                            rowDef.Height = GridLength.Auto;
                            gridPegs.RowDefinitions.Add(rowDef);

                            PegRow pegRow = new PegRow(Game.NUM_PEGS);
                            pegRow.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                            gridPegs.Children.Add(pegRow);
                            Grid.SetRow(pegRow, i);
                        }

                        // update board
                        updateClientWindow(info);

                        // add player's ID to the title bar
                        this.Title = "(" + txtName.Text + ") " + this.Title;

                        // Enable/Disable controls for game play
                        btnJoin.IsEnabled = false;
                        txtName.IsEnabled = false;
                        connectBox.Visibility = System.Windows.Visibility.Hidden;
                        btnSubmit.Visibility = System.Windows.Visibility.Visible;
                        lblTurn.Visibility = System.Windows.Visibility.Visible;
                        lblPlayer.Visibility = System.Windows.Visibility.Visible;

                    }
                    else
                        MessageBox.Show("There already is a player called '" + txtName.Text + "'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " : " + ex.InnerException);
                }
            }
        }

        // the submit button is clicked (a player is submitting a guess)
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = "";
            MMRow guess = new MMRow();

            foreach (Peg p in ((PegRow)gridPegs.Children[guessNumber]).getPegs())
            {
                // must have a complete guess
                if ((int)p.Tag == -1)
                {
                    MessageBox.Show("You must select a colour for every slot!");
                    return;
                }
                guess.pegs.Add(guess.pegs.Count, (int)p.Tag);
            }
            try
            {
                game.submitGuess(guess);
            }
            catch
            {
                MessageBox.Show("Something went wrong with submitting your guess!");
            }
        }

        // the window is closing
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                // leave the game
                if(txtName.Text != "")
                    game.Leave(txtName.Text);
            }
            catch { }
        }

        // reset the game board
        private void Reset()
        {
            // clear all colours
            for (int u = 0; u < Game.MAX_GUESSES; ++u)
            {
                for (int p = 0; p < ((PegRow)gridPegs.Children[u]).getPegs().Count; ++p)
                {
                    ((PegRow)gridPegs.Children[u]).getPegs()[p].setColour(-1);
                }
                for (int m = 0; m < ((PegRow)gridPegs.Children[u]).getResultPegs().Count; ++m)
                {
                    ((PegRow)gridPegs.Children[u]).getResultPegs()[m].SetBlank();
                }
            }

        }
    }
}
