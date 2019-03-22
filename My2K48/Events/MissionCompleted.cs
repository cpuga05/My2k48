using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.Events
{
	class MissionCompleted : Event
	{
		private Guid guid;
		private Point point;
		private DateTime occurred;

		public MissionCompleted(Guid id, Point point)
		{
			this.guid = id;
			this.point = point;
			this.occurred = DateTime.Now;
		}

		public Guid id()
		{
			return this.guid;
		}

		public Point getScore()
		{
			return this.point;
		}

		public DateTime occurredOn()
		{
			return this.occurred;
		}
	}
}
