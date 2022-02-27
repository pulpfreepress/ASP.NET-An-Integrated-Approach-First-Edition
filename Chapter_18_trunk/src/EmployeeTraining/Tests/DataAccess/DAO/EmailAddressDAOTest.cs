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
    public class EmailAddressDAOTest {

     // Constants representing first record in database
        private const int FK_EMPLOYEE_ID = 1;
        private const int FK_EMAIL_TYPE_ID = 1;
        private const string EMAIL_ADDRESS = "rick@warrenworks.com";

     // Other database constants
        private const int NUMBER_OF_EMAIL_ADDRESSES_IN_DATABASE = 4;

        EmailAddressDAO _dao = null;
        EmailVO         _referenceVO = null;

        [SetUp]
        public void SetUp() {
            _dao = new EmailAddressDAO();
            EmailTypeDAO emailTypeDAO = new EmailTypeDAO();
            EmailTypeVO emailType = emailTypeDAO.SelectEmailType(FK_EMAIL_TYPE_ID);
            _referenceVO = new EmailVO(FK_EMPLOYEE_ID, emailType, EMAIL_ADDRESS);
        }

        [Test]
        public void SelectAllTest() {
            List<EmailVO> list = _dao.SelectAllEmailAddresses();
            Assert.IsTrue(list.Count >= NUMBER_OF_EMAIL_ADDRESSES_IN_DATABASE);
        }

        [Test]
        public void SelectEmailForEmployeeTest() {
            List<EmailVO> list = _dao.SelectEmailAddressesForEmployee(FK_EMPLOYEE_ID);
            Assert.IsTrue(list.Count != 0);
        }

        [Test]
        public void InsertUpdateDeleteTest() {
            EmailVO vo = new EmailVO();
            EmailTypeDAO _emailTypeDAO = new EmailTypeDAO();
            vo.EmployeeID = 1;
            vo.EmailType = _emailTypeDAO.SelectEmailType(1);
            vo.EmailAddress = "rick@pulpfreepress.com";
            _dao.InsertEmailAddress(vo);
            List<EmailVO> list = _dao.SelectEmailAddressesForEmployee(vo.EmployeeID);
            Assert.AreEqual(list[2].EmployeeID, 1);
            Assert.AreEqual(list[2].EmailType.EmailTypeID, 1);
            Assert.AreEqual(list[2].EmailAddress, "rick@pulpfreepress.com");
            /*************/
            EmailTypeVO emailTypeVO = _emailTypeDAO.SelectEmailType(2);
            EmailVO oldEmail = list[2];
            EmailVO newEmail = new EmailVO(1, emailTypeVO, "sales@pulpfreepress.com");
            _dao.UpdateEmailAddress(oldEmail, newEmail);
            list = _dao.SelectEmailAddressesForEmployee(newEmail.EmployeeID);
            Assert.AreEqual(list[2].EmployeeID, newEmail.EmployeeID);
            Assert.AreEqual(list[2].EmailType.EmailTypeID, newEmail.EmailType.EmailTypeID);
            Assert.AreEqual(list[2].EmailAddress, newEmail.EmailAddress);


            /************/
            _dao.DeleteEmailAddress(list[2]);
            list = _dao.SelectEmailAddressesForEmployee(1);
            Assert.IsTrue(list.Count == 2);
        }

        [Test]
        public void DeleteAllTest() {
            List<EmailVO> list = _dao.SelectEmailAddressesForEmployee(3);
            Assert.IsTrue(list.Count == 1);
             _dao.DeleteAllEmailAddressesForEmployee(3);
             List<EmailVO> newList = _dao.SelectEmailAddressesForEmployee(3);
            Assert.IsTrue(newList.Count == 0);
            
            foreach(EmailVO vo in list){
                _dao.InsertEmailAddress(vo);
            }
            Assert.IsTrue(list.Count == 1);
        }

    } // end EmailAddressDAOTest class definition
} // end Namespace
