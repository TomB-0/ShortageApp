using System.Collections;
using Newtonsoft.Json;
using ShortageApp.Interfaces;
using ShortageApp.Models;

namespace ShortageApp.Data
{
    public class DataContext : IDataContext
    {
        
        public void SaveToJson<T>(T obj) where T : IList
        {
            string fileName = ".\\ShortageData.json";
            string jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
        }

        public T? LoadFromJson<T>() where T : IList
        {
            string fileName = ".\\ShortageData.json";
            if (!File.Exists(fileName)) 
            {
                using (File.Create(fileName)) { }
            }
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
        }
    }
}
