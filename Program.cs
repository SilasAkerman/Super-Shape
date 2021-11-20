using System;
using System.Numerics;
using Raylib_cs;

namespace Super_Shape
{
    class Program
    {
        const int WIDTH = 1000;
        const int HEIGHT = 800;

        const int NUM_POINTS = 10000;
        static double increment = Math.PI * 12 / NUM_POINTS;

        const int scalar = 100;
        static Vector2[] points = new Vector2[NUM_POINTS];

        const float xOff = WIDTH / 2;
        const float yOff = HEIGHT / 2 - 100;

        static double n1 = 1;
        static double n2 = 1;
        static double n3 = 1;
        static double m = 0;
        static double a = 1;
        static double b = 1;

        static Slider sliderN1;
        static Slider sliderN2;
        static Slider sliderN3;
        static Slider sliderM;
        static Slider sliderA;
        static Slider sliderB;

        static void Main(string[] args)
        {
            sliderN1 = new Slider
            {
                X = 50,
                Y = 450,
                Increment = 0.1,
                Minimum = 0,
                Maximum = 15,
                Placement = 1,
            };

            sliderN2 = new Slider
            {
                X = 50,
                Y = 500,
                Increment = 0.1,
                Minimum = 0,
                Maximum = 15,
                Placement = 1,
            };

            sliderN3 = new Slider
            {
                X = 50,
                Y = 550,
                Increment = 0.1,
                Minimum = 0,
                Maximum = 15,
                Placement = 1,
            };

            sliderM = new Slider
            {
                X = 300,
                Y = 500,
                Increment = 1,
                Minimum = 0,
                Maximum = 10,
                Placement = 0,
            };

            sliderA = new Slider
            {
                X = 550,
                Y = 475,
                Increment = 0.1,
                Minimum = 0,
                Maximum = 2,
                Placement = 1,
            };

            sliderB = new Slider
            {
                X = 550,
                Y = 525,
                Increment = 0.1,
                Minimum = 0,
                Maximum = 2,
                Placement = 1,
            };

            Raylib.InitWindow(WIDTH, HEIGHT, "Super Shape");

            while (!Raylib.WindowShouldClose())
            {
                Update();
                Display();
            }
            Raylib.CloseWindow();
        }

        static void Update()
        {
            sliderN1.Update();
            sliderN2.Update();
            sliderN3.Update();
            sliderM.Update();
            sliderA.Update();
            sliderB.Update();

            n1 = sliderN1.Value;
            n2 = sliderN2.Value;
            n3 = sliderN3.Value;
            m = sliderM.Value;
            a = sliderA.Value;
            b = sliderB.Value;

            for (int i = 0; i < NUM_POINTS; i++)
            {
                double angle = i * increment;
                double radius = SuperShape(angle);
                float x = (float)(radius * Math.Cos(angle) * scalar);
                float y = (float)(radius * Math.Sin(angle) * scalar);
                points[i] = new Vector2(x + xOff, y + yOff);
            }
        }

        static void Display()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            Raylib.DrawLineEx(points[points.Length - 1], points[0], 2, Color.WHITE);
            for (int i = 1; i < points.Length; i++)
            {
                Raylib.DrawLineEx(points[i - 1], points[i], 2, Color.WHITE);
            }

            sliderN1.Display();
            sliderN2.Display();
            sliderN3.Display();
            sliderM.Display();
            sliderA.Display();
            sliderB.Display();

            Raylib.EndDrawing();
        }

        static double SuperShape(double theta)
        {
            double part1 = (1 / a) * Math.Cos((theta * m) / 4);
            part1 = Math.Abs(part1);
            part1 = Math.Pow(part1, n2);

            double part2 = (1 / b) * Math.Sin((theta * m) / 4);
            part2 = Math.Abs(part2);
            part2 = Math.Pow(part2, n3);

            double part3 = Math.Pow(part1 + part2, 1 / n1);
            if (part3 == 0) return 0;
            return 1 / part3;
        }
    }
}
