using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2K48
{
	interface EventSubscriber
	{
		void handle(Event e);

		bool isSubscribed(Event e);
	}
}
