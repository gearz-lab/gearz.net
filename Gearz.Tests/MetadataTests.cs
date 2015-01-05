using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Gearz.Tests
{
    [TestClass]
    public class MetadataTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Dictionary<string, object> x = new Dictionary<string, object>();
            x.Add("teste", new {Name = "André", Age = 30});
            var y = JsonConvert.SerializeObject(x);
        }
    }
}
