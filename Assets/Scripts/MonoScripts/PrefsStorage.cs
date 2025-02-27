using Newtonsoft.Json;
using UnityEngine;

namespace CalculatorStorages
{
    public class PrefsStorage : BaseStorage
    {
        private readonly string storageName;

        public PrefsStorage(string storageName)
        {
            this.storageName = storageName;
        }
        
        protected override void OnSave()
        {
            base.OnSave();
            var json = JsonConvert.SerializeObject(storageModel);
            PlayerPrefs.SetString(storageName, json);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            var json = PlayerPrefs.GetString(storageName, "");
            if (string.IsNullOrEmpty(json))
            {
                PlayerPrefs.SetString(storageName, string.Empty);
                storageModel = new StorageModel();
                return;
            }
            storageModel = JsonConvert.DeserializeObject<StorageModel>(json);
        }
    }
}