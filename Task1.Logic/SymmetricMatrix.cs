using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    public class SymmetricMatrix<T> : AbstractSquareMatrix<T>
    {
        private T[][] matrix;

        public SymmetricMatrix(int dimension)
        {
            Dimension = dimension;
            matrix = new T[Dimension][];
            for (int i = 0; i < Dimension; i++)
                matrix[i] = new T[i + 1];
        }

        public SymmetricMatrix(T[,] matrix)
        {
            Dimension = matrix.GetLength(0);
            this.matrix = new T[Dimension][];
            for (int i = 0; i < Dimension; i++)
            {
                this.matrix[i] = new T[i + 1];
                for (int j = 0; j <= i; j++)
                    this.matrix[i][j] = matrix[i, j];
            }
        }

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
                return matrix[Math.Max(row, column)][ Math.Min(row, column)];
            }
            set
            {
                if (row < 0 || row >= Dimension)
                    throw new ArgumentOutOfRangeException
                        ($"{nameof(row)} is not in range with current dimension");
                if (column < 0 || column >= Dimension)
                    throw new ArgumentOutOfRangeException
                        ($"{nameof(column)} is not in range with current dimension");
                matrix[Math.Max(row, column)][Math.Min(row, column)] = value;
            }
        }
    }
}
