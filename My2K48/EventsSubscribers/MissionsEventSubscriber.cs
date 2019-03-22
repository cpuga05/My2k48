using My2K48_InitVersion.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.EventsSubscribers
{
	class MissionsEventSubscriber : EventSubscriber
	{
		private MissionsController missionsController;

		public MissionsEventSubscriber(MissionsController missionsController)
		{
			this.missionsController = missionsController;
		}

		public void handle(Event e)
		{
			this.missionsController.paint();
		}

		public bool isSubscribed(Event e)
		{
			return e.GetType().Name == "MissionUpdated" || e.GetType().Name == "MissionCompleted";
		}
	}
}
