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
	class UserExpMissionCompletedEventSubscriber : EventSubscriber
	{
		private UserController userController;

		public UserExpMissionCompletedEventSubscriber(UserController userController)
		{
			this.userController = userController;
		}

		public void handle(Event e)
		{
			MissionCompleted ee = (MissionCompleted)e;

			this.userController.addExp(ee.getScore());
		}

		public bool isSubscribed(Event e)
		{
			return e.GetType().Name == "MissionCompleted";
		}
	}
}
