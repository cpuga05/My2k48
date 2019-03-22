using My2K48.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48
{
	abstract class Mission : EventSubscriber
	{
		private EventPublisher eventPublisher;

		protected Guid id;
		protected bool complete;
		protected string title;
		protected int actual;
		protected int goal;
		protected Point score;

		public Mission(EventPublisher eventPublisher)
		{
			this.eventPublisher = eventPublisher;
		}

		public Guid getId()
		{
			return this.id;
		}

		public bool isComplete()
		{
			return this.complete;
		}

		public string getTitle()
		{
			return this.title;
		}

		public int getActual()
		{
			return this.actual;
		}

		public int getGoal()
		{
			return this.goal;
		}

		public Point getScore()
		{
			return this.score;
		}

		abstract public void handle(Event e);

		abstract public bool isSubscribed(Event e);

		protected void updateActual(int actual)
		{
			this.actual = actual;

			this.eventPublisher.publish(new MissionUpdated(this.id));

			this.canComplete();
		}

		private void canComplete()
		{
			if (! this.complete && this.actual == this.goal)
			{
				this.complete = true;

				this.eventPublisher.publish(new MissionCompleted(this.id, this.score));
			} 
		}
	}
}
