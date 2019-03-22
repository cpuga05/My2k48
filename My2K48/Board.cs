using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using My2K48.Exceptions;
using System.Windows.Forms;
using System.Drawing;
using My2K48.Events;

namespace My2K48
{
    class Board
    {
        const int MAX_TRYS = 150;
		private EventPublisher eventPublisher;
		private bool started = false;
        private int[,] board;
        private Control[,] boardControls;
        private Random random;
        private Color[] colors = {
            Color.FromArgb(206, 207, 215),
            Color.FromArgb(94, 112, 122),
            Color.FromArgb(100, 178, 166),
            Color.FromArgb(149, 98, 37),
            Color.FromArgb(170, 60, 142),
            Color.FromArgb(248, 210, 72),
            Color.FromArgb(75, 169, 210),
            Color.FromArgb(58, 132, 87),
            Color.FromArgb(238, 117, 47),
            Color.FromArgb(201, 212, 68),
            Color.FromArgb(25, 69, 120),
            Color.FromArgb(223, 81, 112),
        };
        private int[] directions = new int[4];

        /*public Board(int size)
        {
            this.board = new string[size, size];
        }*/

        public Board(Control[,] board, EventPublisher eventPublisher)
        {
			this.eventPublisher = eventPublisher;
            this.started = false;
            this.boardControls = board;
            this.board = new int[board.GetUpperBound(0) + 1, board.GetUpperBound(1) + 1];
            this.random = new Random();
            this.resetDirections();

            for (int y = 0; y <= this.board.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= this.board.GetUpperBound(1); x++)
                {
                    this.board[y, x] = 0;
                }
            }

			this.print();
        }

        public void start()
        {
            if (this.started)
            {
                throw new AlreadyStartException();
            }

            this.started = true;

			this.eventPublisher.publish(new GameStarted());

            this.generateRandomCell();
            this.generateRandomCell();

            this.print();
        }

        public void move(string direction)
        {
            if (!this.started)
            {
                throw new NotStartedException();
            }

            int moves = 0;

            switch (direction)
            {
                case "up":
                    this.directions[0] = 1;
                    moves = this.up();
                    break;
                case "right":
                    this.directions[1] = 1;
                    moves = this.right();
                    break;
                case "down":
                    this.directions[2] = 1;
                    moves = this.down();
                    break;
                case "left":
                    this.directions[3] = 1;
                    moves = this.left();
                    break;
				default:
					return;
            }

			this.eventPublisher.publish(new MoveTried(direction));

            if (moves != 0)
            {
				this.eventPublisher.publish(new MoveSucceed(direction));
				this.resetDirections();
                this.generateRandomCell();
                this.print();
            }

            if (this.pendingAttempts() == 0)
            {
                this.started = false;
				this.eventPublisher.publish(new GameFinished());
				throw new GameFinishedException();
            }
        }

        private int left()
        {
            int[] cells = new int[this.board.GetUpperBound(1) + 1];
            int i, moves = 0;

            for (int y = 0; y <= this.board.GetUpperBound(0); y++)
            {
                i = 0;

                for (int x = 0; x <= this.board.GetUpperBound(1); x++)
                {
                    cells[i] = this.board[y, x];

                    i++;
                }

                moves += this.moveAndFuse(cells);

                i = 0;

                for (int x = 0; x <= this.board.GetUpperBound(1); x++)
                {
                    this.board[y, x] = cells[i];
                    i++;
                }
            }

            return moves;
        }

        private int right()
        {
            int[] cells = new int[this.board.GetUpperBound(1) + 1];
            int i, moves = 0;

            for (int y = 0; y <= this.board.GetUpperBound(0); y++)
            {
                i = 0;

                for (int x = this.board.GetUpperBound(1); x >= 0; x--)
                {
                    cells[i] = this.board[y, x];

                    i++;
                }

                moves += this.moveAndFuse(cells);

                i = 0;

                for (int x = this.board.GetUpperBound(1); x >= 0; x--)
                {
                    this.board[y, x] = cells[i];
                    i++;
                }
            }

            return moves;
        }

