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
    public class CourseDAOTest {

       // course reference data matches first database record
        private const int COURSE_ID = 1;
        private const string CODE = "IST101";
        private const string TITLE = "Introduction to Programming";
        private const string DESCRIPTION = "Description test here...";


        // other test constants
        private const int NUMBER_OF_COURSES_IN_DATABASE = 5;

        // fields
        private CourseDAO _courseDAO;
        private EmployeeDAO _employeeDAO;
        private CourseVO _referenceCourse;


        [SetUp]
        public void SetUp() {
            _courseDAO = new CourseDAO();
            _employeeDAO = new EmployeeDAO();
            _referenceCourse = new CourseVO(COURSE_ID, CODE, TITLE, DESCRIPTION);
        }

        [Test]
        public void SelectAllCoursesTest() {
            List<CourseVO> list = _courseDAO.SelectAllCourses();
            Assert.IsTrue(list.Count == NUMBER_OF_COURSES_IN_DATABASE);
        }


        [Test]
        public void InsertAndDeleteCourseTest() {
            CourseVO vo = new CourseVO();
            vo.Code = "FRE201";
            vo.Title = "Intermediate French";
            vo.Description = "Dites-moi quelque choses en francais!";
            Assert.IsTrue(vo.CourseID == 0);
            vo = _courseDAO.InsertCourse(vo);
            Assert.IsTrue(vo.CourseID > 0);
            List<CourseVO> list = _courseDAO.SelectAllCourses();
            Assert.IsTrue(list.Count == (NUMBER_OF_COURSES_IN_DATABASE + 1));
            _courseDAO.DeleteCourse(vo);
            list = _courseDAO.SelectAllCourses();
            Assert.IsTrue(list.Count == NUMBER_OF_COURSES_IN_DATABASE);
        }


        [Test]
        public void UpdateCourseTest() {
            CourseVO vo = _courseDAO.SelectCourse(1);
            Assert.IsTrue(vo.CourseID == _referenceCourse.CourseID);
            Assert.IsTrue(vo.Code == _referenceCourse.Code);
            Assert.IsTrue(vo.Title == _referenceCourse.Title);
            Assert.IsTrue(vo.Description == _referenceCourse.Description);
            vo.Code = "1234";
            vo.Title = "Changed";
            vo.Description = "Changed here too...";
            vo = _courseDAO.UpdateCourse(vo);
            Assert.IsTrue(vo.CourseID == _referenceCourse.CourseID);
            Assert.IsTrue(vo.Code == "1234");
            Assert.IsTrue(vo.Title == "Changed");
            Assert.IsTrue(vo.Description == "Changed here too...");
            vo.Code = _referenceCourse.Code;
            vo.Title = _referenceCourse.Title;
            vo.Description = _referenceCourse.Description;
            vo = _courseDAO.UpdateCourse(vo);
            Assert.IsTrue(vo.CourseID == _referenceCourse.CourseID);
            Assert.IsTrue(vo.Code == _referenceCourse.Code);
            Assert.IsTrue(vo.Title == _referenceCourse.Title);
            Assert.IsTrue(vo.Description == _referenceCourse.Description);
        }



    } // end class definition
} // end namespace
