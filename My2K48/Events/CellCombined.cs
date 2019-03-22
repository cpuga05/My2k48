using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.Events
{
	class CellCombined : Event
	{
		private DateTime occurred;
		private int val;

		public CellCombined(int value)
		{
			this.val = value;
			this.occurred = DateTime.Now;
		}

		public int value()
		{
			return this.val;
		}

		public DateTime occurredOn()
		{
			return this.occurred;
		}
	}
}
