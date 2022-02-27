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
    public class UserRolesDropDownTest {

        private UserRolesDropDown _rolesDD;

        [SetUp]
        public void SetUp() {
            _rolesDD = new UserRolesDropDown();
        }


        [Test]
        public void CourseDropDownPopulateTest() {
            _rolesDD.PopulateControl();
            Assert.AreEqual(_rolesDD.Items[0].Value, "1");
            Assert.AreEqual(_rolesDD.Items[0].Text, "Administrator");
        }

    } // end CourseDropDownTest class definition
} // end namespace

