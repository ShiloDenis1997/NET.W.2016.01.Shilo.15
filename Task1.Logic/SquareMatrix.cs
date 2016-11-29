using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    /// <summary>
    /// Represents behavior of square matrix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SquareMatrix<T> : AbstractSquareMatrix<T>
    {
        /// <summary>
        /// Inner storage for matrixes elements
        /// </summary>
        private readonly T[] matrix;

        /// <summary>
        /// Initializes new instance of <see cref="SquareMatrix{T}"/> with
        /// specified <see cref="dimension"/>
        /// </summary>
        /// <param name="dimension">size of side of matrix</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if
        /// <paramref name="dimension"/> is less or equal to zero</exception>
        public SquareMatrix(int dimension)
        {
            Dimension = dimension;
            matrix = new T[dimension * dimension];
        }

        /// <summary>
        /// Initializes new instance of <see cref="SquareMatrix{T}"/> with
        /// specified <see cref="matrix"/> elements
        /// </summary>
        /// <param name="matrix">elements of the matrix</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if
        /// <paramref name="matrix"/> first dimension is less or equal to zero</exception>
        /// <exception cref="ArgumentException">Throws if <paramref name="matrix"/>
        /// is not square</exception>
        public SquareMatrix(T[,] matrix)
        {
            Dimension = matrix.GetLength(0);
            if (matrix.GetLength(1) != Dimension)
                throw new ArgumentException($"{nameof(matrix)} is not square matrix");
            this.matrix = new T[Dimension * Dimension];
            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                    this[i, j] = matrix[i, j];
        }

        /// <summary>
        /// Sets an element of square matrix
        /// </summary>
        protected override void SetElement(T element, int row, int column)
        {
            matrix[row * Dimension + column] = element;
        }

        /// <summary>
        /// Gets an elemen of square matrix
        /// </summary>
        protected override T GetElement(int row, int column)
        {
            return matrix[row * Dimension + column];
        }
    }
}
