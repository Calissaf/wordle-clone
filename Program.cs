using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WordleClone
{
    class Program
    {
        static void Main(string[] args)
        {

            Words test = new Words();
            test.generateComputerChoice();
            test.generateUserChoice();
            test.checkWinner();

            Console.Read();

            // bool keepPlaying = true;

            // while (keepPlaying)
            // {
            // Words test = new Words();
            // test.generateComputerChoice();
            // test.generateUserChoice();
            // test.checkWinner();

            // Console.Read();
                
            // keepPlaying = checkKeepPlaying(keepPlaying);

            // }

        }
        // static bool checkKeepPlaying(bool keepPlaying) {

        //     System.Console.WriteLine("Do you want to play again? (y/n)");
        //     string keepPlayingResponse = Console.ReadLine();

        //     if (keepPlayingResponse == "y")
        //     {
        //        keepPlaying = true;
        //        System.Console.WriteLine(keepPlaying);
        //        return keepPlaying;
        
        //     } else if (keepPlayingResponse == "n")
        //     {
        //         keepPlaying = false;
        //         System.Console.WriteLine(keepPlaying);
        //         return keepPlaying;
        //     } else {
        //         System.Console.WriteLine("Input not recognised");
        //         checkKeepPlaying(keepPlaying);
        //     }

        //     System.Console.WriteLine(keepPlaying);
        //     return false;
        // }

    }
}