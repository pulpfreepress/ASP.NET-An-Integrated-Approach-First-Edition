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
    public class StateDropDownTest {

        private StateDropDown _stateDD;

        [SetUp]
        public void SetUp() {
            _stateDD = new StateDropDown();
        }


        [Test]
        public void StateDropDownPopulateTest() {
            _stateDD.PopulateControl();
            Assert.AreEqual(_stateDD.Items[0].Value, "AK");
            Assert.AreEqual(_stateDD.Items[0].Text, "Alaska");
        }

    } // end StateDropDownTest class definition
} // end namespace

