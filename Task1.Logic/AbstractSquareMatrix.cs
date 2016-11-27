using System;

namespace Task1.Logic
{
    public abstract class AbstractSquareMatrix<T>
    {
        private int dimension;
        public event EventHandler<ElementChangedEventArgs> elementChangedEvent; 

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

    }
}
