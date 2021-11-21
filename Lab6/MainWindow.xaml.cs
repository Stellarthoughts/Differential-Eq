using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Windows;
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

            var res = DiffEqSolve.RK(0,0.65,1000,0,F1FirstDev,0.001);
            var res2 = DiffEqSolve.RKSystem(0, 1, 1000, 1, 0, F2FirstDev, F2SecondDev, 0.001);

            var model = MainViewModel.MyModel;
            model.Series.Clear();
            model.Annotations.Clear();

            LineSeries s = new();
            res2[0].ForEach(x => s.Points.Add(x));
            model.Series.Add(s);

            LineSeries d = new();
            res2[1].ForEach(x => d.Points.Add(x));
            model.Series.Add(d);

            LineSeries f = new();
            res.ForEach(x => f.Points.Add(x));
            model.Series.Add(f);

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

        public static PlotModel MyModel { get; private set; }
    }
}
