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
        public double initValue;
        public List<DiffEqDelegate> eqList;
        public int order;
        public int solveOrder;
    }

    public static class DiffEqSolve
    {
        public static List<DataPoint> RK(DiffEqSolveSettings set)
        {
            List <DataPoint> crds = new();

            DiffEqDelegate eq = set.eqList[set.solveOrder - 1];
            double h = (set.b - set.a) / set.n;
            double xi = set.a;
            double yi = set.initValue;
            double k2_1 = h * eq(xi + h / 2, yi + h * eq(xi, yi) / 2);
            crds.Add(new DataPoint(xi, yi));

            while (xi <= set.b)
            {
                double k1 = h * eq(xi, yi);
                double k2 = h * eq(xi + h / 2, yi + k1 / 2);
                double k3 = h * eq(xi + h / 2, yi + k2 / 2);
                double k4 = h * eq(xi + h, yi + k3);

                yi += (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                crds.Add(new DataPoint(xi, yi));

                double om = Math.Abs((k2 - k3) / (k1 - k2_1));
                if (om < 0.01)
                    h *= 2;
                else
                    h /= 2;

                xi += h;
            }

            return crds;
        }
    }
}
