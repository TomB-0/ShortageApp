using System.Collections;

namespace ShortageApp.Interfaces
{
    public interface IDataContext
    {
        public void SaveToJson<T>(T obj) where T : IList;
        public T? LoadFromJson<T>() where T : IList;
    }
}
