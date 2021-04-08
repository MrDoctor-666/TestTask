using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ConsoleApp1;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Company company = new Company(3, 7);
            company.CreateDepartment(false, 3, 1, 2);
            company.CreateDepartment(true, 1, 3, 1, 3, 3, 3, 3);
            company.CreateDepartment(false, 5, 3, 2);
            Assert.IsFalse(company.StartDetour(2, 3));
            List<int> a = new List<int>();
            Assert.IsFalse(company.WasVisited(1, out a));
            Assert.AreEqual(a.Count, 0);
            Assert.IsTrue(company.WasVisited(2, out a));
            Assert.AreEqual(a.Count, 0);
            Assert.IsTrue(company.WasVisited(3, out a));
            Assert.AreEqual(a.Count, 1);
            Assert.AreEqual(a[0], 5);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Company company = new Company(5, 7);
            company.CreateDepartment(true, 1, 6, 4, 7, 6, 3, 2);
            company.CreateDepartment(false, 2, 1, 4);
            company.CreateDepartment(false, 3, 2, 1);
            company.CreateDepartment(true, 4, 3, 5, 1, 7, 3, 3);
            company.CreateDepartment(false, 5, 3, 2);
            Assert.IsFalse(company.StartDetour(1, 5));

            List<List<int>> b = new List<List<int>>();
            Assert.IsTrue(company.WasVisited(1, out b));
            Assert.AreEqual(b.Count, 2);
            Assert.AreEqual(b[0][0], 6);
            Assert.AreEqual(b[1][0], 7);
            Assert.AreEqual(b[1][1], 3);
            Assert.AreEqual(b[1][2], 1);


            Assert.IsTrue(company.WasVisited(2, out b));
            Assert.AreEqual(b.Count, 1);
            Assert.IsTrue(company.WasVisited(3, out b));
            Assert.AreEqual(b.Count, 1);
            Assert.IsTrue(company.WasVisited(4, out b));
            Assert.AreEqual(b.Count, 2);
            Assert.IsTrue(company.WasVisited(5, out b));
            Assert.AreEqual(b.Count, 1);
        }
        [TestMethod]
        public void TestMethodEndless()
        {
            Company company = new Company(3, 7);
            company.CreateDepartment(false, 3, 1, 2);
            company.CreateDepartment(false, 3, 1, 1);
            company.CreateDepartment(false, 3, 1, 3);
            Assert.IsTrue(company.StartDetour(1, 3));
        }
        [TestMethod]
        public void TestError1()
        {
            Company company1 = new Company(2, 7);
            company1.CreateDepartment(false, 3, 1, 2);
            company1.CreateDepartment(true, 1, 3, 1, 3, 3, 3, 1);
            try { company1.CreateDepartment(false, 5, 3, 2); }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "You have enough Departments");
            }
        }
        [TestMethod]
        public void TestError2()
        {
            Company company2 = new Company(2, 7);
            company2.CreateDepartment(false, 3, 1, 2);
            try
            {
                company2.CreateDepartment(true, 1, 3, 1, 3, 3, 3, 3);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Wrong department number");
            }
        }
        [TestMethod]
        public void TestError3()
        {
            Company company3 = new Company(2, 7);
            company3.CreateDepartment(false, 3, 1, 2);
            try
            {
                company3.StartDetour(1, 2);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Some departments don't exist");
            }
        }
        [TestMethod]
        public void TestError4()
        {
            Company company4 = new Company(2, 7);
            try
            {
                company4.CreateDepartment(true, 1, 8, 1, 3, 3, 3, 3);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Wrong stamp number");
            }
        }
    }
}
