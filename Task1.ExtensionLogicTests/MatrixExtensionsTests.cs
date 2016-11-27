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
            var resultMatrix = (SquareMatrix<int>)MatrixExtensions.MatrixSum
                (firstMatrix, secondMatrix, (a, b) => a + b);
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(matrix[i, j]*2, resultMatrix[i, j]);
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
        public void MatrixSum_IntWrapperSquareSquare_SumMatrixExpected()
        {
            IntWrapper[,] matrix =
            {
                {new IntWrapper(1), new IntWrapper(2), new IntWrapper(3), new IntWrapper(4)},
                {new IntWrapper(1), new IntWrapper(2), new IntWrapper(3), new IntWrapper(4)},
                {new IntWrapper(5), new IntWrapper(2), new IntWrapper(3), new IntWrapper(6)},
                {new IntWrapper(3), new IntWrapper(5), new IntWrapper(2), new IntWrapper(1)},
            };
            AbstractSquareMatrix<IntWrapper> firstMatrix = 
                new SquareMatrix<IntWrapper>(matrix);
            AbstractSquareMatrix<IntWrapper> secondMatrix = 
                new SquareMatrix<IntWrapper>(matrix);
            var resultMatrix = (SquareMatrix<IntWrapper>) MatrixExtensions
                .MatrixSum(firstMatrix, secondMatrix, (a, b) => new IntWrapper(a.X + b.X));
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(matrix[i, j].X * 2, resultMatrix[i, j].X);
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
            var resultMatrix = (SquareMatrix<int>)MatrixExtensions.MatrixSum
                (firstMatrix, secondMatrix, (a, b) => a + b);
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(matrix[i, j] + matrix[Math.Max(i, j), Math.Min(i, j)],
                        resultMatrix[i, j]);
        }

        [Test]
        public void MatrixSum_IntWrapperSquareSymmetric_SumMatrixExpected()
        {
            IntWrapper[,] matrix =
            {
                {new IntWrapper(1), new IntWrapper(2), new IntWrapper(3), new IntWrapper(4)},
                {new IntWrapper(1), new IntWrapper(2), new IntWrapper(3), new IntWrapper(4)},
                {new IntWrapper(5), new IntWrapper(2), new IntWrapper(3), new IntWrapper(6)},
                {new IntWrapper(3), new IntWrapper(5), new IntWrapper(2), new IntWrapper(1)},
            };
            AbstractSquareMatrix<IntWrapper> firstMatrix =
                new SquareMatrix<IntWrapper>(matrix);
            AbstractSquareMatrix<IntWrapper> secondMatrix =
                new SymmetricMatrix<IntWrapper>(matrix);
            var resultMatrix = (SquareMatrix<IntWrapper>)MatrixExtensions
                .MatrixSum(firstMatrix, secondMatrix, (a, b) => new IntWrapper(a.X + b.X));
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    Assert.AreEqual(matrix[i, j].X + matrix[Math.Max(i, j), Math.Min(i, j)].X,
                        resultMatrix[i, j].X);
        }

        [Test]
        public void MatrixSum_IntWrapperSymmetricDiagonal_SumMatrixExpected()
        {
            IntWrapper[,] matrix =
            {
                {new IntWrapper(1), new IntWrapper(2), new IntWrapper(3), new IntWrapper(4)},
                {new IntWrapper(1), new IntWrapper(2), new IntWrapper(3), new IntWrapper(4)},
                {new IntWrapper(5), new IntWrapper(2), new IntWrapper(3), new IntWrapper(6)},
                {new IntWrapper(3), new IntWrapper(5), new IntWrapper(2), new IntWrapper(1)},
            };
            IntWrapper[] diagonal = 
                {new IntWrapper(1), new IntWrapper(2), new IntWrapper(3), new IntWrapper(4)};
            AbstractSquareMatrix<IntWrapper> firstMatrix =
                new SymmetricMatrix<IntWrapper>(matrix);
            AbstractSquareMatrix<IntWrapper> secondMatrix =
                new DiagonalMatrix<IntWrapper>(diagonal);
            var resultMatrix = (SquareMatrix<IntWrapper>)MatrixExtensions
                .MatrixSum(firstMatrix, secondMatrix, (a, b) => 
                new IntWrapper((a?.X ?? 0) + (b?.X ?? 0)));
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    if (i == j)
                        Assert.AreEqual(matrix[i, j].X + diagonal[i].X,
                            resultMatrix[i, j].X);
                    else
                    {
                        Assert.AreEqual(matrix[Math.Max(i, j), Math.Min(i, j)].X, 
                            resultMatrix[i, j].X);
                    }
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
            var resultMatrix = (SquareMatrix<int>)MatrixExtensions.MatrixSum
                (firstMatrix, secondMatrix, (a, b) => a + b);
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    if (i == j)
                        Assert.AreEqual(matrix[i, j] + diagonal[i], resultMatrix[i, j]);
                    else
                        Assert.AreEqual(matrix[Math.Max(i, j), Math.Min(i, j)],
                            resultMatrix[i, j]);
        }
    }
}
