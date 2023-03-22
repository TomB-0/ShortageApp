using ShortageApp.Helpers;
using ShortageApp.Helpers.Exceptions;
using ShortageApp.Interfaces;
using ShortageApp.Models;

namespace ShortageApp.Repositories
{
    public class ShortageRepository : IShortageRepository
    {
        private readonly IDataContext _dataContext;
        public ShortageRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddNewShortage(Shortage shortage)
        {
            var shortages = _dataContext.LoadFromJson<List<Shortage>>();
            
            if (shortages == null)
            {
                shortages = new List<Shortage>();
            }
            var changeIndex = shortages.FindIndex(s => s.Title == shortage.Title && s.Room == shortage.Room);
            
            if(changeIndex == -1)
            {
                shortages.Add(shortage);
                _dataContext.SaveToJson(shortages);
            }
            else if(changeIndex != -1 && shortages[changeIndex].Priority < shortage.Priority)
            {
                shortages[changeIndex] = shortage;
                _dataContext.SaveToJson(shortages);
            }
            else
                throw new ShortageAlreadyExistsException(); //exception shortage already exists
        }

        public List<Shortage> GetShortageByUser(User user)
        {
            var shortages = _dataContext.LoadFromJson<List<Shortage>>();
            if(shortages == null)
            {
                return new List<Shortage>();
            }
            else if(!user.IsAdmin())
            {
                return shortages.Where(s => s.Owner.UserName == user.UserName)
                      .OrderByDescending(s => s.Priority).ToList();
            }
            else
            {
                return shortages.OrderByDescending(s => s.Priority).ToList();
            }    
        }

        public void DeleteShortage(Guid id, User user)
        {
            var shortages = _dataContext.LoadFromJson<List<Shortage>>();
            if (shortages == null) throw new ShortageDoesntExistException();


            var shortage = shortages.SingleOrDefault(s => s.Id == id);
            if(shortage == null) throw new ShortageDoesntExistException();

            if (!shortage.Owner.Equals(user) && !user.IsAdmin())
            {
                throw new CantRemoveOthersShortagesException();
            }

            shortages.Remove(shortage);
            _dataContext.SaveToJson(shortages);
        }

        public List<Shortage> GetShortageByTitle(string title, User user)
        {
            var shortages = _dataContext.LoadFromJson<List<Shortage>>();
            if(shortages == null)
            {
                return new List<Shortage>();
            }
            else if(!user.IsAdmin())
            {
                return shortages.Where(s => s.Title.ToLower().Contains(title.ToLower()) &&
                    s.Owner.UserName == user.UserName).OrderByDescending(s => s.Priority).ToList();
            }
            else
            {
                return shortages.Where(s => s.Title.ToLower().Contains(title.ToLower()))
                    .OrderByDescending(s => s.Priority).ToList();
            }

        }

        public List<Shortage> GetShortageByDate(DateTime fromTime, DateTime toTime, User user)
        {
            var shortages = _dataContext.LoadFromJson<List<Shortage>>();
            if(shortages == null)
            {
                return new List<Shortage>();
            }
            else if(!user.IsAdmin())
            {
                return shortages.Where(s => s.CreatedOn >= fromTime && s.CreatedOn <= toTime &&
                    s.Owner.UserName == user.UserName).OrderByDescending(s => s.Priority).ToList();
            }
            return shortages.Where(s => s.CreatedOn >= fromTime && s.CreatedOn <= toTime)
                .OrderByDescending(s => s.Priority).ToList();
        }

        public List<Shortage> GetShortageByCategory(EnumTypes.CategoryType category, User user)
        {
            var shortages = _dataContext.LoadFromJson<List<Shortage>>();
            if (shortages == null)
            {
                return new List<Shortage>();
            }
            else if(!user.IsAdmin())
            {
                return shortages.Where(s => s.Category == category &&
                    s.Owner.UserName == user.UserName).OrderByDescending(s => s.Priority).ToList();
            }
            return shortages.Where(s => s.Category == category).OrderByDescending(s => s.Priority).ToList();
        }

        public List<Shortage> GetShortageByRoom(EnumTypes.RoomType room, User user)
        {
            var shortages = _dataContext.LoadFromJson<List<Shortage>>();
            if (shortages == null)
            {
                return new List<Shortage>();
            }
            else if(!user.IsAdmin())
            {
                return shortages.Where(s => s.Room == room &&
                    s.Owner.UserName == user.UserName).OrderByDescending(s => s.Priority).ToList();
            }
            return shortages.Where(s => s.Room == room).OrderByDescending(s => s.Priority).ToList();
        }
    }
}
