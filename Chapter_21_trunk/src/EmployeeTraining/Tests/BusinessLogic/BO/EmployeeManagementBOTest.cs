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
using BusinessLogic.BO;
using DataAccess.DAO;

namespace Tests.BusinessLogic.BO {

    [TestFixture]
    public class EmployeeManagementBOTest : BaseTest {

        // Reference employee data
        private const string FIRST_NAME = "Rick";
        private const string MIDDLE_NAME = "Warren";
        private const string LAST_NAME = "Miller";
        private readonly DateTime BIRTHDAY = new DateTime(1970, 1, 1);
        private readonly DateTime HIRE_DATE = new DateTime(1998, 12, 1);
        private const bool IS_ACTIVE = true;

        EmployeeVO _newEmployee;
        EmployeeManagementBO _empMgtBO;
        EmployeeDAO _empDAO;
        CourseDAO _courseDAO;
        private Image _referenceImage;
        private MemoryStream _ms;


        [SetUp]
        public void SetUp() {
            _newEmployee = new EmployeeVO();
            _newEmployee.FirstName = "Denise";
            _newEmployee.MiddleName = "Anne";
            _newEmployee.LastName = "Weber";
            _newEmployee.Birthday = new DateTime(1958, 8, 23);
            _newEmployee.HireDate = new DateTime(2012, 6, 12);
            _empMgtBO = new EmployeeManagementBO();
            _empDAO = new EmployeeDAO();
            _courseDAO = new CourseDAO();
            _referenceImage = new Bitmap(@"..\..\Images\ReferenceImage.tif");
            _ms = new MemoryStream();
            _referenceImage.Save(_ms, ImageFormat.Tiff);
        }

        [Test]
        public void GetEmployeeTest() {
            EmployeeVO vo = _empMgtBO.GetEmployee(1);
            Assert.AreEqual(vo.FirstName, FIRST_NAME);
            Assert.AreEqual(vo.MiddleName, MIDDLE_NAME);
            Assert.AreEqual(vo.LastName, LAST_NAME);
            Assert.AreEqual(vo.Birthday, BIRTHDAY);
            Assert.AreEqual(vo.HireDate, HIRE_DATE);
            Assert.AreEqual(vo.IsActive, IS_ACTIVE);
            Assert.IsTrue(CompareImages(vo.Picture, _ms.ToArray()));
        }


        [Test]
        public void GetAllEmployeesTest() {
          List<EmployeeVO> list = _empMgtBO.GetAllEmployees();
          Assert.IsTrue(list.Count > 0);
          Assert.AreEqual(list[0].FirstName, FIRST_NAME);
          Assert.AreEqual(list[0].MiddleName, MIDDLE_NAME);
          Assert.AreEqual(list[0].LastName, LAST_NAME);
          Assert.AreEqual(list[0].Birthday.ToShortDateString(), BIRTHDAY.ToShortDateString());
          Assert.AreEqual(list[0].HireDate.ToShortDateString(), HIRE_DATE.ToShortDateString());
          Assert.IsTrue(CompareImages(list[0].Picture, _ms.ToArray()));    // compare pixels

        }


        [Test]
        public void CreateEmployeeTest() {
            _newEmployee = _empMgtBO.CreateEmployee(_newEmployee);
            Assert.AreNotEqual(_newEmployee.EmployeeID, 0);
            Assert.AreEqual(_newEmployee.FirstName, "Denise");
            Assert.AreEqual(_newEmployee.MiddleName, "Anne");
            Assert.AreEqual(_newEmployee.LastName, "Weber");
            Assert.AreEqual(_newEmployee.Birthday, new DateTime(1958, 8, 23));
            Assert.AreEqual(_newEmployee.HireDate, new DateTime(2012, 6, 12));
            _empDAO.DeleteEmployee(_newEmployee);
        }


        [Test]
        public void UpdateEmployeeTest() {
            EmployeeVO tempEmployee = _empMgtBO.CreateEmployee(_newEmployee);
            Assert.AreNotEqual(tempEmployee.EmployeeID, 0);
            Assert.AreEqual(tempEmployee.FirstName, "Denise");
            Assert.AreEqual(tempEmployee.MiddleName, "Anne");
            Assert.AreEqual(tempEmployee.LastName, "Weber");
            Assert.AreEqual(tempEmployee.Birthday, new DateTime(1958, 8, 23));
            Assert.AreEqual(tempEmployee.HireDate, new DateTime(2012, 6, 12));

            tempEmployee.FirstName = "Jasmine";
            tempEmployee.LastName = "Pai";

            tempEmployee = _empMgtBO.UpdateEmployee(tempEmployee);
            Assert.AreEqual(tempEmployee.FirstName, "Jasmine");
            Assert.AreEqual(tempEmployee.MiddleName, "Anne");
            Assert.AreEqual(tempEmployee.LastName, "Pai");
            Assert.AreEqual(tempEmployee.Birthday, new DateTime(1958, 8, 23));
            Assert.AreEqual(tempEmployee.HireDate, new DateTime(2012, 6, 12));

            _empDAO.DeleteEmployee(tempEmployee);



        }


        [Test]
        public void AddNewTrainingRecordsTest() {
            // Insert new employee
            _newEmployee = _empMgtBO.CreateEmployee(_newEmployee);

            // Get list of courses
            List<CourseVO> courseList = _courseDAO.SelectAllCourses();

            // Should be at least four courses in database for testing
            Assert.IsTrue(courseList.Count > 4);

            // Assign some completed courses to employee
            _newEmployee.CompletedCourses.Add(new CompletedCourseVO(_newEmployee.EmployeeID, courseList[0], DateTime.Now, 100.00));
            _newEmployee.CompletedCourses.Add(new CompletedCourseVO(_newEmployee.EmployeeID, courseList[1], DateTime.Now, 100.00));
            _newEmployee.CompletedCourses.Add(new CompletedCourseVO(_newEmployee.EmployeeID, courseList[2], DateTime.Now, 100.00));

            // Insert employee training records
            _newEmployee = _empMgtBO.InsertEmployeeTrainingRecords(_newEmployee);
            Assert.AreEqual(_newEmployee.CompletedCourses.Count, 3);

            // Add another completed training record
           _newEmployee.CompletedCourses = _empMgtBO.InsertCompletedTrainingRecord(new CompletedCourseVO(_newEmployee.EmployeeID, courseList[3], DateTime.Now, 100.00));
           Assert.AreEqual(_newEmployee.CompletedCourses.Count, 4);

           _empDAO.DeleteEmployee(_newEmployee); // deletes all related training records too!
           
        }
     

    } // end class definition
} // end namespace
