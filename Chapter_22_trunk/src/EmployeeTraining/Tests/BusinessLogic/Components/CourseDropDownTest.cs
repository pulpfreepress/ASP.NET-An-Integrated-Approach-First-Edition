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
    public class CourseDropDownTest {

        private CourseDropDown _courseDD;

        [SetUp]
        public void SetUp() {
            _courseDD = new CourseDropDown();
        }


        [Test]
        public void CourseDropDownPopulateTest() {
            _courseDD.PopulateControl();
            Assert.AreEqual(_courseDD.Items[0].Value, "1");
            Assert.AreEqual(_courseDD.Items[0].Text, "IST101-Introduction to Programming");
        }

    } // end CourseDropDownTest class definition
} // end namespace
