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
	class UserScoreChangedEventSubscriber : EventSubscriber
	{
		private UserController userController;

		public UserScoreChangedEventSubscriber(UserController userController)
		{
			this.userController = userController;
		}

		public void handle(Event e)
		{
			ScoreChanged ee = (ScoreChanged)e;

			this.userController.maxRecord(ee.point());
			this.userController.addExp(ee.point());
		}

		public bool isSubscribed(Event e)
		{
			return e.GetType().Name == "ScoreChanged";
		}
	}
}
