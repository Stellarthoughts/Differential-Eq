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

            DiffEqSolveSettings settings = new() {
                a = 0, b = 0.65, n = 1000,
                initValue = new List<double> { 0 },
                eqList = new List<DiffEqDelegate> { F2FirstDev, F2SecondDev },
            };

            var res = DiffEqSolve.RK(settings,1,0.001);

            var model = MainViewModel.MyModel;
            model.Series.Clear();
            model.Annotations.Clear();

            LineSeries s = new();
            res.ForEach(x => s.Points.Add(x));
            model.Series.Add(s);

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
