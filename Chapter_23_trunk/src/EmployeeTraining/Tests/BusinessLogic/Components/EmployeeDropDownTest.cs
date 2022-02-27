using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


using NUnit.Framework;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

using BusinessLogic.Components;


namespace Tests.BusinessLogic.Components {
    [TestFixture]
    public class EmployeeDropDownTest {

        private EmployeeDropDown _employeeDD;

        [SetUp]
        public void SetUp() {
            _employeeDD = new EmployeeDropDown();
        }


        [Test]
        public void EmployeeDropDownPopulateTest() {
            _employeeDD.PopulateControl();
            Assert.AreEqual(_employeeDD.Items[0].Value, "4");
            Assert.AreEqual(_employeeDD.Items[0].Text, "Coats, Nancy Jo");
        }

    } // end CourseDropDownTest class definition
} // end namespace
