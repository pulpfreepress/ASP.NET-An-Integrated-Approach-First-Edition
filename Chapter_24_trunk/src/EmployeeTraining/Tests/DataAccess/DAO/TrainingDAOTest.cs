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
using System.Transactions;

using Infrastructure.ValueObjects;
using Infrastructure.Exceptions;
using DataAccess.DAO;

namespace Tests.DataAccess.DAO {
    
    [TestFixture]
    public class TrainingDAOTest {

        private EmployeeVO newEmployee;
        private CourseDAO _courseDAO;
        private EmployeeDAO _employeeDAO;
        private TrainingDAO _trainingDAO;

        [SetUp]
        public void SetUp() {
            newEmployee = new EmployeeVO();
            newEmployee.FirstName = "Diana";
            newEmployee.MiddleName = "Lee";
            newEmployee.LastName = "Bath";
            newEmployee.Birthday = new DateTime(1961, 3, 3);
            newEmployee.HireDate = new DateTime(2012, 12, 24);
            newEmployee.Username = "dbath";
            newEmployee.LoginHash = "fhdjafhajkfhjakshfjadfhjahjahajfhajhaj";
            newEmployee.UserRole.RoleID = 2;
            newEmployee.IsActive = true;

            _courseDAO = new CourseDAO();
            _employeeDAO = new EmployeeDAO();
            _trainingDAO = new TrainingDAO();
        }


        [Test]
        public void InsertAndDeleteTrainingTest() {
            // Insert new employee
            newEmployee = _employeeDAO.InsertEmployee(newEmployee);

            // Get list of courses
            List<CourseVO> courseList = _courseDAO.SelectAllCourses();

            // Should be at least four courses in database for testing
            Assert.IsTrue(courseList.Count > 4);

            // Assign some completed courses to employee
            newEmployee.CompletedCourses.Add(new CompletedCourseVO(newEmployee.EmployeeID, courseList[0], DateTime.Now, 100.00));
            newEmployee.CompletedCourses.Add(new CompletedCourseVO(newEmployee.EmployeeID, courseList[1], DateTime.Now, 100.00));
            newEmployee.CompletedCourses.Add(new CompletedCourseVO(newEmployee.EmployeeID, courseList[2], DateTime.Now, 100.00));

            // Insert one completed employee training into tbl_EmployeeCourse_XREF table
            _trainingDAO.InsertCompletedTraining(new CompletedCourseVO(newEmployee.EmployeeID, courseList[3], DateTime.Now, 100.00));
            
            // Retrieve completed training record
            List<CompletedCourseVO> employeeTrainingList = _trainingDAO.GetTrainingCompletedByEmployee(newEmployee.EmployeeID);
            Assert.IsTrue(employeeTrainingList.Count == 1); 

            // Insert remaining training
            newEmployee.CompletedCourses = _trainingDAO.InsertEmployeeTrainingRecords(newEmployee);
            Assert.IsTrue(newEmployee.CompletedCourses.Count == 4);

            // Delete all employee training records
            _trainingDAO.DeleteEmployeeTrainingRecords(newEmployee.EmployeeID);

            // See if the employee has completed any training .. should return empty list
            newEmployee.CompletedCourses = _trainingDAO.GetTrainingCompletedByEmployee(newEmployee.EmployeeID);
            Assert.IsTrue(newEmployee.CompletedCourses.Count == 0);

            // Delete employee
            _employeeDAO.DeleteEmployee(newEmployee);
        }


        [Test]
        public void EmployeeCourseXREFCascadeDeleteTest() {
            // Insert new employee
            newEmployee = _employeeDAO.InsertEmployee(newEmployee);

            // Get list of courses
            List<CourseVO> courseList = _courseDAO.SelectAllCourses();

            // Should be at least four courses in database for testing
            Assert.IsTrue(courseList.Count > 4);

           // Add some completed courses to employee
            newEmployee.CompletedCourses.Add(new CompletedCourseVO(newEmployee.EmployeeID, courseList[0], DateTime.Now, 100.00));
            newEmployee.CompletedCourses.Add(new CompletedCourseVO(newEmployee.EmployeeID, courseList[1], DateTime.Now, 100.00));
            newEmployee.CompletedCourses.Add(new CompletedCourseVO(newEmployee.EmployeeID, courseList[2], DateTime.Now, 100.00));

            // Insert completed training
            _trainingDAO.InsertEmployeeTrainingRecords(newEmployee);

            // Retrieve completed training
            newEmployee.CompletedCourses = _trainingDAO.GetTrainingCompletedByEmployee(newEmployee.EmployeeID);
            Assert.IsTrue(newEmployee.CompletedCourses.Count == 3);

            // Delete employee
            _employeeDAO.DeleteEmployee(newEmployee);

            // Should be no training for this employee
            List<CompletedCourseVO> completedCourseList = _trainingDAO.GetTrainingCompletedByEmployee(newEmployee.EmployeeID);
            Assert.IsTrue(completedCourseList.Count == 0);
        }
    } // end TrainingDAOTest class definition
} // end namespace
