using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.Events
{
	class GameStarted : Event
	{
		private DateTime occurred;
		
		public GameStarted()
		{
			this.occurred = DateTime.Now;
		}

		public DateTime occurredOn()
		{
			return this.occurred;
		}
	}
}
