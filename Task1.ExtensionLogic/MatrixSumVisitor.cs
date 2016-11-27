using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Logic;

namespace Task1.ExtensionLogic
{
    class MatrixSumVisitor<T> : IMatrixVisitor<T>
    {
        private SquareMatrix<T> resultMatrix;
        private readonly Func<T, T, T> sumRule;

        public AbstractSquareMatrix<T> Result => resultMatrix;

        public MatrixSumVisitor(Func<T, T, T> sumRule)
        {
            if(ReferenceEquals(sumRule, null))
                throw new ArgumentNullException($"{nameof(sumRule)} is null");
            this.sumRule = sumRule;
        }

        public void Visit(SquareMatrix<T> squareMatrix)
        {
            if (ReferenceEquals(resultMatrix, null))
            {
                resultMatrix = new SquareMatrix<T>(squareMatrix.Dimension);
                for (int i = 0; i < resultMatrix.Dimension; i++)
                    for (int j = 0; j < resultMatrix.Dimension; j++)
                        resultMatrix[i, j] = squareMatrix[i, j];
                return;
            }
            if (resultMatrix.Dimension != squareMatrix.Dimension)
                throw new ArgumentException
                    ("Cannot sums matrixes with different dimensions");
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    resultMatrix[i, j] = 
                        sumRule(resultMatrix[i, j], squareMatrix[i, j]);
        }

        public void Visit(DiagonalMatrix<T> diagonalMatrix)
        {
            if (ReferenceEquals(resultMatrix, null))
            {
                resultMatrix = new SquareMatrix<T>(diagonalMatrix.Dimension);
                for (int i = 0; i < resultMatrix.Dimension; i++)
                    for (int j = 0; j < resultMatrix.Dimension; j++)
                        resultMatrix[i, j] = diagonalMatrix[i, j];
                return;
            }
            if (resultMatrix.Dimension != diagonalMatrix.Dimension)
                throw new ArgumentException
                    ("Cannot sums matrixes with different dimensions");
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    resultMatrix[i, j] =
                        sumRule(resultMatrix[i, j], diagonalMatrix[i, j]);
        }

        public void Visit(SymmetricMatrix<T> symmetricMatrix)
        {
            if (ReferenceEquals(resultMatrix, null))
            {
                resultMatrix = new SquareMatrix<T>(symmetricMatrix.Dimension);
                for (int i = 0; i < resultMatrix.Dimension; i++)
                    for (int j = 0; j < resultMatrix.Dimension; j++)
                        resultMatrix[i, j] = symmetricMatrix[i, j];
                return;
            }
            if (resultMatrix.Dimension != symmetricMatrix.Dimension)
                throw new ArgumentException
                    ("Cannot sums matrixes with different dimensions");
            for (int i = 0; i < resultMatrix.Dimension; i++)
                for (int j = 0; j < resultMatrix.Dimension; j++)
                    resultMatrix[i, j] =
                        sumRule(resultMatrix[i, j], symmetricMatrix[i, j]);
        }
    }
}
