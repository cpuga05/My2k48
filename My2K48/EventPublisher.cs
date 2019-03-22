using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48
{
	class EventPublisher
	{
		private List<EventSubscriber> subscribers;
		private static EventPublisher instance;

		public static EventPublisher getInstance()
		{
			if (instance == null)
			{
				instance = new EventPublisher();
			}

			return instance;
		}

		public EventPublisher()
		{
			this.subscribers = new List<EventSubscriber>();
		}

		public void subscribe(EventSubscriber eventSubscriber)
		{
			this.subscribers.Add(eventSubscriber);
		}

		public void unSubscribe(EventSubscriber eventSubscriber)
		{
			this.subscribers.Remove(eventSubscriber);
		}

		public void publish(Event e)
		{
			this.subscribers.ForEach(delegate (EventSubscriber eventSubscriber)
			{
				if (eventSubscriber.isSubscribed(e))
				{
					eventSubscriber.handle(e);
				}
			});
		}
	}
}
