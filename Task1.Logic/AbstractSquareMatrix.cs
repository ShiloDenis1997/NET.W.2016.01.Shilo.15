using System;

namespace Task1.Logic
{
    /// <summary>
    /// Represents abstract square matrix behavior
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractSquareMatrix<T>
    {
        private int dimension;
        /// <summary>
        /// Event happens when element of matrix is changing
        /// </summary>
        public event EventHandler<ElementChangedEventArgs> ElementChanged;

        /// <summary>
        /// Matrix side size
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if setted value
        /// is less or equal to zero</exception>
        public int Dimension {
            get { return dimension; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException
                        ($"{nameof(value)} is less or equal to zero");
                dimension = value;
            }
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
        /// Generates an event of changing elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnElementChanged(object sender, ElementChangedEventArgs e)
        {
            ElementChanged?.Invoke(sender, e);
        }
    }
}
