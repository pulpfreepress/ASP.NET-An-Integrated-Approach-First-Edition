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

using Infrastructure.ValueObjects;
using Infrastructure.Exceptions;
using DataAccess.DAO;


namespace Tests.DataAccess.DAO {
    
    [TestFixture]
    public class PhoneTypeDAOTest {

        // Email reference data matches first database record
        private const int PHONE_TYPE_ID = 1;
        private const string DESCRIPTION = "Home";


        // other test constants
        private const int NUMBER_OF_PHONE_TYPES_IN_DATABASE = 3;

        // fields
        private PhoneTypeDAO _phoneTypeDAO;
        private PhoneTypeVO _referencePhoneType;

        [SetUp]
        public void SetUp() {
            _phoneTypeDAO = new PhoneTypeDAO();
            _referencePhoneType = new PhoneTypeVO(PHONE_TYPE_ID, DESCRIPTION);
        }

        [Test]
        public void SelectAllPhoneTypesTest() {
            List<PhoneTypeVO> list = _phoneTypeDAO.SelectAllPhoneTypes();
            Assert.IsTrue(list.Count == NUMBER_OF_PHONE_TYPES_IN_DATABASE);
        }

        [Test]
        public void SelectPhoneTypeTest() {
            PhoneTypeVO vo = _phoneTypeDAO.SelectPhoneType(PHONE_TYPE_ID);
            Assert.IsTrue(vo.PhoneTypeID == _referencePhoneType.PhoneTypeID);
            Assert.IsTrue(vo.Description == _referencePhoneType.Description);
        }
    } // End PhoneTypeDAOTest class definition
} // End namespace
