using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Racer
{
    //TODO: Dokończyć minimapę, przerzucić całe rysowanie do osbnej klasy


    public partial class Form1 : Form
    {
        //## KONSTRUKTORY I ZMIENNE

        #region KONSTRUKTORY I ZMIENNE
        //## KONSTRUKTOTY

        Maps_import import = new Maps_import();
        Map_constructor constructor = new Map_constructor();
        Draw_Interface draw_interface = new Draw_Interface();
        Race_status race_status = new Race_status();
        MainButton main_button = new MainButton();
        Scores_ImportExport scores_ie = new Scores_ImportExport();
        Stopwatch Time = new Stopwatch();

        //## ZMIENNE

        //główna mapa
        Rectangle show_rect;
        Bitmap croped_bmp;

        //swiatła startu
        Image[] start_squence_image = new Image[5];
        int start_squence_image_id;
        bool start_sequence = false;
        string start_count;

        //właściwości wkranu, mapy i pojazdu
        Point player_point = new Point(500, 500);
        int resolutionX = 0, resolutionY = 0;
        int main_map_Width, main_map_Height;

        //debug info
        short fps_count = 0, fps;
        string[] debug_list = new string[10];

        //dane o wyścigu 
        int final_score;
        int collected_checkpoints;
        string end_race_save_status;
        int race_lenght = 0;
        bool race_end = false;
        string time;
        TimeSpan final_time;
        string nickname;

        //ackcja definiujaca zachowanie PictureBoxa
        string action = "main";

        //grafika tła menu
        Color main_textcolor, main_backcolor;
        bool color_decreasing = true;
        double color_aplha = 0;

        // Dodatkowe
        string direction;
        bool started = false;
        bool accelerate, breaks, handbreak, left, right, canleft = true, canright = true, fullscreen = false;

        //pojazd
        Vehicle MuscleCar; 
        #endregion

        public Form1()
        {
            InitializeComponent();

            Prepare_buttons();
            Map_pictureBox1.Dock = DockStyle.Fill;
            BackColor = Color.OrangeRed;

            resolutionX = Size.Width; resolutionY = Size.Height;
                        
            timer1.Enabled = true;
            BackColorChangerTimer.Enabled = true;
            Map_pictureBox1.SizeMode = PictureBoxSizeMode.Normal;

            Fullscreen();
            ScoreListView.View = View.Details;

            scores_ie.NicknamesImported += OnNicknamesImported;
            scores_ie.ScoresImported += OnScoresImported;
            scores_ie.ScoreSaved += OnScoreSaved;
        }

        private void Prepare_buttons()
        {
            #region Główne Menu
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, mainButton1, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, mainButton2, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, mainButton3, new object[] { true });
            #endregion

            #region Przygotowanie Wyścigu

            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, RacePrepAccButton, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, RacePrepDecButton, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, RacePrepAdd1Button, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, RacePrepAdd10Button, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, RacePrepRem1Button, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, RacePrepRem10Button, new object[] { true });
            #endregion

            #region Pauza
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, PausedAccButton, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, PausedDecButton, new object[] { true });
            #endregion

            #region Wyniki
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, ScoreListView, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, ScoreDecButton, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, ScoreUpButton, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, ScoreDownButton, new object[] { true });
            #endregion

            #region Wyjscie
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, ExitAccButton, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, ExitDecButton, new object[] { true });
            typeof(MainButton).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, EndRaceBackToMenuButton, new object[] { true });
            #endregion

            #region Ukrywanie Przycisków
            ButtonStatus(RacePrepAccButton, false);
            ButtonStatus(RacePrepDecButton, false);
            ButtonStatus(RacePrepAdd1Button, false);
            ButtonStatus(RacePrepAdd10Button, false);
            ButtonStatus(RacePrepRem1Button, false);
            ButtonStatus(RacePrepRem10Button, false);

            ButtonStatus(ScoreDecButton, false);
            ButtonStatus(ScoreDownButton, false);
            ButtonStatus(ScoreUpButton, false);

            ButtonStatus(ExitAccButton, false);
            ButtonStatus(ExitDecButton, false);

            ButtonStatus(PausedAccButton, false);
            ButtonStatus(PausedDecButton, false);

            ButtonStatus(EndRaceBackToMenuButton, false);

            textBox1.Enabled = false;
            textBox1.Visible = false;

            ScoreNickComboBox.Enabled = false;
            ScoreNickComboBox.Visible = false;

            ScoreSortCombobox.Enabled = false;
            ScoreSortCombobox.Visible = false;

            ScoreListView.Enabled = false;
            ScoreListView.Visible = false; 
            #endregion
        }

        private static Color ColorBlend(Color c1, Color c2, double amount)
        {
            byte r = (byte)((c1.R * amount) + c2.R * (1 - amount));
            byte g = (byte)((c1.G * amount) + c2.G * (1 - amount));
            byte b = (byte)((c1.B * amount) + c2.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }

        //## TMIERY & Background WORKERY ##

        #region Timers & Background Workers

        private void Start_sequenceWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 4; i >= 0; i--)
            {
                if (i >= 1 && i <= 3)
                {
                    start_count = i.ToString();
                }
                else if (i == 0)
                {
                    start_count = "START";
                }
                else if (i == 4)
                {
                    start_count = "PRZYGOTUJ SIĘ";
                }
                start_squence_image_id = i;
                Thread.Sleep(1000);
            }
        }

        private void Start_sequenceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Car_Work.RunWorkerAsync();
            start_sequence = true;
        }

        private void Map_loader_DoWork(object sender, DoWorkEventArgs e)
        {
            if (race_lenght <= constructor.Checkpoints_count)
            {
                race_end = true;
            }
            constructor.Map_Genetator(direction, constructor.Last_map_end, constructor.Last_map_location, race_end);
            
        }

        private void Map_loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (constructor.Load)
            {
                if (constructor.Current_main_map == 1)
                {
                    constructor.Current_main_map = 2;
                }
                else
                {
                    constructor.Current_main_map = 1;
                }

                switch (direction)
                {
                    case "up":
                        {
                            MuscleCar.Y += 3000;
                            break;
                        }
                    case "left":
                        {
                            MuscleCar.X += 3000;
                            break;
                        }
                    case "right":
                        {
                            MuscleCar.X -= 3000;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void Car_Work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (action != "paused" && action != "ended")
            {
                Car_Work.RunWorkerAsync();
            }
        }

        private void FPScounter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (action != "paused" && action != "ended")
            {
                FPScounter.RunWorkerAsync();
            }
        }

        private void FPScounter_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            fps = fps_count;
            fps_count = 0;
        }

        private void Car_Work_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(10);

            TimeSpan ts = Time.Elapsed;
            time = string.Format("{0:00}.{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds);

            if (accelerate) { MuscleCar.Accelerate(); } else { MuscleCar.Neutral(); }
            if (breaks) { MuscleCar.Breaks(); }
            if (left) { MuscleCar.TurnLeft(); }
            if (right) { MuscleCar.TurnRight(); }
            if (handbreak) { MuscleCar.Handbreak(); }
            
            debug_list[0] = string.Format("Car_pos {0}/{1}", (int)MuscleCar.X, (int)MuscleCar.Y);
            debug_list[1] = string.Format("Direction: {0} Drift {1}", (int)MuscleCar.Direction, (int)MuscleCar.Drift);
            debug_list[2] = "Speed: " + (int)MuscleCar.V + " Acc: " + (int)MuscleCar.A;
            debug_list[3] = "FPS: " + fps + " F " + fullscreen.ToString();
            debug_list[4] = "Ground Type: "  + MuscleCar.Ground_type;
            debug_list[5] = "Car power: " + MuscleCar.F1.ToString();
            debug_list[6] = "Map loading status: " + Map_loader.IsBusy + " " + constructor.Current_main_map.ToString();
            debug_list[7] = "Map endpoint data: " + constructor.Last_map_end + " " + constructor.Last_map_location;

            MuscleCar.Work(0.01);

        }

        private void BackColorChangerTimer_Tick(object sender, EventArgs e)
        {
            if (color_decreasing)
            {
                if (color_aplha - 0.01 <= 0)
                {
                    color_aplha = 0;
                    color_decreasing = false;
                }
                else
                {
                    color_aplha -= 0.01;
                }                
            }
            else
            {
                if (color_aplha + 0.01 >= 1)
                {
                    color_aplha = 1;
                    color_decreasing = true;
                }
                else
                {
                    color_aplha += 0.01;
                }
            }
            main_textcolor = ColorBlend(Color.Black, Color.OrangeRed, color_aplha);
            main_backcolor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);

            panel2.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);

            mainButton1.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            mainButton2.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            mainButton3.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            RacePrepAccButton.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            RacePrepDecButton.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            RacePrepAdd1Button.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            RacePrepAdd10Button.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            RacePrepRem1Button.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            RacePrepRem10Button.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);

            textBox1.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            textBox1.ForeColor = ColorBlend(Color.Black, Color.OrangeRed, color_aplha);
            ScoreNickComboBox.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            ScoreNickComboBox.ForeColor = ColorBlend(Color.Black, Color.OrangeRed, color_aplha);
            ScoreSortCombobox.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            ScoreSortCombobox.ForeColor = ColorBlend(Color.Black, Color.OrangeRed, color_aplha);
            ScoreListView.BackColor = ColorBlend(Color.OrangeRed, Color.Black, color_aplha);
            ScoreListView.ForeColor = ColorBlend(Color.Black, Color.OrangeRed, color_aplha);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();       
            Map_pictureBox1.Refresh();
        }
        #endregion

        //## STEROWANIE ##

        #region Controls
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        accelerate = true;
                        break;
                    }
                case Keys.Left:
                    {
                        if (canleft)
                        {
                            left = true;
                            canright = false;
                        }
                        break;
                    }
                case Keys.Right:
                    {
                        if (canright)
                        {
                            right = true;
                            canleft = false;
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        breaks = true;
                        break;
                    }
                case Keys.O:
                    {
                        MuscleCar.X = main_map_Width / 2;
                        MuscleCar.Y = main_map_Height / 2;
                        break;
                    }
                case Keys.Space:
                    {
                        handbreak = true;
                        break;
                    }
                case Keys.F:
                    {
                        Fullscreen();
                        break;
                    }
                case Keys.Escape:
                    {
                        if (action == "race")
                        {
                            action = "paused";
                            ButtonStatus(PausedAccButton, true);
                            PausedAccButton.BackColor = Color.OrangeRed;
                            ButtonStatus(PausedDecButton, true);
                            PausedDecButton.BackColor = Color.OrangeRed;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        accelerate = false;
                        break;
                    }
                case Keys.Left:
                    {
                        left = false;
                        canright = true;
                        MuscleCar.EndDrift();
                        break;
                    }
                case Keys.Right:
                    {
                        right = false;
                        canleft = true;
                        MuscleCar.EndDrift();
                        break;
                    }
                case Keys.Down:
                    {
                        breaks = false;
                        break;
                    }
                case Keys.Space:
                    {
                        handbreak = false;
                        break;
                    }
            }
        }
        #endregion

        //## PRZYCISKI ##

        #region Buttons
        private void RacePrepAccButton_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0 && race_lenght > 0)
            {
                nickname = textBox1.Text;
                Prepare_race();
            }
        }

        private void RacePrepDecButton_Click(object sender, EventArgs e)
        {
            ButtonStatus(mainButton1, true);
            ButtonStatus(mainButton2, true);
            ButtonStatus(mainButton3, true);
            ButtonStatus(RacePrepAccButton, false);
            ButtonStatus(RacePrepDecButton, false);
            ButtonStatus(RacePrepAdd1Button, false);
            ButtonStatus(RacePrepAdd10Button, false);
            ButtonStatus(RacePrepRem1Button, false);
            ButtonStatus(RacePrepRem10Button, false);

            textBox1.Enabled = false;
            textBox1.Visible = false;

            action = "main";
        }

        private void RacePrepAdd1Button_Click(object sender, EventArgs e)
        {
            if (race_lenght + 1 >= 1000)
            {
                race_lenght = 1000;
            }
            else
            {
                race_lenght++;
            }
        }

        private void RacePrepRem1Button_Click(object sender, EventArgs e)
        {
            if (race_lenght - 1 <= 0)
            {
                race_lenght = 0;
            }
            else
            {
                race_lenght--;
            }
        }

        private void RacePrepAdd10Button_Click(object sender, EventArgs e)
        {
            if (race_lenght + 10 >= 1000)
            {
                race_lenght = 1000;
            }
            else
            {
                race_lenght += 10;
            }
        }

        private void RacePrepRem10Button_Click(object sender, EventArgs e)
        {
            if (race_lenght - 10 <= 0)
            {
                race_lenght = 0;
            }
            else
            {
                race_lenght -= 10;
            }
        }

        private void ScoreDecButton_Click(object sender, EventArgs e)
        {
            action = "main";
            ButtonStatus(ScoreUpButton, false);
            ButtonStatus(ScoreDownButton, false);
            ButtonStatus(ScoreDecButton, false);
            ScoreNickComboBox.Enabled = false;
            ScoreNickComboBox.Visible = false;
            ScoreSortCombobox.Enabled = false;
            ScoreSortCombobox.Visible = false;
            ScoreListView.Enabled = false;
            ScoreListView.Visible = false;
            ButtonStatus(mainButton1, true);
            ButtonStatus(mainButton2, true);
            ButtonStatus(mainButton3, true);
        }

        private void ScoreDownButton_Click(object sender, EventArgs e)
        {
            if (ScoreListView.Visible && ScoreListView.Items.Count > 0)
            {
                int index = ScoreListView.SelectedIndices[0];
                if (ScoreListView.SelectedIndices[0] < ScoreListView.Items.Count - 1)
                {
                    ScoreListView.Items[index].Selected = false;
                    ScoreListView.Items[index + 1].Selected = true;
                }
                else
                {
                    ScoreListView.Items[index].Selected = false;
                    ScoreListView.Items[0].Selected = true;
                }
            }
        }

        private void ScoreUpButton_Click(object sender, EventArgs e)
        {
            if (ScoreListView.Visible && ScoreListView.Items.Count > 0)
            {
                int index = ScoreListView.SelectedIndices[0];
                if (ScoreListView.SelectedIndices[0] > 0)
                {
                    ScoreListView.Items[index].Selected = false;
                    ScoreListView.Items[index - 1].Selected = true;
                }
                else
                {
                    ScoreListView.Items[index].Selected = false;
                    ScoreListView.Items[ScoreListView.Items.Count - 1].Selected = true;
                }
            }
        }

        private void ExitAcxButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EndRaceBackToMenuButton_Click(object sender, EventArgs e)
        {
            action = "menu";
            ButtonStatus(EndRaceBackToMenuButton, false);
            ButtonStatus(mainButton1, true);
            ButtonStatus(mainButton2, true);
            ButtonStatus(mainButton3, true);
            Map_pictureBox1.Image = null;
        }

        private void ExitDecButton_Click(object sender, EventArgs e)
        {
            action = "main";
            ButtonStatus(ExitAccButton, false);
            ButtonStatus(ExitDecButton, false);
            ButtonStatus(mainButton1, true);
            ButtonStatus(mainButton2, true);
            ButtonStatus(mainButton3, true);
        }

        private void mainButton1_Click(object sender, EventArgs e)
        {
            Race_preparation();

        }

        private void mainButton2_Click(object sender, EventArgs e)
        {
            action = "score";
            ButtonStatus(mainButton1, false);
            ButtonStatus(mainButton2, false);
            ButtonStatus(mainButton3, false);

            ScoreNickComboBox.Enabled = true;
            ScoreNickComboBox.Visible = true;
            ScoreSortCombobox.Enabled = true;
            ScoreSortCombobox.Visible = true;
            ScoreListView.Enabled = true;
            ScoreListView.Visible = true;
            ButtonStatus(ScoreUpButton, true);
            ButtonStatus(ScoreDownButton, true);
            ButtonStatus(ScoreDecButton, true);

            PrepareScores();
        }

        private void mainButton3_Click(object sender, EventArgs e)
        {
            action = "exit";
            ButtonStatus(mainButton1, false);
            ButtonStatus(mainButton2, false);
            ButtonStatus(mainButton3, false);
            ButtonStatus(ExitAccButton, true);
            ButtonStatus(ExitDecButton, true);
        }

        private void PausedAccButton_Click(object sender, EventArgs e)
        {
            ButtonStatus(PausedAccButton, false);
            ButtonStatus(PausedDecButton, false);

            action = "race";
            Time.Start();
            Car_Work.RunWorkerAsync();
        }

        private void PausedDecButton_Click(object sender, EventArgs e)
        {
            ButtonStatus(PausedAccButton, false);
            ButtonStatus(PausedDecButton, false);
            ButtonStatus(mainButton1, true);
            ButtonStatus(mainButton2, true);
            ButtonStatus(mainButton3, true);

            Map_pictureBox1.Image = null;
            action = "menu";
        } 
        #endregion

        private void OnScoreSaved(object source, EventArgs e)
        {
            end_race_save_status = "WYNIK ZOSTAL ZAPISANY";
            EndRaceBackToMenuButton.Enabled = true;
        }

        //## DELEGARY

        private void OnStartFinishReached(object source, EventArgs e)
        {
            if (!started)
            {
                started = true;
                Time.Start();
            }
            else
            {
                Race_summary();
            }
        }

        private void OnNicknamesImported(object source, ImportNicknamesEventArgs e)
        {
            foreach (var nick in e.Nicknames)
            {
                ScoreNickComboBox.Items.Add(nick);
            }
            scores_ie.Import_score("Brak");
        }

        private void OnScoresImported(object source, ImportScoresEventArgs e)
        {
            ScoreListView.Items.Clear();

            foreach (var item in e.Highscores)
            {
                ListViewItem record = new ListViewItem(item.Nick, 0);
                record.SubItems.Add(item.Czas);
                record.SubItems.Add(item.Wynik.ToString());
                record.SubItems.Add(item.Punkty_kontr.ToString());
                record.SubItems.Add(item.Data);
                ScoreListView.Items.AddRange(new ListViewItem[] { record });
            }
            ScoreListView.Select();
            ScoreListView.Items[0].Selected = true;
        }

        //## DODATKOWE METODY

        private void ScoreNickComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ScoreNickComboBox.SelectedIndex != -1 && ScoreSortCombobox.SelectedIndex != -1)
            {
                scores_ie.Import_score(ScoreNickComboBox.Text, ScoreSortCombobox.Text);
            }
            else if (ScoreSortCombobox.SelectedIndex == -1)
            {
                scores_ie.Import_score(ScoreNickComboBox.Text, "Brak");
            }
        }

        private void ScoreSortCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ScoreNickComboBox.SelectedIndex != -1 && ScoreSortCombobox.SelectedIndex != -1)
            {
                scores_ie.Import_score(ScoreNickComboBox.Text, ScoreSortCombobox.Text);
            }
            else if (ScoreSortCombobox.SelectedIndex != -1)
            {
                scores_ie.Import_score(ScoreSortCombobox.Text);
            }
        }

        private void PrepareScores()
        {
            ScoreNickComboBox.Items.Clear();
            ScoreSortCombobox.Items.Clear();
            ScoreNickComboBox.SelectedIndex = -1;
            ScoreSortCombobox.SelectedIndex = -1;
            ScoreNickComboBox.Text = string.Empty;
            ScoreSortCombobox.Text = string.Empty;
            ScoreListView.Columns.Clear();

            ScoreListView.Columns.Add(scores_ie.Get_column_names()[1], (ScoreListView.Width / 100) * 18);
            ScoreListView.Columns.Add(scores_ie.Get_column_names()[2], (ScoreListView.Width / 100) * 25);
            ScoreListView.Columns.Add(scores_ie.Get_column_names()[3], (ScoreListView.Width / 100) * 13);
            ScoreListView.Columns.Add(scores_ie.Get_column_names()[4], (ScoreListView.Width / 100) * 21);
            ScoreListView.Columns.Add(scores_ie.Get_column_names()[5], (ScoreListView.Width / 100) * 29);
            ScoreSortCombobox.Items.Add("Brak");
            for (int i = 1; i < scores_ie.Get_column_names().Count; i++)
            {
                ScoreSortCombobox.Items.Add(scores_ie.Get_column_names()[i]);
            }
            scores_ie.Import_nicknames();
        }

        private void Race_summary()
        {
            EndRaceBackToMenuButton.Visible = true;
            final_time = Time.Elapsed;
            if (race_status.Checkpoint > constructor.Checkpoints_count)
            {
                collected_checkpoints = constructor.Checkpoints_count;
            }
            else
            {
                collected_checkpoints = race_status.Checkpoint;
            }
            int Mili_time = (int)final_time.TotalSeconds;

            final_score = (collected_checkpoints * 100) / Mili_time;

            action = "ended";
            end_race_save_status = "TRWA ZAPISYWANIE WYNIKU";

            string[] save_data = new string[5];
            save_data[0] = nickname;
            save_data[1] = final_time.ToString();
            save_data[2] = final_score.ToString();
            save_data[3] = collected_checkpoints.ToString();
            save_data[4] = DateTime.Now.ToString();

            scores_ie.New_score(save_data);
        }

        private void ButtonStatus(MainButton button, bool value)
        {
            button.Visible = value;
            button.Enabled = value;
        }

        private void Race_preparation()
        {
            ButtonStatus(mainButton1, false);
            ButtonStatus(mainButton2, false);
            ButtonStatus(mainButton3, false);
            ButtonStatus(RacePrepAccButton, true);
            ButtonStatus(RacePrepDecButton, true);
            ButtonStatus(RacePrepAdd1Button, true);
            ButtonStatus(RacePrepAdd10Button, true);
            ButtonStatus(RacePrepRem1Button, true);
            ButtonStatus(RacePrepRem10Button, true);

            textBox1.Enabled = true;
            textBox1.Visible = true;
            action = "race_prep";
        }

        private void Prepare_race()
        {
            ButtonStatus(RacePrepAccButton, false);
            ButtonStatus(RacePrepDecButton, false);
            ButtonStatus(RacePrepAdd1Button, false);
            ButtonStatus(RacePrepAdd10Button, false);
            ButtonStatus(RacePrepRem1Button, false);
            ButtonStatus(RacePrepRem10Button, false);

            textBox1.Enabled = false;
            textBox1.Visible = false;

            MuscleCar = new Vehicle(main_map_Width, main_map_Height, 6000, 1000);
            MuscleCar.Set_position(4400, 5000);

            MuscleCar.CheckpointReached += race_status.OnCheckpointReached;
            MuscleCar.StartFinishReached += race_status.OnStartFinishReached;
            MuscleCar.CheckpointReached += draw_interface.OnCheckpointReached;
            MuscleCar.StartFinishReached += OnStartFinishReached;

            draw_interface.Get_form_size(this.Width, this.Height);

            constructor.Current_main_map = 1;
            constructor.Start_map();

            main_map_Height = constructor.Get_main_map().Height; main_map_Width = constructor.Get_main_map().Width;

            show_rect = new Rectangle(0, 0, panel2.Width, panel2.Height);
            croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);

            Map_pictureBox1.Image = croped_bmp;

            FPScounter.RunWorkerAsync();            

            for (int i = 0; i <= 4; i++)
            {
                start_squence_image[i] = Image.FromFile(@"Resources\Start_lights_" + i +".png");
            }

            action = "race";
            Start_sequenceWorker.RunWorkerAsync();
        }

        private void Fullscreen()
        {
            if (fullscreen)
            {
                FormBorderStyle = FormBorderStyle.FixedSingle;
                WindowState = FormWindowState.Normal;
                this.Size = new Size(resolutionX,resolutionY);
                this.CenterToScreen();
                fullscreen = false;
            }
            else
            {
                resolutionX = Size.Width;
                resolutionY = Size.Height;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                fullscreen = true;
            }
            draw_interface.Get_form_size(this.Width, this.Height);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            #region Pauza
            PausedAccButton.Size = new Size(Width / 10, Height / 10);
            PausedAccButton.Location = new Point((Width / 10) * 3 - PausedAccButton.Size.Width / 2, (Height / 10) * 7 - PausedAccButton.Size.Height / 2);

            PausedDecButton.Size = new Size(Width / 10, Height / 10);
            PausedDecButton.Location = new Point((Width / 10) * 7 - PausedDecButton.Size.Width / 2, (Height / 10) * 7 - PausedDecButton.Size.Height / 2); 
            #endregion

            #region Menu główne
            mainButton1.Size = new Size(Width / 3, Height / 6);
            mainButton1.Location = new Point(Width / 2 - mainButton1.Size.Width / 2, (Height / 6) * 2 - mainButton1.Size.Height / 2);

            mainButton2.Size = new Size(Width / 3, Height / 6);
            mainButton2.Location = new Point(Width / 2 - mainButton1.Size.Width / 2, (Height / 6) * 3 - mainButton1.Size.Height / 2);

            mainButton3.Size = new Size(Width / 3, Height / 6);
            mainButton3.Location = new Point(Width / 2 - mainButton1.Size.Width / 2, (Height / 6) * 4 - mainButton1.Size.Height / 2); 
            #endregion

            #region Przygotowanie Wyścigu

            textBox1.Location = new Point(Width / 2 - textBox1.Size.Width / 2, (Height / 8) * 3 - textBox1.Size.Height / 2);

            RacePrepAccButton.Size = new Size(Width / 10, Height / 10);
            RacePrepAccButton.Location = new Point(Width - 200 - RacePrepAccButton.Size.Width / 2, Height - 100 - RacePrepAccButton.Size.Height / 2);

            RacePrepDecButton.Size = new Size(Width / 10, Height / 10);
            RacePrepDecButton.Location = new Point(200 - RacePrepDecButton.Size.Width / 2, Height - 100 - RacePrepDecButton.Size.Height / 2);

            RacePrepAdd1Button.Size = new Size(Width / 10, Height / 10);
            RacePrepAdd1Button.Location = new Point((Width / 9) * 5 - RacePrepAdd1Button.Size.Width / 2, (Height / 8) * 6 - RacePrepAdd1Button.Size.Height / 2);

            RacePrepAdd10Button.Size = new Size(Width / 10, Height / 10);
            RacePrepAdd10Button.Location = new Point((Width / 9) * 6 - RacePrepAdd10Button.Size.Width / 2, (Height / 8) * 6 - RacePrepAdd10Button.Size.Height / 2);

            RacePrepRem1Button.Size = new Size(Width / 10, Height / 10);
            RacePrepRem1Button.Location = new Point((Width / 9) * 4 - RacePrepRem1Button.Size.Width / 2, (Height / 8) * 6 - RacePrepRem1Button.Size.Height / 2);

            RacePrepRem10Button.Size = new Size(Width / 10, Height / 10);
            RacePrepRem10Button.Location = new Point((Width / 9) * 3 - RacePrepRem10Button.Size.Width / 2, (Height / 8) * 6 - RacePrepRem10Button.Size.Height / 2);
            #endregion

            #region Score

            ScoreNickComboBox.Width = Width / 6;           
            ScoreNickComboBox.Location = new Point((Width / 10) * 1 - ScoreDecButton.Size.Width / 2, (Height / 10) * 3 - ScoreDecButton.Size.Height / 2);
            ScoreSortCombobox.Width = Width / 6;
            ScoreSortCombobox.Location = new Point((Width / 10) * 1 - ScoreDecButton.Size.Width / 2, (Height / 10) * 7 - ScoreDecButton.Size.Height / 2);

            ScoreListView.Size = new Size((Width / 10) * 6, (Height / 10) * 7);
            ScoreListView.Location = new Point((Width / 40) * 21 - ScoreListView.Size.Width / 2, (Height / 10) * 5 - ScoreListView.Size.Height / 2);

            ScoreDecButton.Size = new Size(Width / 10, Height / 10);
            ScoreDecButton.Location = new Point((Width / 10) * 9 - ScoreDecButton.Size.Width / 2, (Height / 10) * 9 - ScoreDecButton.Size.Height / 2);

            ScoreUpButton.Size = new Size(Width / 10, Height / 10);
            ScoreUpButton.Location = new Point((Width / 10) * 9 - ScoreUpButton.Size.Width / 2, (Height / 10) * 2 - ScoreUpButton.Size.Height / 2);

            ScoreDownButton.Size = new Size(Width / 10, Height / 10);
            ScoreDownButton.Location = new Point((Width / 10) * 9 - ScoreDownButton.Size.Width / 2, (Height / 10) * 7 - ScoreDownButton.Size.Height / 2);
            #endregion

            #region Wyjscie
            ExitAccButton.Size = new Size(Width / 10, Height / 10);
            ExitAccButton.Location = new Point((Width / 10) * 1 - ExitAccButton.Size.Width / 2, (Height / 10) * 9 - ExitAccButton.Size.Height / 2);

            ExitDecButton.Size = new Size(Width / 10, Height / 10);
            ExitDecButton.Location = new Point((Width / 10) * 9 - ExitDecButton.Size.Width / 2, (Height / 10) * 9 - ExitDecButton.Size.Height / 2);

            EndRaceBackToMenuButton.Size = new Size(Width / 7, Height / 8);
            EndRaceBackToMenuButton.Location = new Point((Width / 10) * 5 - EndRaceBackToMenuButton.Size.Width / 2, (Height / 10) * 8 - EndRaceBackToMenuButton.Size.Height / 2);
            #endregion
        }

        //## RYSOWANIE MENU i MAPY

        private void Map_pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            switch (action)
            {
                case "main":
                    {                        
                        string text = "RACER";
                        SizeF tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(main_textcolor), new Point(Width / 2 - (int)tempsize.Width / 2, Height / 10 - (int)tempsize.Height / 2));
                        break;
                    }
                case "race_prep":
                    {
                        string text = "NOWY WYSCIG";
                        SizeF tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(main_textcolor), new Point(Width / 2 - (int)tempsize.Width / 2, Height / 10 - (int)tempsize.Height / 2));
                        text = "PODAJ SWOJ NICK";
                        tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(main_textcolor), new Point(Width / 2 - (int)tempsize.Width / 2, (Height / 10) * 2 - (int)tempsize.Height / 2));
                        text = "LICZBA PUNKTOW KONTROLNYCH";
                        tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(main_textcolor), new Point(Width / 2 - (int)tempsize.Width / 2, (Height / 10) * 5 - (int)tempsize.Height / 2));


                        tempsize = e.Graphics.MeasureString(race_lenght.ToString(), new Font("Agency FB", 80));
                        e.Graphics.DrawString(race_lenght.ToString(), new Font("Agency FB", 80), new SolidBrush(main_textcolor), new Point(Width / 2 - (int)tempsize.Width / 2, (Height / 10) * 6 - (int)tempsize.Height / 2));
                        break;
                    }
                case "score":
                    {
                        string text = "WYNIKI";
                        SizeF tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(main_textcolor), new Point(Width / 2 - (int)tempsize.Width / 2, Height / 10 - (int)tempsize.Height / 2));
                        text = "WYBIERZ";
                        tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(main_textcolor), new Point((Width / 10) * 1 - (int)tempsize.Width / 2, (Height / 10) * 2 - (int)tempsize.Height / 2));
                        text = "SORTUJ";
                        tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(main_textcolor), new Point((Width / 10) * 1 - (int)tempsize.Width / 2, (Height / 10) * 6 - (int)tempsize.Height / 2));
                        break;
                    }
                case "exit":
                    {
                        string text = "CZY NA PEWNO CHCESZ WYJSC?";
                        SizeF tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(main_textcolor), new Point(Width / 2 - (int)tempsize.Width / 2, Height / 2 - (int)tempsize.Height / 2));
                        break;
                    }
                case "ended":
                    {
                        e.Graphics.FillRectangle(Brushes.OrangeRed, Width / 10, Height / 10, (Width / 10) * 8, (Height / 10) * 8);
                        e.Graphics.DrawRectangle(new Pen(Color.Black, 10), Width / 10, Height / 10, (Width / 10) * 8, (Height / 10) * 8);

                        string text = "WYSCIG UKONCZONY!";
                        SizeF tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(Color.Black), new Point(Width / 2 - (int)tempsize.Width / 2, (Height / 10) * 2- (int)tempsize.Height / 2));
                        text = "CZAS:" + final_time.ToString();
                        tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(Color.Black), new Point((Width / 10) * 5 - (int)tempsize.Width / 2, (Height / 10) * 4 - (int)tempsize.Height / 2));
                        text = "PUNKTY KONTROLNE:" + collected_checkpoints.ToString();
                        tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(Color.Black), new Point((Width / 10) * 5 - (int)tempsize.Width / 2, (Height / 10) * 5 - (int)tempsize.Height / 2));
                        text = "WYNIK:" + final_score.ToString();
                        tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(Color.Black), new Point((Width / 10) * 5 - (int)tempsize.Width / 2, (Height / 10) * 6 - (int)tempsize.Height / 2));
                        text = end_race_save_status;
                        tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), new SolidBrush(Color.Black), new Point((Width / 10) * 5- (int)tempsize.Width / 2, (Height / 10) * 7 - (int)tempsize.Height / 2));
                        break;
                    }
                case "paused":
                    {
                        e.Graphics.FillRectangle(Brushes.OrangeRed, Width / 10, Height / 10, (Width / 10) * 8, (Height / 10) * 8);
                        e.Graphics.DrawRectangle(new Pen(Color.Black,10), Width / 10, Height / 10, (Width / 10) * 8, (Height / 10) * 8);

                        string text = "PAUZA";
                        SizeF tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), Brushes.Black, new Point(Width / 2 - (int)tempsize.Width / 2, (Height / 10) * 3 - (int)tempsize.Height / 2));
                        text = "CZY CHCESZ KONTYNUOWAC?";
                        tempsize = e.Graphics.MeasureString(text, new Font("Agency FB", 50));
                        e.Graphics.DrawString(text, new Font("Agency FB", 50), Brushes.Black, new Point(Width / 2 - (int)tempsize.Width / 2, (Height / 10) * 5 - (int)tempsize.Height / 2));


                        Time.Stop();

                        #region Render Mapy
                        //Środek Mapy
                        if ((player_point.X + panel2.Width / 2 < main_map_Width && player_point.Y + panel2.Height / 2 < main_map_Height) && (player_point.X > panel2.Width / 2 && player_point.Y > panel2.Height / 2))
                        {
                            croped_bmp.Dispose();
                            show_rect = new Rectangle(player_point.X - panel2.Width / 2, player_point.Y - panel2.Height / 2, panel2.Width, panel2.Height);

                            croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                            Map_pictureBox1.Image = croped_bmp;
                        }
                        else
                        {
                            //Gdy potrze do górnej krawędzi
                            if (player_point.Y < panel2.Height / 2 && player_point.X > panel2.Width / 2 && player_point.X + panel2.Width / 2 < main_map_Width)
                            {
                                croped_bmp.Dispose();
                                show_rect = new Rectangle(player_point.X - panel2.Width / 2, 0, panel2.Width, panel2.Height);

                                croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                                Map_pictureBox1.Image = croped_bmp;
                            }
                            //Gdy dotrze do lewego-górnego rogu
                            if (player_point.X < panel2.Width / 2 && player_point.Y < panel2.Height / 2)
                            {
                                croped_bmp.Dispose();
                                show_rect = new Rectangle(0, 0, panel2.Width, panel2.Height);

                                croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                                Map_pictureBox1.Image = croped_bmp;
                            }
                            //Gdy dotrze do Lewej krawędzi
                            if (player_point.X < panel2.Width / 2 && player_point.Y > panel2.Height / 2 && player_point.Y + panel2.Height / 2 < main_map_Height)
                            {
                                croped_bmp.Dispose();
                                show_rect = new Rectangle(0, player_point.Y - panel2.Height / 2, panel2.Width, panel2.Height);

                                croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                                Map_pictureBox1.Image = croped_bmp;
                            }
                            //Gdy dotrze do lewego-dolnego rogu
                            if (player_point.X < panel2.Width / 2 && player_point.Y + panel2.Height / 2 > main_map_Height)
                            {
                                croped_bmp.Dispose();
                                show_rect = new Rectangle(0, main_map_Height - panel2.Height, panel2.Width, panel2.Height);

                                croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                                Map_pictureBox1.Image = croped_bmp;
                            }
                            // Gdy dotrze do dolnej krawędzi
                            if (player_point.Y + panel2.Height / 2 > main_map_Height && player_point.X > panel2.Width / 2 && player_point.X + panel2.Width / 2 < main_map_Width)
                            {
                                croped_bmp.Dispose();
                                show_rect = new Rectangle(player_point.X - panel2.Width / 2, main_map_Height - panel2.Height, panel2.Width, panel2.Height);

                                croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                                Map_pictureBox1.Image = croped_bmp;
                            }
                            //Gdy dotrze do prawego-dolnego rogu
                            if (player_point.X + panel2.Width / 2 > main_map_Width && player_point.Y + panel2.Height / 2 > main_map_Height)
                            {
                                croped_bmp.Dispose();
                                show_rect = new Rectangle(main_map_Width - panel2.Width, main_map_Height - panel2.Height, panel2.Width, panel2.Height);

                                croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                                Map_pictureBox1.Image = croped_bmp;
                            }
                            //Gdy dotrze do prawej krawędzi
                            if (player_point.X + panel2.Width / 2 > main_map_Width && player_point.Y > panel2.Height / 2 && player_point.Y + panel2.Height / 2 < main_map_Height)
                            {
                                croped_bmp.Dispose();
                                show_rect = new Rectangle(main_map_Width - panel2.Width, player_point.Y - panel2.Height / 2, panel2.Width, panel2.Height);

                                croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                                Map_pictureBox1.Image = croped_bmp;
                            }
                            //Gdy dotrze do prawego-górnego rogu
                            if (player_point.X + panel2.Width / 2 > main_map_Width && player_point.Y < panel2.Height / 2)
                            {
                                croped_bmp.Dispose();
                                show_rect = new Rectangle(main_map_Width - panel2.Width, 0, panel2.Width, panel2.Height);

                                croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                                Map_pictureBox1.Image = croped_bmp;
                            }
                        }
                        #endregion
                        break;
                    }
                case "race":
                    {                        
                        if (!start_sequence)
                        {
                            e.Graphics.DrawImage(start_squence_image[start_squence_image_id], new Point(Width / 2 - start_squence_image[start_squence_image_id].Width / 2, Height / 4 - start_squence_image[start_squence_image_id].Height / 2));
                            SizeF tempsize = e.Graphics.MeasureString(start_count, new Font("Agency FB", 50));
                            e.Graphics.DrawString(start_count, new Font("Agency FB", 50), new SolidBrush(main_textcolor), new Point(Width / 2 - (int)tempsize.Width / 2, Height / 3 - (int)tempsize.Height / 2));

                            show_rect = new Rectangle(4400 - panel2.Width / 2, 5000 - panel2.Height / 2, panel2.Width, panel2.Height);
                            croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                            Map_pictureBox1.Image = croped_bmp;
                            MuscleCar.Draw_car(e.Graphics, panel2.Width / 2, panel2.Height / 2, constructor.Get_main_map());
                        }
                        else
                        {
                            player_point.X = Convert.ToInt32(MuscleCar.X);
                            player_point.Y = Convert.ToInt32(MuscleCar.Y);

                            if (player_point.Y < 3000 && !Map_loader.IsBusy && constructor.Last_map_end == "up" && !race_end)
                            {
                                direction = "up";
                                Map_loader.RunWorkerAsync();
                            }
                            if (player_point.X < 3000 && !Map_loader.IsBusy && constructor.Last_map_end == "left" && !race_end)
                            {
                                direction = "left";
                                Map_loader.RunWorkerAsync();
                            }
                            if (player_point.X > 6000 && !Map_loader.IsBusy && constructor.Last_map_end == "right" && !race_end)
                            {
                                direction = "right";
                                Map_loader.RunWorkerAsync();

                            }
                            fps_count++;

                            draw_interface.Draw_Nickname(e.Graphics, nickname);
                            //draw_interface.Draw_Debug(e.Graphics, debug_list);
                            draw_interface.Draw_FPS(e.Graphics, fps);
                            draw_interface.Draw_Minimap(e.Graphics,player_point.X, player_point.Y, constructor.Get_minimap());
                            draw_interface.Draw_RaceStatus(e.Graphics, time, race_status.Checkpoint, constructor.Checkpoints_count);
                            draw_interface.Draw_Dashboard(e.Graphics, (int)MuscleCar.V, (int)MuscleCar.F1, 6000);

                            if (fullscreen)
                            {
                                Thread.Sleep(2);
                            }
                            else
                            {
                                Thread.Sleep(12);
                            }

                            //## Render Mapy ##

                            #region Render Mapy
                            //Środek Mapy
                            if ((player_point.X + panel2.Width / 2 < main_map_Width && player_point.Y + panel2.Height / 2 < main_map_Height) && (player_point.X > panel2.Width / 2 && player_point.Y > panel2.Height / 2))
                            {
                                croped_bmp.Dispose();
                                show_rect = new Rectangle(player_point.X - panel2.Width / 2, player_point.Y - panel2.Height / 2, panel2.Width, panel2.Height);

                                croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);

                                Map_pictureBox1.Image = croped_bmp;

                                MuscleCar.Draw_car(e.Graphics, panel2.Width / 2, panel2.Height / 2, constructor.Get_main_map());
                            }
                            else
                            {
                                //Gdy potrze do górnej krawędzi
                                if (player_point.Y < panel2.Height / 2 && player_point.X > panel2.Width / 2 && player_point.X + panel2.Width / 2 < main_map_Width)
                                {
                                    croped_bmp.Dispose();
                                    show_rect = new Rectangle(player_point.X - panel2.Width / 2, 0, panel2.Width, panel2.Height);

                                    croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                                    Map_pictureBox1.Image = croped_bmp;

                                    MuscleCar.Draw_car(e.Graphics, panel2.Width / 2, player_point.Y, constructor.Get_main_map());
                                }
                                //Gdy dotrze do lewego-górnego rogu
                                if (player_point.X < panel2.Width / 2 && player_point.Y < panel2.Height / 2)
                                {
                                    croped_bmp.Dispose();
                                    show_rect = new Rectangle(0, 0, panel2.Width, panel2.Height);

                                    croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);

                                    Map_pictureBox1.Image = croped_bmp;

                                    MuscleCar.Draw_car(e.Graphics, player_point.X, player_point.Y, constructor.Get_main_map());
                                }
                                //Gdy dotrze do Lewej krawędzi
                                if (player_point.X < panel2.Width / 2 && player_point.Y > panel2.Height / 2 && player_point.Y + panel2.Height / 2 < main_map_Height)
                                {
                                    croped_bmp.Dispose();
                                    show_rect = new Rectangle(0, player_point.Y - panel2.Height / 2, panel2.Width, panel2.Height);

                                    croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);

                                    Map_pictureBox1.Image = croped_bmp;

                                    MuscleCar.Draw_car(e.Graphics, player_point.X, panel2.Height / 2, constructor.Get_main_map());
                                }
                                //Gdy dotrze do lewego-dolnego rogu
                                if (player_point.X < panel2.Width / 2 && player_point.Y + panel2.Height / 2 > main_map_Height)
                                {
                                    croped_bmp.Dispose();
                                    show_rect = new Rectangle(0, main_map_Height - panel2.Height, panel2.Width, panel2.Height);

                                    croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);

                                    Map_pictureBox1.Image = croped_bmp;

                                    e.Graphics.TranslateTransform(0, panel2.Height - main_map_Height);
                                    MuscleCar.Draw_car(e.Graphics, player_point.X, player_point.Y, constructor.Get_main_map());
                                }
                                // Gdy dotrze do dolnej krawędzi
                                if (player_point.Y + panel2.Height / 2 > main_map_Height && player_point.X > panel2.Width / 2 && player_point.X + panel2.Width / 2 < main_map_Width)
                                {
                                    croped_bmp.Dispose();
                                    show_rect = new Rectangle(player_point.X - panel2.Width / 2, main_map_Height - panel2.Height, panel2.Width, panel2.Height);

                                    croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);

                                    Map_pictureBox1.Image = croped_bmp;

                                    e.Graphics.TranslateTransform(0, panel2.Height - main_map_Height);
                                    MuscleCar.Draw_car(e.Graphics, panel2.Width / 2, player_point.Y, constructor.Get_main_map());
                                }
                                //Gdy dotrze do prawego-dolnego rogu
                                if (player_point.X + panel2.Width / 2 > main_map_Width && player_point.Y + panel2.Height / 2 > main_map_Height)
                                {
                                    croped_bmp.Dispose();
                                    show_rect = new Rectangle(main_map_Width - panel2.Width, main_map_Height - panel2.Height, panel2.Width, panel2.Height);

                                    croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);
                                    Map_pictureBox1.Image = croped_bmp;

                                    e.Graphics.TranslateTransform(panel2.Width - main_map_Width, panel2.Height - main_map_Height);
                                    MuscleCar.Draw_car(e.Graphics, player_point.X, player_point.Y, constructor.Get_main_map());
                                }
                                //Gdy dotrze do prawej krawędzi
                                if (player_point.X + panel2.Width / 2 > main_map_Width && player_point.Y > panel2.Height / 2 && player_point.Y + panel2.Height / 2 < main_map_Height)
                                {
                                    croped_bmp.Dispose();
                                    show_rect = new Rectangle(main_map_Width - panel2.Width, player_point.Y - panel2.Height / 2, panel2.Width, panel2.Height);

                                    croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);

                                    Map_pictureBox1.Image = croped_bmp;

                                    e.Graphics.TranslateTransform(panel2.Width - main_map_Width, 0);
                                    MuscleCar.Draw_car(e.Graphics, player_point.X, panel2.Height / 2, constructor.Get_main_map());
                                }
                                //Gdy dotrze do prawego-górnego rogu
                                if (player_point.X + panel2.Width / 2 > main_map_Width && player_point.Y < panel2.Height / 2)
                                {
                                    croped_bmp.Dispose();
                                    show_rect = new Rectangle(main_map_Width - panel2.Width, 0, panel2.Width, panel2.Height);

                                    croped_bmp = constructor.Get_main_map().Clone(show_rect, constructor.Get_main_map().PixelFormat);

                                    Map_pictureBox1.Image = croped_bmp;

                                    e.Graphics.TranslateTransform(panel2.Width - main_map_Width, 0);
                                    MuscleCar.Draw_car(e.Graphics, player_point.X, player_point.Y, constructor.Get_main_map());
                                }
                            } 
                            #endregion
                        }
                        break;
                    }
            }
        }
    }
}
