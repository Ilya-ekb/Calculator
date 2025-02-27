using System;
using System.Collections.Generic;
using CalculatorStorages;
using UnityEngine;
#if UNITY_EDITOR
using TriInspector;
#endif

namespace MonoScripts
{
    [CreateAssetMenu(menuName = nameof(FileStorage), fileName = nameof(FileStorage))]
    public class FileStorageConfig : StorageConfig
    {
#if UNITY_EDITOR
        [Dropdown(nameof(GetStringValues))]
#endif
        [SerializeField]
        private string directoryPath;


        private const string streamingAssetsPath = "StreamingAssets";
        private const string persistentDataPath = "PersistentData";

        public override IStorage GetStorage()
        {
            var path = directoryPath switch
            {
                streamingAssetsPath => Application.streamingAssetsPath,
                persistentDataPath => Application.persistentDataPath,
                _ => throw new ArgumentOutOfRangeException()
            };
            return new FileStorage(path);
        }
        private IEnumerable<string> GetStringValues()
        {
            yield return streamingAssetsPath;
            yield return persistentDataPath;
        }
    }
}