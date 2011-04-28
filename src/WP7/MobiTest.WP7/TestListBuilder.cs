using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using NUnit.Framework;

namespace MobiTest
{
    public class TestListBuilder
    {
        public List<Test> BuildTestsList()
        {
            var testMethods =  new List<Test>();

            foreach (Type testType in FindAllTestClasses())
            {
                var methods = GetAllTestsForType(testType);
                var container = Activator.CreateInstance(testType);
                var setupMethod = GetMethodWithAttribute<SetUpAttribute>(testType);
                var tearDownMethod = GetMethodWithAttribute<TearDownAttribute>(testType);
                var tests = methods.Select(t =>
                {
                    var test = new Test(t, setupMethod, tearDownMethod, container);
                    var expectedExceptionAttribute = GetAttributeOfType<ExpectedExceptionAttribute>(t);

                    if (expectedExceptionAttribute != null)
                    {
                        test.ExpectedException = new ExpectedException(expectedExceptionAttribute);
                    }

                    if (GetAttributeOfType<IgnoreAttribute>(t) != null)
                    {
                        test.Ignore = true;
                    }

                    return test;
                });
                testMethods.AddRange(tests);
            }

            return testMethods;
        }

        private static MethodInfo GetMethodWithAttribute<TAttribute>(Type containingType) where TAttribute : Attribute
        {
            return containingType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .SingleOrDefault(m => GetAttributeOfType<TAttribute>(m) != null);
        }

        private static IEnumerable<MethodInfo> GetAllTestsForType(Type type)
        {
            var tests = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(IsTest);
            return tests;
        }

        private static IEnumerable<Type> FindAllTestClasses()
        {
            var types = Deployment
                .Current
                .Parts
                .Select(AssemblyFromPart)
                .SelectMany(a => a.GetTypes())
                .Where(IsTestType);
            return types;
        }

        private static Assembly AssemblyFromPart(AssemblyPart assemblyPart)
        {
            var assemblyName = assemblyPart.Source.Replace(".dll", "");
            var assembly = Assembly.Load(assemblyName);
            return assembly;
        }

        private static bool IsTestType(Type type)
        {
            return type.GetCustomAttributes(typeof(TestFixtureAttribute), true).Any();
        }

        private static bool IsTest(MethodInfo methodInfo)
        {
            return GetAttributeOfType<TestAttribute>(methodInfo) != null;
        }

        private static TAttribute GetAttributeOfType<TAttribute>(MethodInfo method) where TAttribute : Attribute
        {
            return method.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault() as TAttribute;
        }

    }
}
