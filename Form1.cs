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

namespace _53Project5
{
    public partial class GUIPicross : Form
    {
        public const int DIMENSIONS = 10; //Dimensions of the grid
        public const int TABLESIZE = 7; //Size of the row and column count boxes

        public Button[,] board; //2D array to keep track of all buttons;
        public int[,] vLengths = new int[TABLESIZE, DIMENSIONS]; 
        public int[,] hLengths = new int[DIMENSIONS,TABLESIZE];
        public int minutes, seconds; //Timer

        public GUIPicross() {
            InitializeComponent();
            createGrid();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            readFile();
            fillHGroup(); //Fill tables with row and column counts
            fillVGroup();
            enableAll();

            minutes = 4;
            seconds = 0;
            timer1.Enabled = true; //Start timer

            buttonStart.Text = "Go!";
            buttonStart.Enabled = false;
        }
        
        private void readFile()
        {
            int picCount, rndPic, n;
            string line;
            StreamReader file = new StreamReader("pics.txt");

            if (!File.Exists("pics.txt")) {
                MessageBox.Show("Could not find file pics.txt.");
                Environment.Exit(0);
            }

            picCount = int.Parse(file.ReadLine()); //Total number of pictures

            Random rnd = new Random();
            rndPic = rnd.Next(picCount) + 1; //Based on the total number, generate a random number

            try {
                do {
                    n = int.Parse(file.ReadLine()); //Read in current picture number (n)

                    for (int i = 0; i < DIMENSIONS; i++) {
                        line = file.ReadLine(); 
                        tagRow(line, i);
                    }
                } while (n != rndPic); //Continues so long as we haven't found our picture
            } finally {
                if (file != null)
                    file.Close();
            }

        }

        private void tagRow(string line, int i)
        {
            for (int j = 0; j < DIMENSIONS; j++) {
                if (line[j] == 'x') {
                    board[j, i].Tag = "black";
                    //board[j, i].Text = "-";
                } else {
                    board[j, i].Tag = "white";
                    board[j, i].Text = " ";
                }
            }
        }

        private void createGrid() 
        {
            board = new Button[DIMENSIONS, DIMENSIONS]; //Sets up the 2D array of pointers to buttons;

            //Width and Height based on available space
            double buttonWidth = groupPicture.Width / (double)DIMENSIONS;
            double buttonHeight = groupPicture.Height / (double)DIMENSIONS;

            for (int i = 0; i < DIMENSIONS; i++) {
                for (int j = 0; j < DIMENSIONS; j++) {
                    board[i, j] = createButton(i, j, buttonWidth, buttonHeight);
                }
            }
        }
        
        private Button createButton(int row, int col, double buttonWidth, double buttonHeight) 
        {
            Button newButton = new Button();

            newButton.Width = (int) buttonWidth;
            newButton.Height = (int) buttonHeight;
            newButton.Location = new Point((int) (row*buttonWidth), (int) (col*buttonHeight));
            newButton.Click += buttonWasClicked;
            newButton.Enabled = false; 

            groupPicture.Controls.Add(newButton);
            return newButton;
        }

        private void fillHGroup()
        {
            for (int i = 0; i < DIMENSIONS; i++) {
                populateHSegments(i); //Calculates table values
            }
            
            for (int i = 0; i < DIMENSIONS; i++) {
                for (int j = 0; j < TABLESIZE; j++) {
                    createHNums(i, j);
                }
            }
        }

        private Label createHNums(int row, int col)
        {
            int hSpace = 25, vSpace = 25; //Space between numbers
            Label number = new Label();
            number.Location = new Point((int)(col * hSpace), (int)(row * vSpace));
            number.Width = 20;
            number.Height = 22;
            number.Font = new Font("Impact", 15);

            if (hLengths[row, col] != 0) {
                number.Text = hLengths[row, col].ToString(); //Print the number if it is not 0
                if (hLengths[row, col] == 10)
                    number.Width = 40; //Widen the label to accomodate 10
            }

            groupHNums.Controls.Add(number);
            return number;
        }

        private void fillVGroup()
        {
            populateVSegments(); //Initializes table

            for (int i = 0; i < DIMENSIONS; i++) {
                for (int j = 0; j < TABLESIZE; j++) {
                    createVNums(j, i);
                }
            }
        }

