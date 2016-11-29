using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    /// <summary>
    /// Represents behavior of diagonal matrix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DiagonalMatrix<T> : AbstractSquareMatrix<T>
    {
        /// <summary>
        /// Main diagonal of matrix, we need only this because other elements are null
        /// </summary>
        private readonly T[] diagonal;

        /// <summary>
        /// Initializes new <see cref="DiagonalMatrix{T}"/> with specified 
        /// <paramref name="dimension"/>
        /// </summary>
        /// <param name="dimension">size of matrix side</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if 
        /// <paramref name="dimension"/> value
        /// is less or equal to zero</exception>
        public DiagonalMatrix(int dimension)
        {
            Dimension = dimension;
            diagonal = new T[Dimension];
        }

        /// <summary>
        /// Initializes new <see cref="DiagonalMatrix{T}"/> with specified 
        ///  <paramref name="diagonal"/>
        /// </summary>
        /// <param name="diagonal">diagonal of matrix</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if 
        /// <paramref name="diagonal"/> length
        /// is less or equal to zero</exception>
        /// <exception cref="ArgumentNullException">Throws if 
        /// <paramref name="diagonal"/> is null</exception>
        public DiagonalMatrix(params T[] diagonal)
        {
            if (ReferenceEquals(diagonal, null))
                throw new ArgumentNullException($"{nameof(diagonal)} is null");
            Dimension = diagonal.Length;
            this.diagonal = new T[Dimension];
            for (int i = 0; i < Dimension; i++)
                this.diagonal[i] = diagonal[i];
        }

        /// <summary>
        /// Sets an element of diagonal matrix
        /// </summary>
        /// <exception cref="InvalidMatrixIndexException">Throws if
        /// <paramref name="row"/> is not equal to <paramref name="column"/></exception>
        protected override void SetElement(T element, int row, int column)
        {
            if (row != column)
                throw new InvalidMatrixIndexException
                    ("Trying to set non-diagonal element");
            diagonal[row] = element;
        }

        /// <summary>
        /// Gets an element of diagonal matrix
        /// </summary>
        protected override T GetElement(int row, int column)
            => row == column ? diagonal[row] : default(T);
    }
}
