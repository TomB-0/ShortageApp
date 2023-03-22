using ShortageApp.Helpers;
using ShortageApp.Interfaces;
using ShortageApp.Models;

namespace ShortageApp.Views
{
    internal class FilteredShortageView : IShortageView
    {
        private readonly IShortageService _shortageService;
        private readonly User _user;

        public FilteredShortageView(IShortageService shortageService, User user)
        {
            _shortageService = shortageService;
            _user = user;
        }
        public void Load()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Filter shortages options:");
                Console.WriteLine("0. Go back\n" +
                                  "1. Load all\n" +
                                  "2. Filter by title\n" +
                                  "3. Filter by date\n" +
                                  "4. Filter by category\n" +
                                  "5. Filter by room\n" +
                                  "Input your choice");
                var choice = InputValidation.NumberInputValidation(Console.ReadLine(), 5, 0);
                var answer = "";
                var shortages = new List<Shortage>();
                Console.Clear();
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        shortages = _shortageService.GetShortageByUser(_user);
                        break;
                    case 2:
                        Console.WriteLine("Input title: ");
                        answer = InputValidation.StringInputValidation(Console.ReadLine());
                        shortages =  _shortageService.GetShortageByTitle(answer, _user);
                        break;
                    case 3:
                        Console.WriteLine("Input dates (from - to, format ): \n" +
                            "From input:");
                        DateTime fromTime = InputValidation.DateTimeValidation(Console.ReadLine());
                        Console.WriteLine("To input:");
                        DateTime toTime = InputValidation.DateTimeValidation(Console.ReadLine());
                        shortages = _shortageService.GetShortageByDate(fromTime, toTime, _user);
                        break;
                    case 4:
                        Console.WriteLine("Input category (0. Electronics | 1. Food | 2. Other)");
                        var categoryType = InputValidation.NumberInputValidation(Console.ReadLine(), (int)EnumTypes.CategoryType.Other, 0);
                        shortages = _shortageService.GetShortageByCategory((EnumTypes.CategoryType)categoryType, _user);
                        break;
                    case 5:
                        Console.WriteLine("Input room (0. MeetingRoom | 1. Kitchen | 2. Bathroom | 3. Other)");
                        var roomType = InputValidation.NumberInputValidation(Console.ReadLine(), (int)EnumTypes.RoomType.Other, 0);
                        shortages = _shortageService.GetShortageByRoom((EnumTypes.RoomType)roomType, _user);
                        break;
                }
                Console.WriteLine($"Shortages found : {shortages.Count}");
                shortages.ForEach(s => Console.WriteLine(s + "\n----------------------\n"));
                InputValidation.PressAnyKey();

            }
        }
    }
}
