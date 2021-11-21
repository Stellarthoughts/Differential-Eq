using OxyPlot;
using System;
using System.Collections.Generic;
using static Lab6.DiffEq;

namespace Lab6
{
    public static class DiffEqSolve
    {
        public static List<DataPoint> RK(double a, double b, double n, double initValue, DiffEqDelegate eq, double estval)
        {
            List <DataPoint> crds = new();

            double h = (b-a) / n;
            double xi = a;
            double yi = initValue;

            while (xi <= b)
            {
                crds.Add(new DataPoint(xi, yi));
                double k1 = h * eq(xi, yi, 0);
                double k2 = h * eq(xi + h / 2, yi + k1 / 2, 0);
                double k3 = h * eq(xi + h / 2, yi + k2 / 2, 0);
                double k4 = h * eq(xi + h, yi + k3, 0);

                double om = Math.Abs((k2 - k3) / (k1 - k2));
                if (om < estval)
                    h *= 2;
                else
                    h /= 2;

                xi += h;
                yi += (k1 + 2 * k2 + 2 * k3 + k4) / 6;
            }

            return crds;
        }

        public static List<List<DataPoint>> RKSystem(double a, double b, double n, double initValue1, double initValue2, DiffEqDelegate eq1, DiffEqDelegate eq2, double estval)
        {
            List<List<DataPoint>> res = new();
            List<DataPoint> crds1 = new();
            List<DataPoint> crds2 = new();
            res.Add(crds1);
            res.Add(crds2);

            double h = (b - a) / n;
            double xi = a;

            double yi = initValue1;
            double zi = initValue2;

            while (xi <= b)
            {
                crds1.Add(new DataPoint(xi, yi));
                crds2.Add(new DataPoint(xi, zi));

                double k1 = h * eq1(xi, yi, zi);
                double l1 = h * eq2(xi, yi, zi);

                double k2 = h * eq1(xi + h / 2, yi + k1 / 2, zi + l1 / 2);
                double l2 = h * eq2(xi + h / 2, yi + k1 / 2 , zi + l1 / 2);

                double k3 = h * eq1(xi + h / 2, yi + k2 / 2,zi + l2 / 2);
                double l3 = h * eq2(xi + h / 2, yi + k2 / 2,zi + l2 / 2);

                double k4 = h * eq1(xi + h, yi + k3, zi + l3);
                double l4 = h * eq2(xi + h, yi + k3, zi + l3);


                double om = Math.Abs((k2 - k3) / (k1 - k2));
                if (om < estval)
                    h *= 2;
                else
                    h /= 2;

                xi += h;
                yi += (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                zi += (l1 + 2 * l2 + 2 * l3 + l4) / 6;
            }

            return res;
        }
    }
}
