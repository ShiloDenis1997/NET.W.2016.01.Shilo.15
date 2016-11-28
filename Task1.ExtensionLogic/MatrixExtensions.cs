using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
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
        /// <returns></returns>
        public static AbstractSquareMatrix<T> AddMatrix<T>
            (this AbstractSquareMatrix<T> firstMatrix, AbstractSquareMatrix<T> secondMatrix)
        {
            if (firstMatrix.Dimension != secondMatrix.Dimension)
                throw new MatrixExtensionsException
                    ($"{nameof(firstMatrix.Dimension)}" +
                     $"is not equal to {nameof(secondMatrix.Dimension)}");
            return Sum((dynamic) firstMatrix,(dynamic) secondMatrix);
        }

        /// <summary>
        /// Square matrix + any other matrix = square matrix
        /// </summary>
        /// <exception cref="MatrixExtensionsException">Throws if <typeparamref name="T"/>
        /// does not support operator+</exception>
        private static AbstractSquareMatrix<T> Sum<T>(SquareMatrix<T> firstMatrix, AbstractSquareMatrix<T> secondMatrix)
        {
            SquareMatrix<T> result = new SquareMatrix<T>(firstMatrix.Dimension);
            try
            {
                for (int i = 0; i < firstMatrix.Dimension; i++)
                    for (int j = 0; j < secondMatrix.Dimension; j++)
                        result[i, j] = (dynamic) firstMatrix[i, j] + secondMatrix[i, j];
            }
            catch (RuntimeBinderException rbe)
            {
                throw new MatrixExtensionsException
                    ($"Elements of type {typeof(T)} does not support operator+");
            }
            return result;
        }

        /// <summary>
        /// Symmetric matrix + square matrix = square matrix
        /// </summary>
        /// <exception cref="MatrixExtensionsException">Throws if <typeparamref name="T"/>
        /// does not support operator+</exception>
        private static AbstractSquareMatrix<T> Sum<T>
            (SymmetricMatrix<T> firstMatrix, SquareMatrix<T> secondMatrix)
            => Sum(secondMatrix, firstMatrix);

        /// <summary>
        /// Symmetric matrix + symmetric or diagonal matrix = symmetric matrix
        /// </summary>
        /// <exception cref="MatrixExtensionsException">Throws if <typeparamref name="T"/>
        /// does not support operator+</exception>
        private static AbstractSquareMatrix<T> Sum<T>
            (SymmetricMatrix<T> firstMatrix, AbstractSquareMatrix<T> secondMatrix)
        {
            SymmetricMatrix<T> result = new SymmetricMatrix<T>(firstMatrix.Dimension);
            try
            {
                for (int i = 0; i < firstMatrix.Dimension; i++)
                    for (int j = 0; j <= i; j++)
                        result[i, j] = (dynamic)firstMatrix[i, j] + secondMatrix[i, j];
            }
            catch (RuntimeBinderException rbe)
            {
                throw new MatrixExtensionsException
                    ($"Elements of type {typeof(T)} does not support operator+");
            }
            return result;
        }

        /// <summary>
        /// Diagonal matrix + square matrix = square matrix
        /// </summary>
        /// <exception cref="MatrixExtensionsException">Throws if <typeparamref name="T"/>
        /// does not support operator+</exception>
        private static AbstractSquareMatrix<T> Sum<T>
            (DiagonalMatrix<T> firstMatrix, SquareMatrix<T> secondMatrix)
            => Sum(secondMatrix, firstMatrix);

        /// <summary>
        /// Diagonal matrix + symmetric matrix = square matrix
        /// </summary>
        /// <exception cref="MatrixExtensionsException">Throws if <typeparamref name="T"/>
        /// does not support operator+</exception>
        private static AbstractSquareMatrix<T> Sum<T>
            (DiagonalMatrix<T> firstMatrix, SymmetricMatrix<T> secondMatrix)
            => Sum(secondMatrix, firstMatrix);

        /// <summary>
        /// Diagonal matrix + diagonal matrix = diagonal matrix
        /// </summary>
        /// <exception cref="MatrixExtensionsException">Throws if <typeparamref name="T"/>
        /// does not support operator+</exception>
        private static AbstractSquareMatrix<T> Sum<T>
            (DiagonalMatrix<T> firstMatrix, DiagonalMatrix<T> secondMatrix)
        {
            DiagonalMatrix<T> result = new DiagonalMatrix<T>(firstMatrix.Dimension);
            try
            {
                for (int i = 0; i < firstMatrix.Dimension; i++)
                        result[i, i] = (dynamic)firstMatrix[i, i] + secondMatrix[i, i];
            }
            catch (RuntimeBinderException rbe)
            {
                throw new MatrixExtensionsException
                    ($"Elements of type {typeof(T)} does not support operator+");
            }
            return result;
        }
    }
}
