using System;
using System.Linq.Expressions;
using Gearz.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gearz.Tests.Helpers
{
    [TestClass]
    public class ExpressionHelperTests
    {
        public class Person
        {
            public string Name { get; set; }
        }

        [TestMethod]
        public void GetPropertyNameFromMemberExpression_Default()
        {
            Expression<Func<Person, Object>> nameExpression = person => person.Name;
            var propertyName = ExpressionHelper.GetPropertyNameFromMemberExpression(nameExpression);
            Assert.AreEqual("Name", propertyName);
        }
    }
}
