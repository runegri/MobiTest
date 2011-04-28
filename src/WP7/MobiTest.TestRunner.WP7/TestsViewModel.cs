using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class TestsViewModel : ViewModelBase
    {
        public override event PropertyChangedEventHandler PropertyChanged;

        public TestsViewModel()
        {
            AllTests = new ObservableCollection<TestViewModel>();
            FailingTests = new FilteredObservableCollection<TestViewModel>(AllTests, t => t.Test.Result.Result == TestResult.Failed);
            PassingTests = new FilteredObservableCollection<TestViewModel>(AllTests, t => t.Test.Result.Result == TestResult.Passed);
        }

        private ObservableCollection<TestViewModel> _allTests;
        public ObservableCollection<TestViewModel> AllTests
        {
            get { return _allTests; }
            private set
            {
                _allTests = value;
                RaisePropertyChanged("AllTests");
            }
        }

        private FilteredObservableCollection<TestViewModel> _failingTests;
        public FilteredObservableCollection<TestViewModel> FailingTests
        {
            get { return _failingTests; }
            private set
            {
                _failingTests = value;
                RaisePropertyChanged("FailingTests");
            }
        }

        private FilteredObservableCollection<TestViewModel> _passingTests;
        public FilteredObservableCollection<TestViewModel> PassingTests
        {
            get { return _passingTests; }
            private set
            {
                _passingTests = value;
                RaisePropertyChanged("PassingTests");
            }
        }
    }
}
