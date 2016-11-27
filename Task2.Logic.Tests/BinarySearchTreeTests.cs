﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

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
    }
}
