using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MonteCarloMethod.Classes.Alghoritm;
using MonteCarloMethod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using MathNet.Symbolics;
using MonteCarloMethod.Classes;

namespace MonteCarloMethod.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            MyMainModel = new MainModel
            {
                Functions = "x^2+1;(x-1)^2:1;2/x",
                A = 0,
                B = 2,
                C = 0,
                D = 2,
                IterationCount = 10,
                ExValue = 2.053
            };
        }

        #region Commands

        public ICommand BStartCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    try
                    {
                        // | x^2+1;(x-1)^2:1;2/x |

                        var f = new List<Element>();

                        if (MyMainModel.Functions.Contains(';'))
                        {
                            var temp = MyMainModel.Functions.Split(';').Where(s => !s.Equals("")).ToList();

;                           var t1 = temp.Where(s => s.Contains(":1")).Select(s => s.Replace(":1", ""));
                            var t2 = temp.Where(s => !s.Contains(":1"));

                            f.AddRange(t1.Select(tmp => new Element { M = true, Expression = tmp }));
                            f.AddRange(t2.Select(tmp => new Element { M = false, Expression = tmp }));
                        }
                        else
                        {
                            f.Add(new Element { M = false, Expression = MyMainModel.Functions });
                        }

                        var a = MyMainModel.A;
                        var b = MyMainModel.B;
                        var c = MyMainModel.C;
                        var d = MyMainModel.D;

                        using (var alg = new McMethod(f, a, b, c, d, MyMainModel.IterationCount))
                        {
                            await Task.Factory.StartNew(() => alg.Calculate());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        #endregion

        #region Properties

        private MainModel _myMainModel;

        public MainModel MyMainModel
        {
            get => _myMainModel;
            set
            {
                _myMainModel = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}