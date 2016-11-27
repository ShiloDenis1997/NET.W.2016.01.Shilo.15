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
        public DiagonalMatrix(T[] diagonal)
        {
            if (ReferenceEquals(diagonal, null))
                throw new ArgumentNullException($"{nameof(diagonal)} is null");
            Dimension = diagonal.Length;
            this.diagonal = new T[Dimension];
            for (int i = 0; i < Dimension; i++)
                this.diagonal[i] = diagonal[i];
        }

        /// <summary>
        /// Indexator to set/get elements of matrix
        /// </summary>
        /// <param name="row">row index</param>
        /// <param name="column">column index</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="row"/>
        /// or <paramref name="column"/> is less than zero or greater,
        /// equal than dimension</exception>
        /// <exception cref="ArgumentException">Throws when trying to set 
        /// non-diagonal element</exception>
        public T this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= Dimension)
                    throw new ArgumentOutOfRangeException
                        ($"{nameof(row)} is not in range with current dimension");
                if (column < 0 || column >= Dimension)
                    throw new ArgumentOutOfRangeException
                        ($"{nameof(column)} is not in range with current dimension");
                return row != column ? default(T) : diagonal[row];
            }
            set
            {
                if (row < 0 || row >= Dimension)
                    throw new ArgumentOutOfRangeException
                        ($"{nameof(row)} is not in range with current dimension");
                if (column < 0 || column >= Dimension)
                    throw new ArgumentOutOfRangeException
                        ($"{nameof(column)} is not in range with current dimension");
                if (row != column)
                    throw new ArgumentException
                        ("Cannot set nondiagonal element for diagonal matrix");
                diagonal[row] = value;
                OnElementChanged(this, new ElementChangedEventArgs(row, column));
            }
        }
    }
}
