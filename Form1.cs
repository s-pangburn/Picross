using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _PicrossGame {

	public partial class GUIPicross : Form {
		public const int DIMENSIONS = 10;
		public const int HINT_NUMS_SIZE = 7;

		public Button[,] board;
		public int[,] columnHintNums = new int[HINT_NUMS_SIZE, DIMENSIONS];
		public int[,] rowHintNums = new int[DIMENSIONS,HINT_NUMS_SIZE];
		public int minutes, seconds; // Timer

		public GUIPicross() {
			InitializeComponent();
			createGrid();
		}

		private void startButton_Click(object sender, EventArgs e) {
			readFile();
			countRows(); // Fill tables and add row/column counts
			countColumns();
			enableAll();

			minutes = 4;
			seconds = 0;
			timer1.Enabled = true; // Start timer

			startButton.Text = "Go!";
			startButton.Enabled = false;
		}

		private void readFile() {
			StreamReader file = new StreamReader("pics.txt");
			int totalPics, rndPic, n;
			string line;

			if (!File.Exists("pics.txt")) {
				MessageBox.Show("Could not find file pics.txt.");
				Environment.Exit(0);
			}

			totalPics = int.Parse(file.ReadLine());

			// Based on the total number of pictures, generate a random number
			Random rnd = new Random();
			chosenPic = rnd.Next(totalPics) + 1;

			try {
				do {
					n = int.Parse(file.ReadLine()); // Read in current picture number (n)

					for (int i = 0; i < DIMENSIONS; i++) {
						line = file.ReadLine();
						tagRow(line, i);
					}
				} while (n != chosenPic);
			} finally {
				if (file != null) file.Close();
			}

		}

		private void tagRow(string line, int i) {
			for (int j = 0; j < DIMENSIONS; j++) {
				if (line[j] == 'x') {
					board[j, i].Tag = "black";
				} else {
					board[j, i].Tag = "white";
					board[j, i].Text = " ";
				}
			}
		}

		private void createGrid() {
			// Sets up a 2D array of pointers to buttons;
			board = new Button[DIMENSIONS, DIMENSIONS];

			// Width and Height based on available space
			double buttonWidth = groupPicture.Width / (double)DIMENSIONS;
			double buttonHeight = groupPicture.Height / (double)DIMENSIONS;

			for (int i = 0; i < DIMENSIONS; i++) {
				for (int j = 0; j < DIMENSIONS; j++) {
					board[i, j] = createButton(i, j, buttonWidth, buttonHeight);
				}
			}
		}

		private Button createButton(int row, int col,
					    double buttonWidth,
					    double buttonHeight) {
			Button newButton = new Button();

			newButton.Width = (int) buttonWidth;
			newButton.Height = (int) buttonHeight;
			newButton.Location = new Point((int) (row*buttonWidth),
						       (int) (col*buttonHeight));
			newButton.Click += buttonWasClicked;
			newButton.Enabled = false;

			groupPicture.Controls.Add(newButton);
			return newButton;
		}

		private void countRows() {
			for (int i = 0; i < DIMENSIONS; i++) {
				countRow(i);
			}

			for (int i = 0; i < DIMENSIONS; i++) {
				for (int j = 0; j < HINT_NUMS_SIZE; j++) {
					createRowHintNums(i, j);
				}
			}
		}

		private void countRow(int col) {
			int numAdjacent = 0;
			int counts_idx = 0;

			for (int i = 0; i < DIMENSIONS; i++) {
				if (board[i, col].Tag == "black") {
					numAdjacent++;
				} else {
					// If we finished a segment
					if (numAdjacent > 0) {
						rowHintNums[col, counts_idx] = numAdjacent;  // Store value
						numAdjacent = 0;
						counts_idx++;
					}
				}
			}

			// We've reached the end of this row. Reset everything.
			if (count > 0) {
				rowHintNums[col, counts_idx] = numAdjacent;
				numAdjacent = 0;
				counts_idx++;
			}
		}

		private Label createRowHintNums(int row, int col) {
			int hSpace = 25, vSpace = 25; // Space between numbers

			Label number = new Label();
			number.Location = new Point((int)(col * hSpace), (int)(row * vSpace));
			number.Width = 20;
			number.Height = 22;
			number.Font = new Font("Impact", 15);

			if (rowHintNums[row, col] != 0) {
				number.Text = rowHintNums[row, col].ToString(); // Print if not 0
				if (rowHintNums[row, col] == 10) number.Width = 40; // Widen the label to accomodate 10
			}

			rowHintNums.Controls.Add(number);
			return number;
		}

		private void countColumns() {
			countColumn(); // Initializes table

			for (int i = 0; i < DIMENSIONS; i++) {
				for (int j = 0; j < HINT_NUMS_SIZE; j++) {
					createColumnHintNums(j, i);
				}
			}
		}

		private void countColumn() {
			int numAdjacent = 0;
			int counts_idx = 0;

			for (int i = 0; i < DIMENSIONS; i++) {
				counts_idx = 0;

				for (int k = 0; k < DIMENSIONS; k++) {
					if (board[i, k].Tag == "black") {
						numAdjacent++;
					} else {
						if (numAdjacent > 0) {
							// We've finished a segment
							columnHintNums[counts_idx, i] = numAdjacent;  // Store value
							numAdjacent = 0;
							counts_idx++;
						}
					}
				}

				// We've reached the end of this column. Reset everything.
				if (numAdjacent > 0) {
					columnHintNums[counts_idx, i] = numAdjacent;
					numAdjacent = 0;
					counts_idx++;
				}
			}
		}

		private Label createColumnHintNums(int row, int col) {
			int hSpace = 26, vSpace = 20;

			Label number = new Label();
			number.Location = new Point((int)((col * hSpace)+ 3),
						    (int)(row * vSpace));
			number.Width = 20;
			number.Height = 22;
			number.Font = new Font("Impact", 15);

			if (columnHintNums[row, col] != 0) {
				number.Text = columnHintNums[row, col].ToString(); // Print if not 0
				if (columnHintNums[row, col] == 10) {
					number.Width = 32; // If the number is 10, widen and reposition
					number.Location = new Point((int)((col * hSpace) - 3),
								    (int)(row * vSpace));
				}
			}

			columnHintNums.Controls.Add(number);
			return number;
		}

		private void buttonWasClicked(object sender, EventArgs e) {
			Button buttonClicked = (Button)sender;
			if (buttonClicked.Tag == "black") {
				buttonClicked.Text = "X";
				buttonClicked.Enabled = false;
				checkWin();
			} else {
				seconds -= 20; // Penalty
				updateTimer();
			}
		}

		private void timer1_Tick(object sender, EventArgs e) {
			updateTimer();
		}

		private void updateTimer() {
			if (seconds > 0 || minutes > 0) {
				if (seconds < 1) {
					// Decrement minutes, taking into account negative seconds
					seconds = 59 + seconds;
					minutes--;
				} else {
					seconds--;
				}
				labelTime.Text = minutes + ":" + seconds.ToString("D2");

			} else {
				// The timer has reached zero
				labelTime.Text = "0:00";
				endGame("You're out of time! Game over!", "Aww. :(");
			}
		}

		private void checkWin() {
			if (hasWon()) endGame("Congratulations, you won!", "Good job!");
		}

		private bool hasWon() {
			for (int i = 0; i < DIMENSIONS; i++) {
				for (int j = 0; j < DIMENSIONS; j++) {
					if (board[i, j].Tag == "black" && board[i, j].Enabled == true) {
						return false;
					}
				}
			}
			return true;
		}

		private void endGame(string message, string button) {
			timer1.Enabled = false;
			disableAll();
			MessageBox.Show(message);
			startButton.Text = button;
		}

		private void disableAll() {
			for (int i = 0; i < DIMENSIONS; i++) {
				for (int j = 0; j < DIMENSIONS; j++) {
					board[i, j].Enabled = false;

					// Display the image
					if (board[i, j].Tag == "black") {
						board[i, j].Text = "X";
						board[i, j].BackColor = Color.White;
					} else {
						board[i, j].BackColor = Color.DarkGray;
					}
				}
			}
		}

		private void enableAll() {
			for (int i = 0; i < DIMENSIONS; i++) {
				for (int j = 0; j < DIMENSIONS; j++) {
					board[i, j].Enabled = true;
				}
			}
		}
	}
}
