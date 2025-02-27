using CalculatorStorages;
using UnityEngine;

namespace MonoScripts
{
    public abstract class StorageConfig : ScriptableObject
    {
        public abstract IStorage GetStorage();
    }
}