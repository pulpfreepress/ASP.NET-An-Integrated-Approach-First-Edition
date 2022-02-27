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
    public class EmailTypeDropDownTest {

        private EmailTypeDropDown _emailTypeDD;

        [SetUp]
        public void SetUp() {
            _emailTypeDD = new EmailTypeDropDown();
        }


        [Test]
        public void EmailTypeDropDownPopulateTest() {
            _emailTypeDD.PopulateControl();
            Assert.AreEqual(_emailTypeDD.Items[0].Value, "1");
            Assert.AreEqual(_emailTypeDD.Items[0].Text, "Personal");
        }

    } // end EmailTypeDropDownTest class definition
} // end namespace


