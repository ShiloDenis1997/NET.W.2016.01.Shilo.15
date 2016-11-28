using System;
using NUnit.Framework;
using Task1.ExtensionLogic;
using Task1.Logic;

namespace Task1.ExtensionLogicTests
{
    [TestFixture]
    public class MatrixExtensionsTests
    {
        [Test]
        public void MatrixSum_IntSquareSquare_SumMatrixExpected()
        {
            int[,] matrix = 
            {
                {1, 2, 3, 4},
                {1, 2, 3, 4},
                {5, 2, 3, 6},
                {3, 5, 2, 1},
            };
            AbstractSquareMatrix<int> firstMatrix = new SquareMatrix<int>(matrix);
            AbstractSquareMatrix<int> secondMatrix = new SquareMatrix<int>(matrix);
            var resultMatrix = MatrixExtensions.AddMatrix
                (firstMatrix, secondMatrix);
            Assert.AreEqual(typeof(SquareMatrix<int>), resultMatrix.GetType());
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(firstMatrix[i, j] + secondMatrix[i, j], resultMatrix[i, j]);
        }

        [Test]
        public void MatrixSum_IntSquareDiagonal_SumMatrixExpected()
        {
            int[,] matrix =
            {
                {1, 2, 3, 4},
                {1, 2, 3, 4},
                {5, 2, 3, 6},
                {3, 5, 2, 1},
            };
            int[] diagonal = {1, 2, 3, 4};
            AbstractSquareMatrix<int> firstMatrix = new SquareMatrix<int>(matrix);
            AbstractSquareMatrix<int> secondMatrix = new DiagonalMatrix<int>(diagonal);
            var resultMatrix = MatrixExtensions.AddMatrix
                (firstMatrix, secondMatrix);
            Assert.AreEqual(typeof(SquareMatrix<int>), resultMatrix.GetType());
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(firstMatrix[i, j] + secondMatrix[i, j], resultMatrix[i, j]);
        }
        public class IntWrapper
        {
            public int X { get; set; }

            public IntWrapper(int x)
            {
                X = x;
            }
        }

        [Test]
        public void MatrixSum_IntSquareSymmetric_SumMatrixExpected()
        {
            int[,] matrix =
            {
                {1,    2, 3, 4},
                {1, 2,    3, 4},
                {5, 2, 3,    6},
                {3, 5, 2, 1},
            };
            AbstractSquareMatrix<int> firstMatrix = new SquareMatrix<int>(matrix);
            AbstractSquareMatrix<int> secondMatrix = new SymmetricMatrix<int>(matrix);
            var resultMatrix = MatrixExtensions.AddMatrix
                (firstMatrix, secondMatrix);
            Assert.AreEqual(typeof(SquareMatrix<int>), resultMatrix.GetType());
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(firstMatrix[i, j] + secondMatrix[i, j], resultMatrix[i, j]);
        }

        [Test]
        public void MatrixSum_IntSymmetricSymmetric_SumMatrixExpected()
        {
            int[,] matrix =
            {
                {1, 2, 3, 4},
                {1, 2, 3, 4},
                {5, 2, 3, 6},
                {3, 5, 2, 1},
            };
            AbstractSquareMatrix<int> firstMatrix = new SymmetricMatrix<int>(matrix);
            AbstractSquareMatrix<int> secondMatrix = new SymmetricMatrix<int>(matrix);
            var resultMatrix = MatrixExtensions.AddMatrix
                (firstMatrix, secondMatrix);
            Assert.AreEqual(typeof(SymmetricMatrix<int>), resultMatrix.GetType());
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(firstMatrix[i, j] + secondMatrix[i, j], resultMatrix[i, j]);
        }

