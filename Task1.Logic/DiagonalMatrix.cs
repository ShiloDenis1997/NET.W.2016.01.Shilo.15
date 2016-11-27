using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    public class DiagonalMatrix<T> : AbstractSquareMatrix<T>
    {
        private T[] diagonal;

        public DiagonalMatrix(int dimension)
        {
            Dimension = dimension;
            diagonal = new T[Dimension];
        }

        public DiagonalMatrix(T[] diagonal)
        {
            Dimension = diagonal.Length;
            diagonal = new T[Dimension];
        }

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
