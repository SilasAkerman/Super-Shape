using System;
using System.Numerics;
using Raylib_cs;

namespace Super_Shape
{
    class Slider
    {
        static Slider touched = null;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; } = 200;
        public int Thickness { get; set; } = 1;

        public double Increment { get; set; } = 1;
        public double Minimum { get; set; } = 0;
        public double Maximum { get; set; } = 100;
        public double Placement { get; set; } = 50;

        public double Value { get { return getValue(); } } // TODO

        public int CircleSize { get; set; } = 10;
        public Color ThisColor { get; set; } = Color.WHITE;

        private double percentage { get { return (Placement - Minimum) / (Maximum - Minimum); } }
        
        private double getValue()
        {
            return (Maximum - Minimum) * percentage;
        }

        public void Update()
        {
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
            {
                Vector2 mousePos = Raylib.GetMousePosition();
                if ((Raylib.CheckCollisionPointRec(mousePos, new Rectangle(X, Y - CircleSize, Width, CircleSize * 2)) && touched is null) || touched == this)
                {
                    touched = this;
                    updatePosition(mousePos.X);
                }
            }
            else touched = null;
        }
        public void Display()
        {
            Raylib.DrawLine(X, Y, X + Width, Y, ThisColor);
            int circelX = (int)(Width * percentage);
            Raylib.DrawCircle(X + circelX, Y, CircleSize, ThisColor);
            Raylib.DrawCircleLines(X + circelX, Y, CircleSize, Color.BLACK);
        }

        private void updatePosition(double mouseX)
        {
            int newCircleX = (int)mouseX;
            if (mouseX < X) newCircleX = X;
            if (mouseX > X + Width) newCircleX = X + Width;
            double percentageDistance = (newCircleX - X) / (Width + 0.0);
            Placement = (Maximum - Minimum) * percentageDistance;
            Placement = (int)(Placement / Increment) * Increment;
        }
    }
}
