using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static string PromptForString(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        static string PromptForMenuString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine().ToUpper();
        }

        static int PromptForInteger(string prompt)

        {
            Console.Write(prompt);
            int userInput;
            if (Int32.TryParse(Console.ReadLine(), out userInput))
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input. I'm using 0 as your answer. ");
                return 0;
            }

        }

        static bool PromptForBool(string prompt)
        {
            Console.Write(prompt);
            var answer = Console.ReadLine().ToUpper();
            if (answer[0] == 'Y')
            {
                return true;
            }
            return false;

        }

        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();

            var keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine(" (M)odify Database");
                Console.WriteLine(" (V)iew Database ");
                Console.WriteLine(" View all of the (b)ands");
                Console.WriteLine(" (E)nter a bands name to view all of their albums.");
                Console.WriteLine(" View all albums ordered by (r)elease date.");
                Console.WriteLine(" View all bands that are (s)igned.");
                Console.WriteLine(" View all bands that are (n)ot signed.");
                Console.WriteLine(" (Q)uit Program.");

                var choice = Console.ReadLine().ToLower();

                switch (choice)
                {
                    case "m":
                        Console.WriteLine("Would you like to:");
                        Console.WriteLine(" (A)dd a new band.");
                        Console.WriteLine(" Add an al(b)um for a band.");
                        Console.WriteLine(" Add a (s)ong to an album.");
                        Console.WriteLine(" (L)et a band go. (update isSigned to false)");
                        Console.WriteLine(" (R)esign a band. (update isSigned to true)");
                        var modification = Console.ReadLine().ToLower();
                        if (modification == "a")
                        {

                            var newName = PromptForString($"What is the name of the band you're adding? ");
                            var newCountryOfOrigin = PromptForString($"What country is the band from? ");
                            var newNumberOfMembers = PromptForInteger($"How many members are in this band? ");
                            var newWebsite = PromptForString("Enter the web address for this band: ");
                            var newStyle = PromptForString("What style/genre of music does this band play? ");
                            var newIsSigned = PromptForBool("Is this band signed? (Y/N) ");
                            var newContactName = PromptForString("Who is the contact point (agent) for this band? ");
                            var newContactNumber = PromptForString("Enter the phone number for the agent: ((555) 555-5555) ");

                            var newBand = new Band
                            {
                                Name = newName,
                                CountryOfOrigin = newCountryOfOrigin,
                                NumberOfMembers = newNumberOfMembers,
                                Website = newWebsite,
                                Style = newStyle,
                                IsSigned = newIsSigned,
                                ContactName = newContactName,
                                ContactPhoneNumber = newContactNumber
                            };

                            context.Bands.Add(newBand);
                            context.SaveChanges();
                            Console.WriteLine($"{newName} was added!");


                        }
                        else if (modification == "b")
                        {

                        }

                        else
                        {
                            Console.WriteLine("Sorry, that's not a valid input.");
                        }

                        break;

                    case "q":
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("That is not a valid response. Please try again.");
                        break;





                }
            }
        }
    }
}