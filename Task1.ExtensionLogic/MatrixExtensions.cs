using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Logic;

namespace Task1.ExtensionLogic
{
    public static class MatrixExtensions
    {
        public static AbstractSquareMatrix<T> MatrixSum<T>
            (AbstractSquareMatrix<T> firstMatrix, AbstractSquareMatrix<T> secondMatrix,
                Func<T, T, T> sumRule)
        {
            MatrixSumVisitor<T> visitor = new MatrixSumVisitor<T>(sumRule);
            firstMatrix.Accept(visitor);
            secondMatrix.Accept(visitor);
            return visitor.Result;
        }
    }
}
