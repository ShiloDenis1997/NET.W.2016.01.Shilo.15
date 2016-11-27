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
            var resultMatrix = (SquareMatrix<int>)MatrixExtensions.MatrixSum(firstMatrix, secondMatrix,
                (a, b) => a + b);
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
    }
}
