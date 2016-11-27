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
    }
}
