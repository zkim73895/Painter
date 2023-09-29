using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Painter
    {
        static float x, y;
        static Graphics graphics; 

        public static void Initialize(Graphics newGraphics)
        {
            graphics = newGraphics;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        { 
            x = x0; 
            y = y0;
        }

        public static void DrawLine(Pen pen, double length, double angle)
        {
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphics.DrawLine(pen, x, y, x1, y1);
            SetPosition(x1, y1);
        }

        public static void ChangePosition(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        static float edgeLengthCoefficient = 0.375f;
        static float edgeWidthCoefficient = 0.04f;
        static double coeffLengthTimesSize;
        static double coeffWidthTimesSize;

        public static void DrawDirection(double direction, int size)
        {
            coeffLengthTimesSize = size * edgeLengthCoefficient;
            coeffWidthTimesSize = size * edgeWidthCoefficient;
            Painter.DrawLine(Pens.Yellow, coeffLengthTimesSize, direction);
            Painter.DrawLine(Pens.Yellow, coeffWidthTimesSize * Math.Sqrt(2), direction + Math.PI / 4);
            Painter.DrawLine(Pens.Yellow, coeffLengthTimesSize, direction + Math.PI);
            Painter.DrawLine(Pens.Yellow, coeffLengthTimesSize - coeffWidthTimesSize, direction + Math.PI / 2);
            Painter.ChangePosition(coeffWidthTimesSize, direction - Math.PI);
            Painter.ChangePosition(coeffWidthTimesSize * Math.Sqrt(2), direction + 3 * Math.PI / 4);
        }

        public static void Draw(int width, int hight, double rotationAngle, Graphics graphics)
        {
            Painter.Initialize(graphics);
            var size = Math.Min(width, hight);
            coeffLengthTimesSize = size * edgeLengthCoefficient;
            coeffWidthTimesSize = size * edgeWidthCoefficient;
            var diagonalLength = Math.Sqrt(2) * (coeffLengthTimesSize + coeffWidthTimesSize) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + hight / 2f;

            Painter.SetPosition(x0, y0);

            DrawImpossibleSquare(size);
        }

        public static void DrawImpossibleSquare(int size)
        {
            DrawDirection(0, size);
            DrawDirection(-Math.PI / 2, size);
            DrawDirection(Math.PI, size);
            DrawDirection(Math.PI / 2, size);
        }
    }
}