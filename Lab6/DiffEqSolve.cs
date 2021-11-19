using OxyPlot;
using System;
using System.Collections.Generic;
using static Lab6.DiffEq;

namespace Lab6
{
    public struct DiffEqSolveSettings
    {
        public double a;
        public double b;
        public double n;
        public List<double> initValue;
        public List<DiffEqDelegate> eqList;
    }

    public static class DiffEqSolve
    {
        public static List<DataPoint> RK(DiffEqSolveSettings set, int order, double estval)
        {
            List <DataPoint> crds = new();

            DiffEqDelegate eq = set.eqList[order - 1];
            double h = (set.b - set.a) / set.n;
            double xi = set.a;
            double yi = set.initValue[order - 1];

            while (xi <= set.b)
            {
                crds.Add(new DataPoint(xi, yi));
                double k1 = h * eq(xi, yi);
                double k2 = h * eq(xi + h / 2, yi + k1 / 2);
                double k3 = h * eq(xi + h / 2, yi + k2 / 2);
                double k4 = h * eq(xi + h, yi + k3);

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

        public static List<DataPoint> HigherOrder(DiffEqSolveSettings set, int order, double estval)
        {
            List<DataPoint> crds = new();

            return crds;
        }
    }
}
