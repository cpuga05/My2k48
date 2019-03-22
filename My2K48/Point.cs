using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48
{
	class Point
	{
		private int val;

		public Point(int value = 0)
		{
			this.val = value;
		}

		public int value()
		{
			return this.val;
		}

		public Point add(int val)
		{
			return new Point(this.val + val);
		}

		public Point add(Point val)
		{
			return new Point(this.val + val.value());
		}

		public bool isBiggetThan(Point point)
		{
			if (this.value() >= point.value())
			{
				return true;
			}

			return false;
		}
	}
}
