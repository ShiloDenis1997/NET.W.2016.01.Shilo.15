using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    /// <summary>
    /// Visitors inteface to extend functionallity of matrixes 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMatrixVisitor<T>
    {
        void Visit(SquareMatrix<T> squareMatrix);
        void Visit(DiagonalMatrix<T> diagonalMatrix);
        void Visit(SymmetricMatrix<T> symmetricMatrix);
    }
}
