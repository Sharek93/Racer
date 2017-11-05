namespace Racer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel2 = new System.Windows.Forms.Panel();
            this.EndRaceBackToMenuButton = new Racer.MainButton();
            this.ScoreListView = new System.Windows.Forms.ListView();
            this.ScoreSortCombobox = new System.Windows.Forms.ComboBox();
            this.ScoreNickComboBox = new System.Windows.Forms.ComboBox();
            this.ExitDecButton = new Racer.MainButton();
            this.ExitAccButton = new Racer.MainButton();
            this.ScoreDownButton = new Racer.MainButton();
            this.ScoreUpButton = new Racer.MainButton();
            this.ScoreDecButton = new Racer.MainButton();
            this.PausedDecButton = new Racer.MainButton();
            this.PausedAccButton = new Racer.MainButton();
            this.RacePrepRem10Button = new Racer.MainButton();
            this.RacePrepAdd10Button = new Racer.MainButton();
            this.RacePrepRem1Button = new Racer.MainButton();
            this.RacePrepAdd1Button = new Racer.MainButton();
            this.RacePrepDecButton = new Racer.MainButton();
            this.RacePrepAccButton = new Racer.MainButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.mainButton3 = new Racer.MainButton();
            this.mainButton2 = new Racer.MainButton();
            this.mainButton1 = new Racer.MainButton();
            this.Map_pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.FPScounter = new System.ComponentModel.BackgroundWorker();
            this.Car_Work = new System.ComponentModel.BackgroundWorker();
            this.Map_loader = new System.ComponentModel.BackgroundWorker();
            this.BackColorChangerTimer = new System.Windows.Forms.Timer(this.components);
            this.Start_sequenceWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Map_pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.EndRaceBackToMenuButton);
            this.panel2.Controls.Add(this.ScoreListView);
            this.panel2.Controls.Add(this.ScoreSortCombobox);
            this.panel2.Controls.Add(this.ScoreNickComboBox);
            this.panel2.Controls.Add(this.ExitDecButton);
            this.panel2.Controls.Add(this.ExitAccButton);
            this.panel2.Controls.Add(this.ScoreDownButton);
            this.panel2.Controls.Add(this.ScoreUpButton);
            this.panel2.Controls.Add(this.ScoreDecButton);
            this.panel2.Controls.Add(this.PausedDecButton);
            this.panel2.Controls.Add(this.PausedAccButton);
            this.panel2.Controls.Add(this.RacePrepRem10Button);
            this.panel2.Controls.Add(this.RacePrepAdd10Button);
            this.panel2.Controls.Add(this.RacePrepRem1Button);
            this.panel2.Controls.Add(this.RacePrepAdd1Button);
            this.panel2.Controls.Add(this.RacePrepDecButton);
            this.panel2.Controls.Add(this.RacePrepAccButton);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.mainButton3);
            this.panel2.Controls.Add(this.mainButton2);
            this.panel2.Controls.Add(this.mainButton1);
            this.panel2.Controls.Add(this.Map_pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(822, 503);
            this.panel2.TabIndex = 1;
            // 
            // EndRaceBackToMenuButton
            // 
            this.EndRaceBackToMenuButton.Active_image = global::Racer.Properties.Resources.Button_p;
            this.EndRaceBackToMenuButton.Background_image = global::Racer.Properties.Resources.Button;
            this.EndRaceBackToMenuButton.BorderThickness = 0F;
            this.EndRaceBackToMenuButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.EndRaceBackToMenuButton.Location = new System.Drawing.Point(244, 393);
            this.EndRaceBackToMenuButton.Name = "EndRaceBackToMenuButton";
            this.EndRaceBackToMenuButton.Size = new System.Drawing.Size(237, 66);
            this.EndRaceBackToMenuButton.TabIndex = 22;
            this.EndRaceBackToMenuButton.Text = "Powrót do menu";
            this.EndRaceBackToMenuButton.Click += new System.EventHandler(this.EndRaceBackToMenuButton_Click);
            // 
            // ScoreListView
            // 
            this.ScoreListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScoreListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ScoreListView.FullRowSelect = true;
            this.ScoreListView.HideSelection = false;
            this.ScoreListView.Location = new System.Drawing.Point(26, 108);
            this.ScoreListView.Name = "ScoreListView";
            this.ScoreListView.Size = new System.Drawing.Size(121, 97);
            this.ScoreListView.TabIndex = 21;
            this.ScoreListView.UseCompatibleStateImageBehavior = false;
            // 
            // ScoreSortCombobox
            // 
            this.ScoreSortCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ScoreSortCombobox.FormattingEnabled = true;
            this.ScoreSortCombobox.Location = new System.Drawing.Point(117, 211);
            this.ScoreSortCombobox.Name = "ScoreSortCombobox";
            this.ScoreSortCombobox.Size = new System.Drawing.Size(85, 45);
            this.ScoreSortCombobox.TabIndex = 20;
            this.ScoreSortCombobox.SelectedIndexChanged += new System.EventHandler(this.ScoreSortCombobox_SelectedIndexChanged);
            // 
            // ScoreNickComboBox
            // 
            this.ScoreNickComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ScoreNickComboBox.FormattingEnabled = true;
            this.ScoreNickComboBox.Location = new System.Drawing.Point(26, 211);
            this.ScoreNickComboBox.Name = "ScoreNickComboBox";
            this.ScoreNickComboBox.Size = new System.Drawing.Size(85, 45);
            this.ScoreNickComboBox.TabIndex = 19;
            this.ScoreNickComboBox.SelectedIndexChanged += new System.EventHandler(this.ScoreNickComboBox_SelectedIndexChanged);
            // 
            // ExitDecButton
            // 
            this.ExitDecButton.Active_image = global::Racer.Properties.Resources.decline_p;
            this.ExitDecButton.Background_image = global::Racer.Properties.Resources.decline;
            this.ExitDecButton.BorderThickness = 0F;
            this.ExitDecButton.Location = new System.Drawing.Point(677, 428);
            this.ExitDecButton.Name = "ExitDecButton";
            this.ExitDecButton.Size = new System.Drawing.Size(50, 50);
            this.ExitDecButton.TabIndex = 18;
            this.ExitDecButton.Click += new System.EventHandler(this.ExitDecButton_Click);
            // 
            // ExitAccButton
            // 
            this.ExitAccButton.Active_image = global::Racer.Properties.Resources.Accept_p;
            this.ExitAccButton.Background_image = global::Racer.Properties.Resources.Accept;
            this.ExitAccButton.BorderThickness = 0F;
            this.ExitAccButton.Location = new System.Drawing.Point(733, 428);
            this.ExitAccButton.Name = "ExitAccButton";
            this.ExitAccButton.Size = new System.Drawing.Size(50, 50);
            this.ExitAccButton.TabIndex = 17;
            this.ExitAccButton.Click += new System.EventHandler(this.ExitAcxButton_Click);
            // 
            // ScoreDownButton
            // 
            this.ScoreDownButton.Active_image = global::Racer.Properties.Resources.Arrow_D_p;
            this.ScoreDownButton.Background_image = global::Racer.Properties.Resources.Arrow_D;
            this.ScoreDownButton.BorderThickness = 0F;
            this.ScoreDownButton.Location = new System.Drawing.Point(106, 354);
            this.ScoreDownButton.Name = "ScoreDownButton";
            this.ScoreDownButton.Size = new System.Drawing.Size(50, 50);
            this.ScoreDownButton.TabIndex = 16;
            this.ScoreDownButton.Click += new System.EventHandler(this.ScoreDownButton_Click);
            // 
            // ScoreUpButton
            // 
            this.ScoreUpButton.Active_image = global::Racer.Properties.Resources.Arrow_U_p;
            this.ScoreUpButton.Background_image = global::Racer.Properties.Resources.Arrow_U;
            this.ScoreUpButton.BorderThickness = 0F;
            this.ScoreUpButton.Location = new System.Drawing.Point(50, 354);
            this.ScoreUpButton.Name = "ScoreUpButton";
            this.ScoreUpButton.Size = new System.Drawing.Size(50, 50);
            this.ScoreUpButton.TabIndex = 15;
            this.ScoreUpButton.Click += new System.EventHandler(this.ScoreUpButton_Click);
            // 
            // ScoreDecButton
            // 
            this.ScoreDecButton.Active_image = global::Racer.Properties.Resources.decline_p;
            this.ScoreDecButton.Background_image = global::Racer.Properties.Resources.decline;
            this.ScoreDecButton.BorderThickness = 0F;
            this.ScoreDecButton.Location = new System.Drawing.Point(50, 298);
            this.ScoreDecButton.Name = "ScoreDecButton";
            this.ScoreDecButton.Size = new System.Drawing.Size(50, 50);
            this.ScoreDecButton.TabIndex = 13;
            this.ScoreDecButton.Click += new System.EventHandler(this.ScoreDecButton_Click);
            // 
            // PausedDecButton
            // 
            this.PausedDecButton.Active_image = global::Racer.Properties.Resources.decline_p;
            this.PausedDecButton.Background_image = global::Racer.Properties.Resources.decline;
            this.PausedDecButton.BorderThickness = 0F;
            this.PausedDecButton.Location = new System.Drawing.Point(671, 41);
            this.PausedDecButton.Name = "PausedDecButton";
            this.PausedDecButton.Size = new System.Drawing.Size(50, 50);
            this.PausedDecButton.TabIndex = 12;
            this.PausedDecButton.Click += new System.EventHandler(this.PausedDecButton_Click);
            // 
            // PausedAccButton
            // 
            this.PausedAccButton.Active_image = global::Racer.Properties.Resources.Accept_p;
            this.PausedAccButton.Background_image = global::Racer.Properties.Resources.Accept;
            this.PausedAccButton.BorderThickness = 0F;
            this.PausedAccButton.Location = new System.Drawing.Point(727, 41);
            this.PausedAccButton.Name = "PausedAccButton";
            this.PausedAccButton.Size = new System.Drawing.Size(50, 50);
            this.PausedAccButton.TabIndex = 11;
            this.PausedAccButton.Click += new System.EventHandler(this.PausedAccButton_Click);
            // 
            // RacePrepRem10Button
            // 
            this.RacePrepRem10Button.Active_image = global::Racer.Properties.Resources.Arrow_L_p;
            this.RacePrepRem10Button.Background_image = global::Racer.Properties.Resources.Arrow_L;
            this.RacePrepRem10Button.BorderThickness = 0F;
            this.RacePrepRem10Button.Location = new System.Drawing.Point(617, 244);
            this.RacePrepRem10Button.Name = "RacePrepRem10Button";
            this.RacePrepRem10Button.Size = new System.Drawing.Size(50, 50);
            this.RacePrepRem10Button.TabIndex = 10;
            this.RacePrepRem10Button.Click += new System.EventHandler(this.RacePrepRem10Button_Click);
            // 
            // RacePrepAdd10Button
            // 
            this.RacePrepAdd10Button.Active_image = global::Racer.Properties.Resources.Arrow_R_p;
            this.RacePrepAdd10Button.Background_image = global::Racer.Properties.Resources.Arrow_R;
            this.RacePrepAdd10Button.BorderThickness = 0F;
            this.RacePrepAdd10Button.Location = new System.Drawing.Point(677, 245);
            this.RacePrepAdd10Button.Name = "RacePrepAdd10Button";
            this.RacePrepAdd10Button.Size = new System.Drawing.Size(50, 50);
            this.RacePrepAdd10Button.TabIndex = 9;
            this.RacePrepAdd10Button.Click += new System.EventHandler(this.RacePrepAdd10Button_Click);
            // 
            // RacePrepRem1Button
            // 
            this.RacePrepRem1Button.Active_image = global::Racer.Properties.Resources.Arrow_D_p;
            this.RacePrepRem1Button.Background_image = global::Racer.Properties.Resources.Arrow_D;
            this.RacePrepRem1Button.BorderThickness = 0F;
            this.RacePrepRem1Button.Location = new System.Drawing.Point(561, 245);
            this.RacePrepRem1Button.Name = "RacePrepRem1Button";
            this.RacePrepRem1Button.Size = new System.Drawing.Size(50, 50);
            this.RacePrepRem1Button.TabIndex = 8;
            this.RacePrepRem1Button.Click += new System.EventHandler(this.RacePrepRem1Button_Click);
            // 
            // RacePrepAdd1Button
            // 
            this.RacePrepAdd1Button.Active_image = global::Racer.Properties.Resources.Arrow_U_p;
            this.RacePrepAdd1Button.Background_image = global::Racer.Properties.Resources.Arrow_U;
            this.RacePrepAdd1Button.BorderThickness = 0F;
            this.RacePrepAdd1Button.Location = new System.Drawing.Point(733, 245);
            this.RacePrepAdd1Button.Name = "RacePrepAdd1Button";
            this.RacePrepAdd1Button.Size = new System.Drawing.Size(50, 50);
            this.RacePrepAdd1Button.TabIndex = 7;
            this.RacePrepAdd1Button.Click += new System.EventHandler(this.RacePrepAdd1Button_Click);
            // 
            // RacePrepDecButton
            // 
            this.RacePrepDecButton.Active_image = global::Racer.Properties.Resources.decline_p;
            this.RacePrepDecButton.Background_image = global::Racer.Properties.Resources.decline;
            this.RacePrepDecButton.BorderThickness = 0F;
            this.RacePrepDecButton.Location = new System.Drawing.Point(615, 301);
            this.RacePrepDecButton.Name = "RacePrepDecButton";
            this.RacePrepDecButton.Size = new System.Drawing.Size(50, 50);
            this.RacePrepDecButton.TabIndex = 6;
            this.RacePrepDecButton.Click += new System.EventHandler(this.RacePrepDecButton_Click);
            // 
            // RacePrepAccButton
            // 
            this.RacePrepAccButton.Active_image = global::Racer.Properties.Resources.Accept_p;
            this.RacePrepAccButton.Background_image = global::Racer.Properties.Resources.Accept;
            this.RacePrepAccButton.BorderThickness = 0F;
            this.RacePrepAccButton.Location = new System.Drawing.Point(671, 301);
            this.RacePrepAccButton.Name = "RacePrepAccButton";
            this.RacePrepAccButton.Size = new System.Drawing.Size(50, 50);
            this.RacePrepAccButton.TabIndex = 5;
            this.RacePrepAccButton.Click += new System.EventHandler(this.RacePrepAccButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.textBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(498, 158);
            this.textBox1.MaxLength = 30;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(346, 80);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mainButton3
            // 
            this.mainButton3.Active_image = global::Racer.Properties.Resources.Button_p;
            this.mainButton3.Background_image = global::Racer.Properties.Resources.Button;
            this.mainButton3.BorderThickness = 0F;
            this.mainButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mainButton3.Location = new System.Drawing.Point(225, 268);
            this.mainButton3.Name = "mainButton3";
            this.mainButton3.Size = new System.Drawing.Size(277, 119);
            this.mainButton3.TabIndex = 3;
            this.mainButton3.Text = "ZAKOŃCZ";
            this.mainButton3.Click += new System.EventHandler(this.mainButton3_Click);
            // 
            // mainButton2
            // 
            this.mainButton2.Active_image = global::Racer.Properties.Resources.Button_p;
            this.mainButton2.Background_image = global::Racer.Properties.Resources.Button;
            this.mainButton2.BorderThickness = 0F;
            this.mainButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mainButton2.Location = new System.Drawing.Point(225, 158);
            this.mainButton2.Name = "mainButton2";
            this.mainButton2.Size = new System.Drawing.Size(277, 115);
            this.mainButton2.TabIndex = 2;
            this.mainButton2.Text = "WYNIKI";
            this.mainButton2.Click += new System.EventHandler(this.mainButton2_Click);
            // 
            // mainButton1
            // 
            this.mainButton1.Active_image = ((System.Drawing.Bitmap)(resources.GetObject("mainButton1.Active_image")));
            this.mainButton1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.mainButton1.Background_image = ((System.Drawing.Bitmap)(resources.GetObject("mainButton1.Background_image")));
            this.mainButton1.BorderThickness = 0F;
            this.mainButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mainButton1.Location = new System.Drawing.Point(225, 41);
            this.mainButton1.Name = "mainButton1";
            this.mainButton1.Size = new System.Drawing.Size(277, 117);
            this.mainButton1.TabIndex = 1;
            this.mainButton1.Text = "NOWY WYŚCIG";
            this.mainButton1.Click += new System.EventHandler(this.mainButton1_Click);
            // 
            // Map_pictureBox1
            // 
            this.Map_pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.Map_pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.Map_pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.Map_pictureBox1.Name = "Map_pictureBox1";
            this.Map_pictureBox1.Size = new System.Drawing.Size(761, 404);
            this.Map_pictureBox1.TabIndex = 0;
            this.Map_pictureBox1.TabStop = false;
            this.Map_pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_pictureBox1_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FPScounter
            // 
            this.FPScounter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.FPScounter_DoWork);
            this.FPScounter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.FPScounter_RunWorkerCompleted);
            // 
            // Car_Work
            // 
            this.Car_Work.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Car_Work_DoWork);
            this.Car_Work.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Car_Work_RunWorkerCompleted);
            // 
            // Map_loader
            // 
            this.Map_loader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Map_loader_DoWork);
            this.Map_loader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Map_loader_RunWorkerCompleted);
            // 
            // BackColorChangerTimer
            // 
            this.BackColorChangerTimer.Interval = 50;
            this.BackColorChangerTimer.Tick += new System.EventHandler(this.BackColorChangerTimer_Tick);
            // 
            // Start_sequenceWorker
            // 
            this.Start_sequenceWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Start_sequenceWorker_DoWork);
            this.Start_sequenceWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Start_sequenceWorker_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 503);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Racer";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Map_pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox Map_pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker FPScounter;
        private System.ComponentModel.BackgroundWorker Car_Work;
        private System.ComponentModel.BackgroundWorker Map_loader;
        private MainButton mainButton1;
        private MainButton mainButton2;
        private MainButton mainButton3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer BackColorChangerTimer;
        private MainButton RacePrepAccButton;
        private MainButton RacePrepDecButton;
        private System.ComponentModel.BackgroundWorker Start_sequenceWorker;
        private MainButton RacePrepAdd1Button;
        private MainButton RacePrepRem1Button;
        private MainButton RacePrepRem10Button;
        private MainButton RacePrepAdd10Button;
        private MainButton PausedDecButton;
        private MainButton PausedAccButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MainButton ScoreDecButton;
        private MainButton ScoreDownButton;
        private MainButton ScoreUpButton;
        private MainButton ExitDecButton;
        private MainButton ExitAccButton;
        private System.Windows.Forms.ComboBox ScoreNickComboBox;
        private System.Windows.Forms.ComboBox ScoreSortCombobox;
        private System.Windows.Forms.ListView ScoreListView;
        private MainButton EndRaceBackToMenuButton;
    }
}

