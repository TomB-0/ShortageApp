using ShortageApp.Helpers;
using ShortageApp.Helpers.Exceptions;
using ShortageApp.Interfaces;
using ShortageApp.Models;

namespace ShortageApp.Views
{
    internal class AddShortageView : IShortageView
    {
        private readonly IShortageService _shortageService;
        private readonly User _user;
        public AddShortageView(IShortageService shortageService, User user)
        {
            _shortageService = shortageService;
            _user = user;
        }
        
        public void Load()
        {
            Console.WriteLine("Add new shortage:");
            Console.WriteLine("Shortage title");
            var title = InputValidation.StringInputValidation(Console.ReadLine());
            Console.WriteLine("Room type (0. MeetingRoom | 1. Kitchen | 2. Bathroom | 3. Other)");
            var roomType = InputValidation.NumberInputValidation(Console.ReadLine(), (int)EnumTypes.RoomType.Other, 0);
            Console.WriteLine("Category type (0. Electronics | 1. Food | 2. Other)");
            var categoryType = InputValidation.NumberInputValidation(Console.ReadLine(), (int)EnumTypes.CategoryType.Other, 0);
            Console.WriteLine("Priority (1-10)");
            var priority = InputValidation.NumberInputValidation(Console.ReadLine(), 10, 1);
            var shortage = new Shortage(title, (EnumTypes.RoomType)roomType, (EnumTypes.CategoryType)categoryType, _user, priority, DateTime.Now);

            try
            {
                _shortageService.AddNewShortage(shortage);
                Console.WriteLine("Shortage Saved...");
            }
            catch (ShortageAlreadyExistsException)
            {
                Console.WriteLine("This shortage with higher or same priority already exists");
            }
            InputValidation.PressAnyKey();
        }
    }
}
