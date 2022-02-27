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
    public class EmailTypeDAOTest {

        // Email reference data matches first database record
        private const int EMAIL_TYPE_ID = 1;
        private const string DESCRIPTION = "Personal";
        
        // other test constants
        private const int NUMBER_OF_EMAIL_TYPES_IN_DATABASE = 2;

        // fields
        private EmailTypeDAO _emailTypeDAO;
        private EmailTypeVO _referenceEmailType;

        [SetUp]
        public void SetUp() {
            _emailTypeDAO = new EmailTypeDAO();
            _referenceEmailType = new EmailTypeVO(EMAIL_TYPE_ID, DESCRIPTION);
        }

        [Test]
        public void SelectAllEmailTypesTest() {
            List<EmailTypeVO> list = _emailTypeDAO.SelectAllEmailTypes();
            Assert.IsTrue(list.Count == NUMBER_OF_EMAIL_TYPES_IN_DATABASE);
        }

        [Test]
        public void SelectEmailTypeTest() {
            EmailTypeVO vo = _emailTypeDAO.SelectEmailType(EMAIL_TYPE_ID);
            Assert.IsTrue(vo.EmailTypeID == _referenceEmailType.EmailTypeID);
            Assert.IsTrue(vo.Description == _referenceEmailType.Description);
        }

        [Test]
        public void InsertUpdateDeleteEmailTypeTest() {
            EmailTypeVO vo = new EmailTypeVO();
            vo.Description = "New Email Type";
            vo = _emailTypeDAO.InsertEmailType(vo);
            Assert.IsTrue(vo.EmailTypeID != 0);

            string old_description = vo.Description;
            vo.Description = "An updated description";
            _emailTypeDAO.UpdateEmailType(vo);
            vo = _emailTypeDAO.SelectEmailType(vo.EmailTypeID);
            Assert.AreNotEqual(old_description, vo.Description);

            _emailTypeDAO.DeleteEmailType(vo.EmailTypeID);
            
        }


    } // End EmailTypeDAOTest class definition
} // End Namespace
