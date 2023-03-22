using ShortageApp.Helpers;
using ShortageApp.Interfaces;
using ShortageApp.Models;

namespace ShortageApp.Views
{
    internal class UserManagementView : IShortageView
    {
        private readonly User _user;

        public UserManagementView(User user)
        {
            _user = user;
        }

        public void Load()
        {
            Console.WriteLine("User management:");
            Console.WriteLine("Select your choice:\n" +
                              "0. Go back\n" +
                              "1. Change role\n" +
                              "Input your choice:");
            var choice = InputValidation.NumberInputValidation(Console.ReadLine(), 3, 0);
            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    Console.Clear();
                    Console.WriteLine("Select role:\n" +
                                      "0. Go back\n" +
                                      "1. User\n" +
                                      "2. Admin\n" +
                                      "Input your choice:");
                    var selected = InputValidation.NumberInputValidation(Console.ReadLine(), 3, 0);
                    switch (selected)
                    {
                        case 0:
                            return;
                        case 1:
                            _user.ChangeRole(EnumTypes.RoleType.User);
                            Console.WriteLine("Role changed to User");
                            InputValidation.PressAnyKey();
                            return;
                        case 2:
                            _user.ChangeRole(EnumTypes.RoleType.Admin);
                            Console.WriteLine("Role changed to Admin");
                            InputValidation.PressAnyKey();
                            return;
                    }
                    return;

            }
        }
    }
}
