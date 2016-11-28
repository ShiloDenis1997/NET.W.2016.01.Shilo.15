using System;
using System.Collections;
using System.Collections.Generic;

namespace Task1.Logic
{
    /// <summary>
    /// Represents abstract square matrix behavior
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractSquareMatrix<T> : IEnumerable<T>
    {
        private int dimension;
        /// <summary>
        /// Event happens when element of matrix is changing
        /// </summary>
        public event EventHandler<ElementChangedEventArgs<T>> ElementChanged;

        /// <summary>
        /// Matrix side size
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if setted value
        /// is less or equal to zero</exception>
        public int Dimension {
            get { return dimension; }
            protected set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException
                        ($"{nameof(value)} is less or equal to zero");
                dimension = value;
            }
        }

        /// <summary>
        /// Indexator to set/get elements of matrix
        /// </summary>
        /// <param name="row">row index</param>
        /// <param name="column">column index</param>
        /// <returns></returns>
        /// <exception cref="InvalidMatrixIndexException">Throws if <paramref name="row"/>
        /// or <paramref name="column"/> or both together are not valid
        /// to current matrix (less or equal to zero or greater or equal to dimension, etc.)</exception>
        public T this[int row, int column]
        {
            get
            {
                ValidateIndexes(row, column);
                return GetElement(row, column);
            }
            set
            {
                ValidateIndexes(row, column);
                T oldValue = GetElement(row, column);
                SetElement(value, row, column);
                OnElementChanged(this, new ElementChangedEventArgs<T>
                    (row, column, oldValue, GetElement(row, column)));
            }
        }

        /// <summary>
        /// Creates enumerator to enumerate all elements of matrix
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                    yield return this[i, j];
        }

        /// <summary>
        /// Accepts visitor to extends functionallity
        /// </summary>
        /// <param name="visitor"></param>
        /// <exception cref="ArgumentNullException">Throws 
        /// if <paramref name="visitor"/> is null</exception>
        public void Accept(IMatrixVisitor<T> visitor)
        {
            if (ReferenceEquals(visitor, null))
                throw new ArgumentNullException($"{nameof(visitor)} is null");
            visitor.Visit((dynamic)this);
        }

        /// <summary>
        /// Abstract method to set element of matrix. Implement it with
        /// specific matrix logic
        /// </summary>
        /// <param name="element"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        protected abstract void SetElement(T element, int row, int column);

        /// <summary>
        /// Abstract method to get element of matrix. Implement it with
        /// specific matrix logic
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        protected abstract T GetElement(int row, int column);

        /// <summary>
        /// Validates two indexes to check if they are in allowable range
        /// </summary>
        /// <param name="row">row index</param>
        /// <param name="column">column index</param>
        /// <exception cref="InvalidMatrixIndexException">Throws if <paramref name="row"/>
        /// or <paramref name="column"/> is less than zero or greater,
        /// equal than dimension</exception>
        protected virtual void ValidateIndexes(int row, int column)
        {
            if (row < 0 || row >= Dimension)
                throw new InvalidMatrixIndexException
                    ($"{nameof(row)} is not in range with current dimension");
            if (column < 0 || column >= Dimension)
                throw new InvalidMatrixIndexException
                    ($"{nameof(column)} is not in range with current dimension");
        }

        /// <summary>
        /// Generates an event of changing elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnElementChanged(object sender, ElementChangedEventArgs<T> e)
        {
            ElementChanged?.Invoke(sender, e);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Provides arguments to ElementChanged event
    /// </summary>
    public class ElementChangedEventArgs<T> : EventArgs
    {
        public int Row { get; }
        public int Column { get; }
        public T OldValue { get; }
        public T NewValue { get; }

        public ElementChangedEventArgs(int row, int column, T oldValue, T newValue)
        {
            Row = row;
            Column = column;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}

