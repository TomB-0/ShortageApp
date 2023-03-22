using Newtonsoft.Json;
using ShortageApp.Helpers;

namespace ShortageApp.Models
{
    public class Shortage
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public EnumTypes.RoomType Room { get; set; }
        public EnumTypes.CategoryType Category { get; set; }
        public User Owner { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedOn { get; set; }

        public Shortage(string title, EnumTypes.RoomType room, EnumTypes.CategoryType category, User owner, int priority, DateTime createdOn)
        {
            Id = Guid.NewGuid();
            Title = title;
            Room = room;
            Category = category;
            Owner = owner;
            Priority = priority;
            CreatedOn = createdOn;
        }
        [JsonConstructor]
        public Shortage(Guid id,string title, EnumTypes.RoomType room, EnumTypes.CategoryType category, User owner, int priority, DateTime createdOn)
        {
            Id = id;
            Title = title;
            Room = room;
            Category = category;
            Owner = owner;
            Priority = priority;
            CreatedOn = createdOn;
        }
        public override string ToString()
        {
            return ($"Owner: {Owner.UserName}\n" +
                    $"Title: {Title}\n" +
                    $"Room: {Room}\n" +
                    $"Category: {Category}\n" +
                    $"Creation time: {CreatedOn}\n" +
                    $"Priority: {Priority}");
        }
    }
}
