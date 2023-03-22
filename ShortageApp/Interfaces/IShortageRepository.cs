using ShortageApp.Helpers;
using ShortageApp.Models;

namespace ShortageApp.Interfaces
{
    public interface IShortageRepository
    {
        public void AddNewShortage(Shortage shortage);
        public List<Shortage> GetShortageByUser(User user);
        public void DeleteShortage(Guid id, User user);
        public List<Shortage> GetShortageByTitle(string title, User user);
        public List<Shortage> GetShortageByDate(DateTime fromTime, DateTime toTime, User user);
        public List<Shortage> GetShortageByCategory(EnumTypes.CategoryType category, User user);
        public List<Shortage> GetShortageByRoom(EnumTypes.RoomType room, User user);

    }
}
