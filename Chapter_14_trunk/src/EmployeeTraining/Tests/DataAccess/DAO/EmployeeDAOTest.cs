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
    public class EmployeeDAOTest : BaseTest {
        // Reference employee data
        private const string FIRST_NAME = "Rick";
        private const string MIDDLE_NAME = "Warren";
        private const string LAST_NAME = "Miller";
        private readonly DateTime BIRTHDAY = new DateTime(1970, 1, 1);
        private readonly DateTime HIRE_DATE = new DateTime(1998, 12, 1);
        private const bool IS_ACTIVE = true;



        private EmployeeDAO _employeeDAO;
        private Image _referenceImage;
        private MemoryStream _ms;



        [SetUp]
        public void SetUp() {
            _employeeDAO = new EmployeeDAO();
            _referenceImage = new Bitmap(@"..\..\Images\ReferenceImage.tif");
            _ms = new MemoryStream();
            _referenceImage.Save(_ms, ImageFormat.Tiff);
        }

        [Test]
        public void SelectAllEmployeesTest() {
            List<EmployeeVO> list = _employeeDAO.SelectAllEmployees();

            Assert.IsTrue(list.Count > 0);
            Assert.AreEqual(list[0].FirstName, FIRST_NAME);
            Assert.AreEqual(list[0].MiddleName, MIDDLE_NAME);
            Assert.AreEqual(list[0].LastName, LAST_NAME);
            Assert.AreEqual(list[0].Birthday.ToShortDateString(), BIRTHDAY.ToShortDateString());
            Assert.AreEqual(list[0].HireDate.ToShortDateString(), HIRE_DATE.ToShortDateString());

            Assert.AreEqual(list[0].Picture.Length, _ms.ToArray().Length);  // compare length
            Assert.IsTrue(CompareImages(list[0].Picture, _ms.ToArray()));    // compare pixels
        }

        [Test]
        public void SelectEmployeeByIDTest() {
            EmployeeVO vo = _employeeDAO.SelectEmployee(1);
            Assert.AreEqual(vo.FirstName, FIRST_NAME);
            Assert.AreEqual(vo.MiddleName, MIDDLE_NAME);
            Assert.AreEqual(vo.LastName, LAST_NAME);
            Assert.AreEqual(vo.Birthday.ToShortDateString(), BIRTHDAY.ToShortDateString());
            Assert.AreEqual(vo.HireDate.ToShortDateString(), HIRE_DATE.ToShortDateString());
            Assert.AreEqual(vo.Picture.Length, _ms.ToArray().Length); // compare length
            Assert.IsTrue(CompareImages(vo.Picture, _ms.ToArray()));  // compare pixels
        }


        [Test]
        public void UpdateEmployeeTest() {
            EmployeeVO vo = _employeeDAO.SelectEmployee(1);
            vo.FirstName = "John";
            vo = _employeeDAO.UpdateEmployee(vo);
            Assert.AreNotEqual(vo.FirstName, FIRST_NAME);
            vo.FirstName = FIRST_NAME;
            vo = _employeeDAO.UpdateEmployee(vo);
            Assert.AreEqual(vo.FirstName, FIRST_NAME);
        }



        [Test]
        public void DeleteEmployeeTest() {
            EmployeeVO newEmployee = new EmployeeVO();
            newEmployee.FirstName = "Jose";
            newEmployee.MiddleName = "Miguel";
            newEmployee.LastName = "Pi";
            newEmployee.Birthday = new DateTime(1965, 11, 12);
            newEmployee.HireDate = new DateTime(2005, 4, 3);
            newEmployee = _employeeDAO.InsertEmployee(newEmployee);
           _employeeDAO.DeleteEmployee(newEmployee);

            try {
                _employeeDAO.SelectEmployee(newEmployee.EmployeeID);
                Assert.Fail("No exception thrown when trying to delete non-existent employee!");
            }
            catch (DBException) {
                // ignore because this is what we expect
            }
        }




        [Test]
        public void SetEmployeeIsActiveStateTest() {
            EmployeeVO vo = _employeeDAO.SelectEmployee(1);
            Assert.AreEqual(vo.IsActive, IS_ACTIVE);
            _employeeDAO.SetEmployeeIsActiveState(vo.EmployeeID, false); // set to false
            vo = _employeeDAO.SelectEmployee(vo.EmployeeID);
            Assert.AreNotEqual(vo.IsActive, IS_ACTIVE); // should now be unequal
            _employeeDAO.SetEmployeeIsActiveState(vo.EmployeeID, true);
            vo = _employeeDAO.SelectEmployee(vo.EmployeeID);
            Assert.AreEqual(vo.IsActive, IS_ACTIVE); // should now be equal
        }



    } // end EmployeeDAOTest class
} // end namespace
