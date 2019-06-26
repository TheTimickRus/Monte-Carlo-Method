using System;
using System.Collections.Generic;
using MathNet.Symbolics;
using MonteCarloMethod.Models;

namespace MonteCarloMethod.Classes.Alghoritm
{
    public class McMethod : IDisposable
    {
        public McMethod(List<Element> symbolicExpressions,
            double a, double b, double c, double d,
            int iterationCount)
        {
            SymbolicExpressions = symbolicExpressions;

            A = a;
            B = b;
            C = c;
            D = d;

            IterationCount = iterationCount;
        }

        public void Dispose()
        {
        }

        public void Calculate()
        {
            var mainArea = (B - A) * (D - C);

            var rnd = new Random();
            var x = GetList(rnd, IterationCount, A, B);
            var y = GetList(rnd, IterationCount, C, D);

            var s = 0.0;
            for (var i = 0; i < IterationCount; i++)
            {
                var flag = true;

                foreach (var symbolicExpression in SymbolicExpressions)
                {
                    var value = symbolicExpression.Expression.Evaluate(
                        new Dictionary<string, FloatingPoint> {{"x", x[i]}}).RealValue;

                    if (!(symbolicExpression.M && y[i] >= value || 
                          !symbolicExpression.M && y[i] <= value))
                    {
                        flag = false;
                        break;
                    }
                }

                if (!flag) continue;

                s++;
                MainModel.Instanse.Progress = (double) i * 100 / IterationCount;
                MainModel.Instanse.Answer = 
                    $"Площадь фигуры: {mainArea * (s / IterationCount)}";
            }

            MainModel.Instanse.Progress = 100;

            var newArea = mainArea * (s / IterationCount);
            var e = MainModel.Instanse.ExValue;
            var ex = 
                Math.Abs(e - newArea) / e;

            MainModel.Instanse.Answer = $"Площадь фигуры: {newArea}\n" +
                                        $"Относительная погрешность: {ex,0:P}";
        }

        private static List<double> GetList(Random rnd, int n, double a, double b)
        {
            var temp = new List<double>();

            for (var i = 0; i < n; i++) temp.Add(a + rnd.NextDouble() * (b - a));

            return temp;
        }

        #region Properties

        private List<Element> SymbolicExpressions { get; }
        private double A { get; }
        private double B { get; }
        private double C { get; }
        private double D { get; }
        private int IterationCount { get; }

        public string Answer { get; set; }

        #endregion
    }
}