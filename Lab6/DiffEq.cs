using System;

namespace Lab6
{
    public static class DiffEq
    {
        public delegate double DiffEqDelegate(double x, double y, double z);
        public static double omega = 1;

        public static double F1FirstDev(double x, double y, double z)
        {
            return 4.3 * x + 2 * y + Math.Exp(x * y);
        }

        public static double F2FirstDev(double x, double y, double z)
        {
            return Math.Exp(x) * Math.Cos(x) - y;
        }

        public static double F2SecondDev(double x, double y, double z)
        {
            return 2 * F2FirstDev(x, y, z) + 2 * y;
        }

        public static double F3FirstDev(double x, double y, double z)
        {
            return z;
        }

        public static double F3SecondDev(double x, double y, double z)
        {
            return -omega * y;
        }
    }
}
