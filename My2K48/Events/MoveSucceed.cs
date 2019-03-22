using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.Events
{
	class MoveSucceed : Event
	{
		private string direc;
		private DateTime occurred;

		public MoveSucceed(string direction)
		{
			this.direc = direction;
			this.occurred = DateTime.Now;
		}

		public string direction()
		{
			return this.direc;
		}

		public DateTime occurredOn()
		{
			return this.occurred;
		}
	}
}
