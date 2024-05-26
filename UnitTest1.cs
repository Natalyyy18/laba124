using ClassLibrary10;
using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using Microsoft.Analytics.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using лаба_12_4_часть;


namespace UnitTestsFor4
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AddItem_AddsElementToHashTable()
        {
            // Arrange
            var hashTable = new HashTable<BankCard, string>();
            BankCard deb2 = new BankCard();
            deb2.RandomInit();

            // Act
            hashTable.Add(deb2, "one");

            // Assert
            Assert.AreEqual("one", hashTable[deb2]);
        }
        [TestMethod]
        public void RemoveItem_RemovesElementFromHashTable()
        {
            // Arrange
            var hashTable = new HashTable<BankCard, string>();
            BankCard deb2 = new BankCard();
            deb2.RandomInit();
           

            hashTable.Add(deb2, "one");

            // Act
            hashTable.Remove(deb2);

            // Assert
            Assert.IsFalse(hashTable.ContainsKey(deb2));
        }
        [TestMethod]
        public void Capacity_ReflectsTableLength()
        {
            // Arrange
            var hashTable = new HashTable<BankCard, string>();

            // Assert
            Assert.AreEqual(16, hashTable.Capacity);
        }
        [TestMethod]
        public void Keys_ReturnsListOfKeys()
        {
            // Arrange
            var hashTable = new HashTable<BankCard, string>();
            BankCard deb2 = new BankCard();
            deb2.RandomInit();

            hashTable.Add(deb2, "one");

            // Act
            var keys = hashTable.Keys.ToList();

            // Assert
            CollectionAssert.AreEqual(new List<int> { 1 }, keys);
        }
        [TestMethod]
        public void Values_ReturnsListOfValues()
        {
            // Arrange
            var hashTable = new HashTable<BankCard, string>();
            BankCard deb2 = new BankCard();
            deb2.RandomInit();

            hashTable.Add(deb2, "one");

            // Act
            var values = hashTable.Values.ToList();

            // Assert
            CollectionAssert.AreEqual(new List<string> { "one" }, values);
        }
        [TestMethod]
        public void Contains_ReturnsTrue_WhenKeyValuePairExists()
        {
            // Arrange
            var dictionary = new HashTable<BankCard, string>();
            BankCard deb2 = new BankCard();
            deb2.RandomInit();
            dictionary.Add(deb2, "One");

            // Act
            var result = dictionary.Contains(new KeyValuePair<BankCard, string>(deb2, "One"));

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Contains_ReturnsFalse_WhenKeyValuePairDoesNotExist()
        {
            // Arrange
            var dictionary = new HashTable<BankCard, string>();
            BankCard deb2 = new BankCard();
            deb2.RandomInit();
            dictionary.Add(deb2, "One");

            // Act
            var result = dictionary.Contains(new KeyValuePair<BankCard, string>(deb2, "Two"));

            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Remove_RemovesKeyValuePair_WhenKeyExists()
        {
            // Arrange
            var dictionary = new HashTable<BankCard, string>();
            BankCard deb2 = new BankCard();
            deb2.RandomInit();
            dictionary.Add(deb2, "One");

            // Act
            var removed = dictionary.Remove(deb2);

            // Assert
            Assert.IsTrue(removed);
            
            
        }

        [TestMethod]
        public void Remove_ReturnsFalse_WhenKeyDoesNotExist()
        {
            // Arrange
            var dictionary = new HashTable<BankCard, string>();
            BankCard deb2 = new BankCard();
            deb2.RandomInit();
            dictionary.Add(deb2, "One");
           

            // Act
            var removed = dictionary.Remove(deb2);

            // Assert
            Assert.IsFalse(removed);
         
        }
        [TestMethod]
        public void GetIndex_ReturnsValidIndex()
        {
            // Arrange
            var hashTable = new HashTable<BankCard, string>();

            // Act
            BankCard deb2 = new BankCard();
            deb2.RandomInit();
           
            int index1 = hashTable.GetIndex(deb2);
            int index2 = hashTable.GetIndex(deb2);

            // Assert
            
            Assert.AreEqual(index1, 0, hashTable.Capacity - 1);
            Assert.AreEqual(index2, 0, hashTable.Capacity - 1);
        }
        [TestMethod]
        public void Clear_RemovesAllElements()
        {
            // Arrange
            var hashTable = new HashTable<BankCard, string>();

            BankCard deb2 = new BankCard();
            deb2.RandomInit();

            hashTable.Add(deb2, "One");
            hashTable.Add(deb2, "Two");

            // Act
            hashTable.Clear();

            // Assert
            Assert.IsNull(hashTable);
        }
        [TestMethod]
        public void CopyTo_CopiesElementsToArray()
        {
            // Arrange
            var hashTable = new HashTable<BankCard, string>();
            BankCard deb2 = new BankCard();
            deb2.RandomInit();
            
            hashTable.Add(deb2, "One");
            hashTable.Add(deb2, "Two");
            var array = new KeyValuePair<BankCard, string>[2];

            // Act
            hashTable.CopyTo(array, 0);

            // Assert
            Assert.AreEqual(new KeyValuePair<int, string>(1, "One"), array[0]);
            Assert.AreEqual(new KeyValuePair<int, string>(2, "Two"), array[1]);
        }

    }
}
