namespace CalculatorStorages
{
    public interface IStorage
    {
        string LastValue { get; set; }
        string History { get; }
        void Load();
        void Save();
        void UpdateHistory(string value);
        void ClearHistory();
    }
}