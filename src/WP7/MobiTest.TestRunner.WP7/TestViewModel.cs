using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MobiTest.TestRunner
{
    public class TestViewModel : ViewModelBase
    {
        private readonly Test _test;

        public TestViewModel(Test test)
        {
            _test = test;
        }

        public Test Test { get { return _test; } }
    }
}
