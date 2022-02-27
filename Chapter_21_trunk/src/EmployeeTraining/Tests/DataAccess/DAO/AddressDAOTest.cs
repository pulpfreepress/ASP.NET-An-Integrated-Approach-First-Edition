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
    public class AddressDAOTest {


        // Address reference data matches first database record
        private const int ADDRESS_ID = 1;
        private const string ADDRESS_1 = "1432 Fox Chase Run";
        private const string ADDRESS_2 = "";
        private const string CITY = "Falls Church";
        private const string STATE = "VA";
        private const string ZIP = "22143";

        // other test constants
        private const int NUMBER_OF_ADDRESSES_IN_DATABASE = 4;

        // fields
        private AddressDAO _addressDAO;
        private AddressVO _referenceAddress;

        [SetUp]
        public void SetUp() {
            _addressDAO = new AddressDAO();
            _referenceAddress = new AddressVO(ADDRESS_ID, ADDRESS_1, ADDRESS_2, CITY, STATE, ZIP);
        }

        [Test]
        public void SelectAllAddressesTest() {
            List<AddressVO> list = _addressDAO.SelectAllAddresses();
            Assert.IsTrue(list.Count == NUMBER_OF_ADDRESSES_IN_DATABASE);
        }


        [Test]
        public void InsertAndDeleteAddressTest() {
            AddressVO vo = new AddressVO();
            vo.Address1 = "7423 Brad Street";
            vo.Address2 = string.Empty;
            vo.City = "Falls Church";
            vo.State = "VA";
            vo.Zip = "22042";
            Assert.IsTrue(vo.AddressID == 0);
            vo = _addressDAO.InsertAddress(vo);
            Assert.IsTrue(vo.AddressID > 0);
            List<AddressVO> list = _addressDAO.SelectAllAddresses();
            Assert.IsTrue(list.Count  > NUMBER_OF_ADDRESSES_IN_DATABASE);
            _addressDAO.DeleteAddress(vo);
            list = _addressDAO.SelectAllAddresses();
            Assert.IsTrue(list.Count == NUMBER_OF_ADDRESSES_IN_DATABASE);
        }

        [Test]
        public void UpdateAddressTest() {
            AddressVO vo = _addressDAO.SelectAddress(1);
            Assert.IsTrue(vo.AddressID == _referenceAddress.AddressID);
            Assert.IsTrue(vo.Address1 == _referenceAddress.Address1);
            Assert.IsTrue(vo.Address2 == _referenceAddress.Address2);
            Assert.IsTrue(vo.City == _referenceAddress.City);
            Assert.IsTrue(vo.State == _referenceAddress.State);
            Assert.IsTrue(vo.Zip == _referenceAddress.Zip);
            vo.Address1 = "Something different";
            vo.Address2 = "Something else different";
            vo.City = "Where on earth?";
            vo.State = "MN";
            vo.Zip = "33333";
            vo = _addressDAO.UpdateAddress(vo);
            Assert.IsTrue(vo.AddressID == _referenceAddress.AddressID);
            Assert.IsTrue(vo.Address1 != _referenceAddress.Address1);
            Assert.IsTrue(vo.Address2 != _referenceAddress.Address2);
            Assert.IsTrue(vo.City != _referenceAddress.City);
            Assert.IsTrue(vo.State != _referenceAddress.State);
            Assert.IsTrue(vo.Zip != _referenceAddress.Zip);
            vo = _addressDAO.UpdateAddress(_referenceAddress);
            Assert.IsTrue(vo.AddressID == _referenceAddress.AddressID);
            Assert.IsTrue(vo.Address1 == _referenceAddress.Address1);
            Assert.IsTrue(vo.Address2 == _referenceAddress.Address2);
            Assert.IsTrue(vo.City == _referenceAddress.City);
            Assert.IsTrue(vo.State == _referenceAddress.State);
            Assert.IsTrue(vo.Zip == _referenceAddress.Zip);
        }


    } //End AddressDAOTest class definition
} // End namespace
