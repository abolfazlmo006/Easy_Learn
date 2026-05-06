using Easy_Learn.UI.Contracts;
using Hanssens.Net;

namespace Easy_Learn.UI.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        LocalStorage _localStorage;

        public LocalStorageService()
        {
            var config = new LocalStorageConfiguration()
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "Easy_Learn"
            };
            _localStorage = new LocalStorage(config);
        }

        public void ClearStorage(List<string> Keys)
        {
            foreach (var Key in Keys)
            {
                _localStorage.Remove(Key);
            }
        }

        public bool Exists(string Key)
        {
            return _localStorage.Exists(Key);
        }

        public T GetStorageValue<T>(string Key)
        {
            return _localStorage.Get<T>(Key);
        }

        public void SetStorageValue<T>(string Key, T Value)
        {
            _localStorage.Store(Key, Value);
            _localStorage.Persist();
        }
    }
}
