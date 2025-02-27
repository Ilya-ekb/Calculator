using System.IO;
using Newtonsoft.Json;

namespace CalculatorStorages
{
    public class FileStorage : BaseStorage
    {
        private string directoryPath;
        private string filePath;

        private const string storageName = "calc.txt";

        public FileStorage(string directoryPath)
        {
            this.directoryPath = directoryPath;
            this.filePath = Path.Combine(directoryPath, storageName);
        }

        ~FileStorage()
        {
            directoryPath = null;
            filePath = null;
            storageModel = null;
        }

        protected override void OnSave()
        {
            base.OnSave();
            if (!File.Exists(filePath))
                CreateStorageFile();
            var json = JsonConvert.SerializeObject(storageModel);
            File.WriteAllText(filePath, json);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            if (!File.Exists(filePath))
                CreateStorageFile();
            var json = File.ReadAllText(filePath);
            storageModel = JsonConvert.DeserializeObject<StorageModel>(json);
        }
        
        private void CreateStorageFile()
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            if (!File.Exists(filePath))
                File.Create(filePath).Close();
        }
    }
}