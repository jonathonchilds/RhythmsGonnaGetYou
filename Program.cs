using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
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
                            AddBand(context);
                        }
                        else if (modification == "b")
                        {
                            AddAlbum(context);
                        }
                        else if (modification == "s")
                        {
                            AddSong(context);
                        }
                        else if (modification == "l")
                        {
                            UnsignBand(context);
                        }
                        else if (modification == "r")
                        {
                            SignBand(context);
                        }
                        else
                        {
                            Console.WriteLine("Sorry, that's not a valid input.");
                        }
                        break;

                    case "v":


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

        public static void SignBand(RhythmsGonnaGetYouContext context)
        {
            var bandToUpdate = PromptForString("Which band is being signed?");
            var band = context.Bands.FirstOrDefault(band => band.Name == bandToUpdate);
            var boolToUpdate = PromptForBool($"You will be signing {band.Name}, correct?");
            band.IsSigned = boolToUpdate;
            context.Bands.Update(band);
            context.SaveChanges();
            Console.WriteLine($"{band.Name} is now signed.");
        }

        public static void UnsignBand(RhythmsGonnaGetYouContext context)
        {
            var bandToUpdate = PromptForString("Which band is being let go?");
            var band = context.Bands.FirstOrDefault(band => band.Name == bandToUpdate);
            var boolToUpdate = PromptForBool($"You will be letting go of {band.Name}, correct?");
            band.IsSigned = !boolToUpdate;
            context.Bands.Update(band);
            context.SaveChanges();
            Console.WriteLine($"{band.Name} is now unsigned.");
        }

        public static void AddBand(RhythmsGonnaGetYouContext context)
        {
            var newBand = new Band
            {
                Name = PromptForString($"What is the name of the band you're adding? "),
                CountryOfOrigin = PromptForString($"What country is the band from? "),
                NumberOfMembers = PromptForInteger($"How many members are in this band? "),
                Website = PromptForString("Enter the web address for this band: "),
                Style = PromptForString("What style/genre of music does this band play? "),
                IsSigned = PromptForBool("Is this band signed? (Y/N) "),
                ContactName = PromptForString("Who is the contact point (agent) for this band? "),
                ContactPhoneNumber = PromptForString("Enter the phone number for the agent: ((555) 555-5555) ")
            };
            context.Bands.Add(newBand);
            context.SaveChanges();
            Console.WriteLine($"{newBand.Name} was added!");
        }

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
            Console.WriteLine(prompt);
            var answer = Console.ReadLine().ToUpper();
            if (answer[0] == 'Y')
            {
                return true;
            }
            return false;

        }

        public static void AddSong(RhythmsGonnaGetYouContext context)
        {
            var albumToUpdate = PromptForString("For which album would you like to add a song? ");
            var album = context.Albums.FirstOrDefault(album => album.Title == albumToUpdate);

            var newSong = new Song
            {
                Title = PromptForString("Enter the name of the song you'd like to add: "),
                TrackNumber = PromptForInteger("Enter the track number: "),
                Duration = PromptForString("Enter the track's duration: "),
                AlbumId = album.Id
            };

            context.Songs.Add(newSong);
            context.SaveChanges();
            Console.WriteLine($"{newSong.Title} was added to {albumToUpdate}!");
        }

        public static void AddAlbum(RhythmsGonnaGetYouContext context)
        {
            var nameToUse = PromptForString("For which band would you like to add an album?");
            var band = context.Bands.FirstOrDefault(band => band.Name == nameToUse);
            Console.WriteLine("Enter the release date for the album (YYYY-MM-DD) ");
            var releaseDate = DateTimeToUTC();


            var newAlbum = new Album
            {
                Title = PromptForString("Please enter the name of the album you'd like to add: "),
                IsExplicit = PromptForBool("Is the album explicit? "),
                ReleaseDate = releaseDate,
                BandId = band.Id
            };

            context.Albums.Add(newAlbum);
            context.SaveChanges();
            Console.WriteLine($"{newAlbum.Title} was added to {band}!");
        }

        public static DateTime DateTimeToUTC()
        {
            var inputValue = DateTime.Parse(Console.ReadLine());
            var inputValueInUTC = inputValue.ToUniversalTime();
            return inputValueInUTC;
        }
    }
}