        private Label createVNums(int row, int col)
        {
            int hSpace = 26, vSpace = 20;
            Label number = new Label();
            number.Location = new Point((int)((col * hSpace)+ 3), (int)(row * vSpace));
            number.Width = 20;
            number.Height = 22;
            number.Font = new Font("Impact", 15);

            if (vLengths[row, col] != 0) {
                number.Text = vLengths[row, col].ToString(); //Print if not 0
                if (vLengths[row, col] == 10) {
                    number.Width = 32; //If the number is 10, widen and reposition
                    number.Location = new Point((int)((col * hSpace) - 3), (int)(row * vSpace));
                }
            }

            groupVNums.Controls.Add(number);
            return number;
        }

        private void buttonWasClicked(object sender, EventArgs e) 
        {
            Button buttonClicked = (Button)sender;
            if (buttonClicked.Tag == "black") {
                buttonClicked.Text = "X";
                buttonClicked.Enabled = false;
                checkWin();
            } else {
                seconds -= 20; //Penalty
                updateTimer();
            }
        }

        private void checkWin()
        {
            if (hasWon())
            {
                endGame("Congratulations, you won!", "Good job!");
            }
        }

        private bool hasWon()
        {
            for (int i = 0; i < DIMENSIONS; i++) {
                for (int j = 0; j < DIMENSIONS; j++) {
                    if (board[i, j].Tag == "black" && board[i, j].Enabled == true) {
                        return false;
                    }
                }
            }
            return true;
        }

        private void disableAll()
        {
            for (int i = 0; i < DIMENSIONS; i++) {
                for (int j = 0; j < DIMENSIONS; j++) {
                    board[i, j].Enabled = false;

                    //Display the image
                    if (board[i, j].Tag == "black") {
                        board[i, j].Text = "X";
                        board[i, j].BackColor = Color.White; 
                    } else {
                        board[i, j].BackColor = Color.DarkGray;
                    }
                }
            }
        }

        private void enableAll()
        {
            for (int i = 0; i < DIMENSIONS; i++) {
                for (int j = 0; j < DIMENSIONS; j++) {
                    board[i, j].Enabled = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateTimer();
        }

        private void updateTimer()
        {
            if (seconds > 0 || minutes > 0) {
                if (seconds < 1) { //If the timer reaches 0 seconds 
                    seconds = 59 + seconds; //Tick down minutes, accomodating extra seconds lost (seconds will be negative)
                    minutes--;
                } else {
                    seconds--;
                }
                labelTime.Text = minutes + ":" + seconds.ToString("D2");

            } else {
                //The timer has reached zero
                labelTime.Text = "0:00";
                endGame("You're out of time! Game over!", "Aww. :(");
            }
        }

        private void endGame(string message, string button)
        {
            timer1.Enabled = false;
            disableAll();
            MessageBox.Show(message);
            buttonStart.Text = button;
        }

        //NOTE: Ported over from Project 1 with little modification
        private void populateHSegments(int col)
        {
	        int count = 0;     //Tracks number of valid spaces
	        int position = 0;  //Index of the array

	        for (int i = 0; i < DIMENSIONS; i++)
	        {
		        if (board[i, col].Tag == "black")
		        {
			        count++;
		        }
		        else
		        {
			        if (count > 0) //If we finished counting a segment
			        {
				        hLengths[col, position] = count;  //Store value
				        position++;                       //Increment index

				        count = 0;                        //Reset count
			        }
		        }
	        }

	        if (count > 0) //If the end is reached without stumbling upon a blank space
	        {
		        hLengths[col, position] = count;
		        position++;

		        count = 0;
	        }
        }

        private void populateVSegments()
        {
	        int count = 0;    //Tracks number of valid spaces
	        int position = 0; //Index of the array

	        for (int i = 0; i < DIMENSIONS; i++)
	        {
		        position = 0;

		        for (int k = 0; k < DIMENSIONS; k++)
		        {

			        if (board[i, k].Tag == "black")
			        {
				        count++;
			        }
			        else
			        {
				        if (count > 0)
				        {
					        vLengths[position, i] = count;
					        position++;

					        count = 0;
				        }
			        }
		        }

		        //Reset count once we reach the end of the column.
		        if (count > 0)
                {
			        vLengths[position, i] = count;
			        position++;

			        count = 0;
		        }
	        }
        }

    }
}
