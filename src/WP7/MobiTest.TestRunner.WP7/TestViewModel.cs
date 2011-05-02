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
            _test.TestInvoked += TestInvoked;
        }

        private void TestInvoked(object sender, EventArgs e)
        {
            RaisePropertyChanged("");
        }

        public void RunTest()
        {
            _test.Invoke();
        }

        public string TestName { get { return _test.TestName; } }
        public string ResultText
        {
            get
            {
                if (_test.Result == null)
                {
                    return "";
                }
                return _test.Result.ToString();
            }
        }

        public TestResult Result
        {
            get
            {
                if (_test.Result == null)
                {
                    return TestResult.None;
                }
                return _test.Result.Result;
            }
        }

        public Test Test { get { return _test; } }
    }
}
