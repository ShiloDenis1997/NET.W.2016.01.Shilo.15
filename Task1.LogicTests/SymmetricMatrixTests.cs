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
    public class SymmetricMatrixTests
    {
        [Test]
        public void Ctor_IntArray_SymmetricMatrixExpected()
        {
            //arrange
            int[,] array =
            {
                {1, 2, 3, 4, 5},
                {1, 2, 3, 4, 5},
                {1, 2, 3, 4, 5},
                {1, 2, 3, 4, 5},
                {1, 2, 3, 4, 5},
            };
            //act
            SymmetricMatrix<int> actualMatrix = new SymmetricMatrix<int>(array);
            //assert
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(0); j++)
                    Assert.AreEqual(array[Math.Max(i, j), Math.Min(i, j)], actualMatrix[i, j]);
        }

        [TestCase(1, 2, 2, typeof(ArgumentOutOfRangeException))]
        [TestCase(3, 2, 3, typeof(ArgumentOutOfRangeException))]
        [TestCase(4, -1, 2, typeof(ArgumentOutOfRangeException))]
        [TestCase(5, 5, 2, typeof(ArgumentOutOfRangeException))]
        [TestCase(5, 3, -1, typeof(ArgumentOutOfRangeException))]
        [Test]
        public void Getter_ExceptionExpected(int size, int i, int j, Type exceptionType)
        {
            //arrange
            SymmetricMatrix<int> matrix = new SymmetricMatrix<int>(size);
            //act-assert
            Assert.Throws(exceptionType, () =>
            {
                int x = matrix[i, j];
            });
        }

        [TestCase(1, 2, 2, typeof(ArgumentOutOfRangeException))]
        [TestCase(3, 2, 3, typeof(ArgumentOutOfRangeException))]
        [TestCase(4, -1, 2, typeof(ArgumentOutOfRangeException))]
        [TestCase(5, 5, 2, typeof(ArgumentOutOfRangeException))]
        [TestCase(5, 3, -1, typeof(ArgumentOutOfRangeException))]
        [Test]
        public void Setter_ExceptionExpected(int size, int i, int j, Type exceptionType)
        {
            //arrange
            SymmetricMatrix<int> matrix = new SymmetricMatrix<int>(size);
            //assert-act
            Assert.Throws(exceptionType, () =>
            {
                matrix[i, j] = 42;
            });
        }

        [TestCase(2, 1, 1)]
        [TestCase(2, 1, 0)]
        [TestCase(1, 0, 0)]
        [Test]
        public void ElementChangedEvent_EventHandlerWorksExpected(int size, int i, int j)
        {
            //arrange
            int resI = i + 1, resJ = 2;
            SymmetricMatrix<int> matrix = new SymmetricMatrix<int>(size);
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
