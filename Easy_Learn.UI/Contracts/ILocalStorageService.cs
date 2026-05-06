namespace Easy_Learn.UI.Contracts
{
    public interface ILocalStorageService
    {
        void ClearStorage(List<string> Keys);
        bool Exists(string Key);
        T GetStorageValue<T>(string Key);
        void SetStorageValue<T>(string Key, T Value);
    }
}
