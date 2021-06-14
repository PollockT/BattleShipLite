using System;
using BatteshipLiteLib.Models;
using BatteshipLiteLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BattleshipLite
{
    class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            PlayerInfoModel player1 = CreatePlayer("Player 1");
            PlayerInfoModel player2 = CreatePlayer("Player 2");
            

            Console.ReadLine();
            Environment.Exit(0);
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite");
            Console.WriteLine("Created by Theodore Pollock\n");
        }

        private static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel();
            Console.WriteLine($"Player information for {playerTitle}");
            // TODO: Ask user for name
            output.UserName = AskForUserName();

            // TODO: load up the shot grid
            GameLogic.InitializeGrid(output);

            // TODO: ask user for ship placement
            PlaceShips(output);

            // TODO: clear screan
            Console.Clear();

            return output;
        }

        private static string AskForUserName()
        {
            Console.Write("What is your name: ");
            string output = Console.ReadLine();
            return output;
        }

        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.Write($"Where do you want to place ship {model.ShipLocations.Count + 1} : ");
                string location = Console.ReadLine();

                bool isValidLocation = GameLogic.PlaceShip(model, location);

                if  (isValidLocation == false)
                {
                    Console.WriteLine("That was an invalid location, please try again.");
                }
            } while (model.ShipLocations.Count < 5);
        }

    }
}
