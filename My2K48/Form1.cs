using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using My2K48.Exceptions;
using My2K48.EventsSubscribers;
using My2K48.Controllers;
using My2K48.Missions;
using My2K48_InitVersion.Controllers;

namespace My2K48
{
    public partial class Form1 : Form
    {
        private Board board;
		private Control[,] controls;
		private EventPublisher eventPublisher;
		private ScoreController scoreController;
		private MissionsController missionController;
		private UserController userController;

		public Form1()
        {
            InitializeComponent();

            this.controls = new Control[,] {
                {button1, button2, button3, button4},
                {button5, button6, button7, button8},
                {button9, button10, button11, button12},
                {button13, button14, button15, button16}
            };

			this.eventPublisher = EventPublisher.getInstance();

			this.scoreController = new ScoreController(scoreTextBox, this.eventPublisher);
			this.missionController = new MissionsController(panelMissions, this.eventPublisher);
			this.userController = new UserController(lblRecord, lblExp, lblLevel, this.eventPublisher);

			this.missionController.add(new CellsCombinedMission(this.eventPublisher));
			this.missionController.add(new MovesSucceedMission(this.eventPublisher));
			this.missionController.add(new CellsGeneratedMission(this.eventPublisher));
			this.missionController.add(new GamesFinishedMission(this.eventPublisher));
			this.missionController.add(new RecordsMaxedMission(this.eventPublisher));

			this.eventPublisher.subscribe(new LoggerEventSubscriber());
			this.eventPublisher.subscribe(new ScoreAddCellCombinedEventSubscriber(this.scoreController));
			this.eventPublisher.subscribe(new UserExpMissionCompletedEventSubscriber(this.userController));
			this.eventPublisher.subscribe(new ScoreNewGameFinishedEventSubscriber(this.scoreController));
			this.eventPublisher.subscribe(new UserScoreChangedEventSubscriber(this.userController));
			this.eventPublisher.subscribe(new MissionsEventSubscriber(this.missionController));

			this.board = new Board(this.controls, this.eventPublisher);
		}

        private void PlayButton_Click(object sender, EventArgs e)
        {
            this.setState("");

            try
            {
                this.board.start();   
            } catch (Exception error)
            {
				if (error is GameFinishedException || error is NotSpaceException)
				{
					this.board = new Board(this.controls, this.eventPublisher);
					this.board.start();
				} else
				{
					this.setState(error.GetType().Name);
				}
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            this.setState("");

            try
            {
                switch (e.KeyValue)
                {
                    case 37: // LEFT
                        this.board.move("left");
                        break;
                    case 38: // UP
                        this.board.move("up");
                        break;
                    case 39: // RIGHT
                        this.board.move("right");
                        break;
                    case 40: // DOWN
                        this.board.move("down");
                        break;
                }
            } catch (Exception error)
            {
                this.setState(error.GetType().Name);
            }
        }

        private void setState(string text)
        {
            stateLabel.Text = text;
        }
	}
}