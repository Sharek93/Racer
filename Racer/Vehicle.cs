using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Racer
{
    class Vehicle
    {
        //## KONSTRUKTOR

        Wheel_Ground_Checker WGC = new Wheel_Ground_Checker();

        //DEFINIOWANIE DELEGATÓW O EVENTÓW

        public delegate void CheckpointCheckerHandler(object source, EventArgs Args);
        public delegate void StartFinishHandler(object source, EventArgs Args);

        public event CheckpointCheckerHandler CheckpointReached;
        public event StartFinishHandler StartFinishReached;

        //## ZMIENNE

        Point start_drift_point;
        double a , F, Fmax, m, x, y, v, direction = 0, drift = 0, total_drift = 0;
        Image model;
        int ster = 0, wait_reverse = 0;
        bool turn, breaks, reverse, accelerate;
        bool on_checkpoint = false, on_startfinish = false;
        int mapWidth, mapHeight;
        string ground_type;

        Point start, end;

        double dt, ground_modifier;

        //## KONSTRUKTOR

        public Vehicle(int mapwidth, int mapheight, int power, int mass)
        {
            Fmax = power;
            m = mass;
            mapHeight = mapheight;
            mapWidth = mapwidth;
            model = Image.FromFile("muscle_top.bmp");
            model.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Bitmap bmp = (Bitmap)model;
            bmp.MakeTransparent(Color.White);
            model = bmp;
        }

        //## WŁAŚCIWOŚCI

        #region Właściwości
        public void Set_position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public double V
        {
            get
            {
                return v;
            }
        }

        public double Drift
        {
            get
            {
                return drift;
            }
        }

        public double Direction
        {
            get
            {
                return direction;
            }
        }

        public double A
        {
            get
            {
                return a;
            }
        }

        public double F1
        {
            get
            {
                return F;
            }
        }

        public double Dt
        {
            set
            {
                dt = value;
            }
        }

        public string Ground_type
        {
            get
            {
                return ground_type;
            }
        }
        #endregion

        //## RYSOWANIE POJAZDU

        public void Draw_car(Graphics g, int trans_X, int trans_Y, Bitmap map)
        {
            WGC.Get_color(map.GetPixel((int)x, (int)y));
            ground_checker();

            var m = g.Transform;
            g.SmoothingMode = SmoothingMode.AntiAlias;            
            g.TranslateTransform((float)trans_X, (float)trans_Y);
            g.RotateTransform((float)direction + 90 + ster * (int)drift);
            g.DrawImage(model, -20, -40, 40, 80);

            var l = g.Transform;
            g.TranslateTransform(13, 37);
            if (breaks == true)
            {
                g.FillRectangle(Brushes.Red, 0, 0, 7, 3);
            }
            if (reverse == true)
            {
                g.FillRectangle(Brushes.White, -7, 0, 7, 3);
            }          
            if (breaks == true)
            {
                g.FillRectangle(Brushes.Red, -33, 0, 7, 3);
            }
            if (reverse == true)
            {
                g.FillRectangle(Brushes.White, -26, 0, 7, 3);
            }

            g.Transform = l;


            g = Graphics.FromImage(map);
            g.TranslateTransform((float)x, (float)y);
            g.RotateTransform((float)direction + 90 + ster * (int)drift);

            //Color pix_col_front_left = map.GetPixel((int)x-18, (int)y-20);
            //Color pix_col_front_right = map.GetPixel(13, -20);
            //Color pix_col_rear_left = map.GetPixel(-18, 20);
            //Color pix_col_rear_right = map.GetPixel(13, 20);

            //WGC.Get_colors(pix_col_front_left, pix_col_front_right, pix_col_rear_left, pix_col_rear_right);


            if (drift >= 20 || a >= Fmax * 0.22)
            {
                //if (WGC.Rear_left_ground == "asphalt" || WGC.Rear_left_ground == "line" || WGC.Rear_left_ground == "checkpoint")
                //{
                //    g.FillEllipse(Brushes.Black, -18, 20, 6, 6);
                //}
                //else if (WGC.Rear_left_ground == "ground")
                //{
                //    g.FillEllipse(Brushes.Brown, -18, 20, 6, 6);
                //}
                if (ground_type == "asphalt" || ground_type == "other")
                {
                    g.FillEllipse(Brushes.Black, -18, 20, 6, 6);
                    g.FillEllipse(Brushes.Black, 13, 20, 6, 6);
                }               
                if (start_drift_point.IsEmpty)
                {
                    start.X = -18;
                    start.Y = 20;
                }
                else
                {
                    end.X = -18;
                    end.Y = 20;

                    g.DrawLine(new Pen(Color.Red, 4f), start, end);

                    //Point end_drift_point = new Point((int)x, (int)y);
                    //var lr = g.Transform;
                    //g.TranslateTransform(-16, 15);
                    //g.RotateTransform((float)direction + 90);
                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    ////g.TransformPoints(CoordinateSpace.World, CoordinateSpace.World, new Point[] { start_drift_point });
                    //g.DrawLine(new Pen(Color.Black, (float)3), 0, 0, (start_drift_point.X - (int)x), start_drift_point.Y - (int)y);

                    //g.Transform = lr;
                    //var rr = g.Transform;
                    //g.TranslateTransform(16, 15);
                    //g.RotateTransform((float)direction + 90);
                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    ////g.TransformPoints(CoordinateSpace.World, CoordinateSpace.World, new Point[] { start_drift_point });
                    //g.DrawLine(new Pen(Color.Black, (float)3), 0, 0, (start_drift_point.X - (int)x), start_drift_point.Y - (int)y);
                    //g.Transform = rr;


                    //g.FillEllipse(Brushes.Black, -18, 20, 6, 6);
                    //g.FillEllipse(Brushes.Black, 13, 20, 6, 6);

                    start = end;
                }
            }
            if (breaks && v > 500 && ground_type == "asphalt")
            {
                g.FillEllipse(Brushes.Black, -18, -20, 6, 6);
                g.FillEllipse(Brushes.Black, 13, -20, 6, 6);
                g.FillEllipse(Brushes.Black, -18, 20, 6, 6);
                g.FillEllipse(Brushes.Black, 13, 20, 6, 6);
            }
            else if (ground_type == "grass")
            {
                g.FillEllipse(Brushes.Brown, -18, -20, 6, 6);
                g.FillEllipse(Brushes.Brown, 13, -20, 6, 6);
                g.FillEllipse(Brushes.Brown, -18, 20, 6, 6);
                g.FillEllipse(Brushes.Brown, 13, 20, 6, 6);
            }
            //else
            //{
            //    start_drift_point = Point.Empty;
            //}

            g.Transform = m;


            turn = false; 
            breaks = false;
        }

        //## SPRAWDZANIA PODŁOŻA

        private void ground_checker()
        {
            ground_type = WGC.Center_ground;

            if (ground_type == "asphalt" || ground_type == "other")
            {
                ground_modifier = 1.0;
            }
            else if (ground_type == "grass")
            {
                ground_modifier = 0.3;
            }

            if (ground_type == "line" && !on_startfinish)
            {
                on_startfinish = true;
            }
            else if (ground_type == "checkpoint" && !on_checkpoint)
            {
                on_checkpoint = true;
            }

            if (on_startfinish)
            {
                if (ground_type == "grass" || ground_type == "asphalt")
                {
                    OnStartFinishReached();
                    on_startfinish = false;
                }
            }
            else if (on_checkpoint)
            {
                if (ground_type == "grass" || ground_type == "asphalt")
                {
                    OnCheckpointReached();
                    on_checkpoint = false;
                }
            }           
        }

        //## DELEGATY

        protected virtual void OnStartFinishReached()
        {
            if (StartFinishReached != null)
            {
                StartFinishReached(this, EventArgs.Empty);
            }
        }

        protected virtual void OnCheckpointReached()
        {
            if (CheckpointReached != null)
            {
                CheckpointReached(this, EventArgs.Empty);
            }
        }

        //OBLICZANIE PREDKOŚCI I WPÓŁRZĘDNYCH

        public void Work(double dt)
        {

            double dx, dy;
            if (direction > 360)
            {
                direction = 0;
            }
            if (direction < 0)
            {
                direction = 360;
            }

            if (drift > 45)
            {
                if (F - drift * 50 * dt > 0)
                {
                    F -= drift * 50 * dt;
                }
                else
                {
                    F = 0;
                }
            }

            if (accelerate == true)
            {
                if (F < 500)
                {
                    F = 500;
                }
                else
                { 
                    if (F < Fmax)
                    {
                        F += (Fmax - F + 1000) / 6 * dt * ground_modifier;
                    }
                    else
                    {
                        F = Fmax;
                    } 
                }
            }
            else
            {
                if (F - 2500 * dt > 0)
                {
                    F -= 2500 * dt;
                }
                else
                {
                    F = 0;
                }
            }

            if (breaks)
            {
                if (F - 5000 * dt > 0)
                {
                    F -= 5000 * dt;
                }
                else
                {
                    F = 0;
                }                
            }

            a = F * ground_modifier + 0.7 * 3 * v * v * Math.Sign(-v) / m;
            
            v += a * dt;

            dx = v * dt * Math.Cos(direction * Math.PI / 180);
            dy = v * dt * Math.Sin(direction * Math.PI / 180);

            if (x + dx >= 8990)
            {
                x = 8990;
            }
            else if (x + dx <= 10)
            {
                x = 10;
            }
            else
            {
                x = x + dx;
            }

            if (y + dy >= 8990)
            {
                y = 8990;
            }
            else if (y + dy <= 10)
            {
                y = 10;
            }
            else
            {
                y = y + dy;
            }
            

            if (drift > 0 && turn == false)
            {
                EndDrift();
            }
        }

        //## DODATKOWE METODY OBSŁUGUJĄCE SAMOCHÓD: SKRĘCANIE, DRIFT, HAMOWANIE ITP.

        public void TurnRight()
        {
            if (ster == -1 && drift > 0)
            {
                drift -= 3;
            }
            else
            {
                if (v > 0)
                {
                    if (v < 200)
                    {
                        direction += v / 50;
                    }
                    else
                    {
                        if (breaks && v > 500)
                        {
                            direction += 0.5;
                            v -= drift / 5;
                        }
                        else
                        {
                            direction += 2;
                        }
                    }
                    turn = true;
                }
                else
                {
                    if (v > -200)
                    {
                        direction += v / 50;
                    }
                    else
                    {
                        direction -= 2;
                    }
                    turn = true;
                }
                ster = 1;

                //Drift

                if (v > 200)
                {
                    if (drift < 90)
                    {
                        Add_drifting(1);
                        total_drift = drift;
                    }
                    if (drift > 10)
                    {
                        v -= drift / 5;
                    }
                    if (drift < 0 && ster == -1)
                    {

                    }
                }
            }
        }
            
        public void TurnLeft()
        {
            if (ster == 1 && drift > 0)
            {
                drift -= 3;
               
            }
            else
            {
                if (v > 0)
                {
                    if (v < 200)
                    {
                        direction -= v / 50;
                    }
                    else
                    {
                        if (breaks && v > 500)
                        {
                            direction -= 0.5;
                            v -= drift / 4;
                        }
                        else
                        {
                            direction -= 2;
                        }
                    }
                    turn = true;
                }
                else
                {
                    if (v > -200)
                    {
                        direction -= v / 50;
                    }
                    else
                    {
                        direction += 2;
                    }
                    turn = true;
                }

                ster = -1;

                //Drift

                if (v > 200)
                {
                    if (drift < 90)
                    {
                        Add_drifting(1);
                        total_drift = drift;
                    }
                    if (drift > 10)
                    {
                        v -= drift / 4;
                    }
                }
            }           

            
        }

        public void Accelerate()
        {
            accelerate = true;
            wait_reverse = 0;            
            if (v < 0)
            {
                breaks = true;
                reverse = true;
            }
            else
            {
                breaks = false;
                reverse = false;
            }
        }

        public void Neutral()
        {
            accelerate = false;
        }

        public void Breaks()
        {
            if (v > 0)
            {
                v -= 8;
                breaks = true;
            }
            else if (wait_reverse == 0)
            {
                wait_reverse++;
                v = 0;
            }
            else
            {
                breaks = false;
                wait_reverse++;
                if (wait_reverse >= 20)
                {
                    if (v > -500)
                    {
                        v -= 4;
                    }
                    reverse = true;
                }
            }                  
        }

        private void Add_drifting(int value)
        {
            if (drift + value < 90)
            {
                if (ground_type == "grass")
                {
                    drift += value*2;
                }
                else
                {
                    drift += value;
                }             
            }
            else
            {
                drift = 90;
            }
        }

        public void EndDrift()
        {
            float step = (float)total_drift / 80;
            if (drift - step > 0)
            {
                direction += ster * step;
                drift -= step;
            }
            else
            {
                drift = 0;
                total_drift = 0;
            }
        }

        public void Handbreak()
        {
            if (v - 15 > 0)
            {
                if (drift + 3 < 90 && turn == true)
                {
                    drift += 3;
                }
                v -= 15; 
            }
            
        }
    }
}
