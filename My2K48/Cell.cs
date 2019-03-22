using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48
{
    class Cell
    {
        private int val;

        public Cell(int value)
        {
            this.ensureValueIsValid(value);

            this.val = value;
        }

        private void ensureValueIsValid(int value)
        {
            if (value % 2 != 0)
            {
                throw new ArgumentException();
            }
        }

        public int value()
        {
            return this.val;
        }
    }
}
