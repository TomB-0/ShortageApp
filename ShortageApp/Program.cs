using Microsoft.Extensions.DependencyInjection;
using ShortageApp.Data;
using ShortageApp.Helpers;
using ShortageApp.Interfaces;
using ShortageApp.Models;
using ShortageApp.Repositories;
using ShortageApp.Services;
using ShortageApp.Views;

var services = new ServiceCollection();

services.AddScoped<IDataContext, DataContext>()
        .AddScoped<IShortageService, ShortageService>()
        .AddScoped<IShortageRepository, ShortageRepository>()
        .BuildServiceProvider();

Console.Title = "Shortage management application";

Console.WriteLine("Whats your name?");
string userName = InputValidation.StringInputValidation(Console.ReadLine());
User user = new User(userName, 0);

IShortageView addShortageView = new AddShortageView(services.BuildServiceProvider()
    .GetRequiredService<IShortageService>(), user);
IShortageView shortageView = new ShortageView(services.BuildServiceProvider()
    .GetRequiredService<IShortageService>(), user);
IShortageView userManagementView = new UserManagementView(user);
IShortageView filteredShortageView = new FilteredShortageView(services.BuildServiceProvider()
    .GetRequiredService<IShortageService>(), user);


while (true)
{
    Console.Clear();
    Console.WriteLine($"Current user: {userName}\n" +
                      $"Role: {user.Role}\n");
    Console.WriteLine("Select your choice:\n" +
                      "0. Register new shortage\n" +
                      "1. Check existing shortages\n" +
                      "2. Filter shortages\n" +
                      "3. Manage the user\n" +
                      "4. Exit the application\n" +
                      "Input your choice: ");
    int input = InputValidation.NumberInputValidation(Console.ReadLine(), 4, 0);
    switch (input)
    {
        case 0:
            Console.Clear();
            addShortageView.Load();
            break;
        case 1:
            Console.Clear();
            shortageView.Load();
            break;
        case 2:
            Console.Clear();
            filteredShortageView.Load();
            break;
        case 3:
            Console.Clear();
            userManagementView.Load();// padaryti, kad tik seedintas admin galetu pakeisti kitu roles, jei liks laiko

            break;
        default:
            Console.Clear();
            Console.WriteLine("Exiting the application");
            return;
    } 
}

