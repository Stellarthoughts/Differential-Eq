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

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {
            double a;
            double b;
            int n;
            double f1_0;
            double f2_0;
            double estval;

            try
            {
                a = Double.Parse(tbA.Text, CultureInfo.InvariantCulture);
                b = Double.Parse(tbB.Text, CultureInfo.InvariantCulture);
                n = Int32.Parse(tbN.Text, CultureInfo.InvariantCulture);
                f1_0 = Double.Parse(tbF1_0.Text, CultureInfo.InvariantCulture);
                f2_0 = Double.Parse(tbF2_0.Text, CultureInfo.InvariantCulture);
                estval = Double.Parse(tbEstval.Text, CultureInfo.InvariantCulture); ;
            }
            catch (FormatException)
            {
                btnCalculate.Content = $"Format error!";
                return;
            }
            btnCalculate.Content = "Calculate";

            var res = DiffEqSolve.RKSystem2(a, b, n, f1_0, f2_0, F2FirstDev, F2SecondDev, estval);

            var model = ((MainViewModel)this.DataContext).MyModel;

            model.Series.Clear();
            model.Annotations.Clear();

            LineSeries s = new();
            res[0].ForEach(x => s.Points.Add(x));
            s.Title = "y' = e^x * cosx - y";
            model.Series.Add(s);
      

            LineSeries d = new();
            res[1].ForEach(x => d.Points.Add(x));
            d.Title = "y'' = 2 e^x cos x";
            model.Series.Add(d);

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
