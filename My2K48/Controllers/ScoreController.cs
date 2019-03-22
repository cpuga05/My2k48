using My2K48.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My2K48.Controllers
{
	class ScoreController
	{
		private List<Point> points;
		private Point point;
		private Control control;
		private EventPublisher eventPublisher;

		public ScoreController(Control control, EventPublisher eventPublisher)
		{
			this.points = new List<Point>();
			this.point = new Point();
			this.control = control;
			this.eventPublisher = eventPublisher;

			this.paint();
		}

		public void add(int value)
		{
			this.point = this.point.add(value);

			this.eventPublisher.publish(new ScoreAdded(this.point));

			this.paint();
		}

		public void newScore()
		{
			

			this.points.Add(this.point);

			this.eventPublisher.publish(new ScoreChanged(this.point));

			this.point = new Point();

			this.paint();
		}

		private void paint()
		{
			if (this.control.InvokeRequired)
			{
				this.control.Invoke((MethodInvoker)delegate ()
				{
					this.control.Text = Convert.ToString(this.point.value());
				});
			}
			else
			{
				this.control.Text = Convert.ToString(this.point.value());
			}
		}
	}
}
