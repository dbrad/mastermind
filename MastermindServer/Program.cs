// Bradley Elliott and David Brad
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

using MastermindLibrary;

namespace MastermindServer
{
    class Program
    {
        // basic server
        static void Main(string[] args)
        {

            RemotingConfiguration.Configure("MastermindServer.exe.config", false);

            // Keep the server running until <Enter> is pressed
            Console.WriteLine("Mastermind server is running. Press <Enter> to quit.");
            Console.ReadLine();

        }
    }
}
