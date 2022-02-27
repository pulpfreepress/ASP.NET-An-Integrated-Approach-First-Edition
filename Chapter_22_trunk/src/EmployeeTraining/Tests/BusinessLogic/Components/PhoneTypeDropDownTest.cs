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
    public class PhoneTypeDropDownTest {

        private PhoneTypeDropDown _phoneTypeDD;

        [SetUp]
        public void SetUp() {
            _phoneTypeDD = new PhoneTypeDropDown();
        }


        [Test]
        public void PhoneTypeDropDownPopulateTest() {
            _phoneTypeDD.PopulateControl();
            Assert.AreEqual(_phoneTypeDD.Items[0].Value, "1");
            Assert.AreEqual(_phoneTypeDD.Items[0].Text, "Home");
        }

    } // end PhoneTypeDropDownTest class definition
} // end namespace

