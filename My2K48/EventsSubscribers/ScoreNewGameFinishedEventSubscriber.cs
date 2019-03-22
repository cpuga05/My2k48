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
	class ScoreNewGameFinishedEventSubscriber : EventSubscriber
	{
		private ScoreController scoreController;

		public ScoreNewGameFinishedEventSubscriber(ScoreController scoreController)
		{
			this.scoreController = scoreController;
		}

		public void handle(Event e)
		{
			this.scoreController.newScore();
		}

		public bool isSubscribed(Event e)
		{
			return e.GetType().Name == "GameStarted";
		}
	}
}
