using System;

namespace CalculatorStorages
{
    public abstract class BaseStorage : IStorage
    {
        public string LastValue
        {
            get => storageModel.lastInput;
            set => storageModel.lastInput = value;
        }

        public string History => storageModel.history;

        protected StorageModel storageModel;
        
        public void UpdateHistory(string value) => storageModel.history = value + "\n" + storageModel.history;

        public void ClearHistory() => storageModel.history = string.Empty;
        
        protected virtual void OnLoad(){}
        protected virtual void OnSave(){}

        public void Load()
        {
            try
            {
                OnLoad();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                storageModel ??= new StorageModel();
            }
        }

        public void Save()
        {
            try
            {
                OnSave();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}