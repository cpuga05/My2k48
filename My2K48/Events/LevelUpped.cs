using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.Events
{
	class LevelUpped : Event
	{
		private DateTime occurred;
		private int val;

		public LevelUpped(int level)
		{
			this.val = level;
			this.occurred = DateTime.Now;
		}

		public int level()
		{
			return this.val;
		}

		public DateTime occurredOn()
		{
			return this.occurred;
		}
	}
}
