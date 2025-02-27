using CalculatorStorages;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor
{
    public class PrefsStorageTests
    {
        private PrefsStorage prefsStorage;
        private const string testPrefsKey = "test";

        [SetUp]
        public void Setup()
        {
            prefsStorage = new PrefsStorage(testPrefsKey);
        }

        [TearDown]
        public void TearDown()
        {
            prefsStorage = null;
            PlayerPrefs.DeleteKey(testPrefsKey);
        }

        [Test]
        public void OnSave_CreatesStorageFile_WhenFileDoesNotExist()
        {
            Assert.IsFalse(PlayerPrefs.HasKey(testPrefsKey));

            prefsStorage.Save();

            Assert.IsTrue(PlayerPrefs.HasKey(testPrefsKey));
        }

        [Test]
        public void OnLoad_LoadsCorrectData_WhenFileExists()
        {
            prefsStorage.Load();
            prefsStorage.LastValue = "1+1";
            prefsStorage.ClearHistory();
            Assert.AreEqual(string.Empty, prefsStorage.History);
            prefsStorage.UpdateHistory("1+1 = 2");
            prefsStorage.Save();
            prefsStorage.Load();
            
            Assert.AreEqual("1+1", prefsStorage.LastValue);
            var args = prefsStorage.History.Split("\n");
            Assert.AreEqual(2, args.Length);
            Assert.AreEqual("1+1 = 2", args[0]);
        }
        
        [Test]
        public void CreateStorageFile_CreatesDirectory_IfDoesNotExist()
        {
            if(PlayerPrefs.HasKey(testPrefsKey))
                PlayerPrefs.DeleteKey(testPrefsKey);
            PrefsStorage storage = new PrefsStorage(testPrefsKey);

            storage.Load();

            Assert.IsTrue(PlayerPrefs.HasKey(testPrefsKey));
        }
    }
}