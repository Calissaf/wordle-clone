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
        public string connectionString = @"Server=127.0.0.1;Database=wordlist;Uid=wordleuser;Pwd=Firebrand.1;";

        public void generateComputerChoice() {
            int wordListLength = 0;
            int numberChoice = 0;
            string columnLengthQuery = "SELECT COUNT(id) FROM allowed_words;";

            // open database connection
            using (var connection = new MySqlConnection(this.connectionString))
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

                        }
                    }
                }

                Random randomNumber = new Random();
                numberChoice = randomNumber.Next(1,wordListLength);

                string computerChoiceQuery = $"SELECT word FROM allowed_words WHERE id = {numberChoice};";

                using (var command = new MySqlCommand(computerChoiceQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.computerChoice = reader.GetString(0);

                            // System.Console.WriteLine($"computer choice: {this.computerChoice}");
                        }
                    }
                }

                connection.Close();
            }
        }

        public void generateUserChoice() {

            System.Console.WriteLine("Enter a 5 letter word: ");
            string userInput = Console.ReadLine();
            int wordID = 0;

            if (userInput.Length == 5 && userInput.All(Char.IsLetter)) {

                userInput = userInput.ToLower();

                // System.Console.WriteLine($"user input: {userInput}");

                string wordExistQuery = $"SELECT id FROM allowed_words WHERE word = '{userInput}';";

                // open database connection
                using (var connection = new MySqlConnection(this.connectionString))
                {
                    connection.Open();

                    // get word id
                    using (var command = new MySqlCommand(wordExistQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                wordID = Convert.ToInt32(reader.GetString(0));
                            }
                        }
                    }
                    connection.Close();
                }

                // System.Console.WriteLine($"word id: {wordID}");

                if (wordID > 0) {
                    this.userChoice = userInput;
                    this.attemps++;
                    // System.Console.WriteLine(this.userChoice);
                } else {
                    System.Console.WriteLine("word not recognised");
                    this.generateUserChoice();
                }
            } else {
                System.Console.WriteLine("input doesn't meet requirements");
                this.generateUserChoice();
            }
        }

        public void checkWinner() {
            if (this.userChoice.Equals(this.computerChoice)) {
                System.Console.WriteLine("Winner!!");
                System.Console.WriteLine($"You took {this.attemps} tries");
                return;
            } 

            int sumTotal = 0;
            char[] userChoiceArray = this.userChoice.ToCharArray();
            char[] computerChoiceArray = this.computerChoice.ToCharArray();
            int[] winPositions = new int[5] {0,0,0,0,0};
            string[] winPositionColors = new string[5] {"grey", "grey", "grey", "grey", "grey"};
            char[] checkedLetters = new char[5] {' ', ' ', ' ', ' ', ' '};

            for (int i = 0; i < userChoiceArray.Length; i++)
            {
                if (userChoiceArray[i] == computerChoiceArray[i]) {
                    winPositions[i] = 2;
                    winPositionColors[i] = "green";
                    if (checkedLetters.Contains(userChoiceArray[i]) == false) {
                        checkedLetters[i] = userChoiceArray[i];
                    }
                } 
            }
            
            for (int j = 0; j < userChoiceArray.Length; j++)
            {
                int letterOccurencesInComputerChoice = this.computerChoice.Count(f => (f == userChoiceArray[j]));
                if (this.computerChoice.Contains(userChoiceArray[j]) && checkedLetters.Contains(userChoiceArray[j]) == false || this.computerChoice.Contains(userChoiceArray[j]) && checkedLetters.Contains(userChoiceArray[j]) == true && letterOccurencesInComputerChoice > 1 && winPositions[j] == 0) {
                    winPositions[j] = 1;
                    winPositionColors[j] = "yellow";
                }
                sumTotal += winPositions[j];
                System.Console.WriteLine($"letter: {userChoiceArray[j]}, color: {winPositionColors[j]}");
            }

            if (sumTotal == 10)
            {
                System.Console.WriteLine("Winner!!");
                return;
            } else if (this.attemps < this.maxAttempts) {
                System.Console.WriteLine($"{this.attemps} attempt, {(this.maxAttempts - this.attemps)} attemps remaining"); // needs refining as get to 0 attempts and allows one more
                this.generateUserChoice();
                this.checkWinner();
            } else {
                System.Console.WriteLine($"no attempts left word was: {this.computerChoice}");
                return;
            }


        }
    }
}
