using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    /// <summary>
    /// Represents behavior of symmetric matrix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SymmetricMatrix<T> : AbstractSquareMatrix<T>
    {
        private readonly T[][] matrix;

        /// <summary>
        /// Initializes neew instance of <see cref="SymmetricMatrix{T}"/>
        /// with specified <paramref name="dimension"/>
        /// </summary>
        /// <param name="dimension">size of side of matrix</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if 
        /// <paramref name="dimension"/> is less or equal to zero</exception>
        public SymmetricMatrix(int dimension)
        {
            Dimension = dimension;
            matrix = new T[Dimension][];
            for (int i = 0; i < Dimension; i++)
                matrix[i] = new T[i + 1];
        }

        /// <summary>
        /// Initializes new instance of <see cref="SquareMatrix{T}"/> with
        /// specified <see cref="matrix"/> elements. Uses only left triangular
        /// </summary>
        /// <param name="matrix">elements of the matrix</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if
        /// <paramref name="matrix"/> first dimension is less or equal to zero</exception>
        /// <exception cref="ArgumentException">Throws if <paramref name="matrix"/>
        /// is not square</exception>
        public SymmetricMatrix(T[,] matrix)
        {
            Dimension = matrix.GetLength(0);
            this.matrix = new T[Dimension][];
            if (matrix.GetLength(1) != Dimension)
                throw new ArgumentException($"{nameof(matrix)} is not square matrix");
            for (int i = 0; i < Dimension; i++)
            {
                this.matrix[i] = new T[i + 1];
                for (int j = 0; j <= i; j++)
                    this.matrix[i][j] = matrix[i, j];
            }
        }

        /// <summary>
        /// Initializes new instance of <see cref="SquareMatrix{T}"/> with
        /// specified <see cref="matrix"/> elements. Uses only left triangular
        /// </summary>
        /// <param name="matrix">elements of the matrix</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if
        /// <paramref name="matrix"/> first dimension is less or equal to zero</exception>
        /// <exception cref="ArgumentException">Throws if <paramref name="matrix"/> is not
        /// triangular
        /// </exception>
        public SymmetricMatrix(T[][] matrix)
        {
            Dimension = matrix.GetLength(0);
            this.matrix = new T[Dimension][];
            for (int i = 0; i < Dimension; i++)
            {
                if (matrix[i].Length != i + 1)
                    throw new ArgumentException($"{nameof(matrix)} is not triangular");
                this.matrix[i] = new T[i + 1];
                for (int j = 0; j <= i; j++)
                    this.matrix[i][j] = matrix[i][j];
            }
        }
        
        /// <summary>
        /// Sets an element of symmetric matrix. Symmetric element is
        /// also setted to <paramref name="element"/> value
        /// </summary>
        protected override void SetElement(T element, int row, int column)
        {
            matrix[Math.Max(row, column)][Math.Min(row, column)] = element;
        }

        /// <summary>
        /// Gets an element of symmetric matrix
        /// </summary>
        protected override T GetElement(int row, int column) 
            => matrix[Math.Max(row, column)][Math.Min(row, column)];
    }
}
