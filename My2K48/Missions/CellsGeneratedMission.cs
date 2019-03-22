using My2K48.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.Missions
{
	class CellsGeneratedMission : Mission
	{
		public CellsGeneratedMission(EventPublisher eventPublisher) : base(eventPublisher)
		{
			this.id = Guid.NewGuid();
			this.complete = false;
			this.title = "Cells Generated";
			this.actual = 0;
			this.goal = 20;
			this.score = new Point(750);
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
			return e.GetType().Name == "CellGenerated";
		}
	}
}
