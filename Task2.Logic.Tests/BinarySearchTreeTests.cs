using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;

namespace Task2.Logic.Tests
{
    [TestFixture]
    public class BinarySearchTreeTests
    {
        #region System.Int32 tests
        public class IntReverseComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y.CompareTo(x);
            }
        }

        public static IEnumerable<TestCaseData> CtorTestData
        {
            get
            {
                yield return new TestCaseData(new[] {1, 2, 2, 3, 4, -5, 10, 12}, 7,
                    new[] {1, 2, 3, 4, -5, 10, 12}, new[] {0, 11, 9}, null);
                yield return new TestCaseData(new[] {1, 2, 2, 3, 4, -5, 10, 12}, 7,
                    new[] {1, 2, 3, 4, -5, 10, 12}, new[] {0, 11, 9}, new IntReverseComparer());
            }
        }
        
        [TestCaseSource(nameof(CtorTestData))]
        [Test]
        public void Ctor_IntArray_TreeWithUniqueElementsExpected
            (int[] array, int expectedCount, int[] expectedElements, int[] unexpectedElements,
            IComparer<int> comparer)
        {
            //act
            BinarySearchTree<int> actual = new BinarySearchTree<int>(array, comparer);
            //assert
            Assert.AreEqual(expectedCount, actual.Count);
            CollectionAssert.AreEquivalent(expectedElements, actual);
            foreach (var item in unexpectedElements)
            {
                CollectionAssert.DoesNotContain(actual, item);
            }
        }
        
        public static IEnumerable<TestCaseData> AddTestData
        {
            get
            {
                yield return new TestCaseData(new[] {1, 2, 2, 3, 3, 3, 4, -5, 10, 10, 12}, 7,
                    new[] {1, 2, 3, 4, -5, 10, 12},
                    new[] {true, true, false, true, false, false, true, true, true, false, true},
                    null);
                yield return new TestCaseData(new[] {1, 2, 2, 3, 3, 3, 4, -5, 10, 10, 12}, 7,
                    new[] {1, 2, 3, 4, -5, 10, 12},
                    new[] {true, true, false, true, false, false, true, true, true, false, true},
                    new IntReverseComparer());
            }
        }
        [TestCaseSource(nameof(AddTestData))]
        [Test]
        public void Add_IntArray_TrueFalseResultsExpected
            (int[] dataArray, int expextedSize, int[] expectedData, bool[] expectedResults,
            IComparer<int> comparer)
        {
            //arrange
            BinarySearchTree<int> tree = new BinarySearchTree<int>(comparer);
            //act-assert
            for (int i = 0; i < dataArray.Length; i++)
            {
                bool actual = tree.Add(dataArray[i]);
                Assert.AreEqual(expectedResults[i], actual);
            }
            CollectionAssert.AreEquivalent(expectedData, tree);
        }
        
        public static IEnumerable<TestCaseData> ClearTestData
        {
            get
            {
                yield return new TestCaseData(new[] {1, 2, 2, 3, 3, 3, 4, -5, 10, 10, 12}, 0,
                    new[] {1, 2, 3, 4, -5, 10, 12}, null);
                yield return new TestCaseData(new[] {1, 2, 2, 3, 3, 3, 4, -5, 10, 10, 12}, 0,
                    new[] {1, 2, 3, 4, -5, 10, 12},
                    new IntReverseComparer());
            }
        }
        [TestCaseSource(nameof(ClearTestData))]
        [Test]
        public void Clear_IntArrayClear_EmptyTreeExpected
            (int[] dataArray, int expextedSize, int[] unexpectedData,
            IComparer<int> comparer)
        {
            //arrange
            BinarySearchTree<int> tree = new BinarySearchTree<int>(dataArray, comparer);
            //act
            tree.Clear();
            //assert
            Assert.AreEqual(expextedSize, tree.Count);
            foreach (var item in unexpectedData)
            {
                CollectionAssert.DoesNotContain(tree, item);
            }
        }
        
        public static IEnumerable<TestCaseData> AddContainsTestData
        {
            get
            {
                yield return new TestCaseData(new[] {1, 2, 2, 3, 3, 3, 4, -5, 10, 10, 12}, 7,
                    new[] {1, 0, 2, 3, 15, 4, -5, 10, 12, 24},
                    new[] {true, false, true, true, false, true, true, true, true, false}, null);
                yield return new TestCaseData(new[] {1, 2, 2, 3, 3, 3, 4, -5, 10, 10, 12}, 7,
                    new[] {1, 0, 2, 3, 15, 4, -5, 10, 12, 24},
                    new[] {true, false, true, true, false, true, true, true, true, false},
                    new IntReverseComparer());
            }
        }
        [TestCaseSource(nameof(AddContainsTestData))]
        [Test]
        public void AddContains_IntArray_TrueFalseResultsExpected
            (int[] dataArray, int expextedSize, int[] testData, bool[] expectedResults,
            IComparer<int> comparer)
        {
            //arrange
            BinarySearchTree<int> tree = new BinarySearchTree<int>(dataArray, comparer);
            //assert
            for (int i = 0; i < testData.Length; i++)
            {
                bool actual = tree.Contains(testData[i]);
                Assert.AreEqual(expectedResults[i], actual);
            }
        }
        
        public static IEnumerable<TestCaseData> CopyToTestData
        {
            get
            {
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 9,
                    new[] {20, 10, 5, 3, 8, 15, 30, 35, 33}, null);
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 9,
                    new[] {20, 30, 35, 33, 10, 15, 5, 8, 3},
                    new IntReverseComparer());
            }
        }
        [TestCaseSource(nameof(CopyToTestData))]
        [Test]
        public void CopyTo_IntArray_PreorderArrayExpected
            (int[] dataArray, int expextedSize, int[] expectedDataOrder,
            IComparer<int> comparer)
        {
            //arrange
            BinarySearchTree<int> tree = new BinarySearchTree<int>(comparer);
            int[] destArray = new int[expextedSize];
            foreach (int t in dataArray)
                tree.Add(t);
            //act
            tree.CopyTo(destArray, 0);
            //assert
            CollectionAssert.AreEqual(expectedDataOrder, destArray);
        }
        
        public static IEnumerable<TestCaseData> RemoveContainsTestData
        {
            get
            {
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 4,
                    new[] {20, 10, 15, 30, 35}, new[] {5, 3, 33, 8}, null);
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 4,
                    new[] {20, 10, 15, 30, 35}, new[] {5, 3, 33, 8},
                    new IntReverseComparer());
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 8,
                    new[] {8}, new[] {20, 10, 30, 5, 15, 35, 3, 33}, null);
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 8,
                    new[] {8}, new[] {20, 10, 30, 5, 15, 35, 3, 33},
                    new IntReverseComparer());
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 8,
                    new[] {30}, new[] {20, 10, 5, 15, 35, 3, 8, 33}, null);
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 8,
                    new[] {30}, new[] {20, 10, 5, 15, 35, 3, 8, 33},
                    new IntReverseComparer());
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 8,
                    new[] {35}, new[] {20, 10, 30, 5, 15, 3, 8, 33}, null);
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 8,
                    new[] {35}, new[] {20, 10, 30, 5, 15, 3, 8, 33},
                    new IntReverseComparer());
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 8,
                    new[] {20}, new[] {10, 30, 5, 15, 35, 3, 8, 33}, null);
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 8,
                    new[] {20}, new[] {10, 30, 5, 15, 35, 3, 8, 33},
                    new IntReverseComparer());
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 8,
                    new[] {10}, new[] {20, 30, 5, 15, 35, 3, 8, 33}, null);
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33}, 8,
                    new[] {10}, new[] {20, 30, 5, 15, 35, 3, 8, 33}, new IntReverseComparer());
            }
        }
        [TestCaseSource(nameof(RemoveContainsTestData))]
        [Test]
        public void RemoveContains_IntArray_TrueFalseResultsExpected
            (int[] dataArray, int expextedSize, int[] dataToDelete, 
                int[]remainingData, IComparer<int> comparer)
        {
            //arrange
            BinarySearchTree<int> tree = new BinarySearchTree<int>(dataArray, comparer);
            //act-assert
            foreach (int t in dataToDelete)
            {
                bool actual = tree.Remove(t);
                Assert.AreEqual(true, actual);
                actual = tree.Remove(t);
                Assert.AreEqual(false, actual);
            }
            //assert
            Assert.AreEqual(expextedSize, tree.Count);
            foreach (var item in dataToDelete)
            {
                Assert.AreEqual(false, tree.Contains(item));
            }
            foreach (var item in remainingData)
            {
                Assert.AreEqual(true, tree.Contains(item));
            }
            CollectionAssert.AreEquivalent(remainingData, tree);
        }
        
        public static IEnumerable<TestCaseData> GetPreorderEnumeratorTestData
        {
            get
            {
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33},
                    new[] {20, 10, 5, 3, 8, 15, 30, 35, 33}, null);
                yield return new TestCaseData(new[] {20, 10, 30, 5, 15, 35, 3, 8, 33},
                    new[] {20, 30, 35, 33, 10, 15, 5, 8, 3},
                    new IntReverseComparer());
            }
        }
        [TestCaseSource(nameof(GetPreorderEnumeratorTestData))]
        [Test]
        public void GetPreorderEnumerator_IntArray_preorderEnumerationExpected
            (int[] dataArray, int[] expectedDataOrder, IComparer<int> comparer)
        {
            //arrange
            BinarySearchTree<int> tree = new BinarySearchTree<int>(dataArray, comparer);
            //act
            IEnumerator<int> actual = tree.GetPreorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }
        
        public static IEnumerable<TestCaseData> GetInorderEnumeratorTestData
        {
            get
            {
                yield return new TestCaseData(new[] { 20, 10, 30, 5, 15, 35, 3, 8, 33 },
            new[] { 3, 5, 8, 10, 15, 20, 30, 33, 35 }, null);
                yield return new TestCaseData(new[] { 20, 10, 30, 5, 15, 35, 3, 8, 33 },
            new[] { 35, 33, 30, 20, 15, 10, 8, 5, 3}, new IntReverseComparer());
            }
        }
        [TestCaseSource(nameof(GetInorderEnumeratorTestData))]
        [Test]
        public void GetInorderEnumerator_IntArray_preorderEnumerationExpected
            (int[] dataArray, int[] expectedDataOrder, IComparer<int> comparer)
        {
            //arrange
            BinarySearchTree<int> tree = new BinarySearchTree<int>(dataArray, comparer);
            //act
            IEnumerator<int> actual = tree.GetInorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }
        
        public static IEnumerable<TestCaseData> GetPostorderEnumeratorTestData
        {
            get
            {
                yield return new TestCaseData(new[] { 20, 10, 30, 5, 15, 35, 3, 8, 33 },
            new[] { 3, 8, 5, 15, 10, 33, 35, 30, 20 }, null);
                yield return new TestCaseData(new[] { 20, 10, 30, 5, 15, 35, 3, 8, 33 },
            new[] { 33, 35, 30, 15, 8, 3, 5, 10, 20}, new IntReverseComparer());
            }
        }
        [TestCaseSource(nameof(GetPostorderEnumeratorTestData))]
        [Test]
        public void GetPostorderEnumerator_IntArray_preorderEnumerationExpected
            (int[] dataArray, int[] expectedDataOrder, IComparer<int> comparer)
        {
            //arrange
            BinarySearchTree<int> tree = new BinarySearchTree<int>(dataArray, comparer);
            //act
            IEnumerator<int> actual = tree.GetPostorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }
        #endregion

        #region System.String tests
        public class StringCustomComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return string.Compare(y, x, false, CultureInfo.InvariantCulture);
            }
        }

        public static IEnumerable<TestCaseData> CtorStringTestData
        {
            get
            {
                yield return new TestCaseData(new[] { "aa", "ab", "ad", "ab", "aab", "ad" }, 4,
                    new[] { "aa", "ab", "aab", "ad" }, new[] { "bc", "adf" }, null);
                yield return new TestCaseData(new[] { "aa", "ab", "ad", "ab", "aab", "ad" }, 4,
                    new[] { "aa", "ab", "aab", "ad" }, new[] { "bc", "adf" }, 
                    new StringCustomComparer());
            }
        }

        [TestCaseSource(nameof(CtorStringTestData))]
        [Test]
        public void Ctor_StringArray_TreeWithUniqueElementsExpected
            (string[] array, int expectedCount, string[] expectedElements, string[] unexpectedElements,
            IComparer<string> comparer)
        {
            //act
            BinarySearchTree<string> actual = new BinarySearchTree<string>(array, comparer);
            //assert
            Assert.AreEqual(expectedCount, actual.Count);
            CollectionAssert.AreEquivalent(expectedElements, actual);
            foreach (var item in unexpectedElements)
            {
                CollectionAssert.DoesNotContain(actual, item);
            }
        }

        public static IEnumerable<TestCaseData> AddStringTestData
        {
            get
            {
                yield return new TestCaseData(new[] { "aa", "ab", "ad", "ab", "aab", "ad" }, 4,
                    new[] { "aa", "ab", "aab", "ad" },
                    new[] { true, true, true, false, true, false },
                    null);
                yield return new TestCaseData(new[] { "aa", "ab", "ad", "ab", "aab", "ad" }, 4,
                    new[] { "aa", "ab", "aab", "ad" },
                    new[] { true, true, true, false, true, false },
                    new StringCustomComparer());
            }
        }
        [TestCaseSource(nameof(AddStringTestData))]
        [Test]
        public void Add_StringArray_TrueFalseResultsExpected
            (string[] dataArray, int expextedSize, string[] expectedData, bool[] expectedResults,
            IComparer<string> comparer)
        {
            //arrange
            BinarySearchTree<string> tree = new BinarySearchTree<string>(comparer);
            //act-assert
            for (int i = 0; i < dataArray.Length; i++)
            {
                bool actual = tree.Add(dataArray[i]);
                Assert.AreEqual(expectedResults[i], actual);
            }
            CollectionAssert.AreEquivalent(expectedData, tree);
        }

        public static IEnumerable<TestCaseData> ClearStringTestData
        {
            get
            {
                yield return new TestCaseData(
                    new[] { "aa", "ab", "ad", "ab", "aab", "ad" }, 0,
                    new[] { "aa", "ab", "aab", "ad" },
                    null);
                yield return new TestCaseData(
                    new[] { "aa", "ab", "ad", "ab", "aab", "ad" }, 0,
                    new[] { "aa", "ab", "aab", "ad" },
                    new StringCustomComparer());
            }
        }
        [TestCaseSource(nameof(ClearStringTestData))]
        [Test]
        public void Clear_StringArrayClear_EmptyTreeExpected
            (string[] dataArray, int expextedSize, string[] unexpectedData,
            IComparer<string> comparer)
        {
            //arrange
            BinarySearchTree<string> tree = new BinarySearchTree<string>(dataArray, comparer);
            //act
            tree.Clear();
            //assert
            Assert.AreEqual(expextedSize, tree.Count);
            foreach (var item in unexpectedData)
            {
                CollectionAssert.DoesNotContain(tree, item);
            }
        }

        public static IEnumerable<TestCaseData> AddContainsStringTestData
        {
            get
            {
                yield return new TestCaseData(new[] { "aa", "ab", "ad", "ab", "aab", "ad" }, 0,
                    new[] { "aa", "ab", "aab", "ad", "haha", "lol"},
                    new[] { true, true, true, true, false, false }, null);
                yield return new TestCaseData(new[] { "aa", "ab", "ad", "ab", "aab", "ad" }, 0,
                    new[] { "aa", "ab", "aab", "ad", "haha", "lol" },
                    new[] { true, true, true, true, false, false },
                    new StringCustomComparer());
            }
        }
        [TestCaseSource(nameof(AddContainsStringTestData))]
        [Test]
        public void AddContains_StringArray_TrueFalseResultsExpected
            (string[] dataArray, int expextedSize, string[] testData, bool[] expectedResults,
            IComparer<string> comparer)
        {
            //arrange
            BinarySearchTree<string> tree = new BinarySearchTree<string>(dataArray, comparer);
            //assert
            for (int i = 0; i < testData.Length; i++)
            {
                bool actual = tree.Contains(testData[i]);
                Assert.AreEqual(expectedResults[i], actual);
            }
        }

        public static IEnumerable<TestCaseData> CopyToStringTestData
        {
            get
            {
                yield return new TestCaseData(
                    new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 9,
                    new[] { "20", "10", "15", "30", "3", "5", "35", "33", "8" }, null);
                yield return new TestCaseData(
                    new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 9,
                    new[] { "20", "30", "5", "8", "35", "33", "3", "10", "15" },
                    new StringCustomComparer());
            }
        }
        [TestCaseSource(nameof(CopyToStringTestData))]
        [Test]
        public void CopyTo_StringArray_PreorderArrayExpected
            (string[] dataArray, int expextedSize, string[] expectedDataOrder,
            IComparer<string> comparer)
        {
            //arrange
            BinarySearchTree<string> tree = new BinarySearchTree<string>(comparer);
            string[] destArray = new string[expextedSize];
            foreach (string t in dataArray)
                tree.Add(t);
            //act
            tree.CopyTo(destArray, 0);
            //assert
            CollectionAssert.AreEqual(expectedDataOrder, destArray);
        }

        public static IEnumerable<TestCaseData> RemoveContainsStringTestData
        {
            get
            {
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 4,
                    new[] { "20", "10", "15", "30", "35" }, new[] { "5", "3", "33", "8" }, null);
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 4,
                    new[] { "20", "10", "15", "30", "35" }, new[] { "5", "3", "33", "8" },
                    new StringCustomComparer());
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 8,
                    new[] { "8" }, new[] { "20", "10", "30", "5", "15", "35", "3", "33" }, null);
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 8,
                    new[] { "8" }, new[] { "20", "10", "30", "5", "15", "35", "3", "33" },
                    new StringCustomComparer());
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 8,
                    new[] { "30" }, new[] { "20", "10", "5", "15", "35", "3", "8", "33" }, null);
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 8,
                    new[] { "30" }, new[] { "20", "10", "5", "15", "35", "3", "8", "33" },
                    new StringCustomComparer());
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 8,
                    new[] { "35" }, new[] { "20", "10", "30", "5", "15", "3", "8", "33" }, null);
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 8,
                    new[] { "35" }, new[] { "20", "10", "30", "5", "15", "3", "8", "33" },
                    new StringCustomComparer());
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 8,
                    new[] { "20" }, new[] { "10", "30", "5", "15", "35", "3", "8", "33" }, null);
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 8,
                    new[] { "20" }, new[] { "10", "30", "5", "15", "35", "3", "8", "33" },
                    new StringCustomComparer());
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 8,
                    new[] { "10" }, new[] { "20", "30", "5", "15", "35", "3", "8", "33" }, null);
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" }, 8,
                    new[] { "10" }, new[] { "20", "30", "5", "15", "35", "3", "8", "33" }, new StringCustomComparer());
            }
        }
        [TestCaseSource(nameof(RemoveContainsStringTestData))]
        [Test]
        public void RemoveContains_StringArray_TrueFalseResultsExpected
            (string[] dataArray, int expextedSize, string[] dataToDelete,
                string[] remainingData, IComparer<string> comparer)
        {
            //arrange
            BinarySearchTree<string> tree = new BinarySearchTree<string>(dataArray, comparer);
            //act-assert
            foreach (string t in dataToDelete)
            {
                bool actual = tree.Remove(t);
                Assert.AreEqual(true, actual);
                actual = tree.Remove(t);
                Assert.AreEqual(false, actual);
            }
            //assert
            Assert.AreEqual(expextedSize, tree.Count);
            foreach (var item in dataToDelete)
            {
                Assert.AreEqual(false, tree.Contains(item));
            }
            foreach (var item in remainingData)
            {
                Assert.AreEqual(true, tree.Contains(item));
            }
            CollectionAssert.AreEquivalent(remainingData, tree);
        }

        public static IEnumerable<TestCaseData> GetPreorderEnumeratorStringTestData
        {
            get
            {
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" },
                    new[] { "20", "10", "15", "30", "3", "5", "35", "33", "8" }, null);
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" },
                    new[] { "20", "30", "5", "8", "35", "33", "3", "10", "15" },
                    new StringCustomComparer());
            }
        }
        [TestCaseSource(nameof(GetPreorderEnumeratorStringTestData))]
        [Test]
        public void GetPreorderEnumerator_StringArray_preorderEnumerationExpected
            (string[] dataArray, string[] expectedDataOrder, IComparer<string> comparer)
        {
            //arrange
            BinarySearchTree<string> tree = new BinarySearchTree<string>(dataArray, comparer);
            //act
            IEnumerator<string> actual = tree.GetPreorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }

        public static IEnumerable<TestCaseData> GetInorderEnumeratorStringTestData
        {
            get
            {
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" },
                    new[] { "10", "15", "20", "3", "30", "33", "35", "5", "8" }, null);
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" },
                    new[] { "8", "5", "35", "33", "30", "3", "20", "15", "10" }, new StringCustomComparer());
            }
        }
        [TestCaseSource(nameof(GetInorderEnumeratorStringTestData))]
        [Test]
        public void GetInorderEnumerator_StringArray_preorderEnumerationExpected
            (string[] dataArray, string[] expectedDataOrder, IComparer<string> comparer)
        {
            //arrange
            BinarySearchTree<string> tree = new BinarySearchTree<string>(dataArray, comparer);
            //act
            IEnumerator<string> actual = tree.GetInorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }

        public static IEnumerable<TestCaseData> GetPostorderEnumeratorStringTestData
        {
            get
            {
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" },
                    new[] { "15", "10", "3", "33", "35", "8", "5", "30", "20" }, null);
                yield return new TestCaseData(new[] { "20", "10", "30", "5", "15", "35", "3", "8", "33" },
                    new[] { "8", "33", "35", "5", "3", "30", "15", "10", "20" }, new StringCustomComparer());
            }
        }
        [TestCaseSource(nameof(GetPostorderEnumeratorStringTestData))]
        [Test]
        public void GetPostorderEnumerator_StringArray_preorderEnumerationExpected
            (string[] dataArray, string[] expectedDataOrder, IComparer<string> comparer)
        {
            //arrange
            BinarySearchTree<string> tree = new BinarySearchTree<string>(dataArray, comparer);
            //act
            IEnumerator<string> actual = tree.GetPostorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }
        #endregion

        #region Book tests
        public class BookCustomComparer : IComparer<Book>
        {
            public int Compare(Book x, Book y)
            {
                return y.CompareTo(x);
            }
        }

        public static IEnumerable<TestCaseData> CtorBookTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("ad", "ad", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) }, 4,
                    new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) }, new[] { new Book("bc", "bc", 2016, 12m), new Book("adf", "adf", 2016, 12m) }, null);
                yield return new TestCaseData(new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("ad", "ad", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) }, 4,
                    new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) }, new[] { new Book("bc", "bc", 2016, 12m), new Book("adf", "adf", 2016, 12m) },
                    new BookCustomComparer());
            }
        }

        [TestCaseSource(nameof(CtorBookTestData))]
        [Test]
        public void Ctor_BookArray_TreeWithUniqueElementsExpected
            (Book[] array, int expectedCount, Book[] expectedElements, Book[] unexpectedElements,
            IComparer<Book> comparer)
        {
            //act
            BinarySearchTree<Book> actual = new BinarySearchTree<Book>(array, comparer);
            //assert
            Assert.AreEqual(expectedCount, actual.Count);
            CollectionAssert.AreEquivalent(expectedElements, actual);
            foreach (var item in unexpectedElements)
            {
                CollectionAssert.DoesNotContain(actual, item);
            }
        }

        public static IEnumerable<TestCaseData> AddBookTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("ad", "ad", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) }, 4,
                    new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) },
                    new[] { true, true, true, false, true, false },
                    null);
                yield return new TestCaseData(new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("ad", "ad", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) }, 4,
                    new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) },
                    new[] { true, true, true, false, true, false },
                    new BookCustomComparer());
            }
        }
        [TestCaseSource(nameof(AddBookTestData))]
        [Test]
        public void Add_BookArray_TrueFalseResultsExpected
            (Book[] dataArray, int expextedSize, Book[] expectedData, bool[] expectedResults,
            IComparer<Book> comparer)
        {
            //arrange
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(comparer);
            //act-assert
            for (int i = 0; i < dataArray.Length; i++)
            {
                bool actual = tree.Add(dataArray[i]);
                Assert.AreEqual(expectedResults[i], actual);
            }
            CollectionAssert.AreEquivalent(expectedData, tree);
        }

        public static IEnumerable<TestCaseData> ClearBookTestData
        {
            get
            {
                yield return new TestCaseData(
                    new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("ad", "ad", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) }, 0,
                    new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) },
                    null);
                yield return new TestCaseData(
                    new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("ad", "ad", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) }, 0,
                    new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) },
                    new BookCustomComparer());
            }
        }
        [TestCaseSource(nameof(ClearBookTestData))]
        [Test]
        public void Clear_BookArrayClear_EmptyTreeExpected
            (Book[] dataArray, int expextedSize, Book[] unexpectedData,
            IComparer<Book> comparer)
        {
            //arrange
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(dataArray, comparer);
            //act
            tree.Clear();
            //assert
            Assert.AreEqual(expextedSize, tree.Count);
            foreach (var item in unexpectedData)
            {
                CollectionAssert.DoesNotContain(tree, item);
            }
        }

        public static IEnumerable<TestCaseData> AddContainsBookTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("ad", "ad", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) }, 0,
                    new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m), new Book("haha", "haha", 2016, 12m), new Book("lol", "lol", 2016, 12m) },
                    new[] { true, true, true, true, false, false }, null);
                yield return new TestCaseData(new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("ad", "ad", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m) }, 0,
                    new[] { new Book("aa", "aa", 2016, 12m), new Book("ab", "ab", 2016, 12m), new Book("aab", "aab", 2016, 12m), new Book("ad", "ad", 2016, 12m), new Book("haha", "haha", 2016, 12m), new Book("lol", "lol", 2016, 12m) },
                    new[] { true, true, true, true, false, false },
                    new BookCustomComparer());
            }
        }
        [TestCaseSource(nameof(AddContainsBookTestData))]
        [Test]
        public void AddContains_BookArray_TrueFalseResultsExpected
            (Book[] dataArray, int expextedSize, Book[] testData, bool[] expectedResults,
            IComparer<Book> comparer)
        {
            //arrange
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(dataArray, comparer);
            //assert
            for (int i = 0; i < testData.Length; i++)
            {
                bool actual = tree.Contains(testData[i]);
                Assert.AreEqual(expectedResults[i], actual);
            }
        }

        public static IEnumerable<TestCaseData> CopyToBookTestData
        {
            get
            {
                yield return new TestCaseData(
                    new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 9,
                    new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("15", "15", 2016, 15m), new Book("30", "30", 2016, 30m), new Book("3", "3", 2016, 3m), new Book("5", "5", 2016, 5m), new Book("35", "35", 2016, 35m), new Book("33", "33", 2016, 33m), new Book("8", "8", 2016, 8m) }, null);
                yield return new TestCaseData(
                    new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 9,
                    new[] { new Book("20", "20", 2016, 20m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("8", "8", 2016, 8m), new Book("35", "35", 2016, 35m), new Book("33", "33", 2016, 33m), new Book("3", "3", 2016, 3m), new Book("10", "10", 2016, 10m), new Book("15", "15", 2016, 15m) },
                    new BookCustomComparer());
            }
        }
        [TestCaseSource(nameof(CopyToBookTestData))]
        [Test]
        public void CopyTo_BookArray_PreorderArrayExpected
            (Book[] dataArray, int expextedSize, Book[] expectedDataOrder,
            IComparer<Book> comparer)
        {
            //arrange
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(comparer);
            Book[] destArray = new Book[expextedSize];
            foreach (Book t in dataArray)
                tree.Add(t);
            //act
            tree.CopyTo(destArray, 0);
            //assert
            CollectionAssert.AreEqual(expectedDataOrder, destArray);
        }

        public static IEnumerable<TestCaseData> RemoveContainsBookTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 4,
                    new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("15", "15", 2016, 15m), new Book("30", "30", 2016, 30m), new Book("35", "35", 2016, 35m) }, new[] { new Book("5", "5", 2016, 5m), new Book("3", "3", 2016, 3m), new Book("33", "33", 2016, 33m), new Book("8", "8", 2016, 8m) }, null);
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 4,
                    new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("15", "15", 2016, 15m), new Book("30", "30", 2016, 30m), new Book("35", "35", 2016, 35m) }, new[] { new Book("5", "5", 2016, 5m), new Book("3", "3", 2016, 3m), new Book("33", "33", 2016, 33m), new Book("8", "8", 2016, 8m) },
                    new BookCustomComparer());
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 8,
                    new[] { new Book("8", "8", 2016, 8m) }, new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("33", "33", 2016, 33m) }, null);
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 8,
                    new[] { new Book("8", "8", 2016, 8m) }, new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("33", "33", 2016, 33m) },
                    new BookCustomComparer());
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 8,
                    new[] { new Book("30", "30", 2016, 30m) }, new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, null);
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 8,
                    new[] { new Book("30", "30", 2016, 30m) }, new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) },
                    new BookCustomComparer());
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 8,
                    new[] { new Book("35", "35", 2016, 35m) }, new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, null);
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 8,
                    new[] { new Book("35", "35", 2016, 35m) }, new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) },
                    new BookCustomComparer());
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 8,
                    new[] { new Book("20", "20", 2016, 20m) }, new[] { new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, null);
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 8,
                    new[] { new Book("20", "20", 2016, 20m) }, new[] { new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) },
                    new BookCustomComparer());
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 8,
                    new[] { new Book("10", "10", 2016, 10m) }, new[] { new Book("20", "20", 2016, 20m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, null);
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, 8,
                    new[] { new Book("10", "10", 2016, 10m) }, new[] { new Book("20", "20", 2016, 20m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) }, new BookCustomComparer());
            }
        }
        [TestCaseSource(nameof(RemoveContainsBookTestData))]
        [Test]
        public void RemoveContains_BookArray_TrueFalseResultsExpected
            (Book[] dataArray, int expextedSize, Book[] dataToDelete,
                Book[] remainingData, IComparer<Book> comparer)
        {
            //arrange
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(dataArray, comparer);
            //act-assert
            foreach (Book t in dataToDelete)
            {
                bool actual = tree.Remove(t);
                Assert.AreEqual(true, actual);
                actual = tree.Remove(t);
                Assert.AreEqual(false, actual);
            }
            //assert
            Assert.AreEqual(expextedSize, tree.Count);
            foreach (var item in dataToDelete)
            {
                Assert.AreEqual(false, tree.Contains(item));
            }
            foreach (var item in remainingData)
            {
                Assert.AreEqual(true, tree.Contains(item));
            }
            CollectionAssert.AreEquivalent(remainingData, tree);
        }

        public static IEnumerable<TestCaseData> GetPreorderEnumeratorBookTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) },
                    new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("15", "15", 2016, 15m), new Book("30", "30", 2016, 30m), new Book("3", "3", 2016, 3m), new Book("5", "5", 2016, 5m), new Book("35", "35", 2016, 35m), new Book("33", "33", 2016, 33m), new Book("8", "8", 2016, 8m) }, null);
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) },
                    new[] { new Book("20", "20", 2016, 20m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("8", "8", 2016, 8m), new Book("35", "35", 2016, 35m), new Book("33", "33", 2016, 33m), new Book("3", "3", 2016, 3m), new Book("10", "10", 2016, 10m), new Book("15", "15", 2016, 15m) },
                    new BookCustomComparer());
            }
        }
        [TestCaseSource(nameof(GetPreorderEnumeratorBookTestData))]
        [Test]
        public void GetPreorderEnumerator_BookArray_preorderEnumerationExpected
            (Book[] dataArray, Book[] expectedDataOrder, IComparer<Book> comparer)
        {
            //arrange
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(dataArray, comparer);
            //act
            IEnumerator<Book> actual = tree.GetPreorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }

        public static IEnumerable<TestCaseData> GetInorderEnumeratorBookTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) },
                    new[] { new Book("10", "10", 2016, 10m), new Book("15", "15", 2016, 15m), new Book("20", "20", 2016, 20m), new Book("3", "3", 2016, 3m), new Book("30", "30", 2016, 30m), new Book("33", "33", 2016, 33m), new Book("35", "35", 2016, 35m), new Book("5", "5", 2016, 5m), new Book("8", "8", 2016, 8m) }, null);
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) },
                    new[] { new Book("8", "8", 2016, 8m), new Book("5", "5", 2016, 5m), new Book("35", "35", 2016, 35m), new Book("33", "33", 2016, 33m), new Book("30", "30", 2016, 30m), new Book("3", "3", 2016, 3m), new Book("20", "20", 2016, 20m), new Book("15", "15", 2016, 15m), new Book("10", "10", 2016, 10m) }, new BookCustomComparer());
            }
        }
        [TestCaseSource(nameof(GetInorderEnumeratorBookTestData))]
        [Test]
        public void GetInorderEnumerator_BookArray_preorderEnumerationExpected
            (Book[] dataArray, Book[] expectedDataOrder, IComparer<Book> comparer)
        {
            //arrange
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(dataArray, comparer);
            //act
            IEnumerator<Book> actual = tree.GetInorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }

        public static IEnumerable<TestCaseData> GetPostorderEnumeratorBookTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) },
                    new[] { new Book("15", "15", 2016, 15m), new Book("10", "10", 2016, 10m), new Book("3", "3", 2016, 3m), new Book("33", "33", 2016, 33m), new Book("35", "35", 2016, 35m), new Book("8", "8", 2016, 8m), new Book("5", "5", 2016, 5m), new Book("30", "30", 2016, 30m), new Book("20", "20", 2016, 20m) }, null);
                yield return new TestCaseData(new[] { new Book("20", "20", 2016, 20m), new Book("10", "10", 2016, 10m), new Book("30", "30", 2016, 30m), new Book("5", "5", 2016, 5m), new Book("15", "15", 2016, 15m), new Book("35", "35", 2016, 35m), new Book("3", "3", 2016, 3m), new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m) },
                    new[] { new Book("8", "8", 2016, 8m), new Book("33", "33", 2016, 33m), new Book("35", "35", 2016, 35m), new Book("5", "5", 2016, 5m), new Book("3", "3", 2016, 3m), new Book("30", "30", 2016, 30m), new Book("15", "15", 2016, 15m), new Book("10", "10", 2016, 10m), new Book("20", "20", 2016, 20m) }, new BookCustomComparer());
            }
        }
        [TestCaseSource(nameof(GetPostorderEnumeratorBookTestData))]
        [Test]
        public void GetPostorderEnumerator_BookArray_preorderEnumerationExpected
            (Book[] dataArray, Book[] expectedDataOrder, IComparer<Book> comparer)
        {
            //arrange
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(dataArray, comparer);
            //act
            IEnumerator<Book> actual = tree.GetPostorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }
        #endregion

        #region struct Point tests

        public struct Point
        {
            private int x;
            private int y;
            public int X => x;
            public int Y => y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public class PointOrderComparer : IComparer<Point>
        {
            public int Compare(Point x, Point y)
            {
                int res = x.X.CompareTo(y.X);
                if (res == 0)
                    res = x.Y.CompareTo(y.Y);
                return res;
            }
        }

        public class PointReverseComparer : IComparer<Point>
        {
            public int Compare(Point x, Point y)
            {
                int res = y.X.CompareTo(x.X);
                if (res == 0)
                    res = y.Y.CompareTo(x.Y);
                return res;
            }
        }

        public static IEnumerable<TestCaseData> CtorPointTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Point(1, 1), new Point(2, 2), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(12, 12) }, 7,
                    new[] { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(12, 12) }, new[] { new Point(0, 0), new Point(11, 11), new Point(9, 9) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(1, 1), new Point(2, 2), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(12, 12) }, 7,
                    new[] { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(12, 12) }, new[] { new Point(0, 0), new Point(11, 11), new Point(9, 9) }, new PointReverseComparer());
            }
        }

        [TestCaseSource(nameof(CtorPointTestData))]
        [Test]
        public void Ctor_PointArray_TreeWithUniqueElementsExpected
            (Point[] array, int expectedCount, Point[] expectedElements, Point[] unexpectedElements,
            IComparer<Point> comparer)
        {
            //act
            BinarySearchTree<Point> actual = new BinarySearchTree<Point>(array, comparer);
            //assert
            Assert.AreEqual(expectedCount, actual.Count);
            CollectionAssert.AreEquivalent(expectedElements, actual);
            foreach (var item in unexpectedElements)
            {
                CollectionAssert.DoesNotContain(actual, item);
            }
        }

        public static IEnumerable<TestCaseData> AddPointTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Point(1, 1), new Point(2, 2), new Point(2, 2), new Point(3, 3), new Point(3, 3), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(10, 10), new Point(12, 12) }, 7,
                    new[] { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(12, 12) },
                    new[] { true, true, false, true, false, false, true, true, true, false, true },
                    new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(1, 1), new Point(2, 2), new Point(2, 2), new Point(3, 3), new Point(3, 3), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(10, 10), new Point(12, 12) }, 7,
                    new[] { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(12, 12) },
                    new[] { true, true, false, true, false, false, true, true, true, false, true },
                    new PointReverseComparer());
            }
        }
        [TestCaseSource(nameof(AddPointTestData))]
        [Test]
        public void Add_PointArray_TrueFalseResultsExpected
            (Point[] dataArray, int expextedSize, Point[] expectedData, bool[] expectedResults,
            IComparer<Point> comparer)
        {
            //arrange
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(comparer);
            //act-assert
            for (int i = 0; i < dataArray.Length; i++)
            {
                bool actual = tree.Add(dataArray[i]);
                Assert.AreEqual(expectedResults[i], actual);
            }
            CollectionAssert.AreEquivalent(expectedData, tree);
        }

        public static IEnumerable<TestCaseData> ClearPointTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Point(1, 1), new Point(2, 2), new Point(2, 2), new Point(3, 3), new Point(3, 3), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(10, 10), new Point(12, 12) }, 0,
                    new[] { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(12, 12) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(1, 1), new Point(2, 2), new Point(2, 2), new Point(3, 3), new Point(3, 3), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(10, 10), new Point(12, 12) }, 0,
                    new[] { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(12, 12) },
                    new PointReverseComparer());
            }
        }
        [TestCaseSource(nameof(ClearPointTestData))]
        [Test]
        public void Clear_PointArrayClear_EmptyTreeExpected
            (Point[] dataArray, int expextedSize, Point[] unexpectedData,
            IComparer<Point> comparer)
        {
            //arrange
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(dataArray, comparer);
            //act
            tree.Clear();
            //assert
            Assert.AreEqual(expextedSize, tree.Count);
            foreach (var item in unexpectedData)
            {
                CollectionAssert.DoesNotContain(tree, item);
            }
        }

        public static IEnumerable<TestCaseData> AddContainsPointTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Point(1, 1), new Point(2, 2), new Point(2, 2), new Point(3, 3), new Point(3, 3), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(10, 10), new Point(12, 12) }, 7,
                    new[] { new Point(1, 1), new Point(0, 0), new Point(2, 2), new Point(3, 3), new Point(15, 15), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(12, 12), new Point(24, 24) },
                    new[] { true, false, true, true, false, true, true, true, true, false }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(1, 1), new Point(2, 2), new Point(2, 2), new Point(3, 3), new Point(3, 3), new Point(3, 3), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(10, 10), new Point(12, 12) }, 7,
                    new[] { new Point(1, 1), new Point(0, 0), new Point(2, 2), new Point(3, 3), new Point(15, 15), new Point(4, 4), new Point(-5, -5), new Point(10, 10), new Point(12, 12), new Point(24, 24) },
                    new[] { true, false, true, true, false, true, true, true, true, false },
                    new PointReverseComparer());
            }
        }
        [TestCaseSource(nameof(AddContainsPointTestData))]
        [Test]
        public void AddContains_PointArray_TrueFalseResultsExpected
            (Point[] dataArray, int expextedSize, Point[] testData, bool[] expectedResults,
            IComparer<Point> comparer)
        {
            //arrange
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(dataArray, comparer);
            //assert
            for (int i = 0; i < testData.Length; i++)
            {
                bool actual = tree.Contains(testData[i]);
                Assert.AreEqual(expectedResults[i], actual);
            }
        }

        public static IEnumerable<TestCaseData> CopyToPointTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 9,
                    new[] { new Point(20, 20), new Point(10, 10), new Point(5, 5), new Point(3, 3), new Point(8, 8), new Point(15, 15), new Point(30, 30), new Point(35, 35), new Point(33, 33) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 9,
                    new[] { new Point(20, 20), new Point(30, 30), new Point(35, 35), new Point(33, 33), new Point(10, 10), new Point(15, 15), new Point(5, 5), new Point(8, 8), new Point(3, 3) },
                    new PointReverseComparer());
            }
        }
        [TestCaseSource(nameof(CopyToPointTestData))]
        [Test]
        public void CopyTo_PointArray_PreorderArrayExpected
            (Point[] dataArray, int expextedSize, Point[] expectedDataOrder,
            IComparer<Point> comparer)
        {
            //arrange
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(comparer);
            Point[] destArray = new Point[expextedSize];
            foreach (Point t in dataArray)
                tree.Add(t);
            //act
            tree.CopyTo(destArray, 0);
            //assert
            CollectionAssert.AreEqual(expectedDataOrder, destArray);
        }

        public static IEnumerable<TestCaseData> RemoveContainsPointTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 4,
                    new[] { new Point(20, 20), new Point(10, 10), new Point(15, 15), new Point(30, 30), new Point(35, 35) }, new[] { new Point(5, 5), new Point(3, 3), new Point(33, 33), new Point(8, 8) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 4,
                    new[] { new Point(20, 20), new Point(10, 10), new Point(15, 15), new Point(30, 30), new Point(35, 35) }, new[] { new Point(5, 5), new Point(3, 3), new Point(33, 33), new Point(8, 8) },
                    new PointReverseComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 8,
                    new[] { new Point(8, 8) }, new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(33, 33) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 8,
                    new[] { new Point(8, 8) }, new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(33, 33) },
                    new PointReverseComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 8,
                    new[] { new Point(30, 30) }, new[] { new Point(20, 20), new Point(10, 10), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 8,
                    new[] { new Point(30, 30) }, new[] { new Point(20, 20), new Point(10, 10), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) },
                    new PointReverseComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 8,
                    new[] { new Point(35, 35) }, new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 8,
                    new[] { new Point(35, 35) }, new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(3, 3), new Point(8, 8), new Point(33, 33) },
                    new PointReverseComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 8,
                    new[] { new Point(20, 20) }, new[] { new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 8,
                    new[] { new Point(20, 20) }, new[] { new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) },
                    new PointReverseComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 8,
                    new[] { new Point(10, 10) }, new[] { new Point(20, 20), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, 8,
                    new[] { new Point(10, 10) }, new[] { new Point(20, 20), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) }, new PointReverseComparer());
            }
        }
        [TestCaseSource(nameof(RemoveContainsPointTestData))]
        [Test]
        public void RemoveContains_PointArray_TrueFalseResultsExpected
            (Point[] dataArray, int expextedSize, Point[] dataToDelete,
                Point[] remainingData, IComparer<Point> comparer)
        {
            //arrange
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(dataArray, comparer);
            //act-assert
            foreach (Point t in dataToDelete)
            {
                bool actual = tree.Remove(t);
                Assert.AreEqual(true, actual);
                actual = tree.Remove(t);
                Assert.AreEqual(false, actual);
            }
            //assert
            Assert.AreEqual(expextedSize, tree.Count);
            foreach (var item in dataToDelete)
            {
                Assert.AreEqual(false, tree.Contains(item));
            }
            foreach (var item in remainingData)
            {
                Assert.AreEqual(true, tree.Contains(item));
            }
            CollectionAssert.AreEquivalent(remainingData, tree);
        }

        public static IEnumerable<TestCaseData> GetPreorderEnumeratorPointTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) },
                    new[] { new Point(20, 20), new Point(10, 10), new Point(5, 5), new Point(3, 3), new Point(8, 8), new Point(15, 15), new Point(30, 30), new Point(35, 35), new Point(33, 33) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) },
                    new[] { new Point(20, 20), new Point(30, 30), new Point(35, 35), new Point(33, 33), new Point(10, 10), new Point(15, 15), new Point(5, 5), new Point(8, 8), new Point(3, 3) },
                    new PointReverseComparer());
            }
        }
        [TestCaseSource(nameof(GetPreorderEnumeratorPointTestData))]
        [Test]
        public void GetPreorderEnumerator_PointArray_preorderEnumerationExpected
            (Point[] dataArray, Point[] expectedDataOrder, IComparer<Point> comparer)
        {
            //arrange
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(dataArray, comparer);
            //act
            IEnumerator<Point> actual = tree.GetPreorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }

        public static IEnumerable<TestCaseData> GetInorderEnumeratorPointTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33), },
            new[] { new Point(3, 3), new Point(5, 5), new Point(8, 8), new Point(10, 10), new Point(15, 15), new Point(20, 20), new Point(30, 30), new Point(33, 33), new Point(35, 35), }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) },
            new[] { new Point(35, 35), new Point(33, 33), new Point(30, 30), new Point(20, 20), new Point(15, 15), new Point(10, 10), new Point(8, 8), new Point(5, 5), new Point(3, 3) }, new PointReverseComparer());
            }
        }
        [TestCaseSource(nameof(GetInorderEnumeratorPointTestData))]
        [Test]
        public void GetInorderEnumerator_PointArray_preorderEnumerationExpected
            (Point[] dataArray, Point[] expectedDataOrder, IComparer<Point> comparer)
        {
            //arrange
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(dataArray, comparer);
            //act
            IEnumerator<Point> actual = tree.GetInorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }

        public static IEnumerable<TestCaseData> GetPostorderEnumeratorPointTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) },
            new[] { new Point(3, 3), new Point(8, 8), new Point(5, 5), new Point(15, 15), new Point(10, 10), new Point(33, 33), new Point(35, 35), new Point(30, 30), new Point(20, 20) }, new PointOrderComparer());
                yield return new TestCaseData(new[] { new Point(20, 20), new Point(10, 10), new Point(30, 30), new Point(5, 5), new Point(15, 15), new Point(35, 35), new Point(3, 3), new Point(8, 8), new Point(33, 33) },
            new[] { new Point(33, 33), new Point(35, 35), new Point(30, 30), new Point(15, 15), new Point(8, 8), new Point(3, 3), new Point(5, 5), new Point(10, 10), new Point(20, 20) }, new PointReverseComparer());
            }
        }
        [TestCaseSource(nameof(GetPostorderEnumeratorPointTestData))]
        [Test]
        public void GetPostorderEnumerator_PointArray_preorderEnumerationExpected
            (Point[] dataArray, Point[] expectedDataOrder, IComparer<Point> comparer)
        {
            //arrange
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(dataArray, comparer);
            //act
            IEnumerator<Point> actual = tree.GetPostorderEnumerator();
            //assert
            int i = 0;
            while (actual.MoveNext())
            {
                Assert.AreEqual(expectedDataOrder[i++], actual.Current);
            }
            Assert.AreEqual(i, expectedDataOrder.Length);
        }
        #endregion
    }
}
