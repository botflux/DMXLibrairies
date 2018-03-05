using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VPackage.Files;
using System.IO;

namespace VPackage.Files.Test
{
    [TestClass]
    public class FileManagerTest
    {

        #region Test fields
        private string path;
        private const string CONTENT = "Hello world";
        private string unexistantDirectoryPath;
        private string unexistantFilePath;

        private string unexistantFilePathToCreate;

        private string pathFileToWrite;
        #endregion

        #region Test Methods

        [TestInitialize]
        public void Initialization ()
        {
            path = AppDomain.CurrentDomain.BaseDirectory + @"\test.txt";
            pathFileToWrite = AppDomain.CurrentDomain.BaseDirectory + @"\testToWrite.txt";
            unexistantFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\thisfileisnotexisting.txt";
            unexistantFilePathToCreate = AppDomain.CurrentDomain.BaseDirectory + @"\thisdirectorywillbecreate\test.txt";
            unexistantDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + @"\thispathisnotexisting\test.txt";

            FileManager.Write(path, CONTENT, FileManager.WriteOptions.CreateDirectory);
        }
        
        [TestCleanup]
        public void CleanUp()
        {
            File.Delete(path);
            if (File.Exists(pathFileToWrite))
                File.Delete(pathFileToWrite);
        }
        #endregion

        #region Read test
        /// <summary>
        /// </summary>
        /// <case>Le chemin est correcte</case>
        /// <result>Le contenu du fichier</result>
        [TestMethod]
        public void Read_RightPath_Content()
        {
            string content = FileManager.Read(path);

            Assert.AreEqual(CONTENT, content);
        }

        /// <summary>
        /// </summary>
        /// <case>Le chemin d'accès est trop long</case>
        /// <result>PathTooLongException</result>
        [TestMethod]
        [ExpectedException(typeof(PathTooLongException))]
        public void Read_PathTooLong_PathTooLongException ()
        {
            FileManager.Read(new string('a', 300));
        }

        /// <summary>
        /// </summary>
        /// <case>le chemin est null</case>
        /// <result>ArgumentNullException</result>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Read_PathNull_ArgumentNullException ()
        {
            FileManager.Read(null);
        }

        /// <summary>
        /// </summary>
        /// <case>le chemin est vide</case>
        /// <result>ArgumentNullException</result>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Read_PathEmpty_ArgumentNullException()
        {
            FileManager.Read("");
        }

        /// <summary>
        /// </summary>
        /// <case>Le chemin spécifié n'existe pas</case>
        /// <result>DirectoryNotFoundException</result>
        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void Read_DirectoryPathNotExisting_ArgumentNullException()
        {
            FileManager.Read(unexistantDirectoryPath);
        }
        #endregion

        #region Write test

        /// <summary>
        /// </summary>
        /// <case>Tout est correct</case>
        /// <result>Le fichier est créer</result>
        [TestMethod]
        public void Write_AllCorrect_Write ()
        {
            FileManager.Write(pathFileToWrite, CONTENT);
        }

        /// <summary>
        /// </summary>
        /// <case>Le chemin est null</case>
        /// <result>ArgumentNullException</result>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Write_PathNull_ArgumentNullException ()
        {
            FileManager.Write(null, CONTENT);
        }

        /// <summary>
        /// </summary>
        /// <case>Le chemin est vide</case>
        /// <result>ArgumentNullException</result>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Write_PathEmpty_ArgumentNullException()
        {
            FileManager.Write("", CONTENT);
        }

        /// <summary>
        /// </summary>
        /// <case>Le contenu est null</case>
        /// <result>ArgumentNullException</result>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Write_ContentNull_ArgumentNullException()
        {
            FileManager.Write(pathFileToWrite, null);
        }

        /// <summary>
        /// </summary>
        /// <case>Le chemin est trop long</case>
        /// <result>PathTooLongException</result>
        [TestMethod]
        [ExpectedException(typeof(PathTooLongException))]
        public void Write_PathTooLong_PathTooLongException ()
        {
            FileManager.Write(new string('a', 350), CONTENT);
        }

        /// <summary>
        /// </summary>
        /// <case>Le chemin n'existe pas</case>
        /// <result>DirectoryNotFoundException</result>
        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void Write_PathDirectoryNotFound_DirectoryNotFound ()
        {
            FileManager.Write(unexistantDirectoryPath, CONTENT);
        }

        /// <summary>
        /// </summary>
        /// <case>Tout est correcte</case>
        /// <result>Le fichier est créer</result>
        [TestMethod]
        public void Write_PathDirectoryNotAlreadyCreate_Write()
        {
            FileManager.Write(unexistantFilePathToCreate, CONTENT, FileManager.WriteOptions.CreateDirectory);
        }
        #endregion
    }
}
