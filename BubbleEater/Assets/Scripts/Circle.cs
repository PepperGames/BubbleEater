using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Circle
{
    public float X { get; set; }
    public float Y { get; set; }
    public float R { get; set; }

    public Circle(float x, float y, float r)
    {
        X = x;
        Y = y;
        R = r;
    }

    public bool IsIntersect(Circle other)
    {
        bool result;
        float distance = new Point(X,Y).DistanceTo(new Point(other.X, other.Y));
        // Окружности совпадают (это одна и та же окружность)
        if (distance == 0 && R == other.R)
        {
            result = true;
        }
        // Окружности не касаются друг друга
        else if (distance > R + other.R)
        {
            result = false;
        }
        // Одна окружность содержится внутри другой и не касается ее
        else if (distance < Math.Abs(R - other.R))
        {
            result = true;
        }
        // Окружности соприкасаются в одной точке
        else if ((distance == R + other.R) || (distance == Math.Abs(R - other.R)))
        {
            result = true;
        }
        // Окружности пересекаются в двух точках
        else
        {
            result = true;
        }

        return result;
    }

    public class Point
    {
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; private set; }

        public float Y { get; private set; }

        // Расстояние до точки
        public float DistanceTo(Point other)
        {
            return Mathf.Sqrt(Mathf.Pow(X - other.X, 2) + Mathf.Pow(Y - other.Y, 2));
        }
    }
}
