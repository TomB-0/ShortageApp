namespace ShortageApp.Helpers
{
    public class InputValidation
    {
        public static string StringInputValidation(string? input)
        {
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid text input. Try again:");
                input = Console.ReadLine();
            }
            return input;
        }
        public static int NumberInputValidation(string? input, int max, int min)
        {
            var num = -1;
            while(!int.TryParse(input, out num) || (num < min || num > max))
            {
                Console.WriteLine("Wrong number input. Try again:");
                input = Console.ReadLine();
            }
            return num;
        }
        public static DateTime DateTimeValidation(string? input)
        {
            DateTime time;
            while(!DateTime.TryParse(input, out time))
            {
                Console.WriteLine("Invalid date input. Try again:");
                input = Console.ReadLine();
            }
            return time;
        }
        public static void PressAnyKey()
        {
            Console.WriteLine("Press any key to go back.");
            Console.ReadKey();
        }

    }
}
