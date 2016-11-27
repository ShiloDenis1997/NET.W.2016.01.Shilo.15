using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    public class SquareMatrix<T> : AbstractSquareMatrix<T>
    {
        private readonly T[,] matrix;

        public SquareMatrix(int dimension)
        {
            Dimension = dimension;
            matrix = new T[dimension, dimension];
        }

        public SquareMatrix(T[,] matrix)
        {
            Dimension = matrix.GetLength(0);
            this.matrix = new T[Dimension, Dimension];
            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                    this.matrix[i, j] = matrix[i, j];
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
                return matrix[row, column];
            }
            set
            {
                if (row < 0 || row >= Dimension)
                    throw new ArgumentOutOfRangeException
                        ($"{nameof(row)} is not in range with current dimension");
                if (column < 0 || column >= Dimension)
                    throw new ArgumentOutOfRangeException
                        ($"{nameof(column)} is not in range with current dimension");
                matrix[row, column] = value;
                OnElementChanged(this, new ElementChangedEventArgs(row, column));
            }
        }
    }
}
