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
                a = 0, b = 0.5, n = 10, initValue = 0,
                eqList = new List<DiffEqDelegate> {F1FirstDev},
                order = 1, solveOrder = 1
            };

            var res = DiffEqSolve.RK(settings);
        }
    }
}
