using System;

namespace MapGenerator.DataModels
{
    public struct Vector2Float
    {
        public float X { get; }
        public float Y { get; }

        public Vector2Float(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2Float operator + (Vector2Float a, Vector2Float b)
        {
            return new Vector2Float(a.X + b.X, a.Y + b.Y);
        }

        public static double CalculateDistance(Vector2Float a, Vector2Float b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
    }
}