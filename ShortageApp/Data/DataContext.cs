using System.Collections;
using Newtonsoft.Json;
using ShortageApp.Interfaces;
using ShortageApp.Models;

namespace ShortageApp.Data
{
    public class DataContext : IDataContext
    {
        public List<User> Users { get; set; }
        public List<Shortage> Shortages { get; set; }
        
        public void SaveToJson<T>(T obj) where T : IList
        {
            string fileName = ".\\ShortageData.json";
            string jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
        }

        public T? LoadFromJson<T>() where T : IList
        {
            string fileName = ".\\ShortageData.json";
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
        }

        public void SaveDataToJsonTemp(IList users, IList shortages)
        {
            var data = new { Users = users, Shortages = shortages };
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText("data.json", json);
        }
    }
}
