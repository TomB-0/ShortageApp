using ShortageApp.Helpers;
using ShortageApp.Interfaces;
using ShortageApp.Models;

namespace ShortageApp.Services
{
    public class ShortageService : IShortageService
    {
        private readonly IShortageRepository _repository;

        public ShortageService(IShortageRepository repository)
        {
            _repository = repository;
        }

        public void AddNewShortage(Shortage shortage)
        {
            _repository.AddNewShortage(shortage);
        }
        public List<Shortage> GetShortageByUser(User user)
        {
            return _repository.GetShortageByUser(user);
        }
        public void DeleteShortage(Guid id, User user)
        {
            _repository.DeleteShortage(id, user);
        }

        public List<Shortage> GetShortageByTitle(string title, User user)
        {
            return _repository.GetShortageByTitle(title, user);
        }

        public List<Shortage> GetShortageByDate(DateTime fromTime, DateTime toTime, User user)
        {
            return _repository.GetShortageByDate(fromTime, toTime, user);
        }

        public List<Shortage> GetShortageByCategory(EnumTypes.CategoryType category, User user)
        {
            return _repository.GetShortageByCategory(category, user);
        }

        public List<Shortage> GetShortageByRoom(EnumTypes.RoomType room, User user)
        {
            return _repository.GetShortageByRoom(room, user);
        }
    }
}
