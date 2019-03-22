using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.Events
{
	class ScoreAdded : Event
	{
		private DateTime occurred;
		private Point val;

		public ScoreAdded(Point point)
		{
			this.val = point;
			this.occurred = DateTime.Now;
		}

		public Point point()
		{
			return this.val;
		}

		public DateTime occurredOn()
		{
			return this.occurred;
		}
	}
}
