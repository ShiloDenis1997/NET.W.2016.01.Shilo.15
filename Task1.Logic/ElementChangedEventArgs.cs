using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    /// <summary>
    /// Provides arguments to ElementChanged event
    /// </summary>
    public class ElementChangedEventArgs : EventArgs
    {
        public int Row { get; }
        public int Column { get; }

        public ElementChangedEventArgs(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
