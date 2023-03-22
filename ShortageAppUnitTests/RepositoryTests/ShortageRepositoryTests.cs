using Moq;
using ShortageApp.Helpers;
using ShortageApp.Helpers.Exceptions;
using ShortageApp.Interfaces;
using ShortageApp.Models;
using ShortageApp.Repositories;
using System;
using System.Collections.Generic;


namespace ShortageAppUnitTests.RepositoryTests
{
    public class ShortageRepositoryTests
    {
        [Fact]
        public void AddNew_Shortage_ShortageAlreadyExistWithLowerPriority()
        {
            //Arrange
            var existingShortages = new List<Shortage>
            {
                new Shortage("TestShortage1", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, new User("Tomas", EnumTypes.RoleType.User), 5, DateTime.Now),
                new Shortage("TestShortage2", EnumTypes.RoomType.MeetingRoom, EnumTypes.CategoryType.Food, new User("Tadas", EnumTypes.RoleType.User), 8, DateTime.Now)
            };
            var newShortage = new Shortage("TestShortage1", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, new User("Jurgis", 0), 8, DateTime.Now);

            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(s => s.LoadFromJson<List<Shortage>>()).Returns(existingShortages);

            var repository = new ShortageRepository(dataContext.Object);
            
            //Act
            repository.AddNewShortage(newShortage);

        }
        
        [Fact]
        public void AddNew_Shortage_ReturnsExceptionAlreadyExistWithHigherPriority()
        {
            //Arrange
            var existingShortages = new List<Shortage>
            {
                new Shortage("TestShortage1", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, new User("Tomas", EnumTypes.RoleType.User), 8, DateTime.Now),
                new Shortage("TestShortage2", EnumTypes.RoomType.MeetingRoom, EnumTypes.CategoryType.Food, new User("Tadas", EnumTypes.RoleType.User), 8, DateTime.Now)
            };
            var newShortage = new Shortage("TestShortage1", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, new User("Jurgis", 0), 5, DateTime.Now);
            
            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(s => s.LoadFromJson<List<Shortage>>()).Returns(existingShortages);

            var repository = new ShortageRepository(dataContext.Object);
            
            //Act
            Assert.Throws<ShortageAlreadyExistsException>(() => repository.AddNewShortage(newShortage));
        }
        [Fact]
        public void Remove_Shortage_ThrowsShortageDoesntExistException()
        {
            //Arrange
            var existingShortages = new List<Shortage>
            {
                new Shortage("TestShortage1", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, new User("Tomas", EnumTypes.RoleType.User), 8, DateTime.Now),
                new Shortage("TestShortage2", EnumTypes.RoomType.MeetingRoom, EnumTypes.CategoryType.Food, new User("Tadas", EnumTypes.RoleType.User), 8, DateTime.Now)
            };
            var nonExistingShortage = new Shortage("TestShortage3", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, new User("Jurgis", 0), 8, DateTime.Now);

            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(s => s.LoadFromJson<List<Shortage>>()).Returns(existingShortages);

            var repository = new ShortageRepository(dataContext.Object);

            //Act
            Assert.Throws<ShortageDoesntExistException>(() => repository.DeleteShortage(nonExistingShortage.Id, nonExistingShortage.Owner));
        }
        [Fact]
        public void Remove_Shortage_ThrowsCantRemoveOthersShortagesException()
        {
            //Arrange
            var existingShortages = new List<Shortage>
            {
                new Shortage("TestShortage1", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, new User("Tomas", EnumTypes.RoleType.User), 8, DateTime.Now),
                new Shortage("TestShortage2", EnumTypes.RoomType.MeetingRoom, EnumTypes.CategoryType.Food, new User("Tadas", EnumTypes.RoleType.User), 8, DateTime.Now)
            };

            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(s => s.LoadFromJson<List<Shortage>>()).Returns(existingShortages);

            var repository = new ShortageRepository(dataContext.Object);
            //Act
            Assert.Throws<CantRemoveOthersShortagesException>(() => repository.DeleteShortage(existingShortages[0].Id, existingShortages[1].Owner));
        }
        [Fact]
        public void Remove_Shortage_OthersUsersAsAdmin()
        {
            //Arrange
            var searchUser = new User("Admin", EnumTypes.RoleType.Admin);
            var existingShortages = new List<Shortage>
            {
                new Shortage("TestShortage1", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, new User("Tomas", EnumTypes.RoleType.User), 8, DateTime.Now),
                new Shortage("TestShortage2", EnumTypes.RoomType.MeetingRoom, EnumTypes.CategoryType.Food, new User("Tadas", EnumTypes.RoleType.User), 8, DateTime.Now)
            };

            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(s => s.LoadFromJson<List<Shortage>>()).Returns(existingShortages);

            var repository = new ShortageRepository(dataContext.Object);
            //Act
            repository.DeleteShortage(existingShortages[0].Id, searchUser);
        }
        [Fact]
        public void Remove_Shortage_Success()
        {   
            //Arrange
            var existingShortages = new List<Shortage>
            {
                new Shortage("TestShortage1", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, new User("Tomas", EnumTypes.RoleType.User), 8, DateTime.Now),
                new Shortage("TestShortage2", EnumTypes.RoomType.MeetingRoom, EnumTypes.CategoryType.Food, new User("Tadas", EnumTypes.RoleType.User), 8, DateTime.Now)
            };

            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(s => s.LoadFromJson<List<Shortage>>()).Returns(existingShortages);

            var repository = new ShortageRepository(dataContext.Object);

            //Act
            repository.DeleteShortage(existingShortages[0].Id, existingShortages[0].Owner);
        }
        [Fact]
        public void Get_Shortages_ByTitle()
        {
            //Arrange
            var searchUser = new User("Tomas", EnumTypes.RoleType.User);
            var existingShortages = new List<Shortage>
            {
                new Shortage("Test SHORTAGE", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, searchUser, 8, DateTime.Now),
                new Shortage("Problema", EnumTypes.RoomType.MeetingRoom, EnumTypes.CategoryType.Food, new User("Tadas", EnumTypes.RoleType.User), 8, DateTime.Now),
                new Shortage("TESTAS", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, searchUser, 8, DateTime.Now)
            };
            var title = "test";
            var expected = 2;

            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(s => s.LoadFromJson<List<Shortage>>()).Returns(existingShortages);

            var repository = new ShortageRepository(dataContext.Object);

            //Act
            var result = repository.GetShortageByTitle(title, searchUser);

            //Assert
            Assert.Equal(expected, result.Count);
        }
        [Fact]
        public void Get_Shortages_AsAdmin()
        {
            //Arrange
            var searchUser = new User("Admin", EnumTypes.RoleType.Admin);
            var existingShortages = new List<Shortage>
            {
                new Shortage("TestShortage1", EnumTypes.RoomType.Bathroom, EnumTypes.CategoryType.Food, new User("Tomas", EnumTypes.RoleType.User), 8, DateTime.Now),
                new Shortage("TestShortage2", EnumTypes.RoomType.MeetingRoom, EnumTypes.CategoryType.Food, new User("Tadas", EnumTypes.RoleType.User), 8, DateTime.Now)
            };
            var expected = 2;

            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(s => s.LoadFromJson<List<Shortage>>()).Returns(existingShortages);

            var repository = new ShortageRepository(dataContext.Object);

            //Act
            var result = repository.GetShortageByUser(searchUser);

            //Assert
            Assert.Equal(expected, result.Count);
        }

    }
}
