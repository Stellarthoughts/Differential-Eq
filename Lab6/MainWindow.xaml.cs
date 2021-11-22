using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using static Lab6.DiffEq;

namespace Lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Calculate();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {
            CultureInfo ci = new("en-us");
            double a;
            double b;
            int n;
            double f1_0;
            double f2_0;
            double estval;
            double omega;

            try
            {
                a = Double.Parse(tbA.Text, CultureInfo.InvariantCulture);
                b = Double.Parse(tbB.Text, CultureInfo.InvariantCulture);
                n = Int32.Parse(tbN.Text, CultureInfo.InvariantCulture);
                f1_0 = Double.Parse(tbF1_0.Text, CultureInfo.InvariantCulture);
                f2_0 = Double.Parse(tbF2_0.Text, CultureInfo.InvariantCulture);
                estval = Double.Parse(tbEstval.Text, CultureInfo.InvariantCulture);
                omega = Double.Parse(tbOmega.Text, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                btnCalculate.Content = $"Format error!";
                return;
            }
            btnCalculate.Content = "Calculate";

            var res = DiffEqSolve.RKSystem2(a, b, n, f1_0, f2_0, F3FirstDev, F3SecondDev, estval, omega);

            string s = "";
            for (int i = 0; i < res[0].Count; i++)
                s += $"{i}: x = {res[0][i].X.ToString("F03",ci)} y' = {res[0][i].Y.ToString("F03", ci)} y'' = {res[1][i].Y.ToString("F03", ci)}\n";
            tbOutput.Text = s;

            var model = ((MainViewModel)this.DataContext).MyModel;

            model.Series.Clear();
            model.Annotations.Clear();

            LineSeries ls1 = new();
            res[0].ForEach(x => ls1.Points.Add(x));
            ls1.Title = "y' = z";
            model.Series.Add(ls1);
      
            LineSeries ls2 = new();
            res[1].ForEach(x => ls2.Points.Add(x));
            ls2.Title = "y'' = -omega * y";
            model.Series.Add(ls2);

            model.Legends.Add(new Legend()
            {
                LegendTitle = "Legend",
                LegendPosition = LegendPosition.RightTop
            });

            model.ResetAllAxes();
            model.InvalidatePlot(true);
        }
    }

    public class MainViewModel
    {
        public MainViewModel()
        {
            MyModel = new PlotModel();
        }

        public PlotModel MyModel { get; private set; }
    }
}
