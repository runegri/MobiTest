using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MobiTest
{
    public class TestResultDescription
    {
        public string TestName { get; set; }
        public TestResult Result { get; set; }
        public string Message { get; set; }


        public TestResultDescription(string testName, TestResult result, string message)
        {
            TestName = testName;
            Result = result;
            Message = message;
        }

        public static TestResultDescription Passed(string message)
        {
            var testName = new StackTrace().GetFrame(1).GetMethod().Name;
            return new TestResultDescription(testName, TestResult.Passed, message);
        }

        public static TestResultDescription Passed()
        {
            var testName = new StackTrace().GetFrame(1).GetMethod().Name;
            return new TestResultDescription(testName, TestResult.Passed, "");
        }

        public static TestResultDescription Failed(string message)
        {
            var testName = new StackTrace().GetFrame(1).GetMethod().Name;
            return new TestResultDescription(testName, TestResult.Failed, message);
        }

        public static TestResultDescription Failed(Exception exception)
        {
            var stackTrace = new StackTrace(exception);
            var testName = stackTrace.GetFrame(stackTrace.FrameCount - 1).GetMethod().Name;
            return new TestResultDescription(testName, TestResult.Failed, exception.Message);
        }

        public override string ToString()
        {
            var result = TestName + ": " + Result;
            if (!string.IsNullOrEmpty(Message))
            {
                result += Environment.NewLine + Message;
            }
            return result;
        }
    }
}