using System;

namespace Lab6
{
    public static class DiffEq
    {
        public delegate double DiffEqDelegate(double x, double y);

        public static double F1FirstDev(double x, double y)
        {
            return 4.3 * x + 2 * y + Math.Exp(x * y);
        }

        public static double F2FirstDev(double x, double y)
        {
            return 2 * Math.Exp(x) * Math.Cos(x) - y;
        }

        public static double F2SecondDev(double x, double y)
        {
            return 2 * F2FirstDev(x, y) + 2 * y;
        }
    }
}
