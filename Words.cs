using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WordleClone
{
    public class Words
    {
        public string userChoice {get; private set;} = "";
        public string computerChoice {get; private set;} = "";
        public int attemps {get; private set;} = 0;

        public int maxAttempts {get; private set;} = 5;

        public void generateComputerChoice() {
            int wordListLength = 0;
            int numberChoice = 0;
            string connectionString = @"Server=127.0.0.1;Database=wordlist;Uid=wordleuser;Pwd=Firebrand.1;";
            string columnLengthQuery = "SELECT COUNT(id) FROM words;";

            // open database connection
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // get column length
                using (var command = new MySqlCommand(columnLengthQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wordListLength = Convert.ToInt32(reader.GetString(0));

                            // System.Console.WriteLine($"{wordListLength}");
                        }
                    }
                }

            }

            Random randomNumber = new Random();
            numberChoice = randomNumber.Next(1,wordListLength);

           // System.Console.WriteLine(numberChoice);

            string computerChoiceQuery = $"SELECT word FROM words WHERE id = {numberChoice};";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // get column length
                using (var command = new MySqlCommand(computerChoiceQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.computerChoice = reader.GetString(0);

                            System.Console.WriteLine($"computer choice: {this.computerChoice}");
                        }
                    }
                }

            }
        }

        public void generateUserChoice() {

            System.Console.WriteLine("Enter a 5 letter word: ");
            string userInput = Console.ReadLine();

            if (userInput.Length == 5 && userInput.All(Char.IsLetter)) {
                this.userChoice = userInput.ToLower();
                System.Console.WriteLine(this.userChoice);
            } else {
                System.Console.WriteLine("input doesn't meet requirements");
                this.generateUserChoice();
            }
        }

        public void checkWinner() {
            if (this.userChoice.Equals(this.computerChoice)) {
                System.Console.WriteLine("Winner!!");
                System.Console.WriteLine($"You took {this.attemps + 1} tries");
                return;
            } 

            int sumTotal = 0;
            char[] userChoiceArray = this.userChoice.ToCharArray();
            char[] computerChoiceArray = this.computerChoice.ToCharArray();
            int[] winPositions = new int[5] {0,0,0,0,0};
            string[] winPositionColors = new string[5] {"grey", "grey", "grey", "grey", "grey"};

            for (int i = 0; i < userChoiceArray.Length; i++)
            {
                if (userChoiceArray[i] == computerChoiceArray[i]) {
                    winPositions[i] = 2;
                    winPositionColors[i] = "green";
                } else if (this.computerChoice.Contains(userChoiceArray[i])) {
                    winPositions[i] = 1;
                    winPositionColors[i] = "yellow";
                }

                sumTotal += winPositions[i];
                System.Console.WriteLine($"letter: {userChoiceArray[i]}, color: {winPositionColors[i]}");
            }

            if (sumTotal == 10)
            {
                System.Console.WriteLine("Winner!!");
            } else if (this.attemps < this.maxAttempts) {
                this.attemps++;
                System.Console.WriteLine($"{this.attemps} attempt, {(this.maxAttempts - this.attemps)} attemps remaining"); // needs refining as get to 0 attempts and allows one more
                System.Console.WriteLine($"sum total: {sumTotal}");
                this.generateUserChoice();
                this.checkWinner();
            } else {
                System.Console.WriteLine($"no attempts left word was: {this.computerChoice}");
            }


        }
    }
}