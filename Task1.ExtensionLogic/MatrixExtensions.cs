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
        /// <summary>
        /// Get sum of two matrixes according to <paramref name="sumRule"/>
        /// </summary>
        /// <typeparam name="T">matrix element type</typeparam>
        /// <param name="firstMatrix">first matrix to sum</param>
        /// <param name="secondMatrix">second matrix to sum</param>
        /// <param name="sumRule">specified how to sum elements of matrix</param>
        /// <returns></returns>
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
