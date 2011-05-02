using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace MobiTest.TestRunner
{
    public class TestsViewModel : ViewModelBase
    {
        public override event PropertyChangedEventHandler PropertyChanged;

        public TestsViewModel()
        {
            AllTests = new ObservableCollection<TestViewModel>();
            FailingTests = new FilteredObservableCollection<TestViewModel>(AllTests, IsFailingTest);
            PassingTests = new FilteredObservableCollection<TestViewModel>(AllTests, IsPassingTest);
        }

        private static bool IsFailingTest(TestViewModel testViewModel)
        {
            return testViewModel.Result == TestResult.Failed;
        }

        private static bool IsPassingTest(TestViewModel testViewModel)
        {
            return testViewModel.Result == TestResult.Passed;
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

        public bool IsDataLoaded
        {
            get { return _allTests.Any(); }
        }

        public void LoadData()
        {
            var testListBuilder = new TestListBuilder();
            var allTests = testListBuilder.BuildTestsList();
            foreach (var test in allTests)
            {
                AllTests.Add(new TestViewModel(test));
            }
        }

        public ICommand RunAllTests
        {
            get { return new RunAllTestsCommand(this); }
        }

        private class RunAllTestsCommand : ICommand, IDisposable
        {
            private readonly TestsViewModel _testsViewModel;
            private bool _testsRunning;

            public RunAllTestsCommand(TestsViewModel testsViewModel)
            {
                _testsViewModel = testsViewModel;
                _testsViewModel.PropertyChanged += PropertyChanged;
            }

            void PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                RaiseCanExecuteChanged();
            }

            private void RaiseCanExecuteChanged()
            {
                var canExecuteChanged = CanExecuteChanged;
                if (canExecuteChanged != null)
                {
                    canExecuteChanged(this, new EventArgs());
                }

            }

            public bool CanExecute(object parameter)
            {
                return !_testsRunning && _testsViewModel.AllTests.Any();
            }

            public void Execute(object parameter)
            {
                ThreadPool.QueueUserWorkItem(InvokeAllTests);
            }

            private void InvokeAllTests(object dummy)
            {
                try
                {
                    _testsRunning = true;
                    RaiseCanExecuteChanged();
                    var allTests = _testsViewModel.AllTests.ToList();
                    foreach (var testViewModel in allTests)
                    {
                        testViewModel.RunTest();
                    }
                }
                finally
                {
                    _testsRunning = false;
                    RaiseCanExecuteChanged();
                }
            }

            public event EventHandler CanExecuteChanged;

            public void Dispose()
            {
                _testsViewModel.PropertyChanged -= PropertyChanged;
            }
        }
    }
}
