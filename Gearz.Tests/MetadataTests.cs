using System.Collections.Generic;
using Gearz.Core.Metadata;
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
            x.Add("teste", new { Name = "André", Age = 30 });
            var y = JsonConvert.SerializeObject(x);
        }

        [TestMethod]
        public void TestMetadataRegistration()
        {
            Metadata.Register(new SomeMetadata());
            var metadata = Metadata.GetMetadata();
        }
    }
}
