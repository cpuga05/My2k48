using My2K48.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48.EventsSubscribers
{
	class LoggerEventSubscriber : EventSubscriber
	{
		public void handle(Event e)
		{
			//CellCombined ee = (CellCombined)e;
			System.Diagnostics.Debug.WriteLine("Event: " + e.GetType() + " (" + e.occurredOn() + ")");
			Task.Factory.StartNew(() =>
			{
				//System.Windows.Forms.MessageBox.Show("Evento combinado: " + ee.value());
			});
		}

		public bool isSubscribed(Event e)
		{
			//return e.GetType().Name == "CellCombined";
			return true;
		}
	}
}
