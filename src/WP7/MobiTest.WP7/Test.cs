using System;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NUnit.Framework;

namespace MobiTest
{    
    public class Test
    {

        public event EventHandler TestInvoked;

        public Action SetupMethod { get; private set; }
        public Action TestMethod { get; private set; }
        public Action TearDownMethod { get; private set; }
        public string TestName { get; private set; }
        public TestResultDescription Result { get; private set; }
        public ExpectedException ExpectedException { get; set; }
        public bool Ignore { get; set; }

        public Test(MethodInfo method, object containingClass)
        {
            TestMethod = ActionFromMethodInfo(method, containingClass);
            TestName = method.Name;
        }

        public Test(MethodInfo testMethod, MethodInfo setupMethod, MethodInfo tearDownMethod, object containingClass)
            : this(testMethod, containingClass)
        {
            SetupMethod = ActionFromMethodInfo(setupMethod, containingClass);
            TearDownMethod = ActionFromMethodInfo(tearDownMethod, containingClass);
        }

        private static Action ActionFromMethodInfo(MethodInfo method, object containingClass)
        {
            if (containingClass == null || method == null)
            {
                return null;
            }
            return (Action)Delegate.CreateDelegate(typeof(Action), containingClass, method);
        }

        public void Invoke()
        {
            if (Ignore)
            {
                Result = new TestResultDescription(TestName, TestResult.Ignored, "");
            }
            else
            {
                try
                {
                    if (SetupMethod != null)
                    {
                        SetupMethod();
                    }
                    TestMethod();
                    if (TearDownMethod != null)
                    {
                        TearDownMethod();
                    }
                    Result = new TestResultDescription(TestName, TestResult.Passed, "");
                }
                catch (Exception exception)
                {
                    if (ExpectedException != null && exception.GetType().Equals(ExpectedException.ExpectedExceptionType))
                    {
                        Result = new TestResultDescription(TestName, TestResult.Passed, "");
                    }
                    else
                    {
                        Result = new TestResultDescription(TestName, TestResult.Failed, exception.Message);
                    }
                }
            }

            var testInvoked = TestInvoked;
            if (testInvoked != null)
            {
                testInvoked(this, new EventArgs());
            }
        }

        public override string ToString()
        {
            return "Test: " + TestName;
        }
    }

    public class ExpectedException
    {

        public Type ExpectedExceptionType { get; private set; }

        public ExpectedException(ExpectedExceptionAttribute exceptionAttribute)
        {
            ExpectedExceptionType = exceptionAttribute.ExpectedException;
        }

    }
}
