using My2K48.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.Missions
{
	class CellsCombinedMission : Mission
	{
		public CellsCombinedMission(EventPublisher eventPublisher) : base(eventPublisher)
		{
			this.id = Guid.NewGuid();
			this.complete = false;
			this.title = "Cells Combined";
			this.actual = 0;
			this.goal = 10;
			this.score = new Point(1000);
		}

		public override void handle(Event e)
		{
				if (this.complete)
				{
					return;
				}

				this.updateActual(++this.actual);
			Task.Factory.StartNew(() =>
			{
			});
		}

		public override bool isSubscribed(Event e)
		{
			return e.GetType().Name == "CellCombined";
		}
	}
}
