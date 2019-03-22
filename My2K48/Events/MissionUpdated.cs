using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.Events
{
	class MissionUpdated : Event
	{
		private Guid guid;
		private DateTime occurred;

		public MissionUpdated(Guid id)
		{
			this.guid = id;
			this.occurred = DateTime.Now;
		}

		public Guid id()
		{
			return this.guid;
		}

		public DateTime occurredOn()
		{
			return this.occurred;
		}
	}
}
