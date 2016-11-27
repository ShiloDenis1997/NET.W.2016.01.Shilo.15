using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    public interface IMatrixVisitor<T>
    {
        void Visit(SquareMatrix<T> squareMatrix);
        void Visit(DiagonalMatrix<T> diagonalMatrix);
        void Visit(SymmetricMatrix<T> symmetricMatrix);
    }
}
