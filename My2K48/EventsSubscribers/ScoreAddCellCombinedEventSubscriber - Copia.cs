using My2K48.Controllers;
using My2K48.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My2K48.EventsSubscribers
{
	class ScoreAddCellCombinedEventSubscriber : EventSubscriber
	{
		private ScoreController scoreController;

		public ScoreAddCellCombinedEventSubscriber(ScoreController scoreController)
		{
			this.scoreController = scoreController;
		}

		public void handle(Event e)
		{
			CellCombined ee = (CellCombined)e;

			this.scoreController.add(ee.value());
		}

		public bool isSubscribed(Event e)
		{
			return e.GetType().Name == "CellCombined";
		}
	}
}
