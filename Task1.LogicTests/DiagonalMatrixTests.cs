using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task1.Logic;

namespace Task1.LogicTests
{
    [TestFixture]
    public class DiagonalMatrixTests
    {
        [TestCase(new [] {1, 2, 3, 4, 5})]
        [TestCase(new [] {1})]
        [Test]
        public void Ctor_IntArray_DiagonalMatrixExpected(int[] array)
        {
            //act
            DiagonalMatrix<int> actualMatrix = new DiagonalMatrix<int>(array);
            //assert
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array.Length; j++)
                    if (i == j)
                        Assert.AreEqual(array[i], actualMatrix[i, i]);
                    else
                    {
                        Assert.AreEqual(0, actualMatrix[i, j]);
                    }
        }

        [Test]
        public void GetEnumerator_ElementsOfMatrixExpected()
        {
            //arrange
            int[] array = {1, 2, 3, 4, 5};

            DiagonalMatrix<int> actualMatrix = new DiagonalMatrix<int>(array);
            //act
            IEnumerator<int> enumerator = actualMatrix.GetEnumerator();
            enumerator.MoveNext();
            //assert
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    if (i == j)
                        Assert.AreEqual(array[i], enumerator.Current);
                    else
                        Assert.AreEqual(0, enumerator.Current);
                    enumerator.MoveNext();
                }
        }

        [TestCase(1, 2, 2, typeof(InvalidMatrixIndexException))]
        [TestCase(3, 2, 3, typeof(InvalidMatrixIndexException))]
        [TestCase(4, -1, 2, typeof(InvalidMatrixIndexException))]
        [TestCase(5, 5, 2, typeof(InvalidMatrixIndexException))]
        [TestCase(5, 3, -1, typeof(InvalidMatrixIndexException))]
        [Test]
        public void Getter_ExceptionExpected(int size, int i, int j, Type exceptionType)
        {
            //arrange
            DiagonalMatrix<int> matrix = new DiagonalMatrix<int>(size);
            //act-assert
            Assert.Throws(exceptionType, () =>
            {
                int x = matrix[i, j];
            });
        }

        [TestCase(1, 2, 2, typeof(InvalidMatrixIndexException))]
        [TestCase(3, 2, 3, typeof(InvalidMatrixIndexException))]
        [TestCase(4, -1, 2, typeof(InvalidMatrixIndexException))]
        [TestCase(5, 5, 2, typeof(InvalidMatrixIndexException))]
        [TestCase(5, 3, -1, typeof(InvalidMatrixIndexException))]
        [TestCase(5, 3, 2, typeof(InvalidMatrixIndexException))]
        [TestCase(5, 3, 1, typeof(InvalidMatrixIndexException))]
        [Test]
        public void Setter_ExceptionExpected(int size, int i, int j, Type exceptionType)
        {
            //arrange
            DiagonalMatrix<int> matrix = new DiagonalMatrix<int>(size);
            //assert-act
            Assert.Throws(exceptionType, () =>
            {
                matrix[i, j] = 42;
            });
        }

        [Test]
        public void ElementChangedEvent_EventHandlerWorksExpected()
        {
            //arrange
            int i = 1;
            int j = 1;
            int resI = i + 1, resJ = 2;
            DiagonalMatrix<int> matrix = new DiagonalMatrix<int>(3);
            matrix.ElementChanged += (sender, args) =>
            {
                resI = args.Row;
                resJ = args.Column;
            };
            //act
            matrix[i, j] = 5;
            //assert
            Assert.AreEqual(i, resI);
            Assert.AreEqual(j, resJ);
        }
    }
}