        private int up()
        {
            int[] cells = new int[this.board.GetUpperBound(0) + 1];
            int i, moves = 0;

            for (int x = 0; x <= this.board.GetUpperBound(1); x++)
            {
                i = 0;

                for (int y = 0; y <= this.board.GetUpperBound(0); y++)
                {
                    cells[i] = this.board[y, x];

                    i++;
                }

                moves += this.moveAndFuse(cells);

                i = 0;

                for (int y = 0; y <= this.board.GetUpperBound(0); y++)
                {
                    this.board[y, x] = cells[i];
                    i++;
                }
            }

            return moves;
        }

        private int down()
        {
            int[] cells = new int[this.board.GetUpperBound(0) + 1];
            int i, moves = 0;

            for (int x = 0; x <= this.board.GetUpperBound(1); x++)
            {
                i = 0;

                for (int y = this.board.GetUpperBound(0); y >= 0; y--)
                {
                    cells[i] = this.board[y, x];

                    i++;
                }

                moves += this.moveAndFuse(cells);

                i = 0;

                for (int y = this.board.GetUpperBound(0); y >= 0; y--)
                {
                    this.board[y, x] = cells[i];
                    i++;
                }
            }

            return moves;
        }

        private int moveAndFuse(int[] cells)
        {
            int moves = 0;

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] == 0)
                {
                    continue;
                }

                for (int ii = i + 1; ii < cells.Length; ii++)
                {
                    if (cells[i] != cells[ii] && cells[ii] != 0)
                    {
                        ii = cells.Length;
                        continue;
                    }

                    if (cells[i] == cells[ii])
                    {
                        cells[i] = cells[i] * 2;
                        cells[ii] = 0;
						this.eventPublisher.publish(new CellCombined(cells[i]));
                        i = ii;
                        ii = cells.Length;

                        moves++;
                    }
                }
            }

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] != 0)
                {
                    continue;
                }

                if (i + 1 > cells.Length)
                {
                    continue;
                }

                for (int ii = i + 1; ii < cells.Length; ii++)
                {
                    if (cells[ii] == 0)
                    {
                        continue;
                    }

                    cells[i] = cells[ii];
                    cells[ii] = 0;
                    ii = cells.Length;

                    moves++;
                }
            }

            return moves;
        }

        private void generateRandomCell()
        {
            int x, y, trys = 0;

            do
            {
                x = this.random.Next(0, this.board.GetUpperBound(1) + 1);
                y = this.random.Next(0, this.board.GetUpperBound(0) + 1);

                trys++;

                if (trys > MAX_TRYS)
                {
                    throw new NotSpaceException();
                }
            } while (this.board[y, x] != 0);

            //Debug.WriteLine("Trys:" + trys);

            this.board[y, x] = this.newValue();

			this.eventPublisher.publish(new CellGenerated(this.board[y, x]));
        }

        private void print()
        {
            for (int y = 0; y <= this.board.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= this.board.GetUpperBound(1); x++)
                {
					//Debug.Write(board[y, x]);
					int indexColor = 0;

					if (board[y, x] == 0)
                    {
                        boardControls[y, x].Text = "";
					}
                    else
                    {
                        boardControls[y, x].Text = Convert.ToString(board[y, x]);
						indexColor = Convert.ToInt32(Math.Log(board[y, x], 2));
					}

                    /*if (this.colors.Length <= indexColor)
                    {
                        indexColor += -this.colors.Length + 1;
                    }*/

                    boardControls[y, x].BackColor = this.colors[indexColor];
                }
                //Debug.WriteLine("");
            }
        }

        private void resetDirections()
        {
            for (int i = 0; i < this.directions.Length; i++)
            {
                this.directions[i] = 0;
            }
        }

        private int pendingAttempts()
        {
            int pendingAttemps = directions.Length;

            for (int i = 0; i < this.directions.Length; i++)
            {
                if (this.directions[i] == 1)
                {
                    pendingAttemps--;
                }
            }

            return pendingAttemps;
        }

        private int newValue()
        {
            int value = random.Next(0, 5);

            if (value != 4)
            {
                value = 2;
            }

            return value;
        }
    }
}
