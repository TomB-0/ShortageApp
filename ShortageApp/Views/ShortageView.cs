using ShortageApp.Helpers;
using ShortageApp.Helpers.Exceptions;
using ShortageApp.Interfaces;
using ShortageApp.Models;

namespace ShortageApp.Views
{
    internal class ShortageView : IShortageView
    {
        private readonly IShortageService _shortageService;
        private readonly User _user;

        public ShortageView(IShortageService shortageService, User user)
        {
            _shortageService = shortageService;
            _user = user;
        }
        public void Load()
        {
            try {
                List<Shortage> shortages = _shortageService.GetShortageByUser(_user);
                Console.WriteLine($"Shortages found : {shortages.Count}");
                Console.WriteLine("0. Go back\n");
                shortages.ForEach(s => Console.WriteLine($"{shortages.IndexOf(s) + 1}" +
                    $". Title: {s.Title}\n " +
                    $"  Priority: {s.Priority}" +
                    $"\n-------------------\n"));
                Console.WriteLine("Input your choice: ");
                var choice = InputValidation.NumberInputValidation(Console.ReadLine(), shortages.Count(), 0);
                if (choice == 0) return;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(shortages[choice - 1].ToString() + "\n");
                    Console.WriteLine("Shortage menu\n");
                    Console.WriteLine("0. Go back\n" +
                                      "1. Delete shortage\n" +
                                      "");
                    Console.WriteLine("Input your choice:");
                    var userInput = InputValidation.NumberInputValidation(Console.ReadLine(), 2, 0);
                    switch (userInput)
                    {
                        case 0:
                            return;
                        case 1:
                            while (true)
                            {
                                Console.WriteLine("Delete selected shortage:" + shortages[choice - 1].Title + "?\n" +
                                    "0. Yes\n" +
                                    "1. No\n" +
                                    "Input your choice:");
                                var answer = InputValidation.NumberInputValidation(Console.ReadLine(), 2, 0);
                                if (answer == 1) break;
                                else
                                {
                                    try
                                    {
                                        _shortageService.DeleteShortage(shortages[choice - 1].Id, _user);
                                        Console.WriteLine("Shortage deleted...");
                                        InputValidation.PressAnyKey();
                                    }
                                    catch (ShortageDoesntExistException)
                                    {
                                        Console.WriteLine("Shortage doesnt exist");
                                        InputValidation.PressAnyKey();
                                    }
                                    catch (CantRemoveOthersShortagesException)
                                    {
                                        Console.WriteLine("You cannot remove others users shortage");
                                        InputValidation.PressAnyKey();
                                    }
                                    break;
                                }
                            }
                            return;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Unexpected error occured");
                InputValidation.PressAnyKey();
            }
        }
    }
}
