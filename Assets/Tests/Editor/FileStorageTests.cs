using System.IO;
using CalculatorStorages;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor
{
    public class FileStorageTests
    {
        private string testDirectory;
        private FileStorage fileStorage;

        [SetUp]
        public void Setup()
        {
            testDirectory = Path.Combine(Application.persistentDataPath, "TestStorage");
            fileStorage = new FileStorage(testDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(testDirectory))
                Directory.Delete(testDirectory, true);
        }

        [Test]
        public void OnSave_CreatesStorageFile_WhenFileDoesNotExist()
        {
            Assert.IsFalse(File.Exists(Path.Combine(testDirectory, "calc.txt")));

            fileStorage.Save();

            Assert.IsTrue(File.Exists(Path.Combine(testDirectory, "calc.txt")));
        }

        [Test]
        public void OnLoad_LoadsCorrectData_WhenFileExists()
        {
            fileStorage.Load();
            fileStorage.LastValue = "1+1";
            fileStorage.ClearHistory();
            Assert.AreEqual(string.Empty, fileStorage.History);
            fileStorage.UpdateHistory("1+1 = 2");
            fileStorage.Save();
            fileStorage.Load();
            
            Assert.AreEqual("1+1", fileStorage.LastValue);
            var args = fileStorage.History.Split("\n");
            Assert.AreEqual(2, args.Length);
            Assert.AreEqual("1+1 = 2", args[0]);
        }
        
        [Test]
        public void CreateStorageFile_CreatesDirectory_IfDoesNotExist()
        {
            var filePath = Path.Combine(testDirectory, "calc.txt");
            if(File.Exists(filePath))
                File.Delete(filePath);
            FileStorage storage = new FileStorage(testDirectory);

            storage.Load();

            Assert.IsTrue(File.Exists(filePath));
        }
    }
}
