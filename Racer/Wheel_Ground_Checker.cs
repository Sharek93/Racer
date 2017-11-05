using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racer
{
    class Wheel_Ground_Checker
    {
        Color center;

        Color front_left;
        Color front_right;
        Color rear_left;
        Color rear_right;

        string center_ground;

        string front_left_ground;
        string front_right_ground;
        string rear_left_ground;
        string rear_right_ground;

        public string Front_left_ground
        {
            get
            {
                return front_left_ground;
            }
        }

        public string Front_right_ground
        {
            get
            {
                return front_right_ground;
            }
        }

        public string Rear_left_ground
        {
            get
            {
                return rear_left_ground;
            }
        }

        public string Rear_right_ground
        {
            get
            {
                return rear_right_ground;
            }
        }

        public string Center_ground
        {
            get
            {
                return center_ground;
            }
        }

        public void Get_color(Color center)
        {
            this.center = center;
            Translate();
        }

        public void Get_colors(Color front_left, Color front_right, Color rear_left, Color rear_right)
        {
            this.front_left = front_left;
            this.front_right = front_right;
            this.rear_left = rear_left;
            this.rear_right = rear_right;

            Translate_color_to_groud();
        }

        private void Translate_color_to_groud()
        {
            if (front_left.R == 0 && front_left.G == 0 && front_left.B == 0 || (front_left.R <= 255 && front_left.R >= 127) && (front_left.G <= 255 && front_left.G >= 127) && (front_left.B <= 255 && front_left.B >= 127))
            {
                front_left_ground = "asphalt";
            }
            else if (front_left.G >= 119 && front_left.G >= 188)
            {
                front_left_ground = "grass";
            }
            else if (front_left.R == 220 && front_left.G == 220 && front_left.B == 220)
            {
                front_left_ground = "line";
            }
            else if (front_left.R == 230 && front_left.G == 230 && front_left.B == 230)
            {
                front_left_ground = "checkpoint";
            }

            if (front_right.R == 0 && front_right.G == 0 && front_right.B == 0 || (front_right.R <= 255 && front_right.R >= 127) && (front_right.G <= 255 && front_right.G >= 127) && (front_right.B <= 255 && front_right.B >= 127))
            {
                front_right_ground = "asphalt";
            }
            else if (front_right.G >= 119 && front_right.G >= 188)
            {
                front_right_ground = "grass";
            }
            else if (front_right.R == 220 && front_right.G == 220 && front_right.B == 220)
            {
                front_right_ground = "line";
            }
            else if (front_right.R == 230 && front_right.G == 230 && front_right.B == 230)
            {
                front_right_ground = "checkpoint";
            }

            if (rear_left.R == 0 && rear_left.G == 0 && rear_left.B == 0 || (rear_left.R <= 255 && rear_left.R >= 127) && (rear_left.G <= 255 && rear_left.G >= 127) && (rear_left.B <= 255 && rear_left.B >= 127))
            {
                rear_left_ground = "asphalt";
            }
            else if (rear_left.G >= 119 && rear_left.G >= 188)
            {
                rear_left_ground = "grass";
            }
            else if (rear_left.R == 220 && rear_left.G == 220 && rear_left.B == 220)
            {
                rear_left_ground = "line";
            }
            else if (rear_left.R == 230 && rear_left.G == 230 && rear_left.B == 230)
            {
                rear_left_ground = "checkpoint";
            }

            if (rear_right.R == 0 && rear_right.G == 0 && rear_right.B == 0 || (rear_right.R <= 255 && rear_right.R >= 127) && (rear_right.G <= 255 && rear_right.G >= 127) && (rear_right.B <= 255 && rear_right.B >= 127))
            {
                rear_right_ground = "asphalt";
            }
            else if (rear_right.G >= 119 && rear_right.G >= 188)
            {
                rear_right_ground = "grass";
            }
            else if (rear_right.R == 220 && rear_right.G == 220 && rear_right.B == 220)
            {
                rear_right_ground = "line";
            }
            else if (rear_right.R == 230 && rear_right.G == 230 && rear_right.B == 230)
            {
                rear_right_ground = "checkpoint";
            }
        }

        protected void Translate()
        {
            if (center.R == 255 && center.G == 216 && center.B == 0)
            {
                center_ground = "line";
            }
            else if (center.R == 230 && center.G == 230 && center.B == 230)
            {
                center_ground = "checkpoint";
            }
            else if (center.R == 127 && center.G == 127 && center.B == 127)
            {
                center_ground = "asphalt";
            }
            else if (center.G >= 100 && center.G <= 255 && center.B < 50)
            {
                center_ground = "grass";
            }
            else if (center == Color.Black || center == Color.Brown)
            {
                center_ground = "marks";
            }
            else
            {
                center_ground = "other";
            }
        }

    }
}
