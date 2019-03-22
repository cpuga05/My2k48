using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.Events
{
	class GameFinished : Event
	{
		private DateTime occurred;
		
		public GameFinished()
		{
			this.occurred = DateTime.Now;
		}

		public DateTime occurredOn()
		{
			return this.occurred;
		}
	}
}
