using My2K48.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My2K48.Controllers
{
	class UserController
	{
		private EventPublisher eventPublisher;
		private Point record;
		private Point exp;
		private int level;
		private Control controlRecord;
		private Control controlExp;
		private Control controlLevel;
		private int[] expPerLevel = new int[] { 0, 1000, 2500, 5000 };

		public UserController(Control controlRecord, Control controlExp, Control controlLevel, EventPublisher eventPublisher)
		{
			this.eventPublisher = eventPublisher;
			this.controlRecord = controlRecord;
			this.controlExp = controlExp;
			this.controlLevel = controlLevel;
			this.record = new Point();
			this.exp = new Point();
			this.level = 1;
		}

		public void maxRecord(Point record)
		{
			if (! this.record.isBiggetThan(record))
			{
				this.record = record;

				this.eventPublisher.publish(new RecordMaxed(exp));
				this.paint();
			}
		}

		public void addExp(Point exp)
		{
			this.exp = this.exp.add(exp);

			this.eventPublisher.publish(new ExpAdded(exp));
			this.canLevelUp();
			this.paint();
		}

		private void canLevelUp()
		{
			if (this.exp.value() >= this.expNeeded(level))
			{
				this.level++;

				this.eventPublisher.publish(new LevelUpped(this.level));
			}
		}

		private void paint()
		{
			this.controlRecord.Text = Convert.ToString(this.record.value());
			this.controlExp.Text = Convert.ToString(this.exp.value());
			this.controlLevel.Text = Convert.ToString(this.level);
		}

		private int expNeeded(int level)
		{
			if (this.expPerLevel.Length > level)
			{
				return this.expPerLevel[level];
			}

			int need = this.expPerLevel[this.expPerLevel.Length - 1];

			for (int i = this.expPerLevel.Length - 1; i < level; i++)
			{
				need *= 2;
			}

			return need;
		}
	}
}
