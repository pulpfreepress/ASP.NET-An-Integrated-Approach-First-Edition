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
    public class PhoneNumberDAOTest {

        // Constants representing first record in database
        private const int FK_EMPLOYEE_ID = 1;
        private const int FK_PHONE_TYPE_ID = 1;
        private const string PHONE_NUMBER = "(703)207-0532";

        // Other database constants
        private const int NUMBER_OF_PHONE_NUMBERS_IN_DATABASE = 4;

        PhoneNumberDAO _dao = null;
        PhoneNumberVO _referenceVO = null;

        [SetUp]
        public void SetUp() {
            _dao = new PhoneNumberDAO();
            PhoneTypeDAO phoneTypeDAO = new PhoneTypeDAO();
            PhoneTypeVO phoneType = phoneTypeDAO.SelectPhoneType(FK_PHONE_TYPE_ID);
            _referenceVO = new PhoneNumberVO(FK_EMPLOYEE_ID, phoneType, PHONE_NUMBER);
        }

        [Test]
        public void SelectAllTest() {
            List<PhoneNumberVO> list = _dao.SelectAllPhoneNumbers();
            Assert.IsTrue(list.Count >= NUMBER_OF_PHONE_NUMBERS_IN_DATABASE);
        }

        [Test]
        public void SelectEmailForEmployeeTest() {
            List<PhoneNumberVO> list = _dao.SelectPhoneNumbersForEmployee(FK_EMPLOYEE_ID);
            Assert.IsTrue(list.Count != 0);
        }

        [Test]
        public void InsertUpdateDeleteTest() {
            PhoneNumberVO vo = new PhoneNumberVO();
            PhoneTypeDAO _phoneTypeDAO = new PhoneTypeDAO();
            vo.EmployeeID = 1;
            vo.PhoneType = _phoneTypeDAO.SelectPhoneType(1);
            vo.PhoneNumber = "(815) 213-2345";
             _dao.InsertPhoneNumber(vo);
             List<PhoneNumberVO> list = _dao.SelectPhoneNumbersForEmployee(1);
            Assert.AreEqual(list[2].EmployeeID, 1);
            Assert.AreEqual(list[2].PhoneType.PhoneTypeID, 1);
            Assert.AreEqual(list[2].PhoneNumber, "(815) 213-2345");
           
            PhoneTypeVO phoneTypeVO = _phoneTypeDAO.SelectPhoneType(2);
            PhoneNumberVO oldPhone = list[2];
            PhoneNumberVO newPhone = new PhoneNumberVO(1, phoneTypeVO, "(444) 444-4444");
            _dao.UpdatePhoneNumber(oldPhone, newPhone);
            list = _dao.SelectPhoneNumbersForEmployee(1);
            Assert.AreEqual(list[2].EmployeeID, newPhone.EmployeeID);
            Assert.AreEqual(list[2].PhoneType.PhoneTypeID, newPhone.PhoneType.PhoneTypeID);
            Assert.AreEqual(list[2].PhoneNumber, newPhone.PhoneNumber);

            _dao.DeletePhoneNumber(list[2]);
            list = _dao.SelectPhoneNumbersForEmployee(1);
            Assert.IsTrue(list.Count == 2);
        }


        
        [Test]
        public void DeleteAllTest() {
            List<PhoneNumberVO> list = _dao.SelectPhoneNumbersForEmployee(2);
            Assert.IsTrue(list.Count == 2);
             _dao.DeleteAllPhoneNumbersForEmployee(2);
             List<PhoneNumberVO> newList = _dao.SelectPhoneNumbersForEmployee(2);
            Assert.IsTrue(newList.Count == 0);

            foreach (PhoneNumberVO vo in list) {
                _dao.InsertPhoneNumber(vo);
            }
            Assert.IsTrue(list.Count == 2);
        }
    } // end PhoneNumberDAOTest class definition
} // end namespace
