using System;
using Task1.Logic;

namespace Task1.ExtensionLogic
{
    /// <summary>
    /// Implements <see cref="IMatrixVisitor{T}"/> to provide
    /// a sum operation for matrixes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class MatrixSumVisitor<T> : IMatrixVisitor<T>
    {
        /// <summary>
        /// result of operation
        /// </summary>
        private SquareMatrix<T> resultMatrix;
        /// <summary>
        /// specifies how to sum elements of matrixes
        /// </summary>
        private readonly Func<T, T, T> sumRule;

        /// <summary>
        /// returns a result of operation
        /// </summary>
        public AbstractSquareMatrix<T> Result => resultMatrix;

        /// <summary>
        /// Initializes new instance of <see cref="MatrixSumVisitor{T}"/>
        /// with specified <paramref name="sumRule"/>
        /// </summary>
        /// <param name="sumRule"></param>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="sumRule"/>
        /// is null</exception>
        public MatrixSumVisitor(Func<T, T, T> sumRule)
        {
            if(ReferenceEquals(sumRule, null))
                throw new ArgumentNullException($"{nameof(sumRule)} is null");
            this.sumRule = sumRule;
        }

        /// <summary>
        /// Adds squareMatrix to result
        /// </summary>
        /// <param name="squareMatrix"></param>
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

        /// <summary>
        /// Adds diagonal matrix to result
        /// </summary>
        /// <param name="diagonalMatrix"></param>
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

        /// <summary>
        /// Addes symmetric matrix to result
        /// </summary>
        /// <param name="symmetricMatrix"></param>
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