        [Test]
        public void MatrixSum_IntSymmetricSquare_SumMatrixExpected()
        {
            int[,] matrix =
            {
                {1,    2, 3, 4},
                {1, 2,    3, 4},
                {5, 2, 3,    6},
                {3, 5, 2, 1},
            };
            AbstractSquareMatrix<int> firstMatrix = new SquareMatrix<int>(matrix);
            AbstractSquareMatrix<int> secondMatrix = new SymmetricMatrix<int>(matrix);
            var resultMatrix = MatrixExtensions.AddMatrix
                (secondMatrix, firstMatrix);
            Assert.AreEqual(typeof(SquareMatrix<int>), resultMatrix.GetType());
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(firstMatrix[i, j] + secondMatrix[i, j], resultMatrix[i, j]);
        }

        [Test]
        public void MatrixSum_IntSymmetricDiagonal_SumMatrixExpected()
        {
            int[,] matrix =
            {
                {1,    2, 3, 4},
                {1, 2,    3, 4},
                {5, 2, 3,    6},
                {3, 5, 2, 1},
            };
            int[] diagonal = {1, 2, 3, 4};
            AbstractSquareMatrix<int> firstMatrix = new SymmetricMatrix<int>(matrix);
            AbstractSquareMatrix<int> secondMatrix = new DiagonalMatrix<int>(diagonal);
            var resultMatrix = MatrixExtensions.AddMatrix
                (firstMatrix, secondMatrix);
            Assert.AreEqual(typeof(SymmetricMatrix<int>), resultMatrix.GetType());
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(firstMatrix[i, j] + secondMatrix[i, j], resultMatrix[i, j]);
        }

        [Test]
        public void MatrixSum_IntDiagonalSymmetric_SumMatrixExpected()
        {
            int[,] matrix =
            {
                {1,    2, 3, 4},
                {1, 2,    3, 4},
                {5, 2, 3,    6},
                {3, 5, 2, 1},
            };
            int[] diagonal = { 1, 2, 3, 4 };
            AbstractSquareMatrix<int> firstMatrix = new SymmetricMatrix<int>(matrix);
            AbstractSquareMatrix<int> secondMatrix = new DiagonalMatrix<int>(diagonal);
            var resultMatrix = MatrixExtensions.AddMatrix
                (secondMatrix, firstMatrix);
            Assert.AreEqual(typeof(SymmetricMatrix<int>), resultMatrix.GetType());
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(firstMatrix[i, j] + secondMatrix[i, j], resultMatrix[i, j]);
        }

        [Test]
        public void MatrixSum_IntDiagonalSquare_SumMatrixExpected()
        {
            int[,] matrix =
            {
                {1,    2, 3, 4},
                {1, 2,    3, 4},
                {5, 2, 3,    6},
                {3, 5, 2, 1},
            };
            int[] diagonal = { 1, 2, 3, 4 };
            AbstractSquareMatrix<int> firstMatrix = new SquareMatrix<int>(matrix);
            AbstractSquareMatrix<int> secondMatrix = new DiagonalMatrix<int>(diagonal);
            var resultMatrix = MatrixExtensions.AddMatrix
                (secondMatrix, firstMatrix);
            Assert.AreEqual(typeof(SquareMatrix<int>), resultMatrix.GetType());
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(firstMatrix[i, j] + secondMatrix[i, j], resultMatrix[i, j]);
        }

        [Test]
        public void MatrixSum_IntDiagonalDiagonal_SumMatrixExpected()
        {
            int[] diagonal = { 1, 2, 3, 4 };
            AbstractSquareMatrix<int> firstMatrix = new DiagonalMatrix<int>(diagonal);
            AbstractSquareMatrix<int> secondMatrix = new DiagonalMatrix<int>(diagonal);
            var resultMatrix = MatrixExtensions.AddMatrix
                (secondMatrix, firstMatrix);
            Assert.AreEqual(typeof(DiagonalMatrix<int>), resultMatrix.GetType());
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(firstMatrix[i, j] + secondMatrix[i, j], resultMatrix[i, j]);
        }

        [Test]
        public void MatrixSum_MarixExtensionsExceptionExpected()
        {
            //arrange
            DiagonalMatrix<Random> matrix = new DiagonalMatrix<Random>
                (new [] {new Random(), new Random()});
            //assert
            Assert.Throws(typeof(MatrixExtensionsException), () => matrix.AddMatrix(matrix));
        }
    }
}
