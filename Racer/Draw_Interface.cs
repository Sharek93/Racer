using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racer
{
    class Draw_Interface
    {
        private int width, height;
        bool checkpoint_reached = false;
        string checkpoint_time;

        public void Get_form_size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Draw_Debug(Graphics d, string[] data)
        {
            int x = 10, y = 50;
            for (int i = 0; i < data.Length; i++)
            {
                Point point = new Point(x, y);
                d.DrawString(data[i], new Font("Arial", 10), Brushes.Black, point);
                y += 12;                
            }
        }

        public void Draw_FPS(Graphics f, int fps)
        {
            f.DrawString(fps.ToString(), new Font("Arial", 20), Brushes.Black, new Point(10, 10));
        }

        public void Draw_Nickname(Graphics n, string nickname)
        {
            SizeF tempsize = n.MeasureString(nickname, new Font("Agency FB", 50));
            n.DrawString(nickname, new Font("Arial", 50), Brushes.Black, new Point(width / 2 - (int)tempsize.Width / 2, 10));
        }

        public void Draw_Minimap(Graphics m, int PlayerX, int PlayerY, Bitmap minimap)
        {
            int x = 10, y = height - 350;
            try
            {              
                m.DrawImage(minimap, x, y, 300, 300);
                m.FillEllipse(Brushes.Red, x + (PlayerX / 30) - 5, y + (PlayerY / 30) - 5, 10, 10);
            }
            catch (Exception){ }
            
        }

        public void Draw_Dashboard(Graphics b, int speed, int trotle, int trotlemax)
        {
            int x = width - 300, y = height - 300;
            b.DrawArc(new Pen(Color.OrangeRed, 7), new Rectangle(x, y, 300, 300), 130, 200);
            b.DrawArc(new Pen(Color.Black, 11), new Rectangle(x, y, 300, 300), 130, 10);
            b.DrawArc(new Pen(Color.Black, 11), new Rectangle(x, y, 300, 300), 160, 10);
            b.DrawArc(new Pen(Color.Black, 11), new Rectangle(x, y, 300, 300), 190, 10);
            b.DrawArc(new Pen(Color.Black, 11), new Rectangle(x, y, 300, 300), 220, 10);
            b.DrawArc(new Pen(Color.Black, 11), new Rectangle(x, y, 300, 300), 250, 10);
            b.DrawArc(new Pen(Color.Red, 11), new Rectangle(x, y, 300, 300), 280, 50);
            b.FillEllipse(Brushes.OrangeRed, x + 130, y + 130, 40, 40);

            int gears = trotlemax / 1200;
            string gear = ((trotle / 1200) + 1).ToString();
            int showntrotle = trotle % 1200;
            if (trotle < 200)
            {                
                showntrotle = 200;
            }
            else if (speed == 0)
            {
                gear = "N";
                showntrotle = 200;
            }
            if (speed < 0)
            {
                gear = "R";
                showntrotle = 200;
            }

            int steps = trotlemax / 200;
            int arc = 40 + showntrotle / steps * gears;

            b.DrawString((speed/10).ToString(), new Font("IrisUPC", 80), Brushes.OrangeRed, new Point(x-140, y+170));
            b.DrawString(gear, new Font("IrisUPC", 80), Brushes.Black, new Point(x + 200, y + 170));

            Matrix matrix = new Matrix();
            var tra = b.Transform;
            
            matrix.RotateAt(arc, new PointF(x + 150, y + 150));
            b.Transform = matrix;
            b.FillRectangle(Brushes.Black, x + 145, y + 145, 10, 120);

            b.Transform = tra;
        }

        public void Draw_RaceStatus(Graphics r, string time, int checkpoint, int total_checkpoints)
        {
            int x = width - 200, y = 10;
            r.DrawString(time, new Font("Arial", 24), Brushes.Black, new Point(x, y));

            y += 40;
            r.DrawString(string.Format("{0} / {1}", checkpoint, total_checkpoints), new Font("Arial", 30), Brushes.Black, new Point(x, y));

            y += 40;
            if (checkpoint_reached)
            {
                checkpoint_time = time;
                checkpoint_reached = false;
            }
            r.DrawString(checkpoint_time, new Font("Arial", 30), Brushes.Black, new Point(x, y));
            
        }
        public void OnCheckpointReached(object source, EventArgs e)
        {
            checkpoint_reached = true;
        }
    }
}
