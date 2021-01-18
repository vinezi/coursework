using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Test
{
    [TestClass]
    public class test
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                var path = @"..\..\TabMAIN.xaml";
                var exist = File.Exists(path);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                var path = @"..\..\TabNOTES.xaml";
                var exist = File.Exists(path);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            try
            {
                var path = @"..\..\TabSCHDULE.xaml";
                var exist = File.Exists(path);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestMethod4()
        {
            try
            {
                var path = @"..\..\TabSETTING.xaml";
                var exist = File.Exists(path);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestMethod5()
        {
            try
            {
                var path = @"..\..\LangEN.xaml";
                var exist = File.Exists(path);
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestMethod6()
        {
            try
            {
                var path = @"..\..\LangRU.xaml";
                var exist = File.Exists(path);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
