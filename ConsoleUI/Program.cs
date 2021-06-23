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

            PlayerInfoModel activePlayer = CreatePlayer("Player 1");
            PlayerInfoModel oppenent = CreatePlayer("Player 2");

            PlayerInfoModel winner = null;
            do
            {
                
                DisplayShotGrid(activePlayer);

                RecordPlayerShot(activePlayer, oppenent);

                bool doesGameContinue = GameLogic.PlayerStillActive(oppenent);

                if (doesGameContinue == true)
                {
                    /*temp variable
                    PlayerInfoModel tempHolder = opponent;
                    opponent = activePlayer;
                    activePlayer = tempHolder;*/

                    //Use Tuple
                    (activePlayer, oppenent) = (oppenent, activePlayer);

                }
                else
                {
                    winner = activePlayer;
                }

            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();            
        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations to { winner.UserName } for winning!");
            Console.WriteLine($"{ winner.UserName } took { GameLogic.GetShotCount(winner) } shots.");
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel oppenent)
        {
            bool isValidShot = false;
            string row = "";
            int column = 0;
            do
            {
                string shot = AskForShot();
                (row,  column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                isValidShot = GameLogic.ValidateShot(activePlayer, row, column);

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid shot location, please try again.");
                }
            } while (isValidShot == false);

            
            bool isAHit = GameLogic.IdentifyShotResults(oppenent, row, column);

            GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
        }

        private static string AskForShot()
        {
            Console.Write("Please enter your shot selection: ");
            string output = Console.ReadLine();
            return output;
        }

        //TODO update grid to be lined up
        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }                

                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($"{ gridSpot.SpotLetter }{ gridSpot.SpotNumber }");
                }   
                
                else if(gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }
                else if(gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write( " O ");
                }
                else
                {
                    Console.Write(" ? ");
                }
            }
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

            output.UserName = AskForUserName();

            GameLogic.InitializeGrid(output);

            PlaceShips(output);

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
