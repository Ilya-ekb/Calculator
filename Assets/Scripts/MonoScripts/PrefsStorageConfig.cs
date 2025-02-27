using CalculatorStorages;
using UnityEngine;

namespace MonoScripts
{
    [CreateAssetMenu(menuName = nameof(PrefsStorage), fileName = nameof(PrefsStorage))]
    public class PrefsStorageConfig : StorageConfig
    {
        [SerializeField] private string prefKey = "calculator";
        public override IStorage GetStorage() => new PrefsStorage(prefKey);
    }
}