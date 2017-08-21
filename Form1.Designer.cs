namespace _PicrossGame
{
    partial class GUIPicross
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupPicture = new System.Windows.Forms.GroupBox();
            this.groupHNums = new System.Windows.Forms.GroupBox();
            this.groupVNums = new System.Windows.Forms.GroupBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelTitle = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // groupPicture
            //
            this.groupPicture.Location = new System.Drawing.Point(14, 49);
            this.groupPicture.Name = "groupPicture";
            this.groupPicture.Size = new System.Drawing.Size(259, 252);
            this.groupPicture.TabIndex = 0;
            this.groupPicture.TabStop = false;
            this.groupPicture.Text = " ";
            //
            // groupHNums
            //
            this.groupHNums.Location = new System.Drawing.Point(279, 49);
            this.groupHNums.Name = "groupHNums";
            this.groupHNums.Size = new System.Drawing.Size(97, 252);
            this.groupHNums.TabIndex = 1;
            this.groupHNums.TabStop = false;
            //
            // groupVNums
            //
            this.groupVNums.Location = new System.Drawing.Point(14, 307);
            this.groupVNums.Name = "groupVNums";
            this.groupVNums.Size = new System.Drawing.Size(259, 107);
            this.groupVNums.TabIndex = 2;
            this.groupVNums.TabStop = false;
            //
            // labelTime
            //
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.Location = new System.Drawing.Point(300, 325);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(54, 26);
            this.labelTime.TabIndex = 3;
            this.labelTime.Text = "4:00";
            //
            // timer1
            //
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(73, 6);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(137, 39);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "PICROSS!";
            //
            // startButton
            //
            this.startButton.Location = new System.Drawing.Point(290, 366);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(73, 23);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            //
            // GUIPicross
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(388, 417);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.groupVNums);
            this.Controls.Add(this.groupHNums);
            this.Controls.Add(this.groupPicture);
            this.Name = "GUIPicross";
            this.Text = "GUI Picross";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupPicture;
        private System.Windows.Forms.GroupBox groupHNums;
        private System.Windows.Forms.GroupBox groupVNums;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button startButton;
    }
}
