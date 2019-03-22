using My2K48;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My2K48_InitVersion.Controllers
{
	class MissionsController
	{
		private FlowLayoutPanel control;
		private EventPublisher eventPublisher;
		private List<Mission> missions;

		public MissionsController(FlowLayoutPanel control, EventPublisher eventPublisher)
		{
			this.control = control;
			this.eventPublisher = eventPublisher;
			this.missions = new List<Mission>();
		}

		public void add(Mission mission)
		{
			this.missions.Add(mission);
			this.eventPublisher.subscribe(mission);
			this.paint();
		}

		public void paint()
		{
			Random random = new Random();

			if (this.control.InvokeRequired)
			{
				this.control.Invoke((MethodInvoker)delegate()
				{
					this.control.Controls.Clear();
				});
			} else
			{
				this.control.Controls.Clear();
			}

			this.missions.ForEach(delegate(Mission mission)
			{
				FlowLayoutPanel panel = new FlowLayoutPanel();
				panel.FlowDirection = FlowDirection.TopDown;
				panel.Name = mission.getId().ToString();
				if (mission.isComplete())
				{
					panel.BackColor = Color.FromArgb(100, 178, 166);
				} else
				{
					panel.BackColor = Color.FromArgb(94, 112, 122);
				}
				// panel.BackColor = Color.FromArgb(123, random.Next(222), random.Next(222));
				panel.Size = new Size(this.control.ClientSize.Width - 6, 50);

				Label title = new Label();
				title.Text = mission.getTitle();
				title.Font = new Font("Microsoft Tai Le", 14.0f, FontStyle.Bold);
				title.AutoSize = true;
				panel.Controls.Add(title);

				Label progress = new Label();
				progress.Text = mission.getActual() + " / " + mission.getGoal();
				progress.Font = new Font("Microsoft Tai Le", 12.0f, FontStyle.Regular);
				progress.AutoSize = true;
				panel.Controls.Add(progress);

				Label exp = new Label();
				exp.Text = mission.getScore().value() + " exp.";
				exp.Font = new Font("Microsoft Tai Le", 10.0f, FontStyle.Regular);
				exp.AutoSize = true;
				panel.Controls.Add(exp);

				if (this.control.InvokeRequired)
				{
					this.control.Invoke((MethodInvoker)delegate ()
					{
						this.control.Controls.Add(panel);
					});
				}
				else
				{
					this.control.Controls.Add(panel);
				}
			});
		}
	}
}
