using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Racer
{
    class MainButton : Control
    {      
        Bitmap background_image, active_image;

        private SolidBrush borderbrush, textbrush;
        private Rectangle borderrectangle;
        private bool active, focused;
        private StringFormat stringformat = new StringFormat();

        public override Cursor Cursor { get; set; } = Cursors.Hand;
        public float BorderThickness { get; set; } = 0;

        public Bitmap Background_image
        {
            get
            {
                return background_image;
            }

            set
            {
                background_image = value;
            }
        }

        public Bitmap Active_image
        {
            get
            {
                return active_image;
            }

            set
            {
                active_image = value;
            }
        }

        public MainButton()
        {
            borderbrush = new SolidBrush(Color.Black);
            textbrush = new SolidBrush(Color.Black);            

            stringformat.Alignment = StringAlignment.Center;
            stringformat.LineAlignment = StringAlignment.Center;

            this.Paint += MainButton_Paint; 
        }

        private void MainButton_Paint(object sender, PaintEventArgs e)
        {
            if (focused)
            {
                Bitmap image = new Bitmap(active_image, Size);
                image.MakeTransparent(Color.White);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawImage(image, 0, 0);
                if (Text.Length > 0)
                {
                    SizeF size = e.Graphics.MeasureString(Text, Font);

                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.DrawString(Text, Font, new SolidBrush(Color.White), (ClientSize.Width - size.Width) / 2, (ClientSize.Height - size.Height) / 2);
                }
            }
            else
            {
                Bitmap image = new Bitmap(background_image, Size);
                image.MakeTransparent(Color.White);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawImage(image, 0, 0);
                if (Text.Length > 0)
                {
                    SizeF size = e.Graphics.MeasureString(Text, Font);

                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.DrawString(Text, Font, new SolidBrush(Color.Black), (ClientSize.Width - size.Width) / 2, (ClientSize.Height - size.Height) / 2);
                }
            }
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            focused = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            focused = false;
        }
    }
}
