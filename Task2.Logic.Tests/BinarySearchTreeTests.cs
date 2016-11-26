using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task2.Logic.Tests
{
    [TestFixture]
    public class BinarySearchTreeTests
    {
        [TestCase(new [] {1, 2, 2, 3, 4,-5, 10, 12}, 6, 
            new [] {1, 2, 3, 4, -5, 10, 12}, new [] {0, 11, 9})]
        [Test]
        public void Ctor_IntArray_TreeWithUniqueElementsExpected
            (int[] array, int expectedCount, int[] expectedElements, int[] unexpectedElements)
        {
            //act
            BinarySearchTree<int> actual = new BinarySearchTree<int>(array);
            //assert
            Assert.AreEqual(expectedCount, actual.Count);
            CollectionAssert.AreEquivalent(expectedElements, actual);
            CollectionAssert.IsNotSubsetOf(unexpectedElements, actual);
        }
    }
}
