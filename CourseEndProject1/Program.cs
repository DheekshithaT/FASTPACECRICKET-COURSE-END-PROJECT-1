using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseEndProject1
{
    public class CricketPlayer
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerAge { get; set; }
    }

    public interface ICricketTeam
    {
        void EnlistPlayer(CricketPlayer player);
        void ExpelPlayer(int playerId);
        CricketPlayer GetPlayerById(int playerId);
        CricketPlayer GetPlayerByName(string playerName);
        List<CricketPlayer> GetAllPlayers();
    }

    public class CricketTeam : ICricketTeam
    {
        private List<CricketPlayer> teamPlayers = new List<CricketPlayer>();
        private const int TeamCapacity = 11;

        public void EnlistPlayer(CricketPlayer player)
        {
            if (teamPlayers.Count < TeamCapacity)
            {
                teamPlayers.Add(player);
                Console.WriteLine("Player enlisted successfully!");
            }
            else
            {
                Console.WriteLine("Cannot enlist more than 11 players to the team.");
            }
        }

        public void ExpelPlayer(int playerId)
        {
            CricketPlayer playerToExpel = teamPlayers.Find(p => p.PlayerId == playerId);
            if (playerToExpel != null)
            {
                teamPlayers.Remove(playerToExpel);
                Console.WriteLine("Player expelled successfully.");
            }
            else
            {
                Console.WriteLine("Player not found.");
            }
        }

        public CricketPlayer GetPlayerById(int playerId)
        {
            return teamPlayers.Find(p => p.PlayerId == playerId);
        }

        public CricketPlayer GetPlayerByName(string playerName)
        {
            return teamPlayers.Find(p => p.PlayerName == playerName);
        }

        public List<CricketPlayer> GetAllPlayers()
        {
            return new List<CricketPlayer>(teamPlayers);
        }
    }

    class Program
    {
        private readonly ICricketTeam cricketTeam;

        public Program(ICricketTeam cricketTeam)
        {
            this.cricketTeam = cricketTeam;
        }

        static void Main()
        {
            ICricketTeam cricketTeam = new CricketTeam();
            Program program = new Program(cricketTeam);

            while (true)
            {
                DisplayMenu();

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            program.EnlistPlayer();
                            break;
                        case 2:
                            program.ExpelPlayer();
                            break;
                        case 3:
                            program.GetPlayerById();
                            break;
                        case 4:
                            program.GetPlayerByName();
                            break;
                        case 5:
                            program.GetAllPlayers();
                            break;
                        default:
                            Console.WriteLine("PLEASE CHOOSE FROM THE OPTIONS LISTED.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("***INVALID, PLEASE ENTER A NUMBER***");
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("SELECT THE BELOW OPTIONS: \n 1: To Add Player \n 2: To Delete Player by ID \n 3: Get Player by ID \n 4. Get Player by Name \n 5. Get All Players ");
        }
        private void EnlistPlayer()
        {
            CricketPlayer newPlayer = ReadPlayerDetailsFromUser();
            cricketTeam.EnlistPlayer(newPlayer);
        }

        private void ExpelPlayer()
        {
            int playerIdToExpel = ReadPlayerIdFromUser();
            cricketTeam.ExpelPlayer(playerIdToExpel);
        }

        private void GetPlayerById()
        {
            int playerIdToGetById = ReadPlayerIdFromUser();
            CricketPlayer playerById = cricketTeam.GetPlayerById(playerIdToGetById);
            DisplayPlayerDetails(playerById);
        }

        private void GetPlayerByName()
        {
            string playerNameToGet = ReadPlayerNameFromUser();
            CricketPlayer playerByName = cricketTeam.GetPlayerByName(playerNameToGet);
            DisplayPlayerDetails(playerByName);
        }

        private void GetAllPlayers()
        {
            List<CricketPlayer> allPlayers = cricketTeam.GetAllPlayers();
            DisplayAllPlayersDetails(allPlayers);
        }

        private static CricketPlayer ReadPlayerDetailsFromUser()
        {
            int playerId;
            while (true)
            {
                Console.WriteLine("Enter Player Id:");
                if (int.TryParse(Console.ReadLine(), out playerId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("***PLEASE ENTER A NUMBER FOR ID***");
                }
            }

            Console.WriteLine("Enter Player Name:");
            string playerName = Console.ReadLine();

            int playerAge;
            while (true)
            {
                Console.WriteLine("Enter Player Age:");
                if (int.TryParse(Console.ReadLine(), out playerAge))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("*** PLEASE ENTER A NUMBER FOR AGE***");
                }
            }

            return new CricketPlayer { PlayerId = playerId, PlayerName = playerName, PlayerAge = playerAge };
        }

        private static int ReadPlayerIdFromUser()
        {
            int playerId;
            while (true)
            {
                Console.WriteLine("Enter Player Id:");
                if (int.TryParse(Console.ReadLine(), out playerId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("***PLEASE ENTER A NUMBER FOR AGE***");
                }
            }
            return playerId;
        }

        private static string ReadPlayerNameFromUser()
        {
            Console.WriteLine("Enter Player Name:");
            return Console.ReadLine();
        }

        private static void DisplayPlayerDetails(CricketPlayer player)
        {
            if (player != null)
            {
                Console.WriteLine($"Player Id: {player.PlayerId}, Name: {player.PlayerName}, Age: {player.PlayerAge}");
            }
            else
            {
                Console.WriteLine("Player not found.");
            }
        }

        private static void DisplayAllPlayersDetails(List<CricketPlayer> players)
        {
            if (players != null && players.Count > 0)
            {
                foreach (var player in players)
                {
                    DisplayPlayerDetails(player);
                }
            }
            else
            {
                Console.WriteLine("No players found.");
            }
        }
    }
}
