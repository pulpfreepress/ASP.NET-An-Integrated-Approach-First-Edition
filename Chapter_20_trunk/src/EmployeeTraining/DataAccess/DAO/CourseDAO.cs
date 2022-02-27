using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using Infrastructure.Exceptions;
using Infrastructure.ValueObjects;

namespace DataAccess.DAO {
    public class CourseDAO : BaseDAO {
        #region SQL Command Constants
        // SQL command parameter constants
        private const string COURSE_ID = "@courseid";
        private const string CODE = "@code";
        private const string TITLE = "@title";
        private const string DESCRIPTION = "@description";
        #endregion SQL Command Constants

        #region SQL Query String Constants
        // SQL string constants
        private const string SELECT_ALL_COLUMNS =
            "SELECT tbl_Course_LU.CourseID, " +
                    "Code, " +
                    "Title, " +
                    "Description ";

        private const string SELECT_ALL_COURSES =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Course_LU";

        private const string SELECT_COURSE_BY_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Course_LU " +
            "WHERE CourseID = " + COURSE_ID;


        private const string INSERT_COURSE =
            "INSERT INTO tbl_Course_LU " +
               "(Code, Title, Description) " +
            "VALUES (" + CODE + ", " + TITLE + ", " + DESCRIPTION + ") " +
            "SELECT scope_identity()";


        private const string UPDATE_COURSE =
            "UPDATE tbl_Course_LU " +
            "SET Code = " + CODE + ", " +
                 "Title = " + TITLE + ", " +
                 "Description = " + DESCRIPTION + " " +
            "WHERE CourseID = " + COURSE_ID;

        private const string DELETE_COURSE =
            "DELETE FROM tbl_Course_LU " +
            "WHERE CourseID = " + COURSE_ID;
        #endregion SQL Query String Constants


        #region Constructor
        public CourseDAO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }
        #endregion Constructor

        #region Public Methods

        public List<CourseVO> SelectAllCourses() {
            LogDebug("Entering SelectAllCourses() method...");
            List<CourseVO> list = new List<CourseVO>();
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_COURSES);
                reader = Database.ExecuteReader(command);
                while (reader.Read()) {
                    list.Add(FillInCourseVO(reader));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectAllCourses() method...", e);
                throw new DBException("Exception in SelectAllCourses() method...", e);
            }
            finally {
                CloseReader(reader);
            }

            return list;
        }


        public CourseVO SelectCourse(int id) {
            LogDebug("Entering SelectCourse() method with CourseID = " + id);
            CourseVO vo = null;
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_COURSE_BY_ID);
                Database.AddInParameter(command, COURSE_ID, DbType.Int32, id);
                reader = Database.ExecuteReader(command);
                if (reader.Read()) {
                    vo = FillInCourseVO(reader);
                }
                else {
                    throw new DBException("No course found for CourseID: " + id);
                }
            }
            catch (Exception e) {
                LogError("Error getting course by CourseID " + id, e);
                throw new DBException("Error getting course by CourseID " + id);
            }
            finally {
                base.CloseReader(reader);
            }

            return vo;
        }



        public CourseVO InsertCourse(CourseVO vo) {
            LogDebug("Entering InsertCourse() method for course: " + vo);
            int courseID = 0;

            try {
                DbCommand command = Database.GetSqlStringCommand(INSERT_COURSE);
                Database.AddInParameter(command, CODE, DbType.String, vo.Code);
                Database.AddInParameter(command, TITLE, DbType.String, vo.Title);
                Database.AddInParameter(command, DESCRIPTION, DbType.String, vo.Description);
                courseID = Convert.ToInt32(Database.ExecuteScalar(command));
            }
            catch (Exception e) {
                LogError("Exception inserting course into database!", e);
                throw new DBException("Exception inserting course into database!", e);
            }

            return SelectCourse(courseID);
        }


        public CourseVO UpdateCourse(CourseVO vo) {
            LogDebug("Entering the UpdateCourse() method with course: " + vo);
            int rowsAffected = 0;

            try {
                DbCommand command = Database.GetSqlStringCommand(UPDATE_COURSE);
                Database.AddInParameter(command, CODE, DbType.String, vo.Code);
                Database.AddInParameter(command, TITLE, DbType.String, vo.Title);
                Database.AddInParameter(command, DESCRIPTION, DbType.String, vo.Description);
                Database.AddInParameter(command, COURSE_ID, DbType.Int32, vo.CourseID);
                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Error updating course record: " + e);
                throw new DBException("Error updating course record: " + e);
            }

            if (rowsAffected == 0) {
                LogError("No rows updated for course: " + vo);
                throw new DBException("No rows updated for course: " + vo);
            }
            return SelectCourse(vo.CourseID);
        }



        public void DeleteCourse(CourseVO vo) {
            LogDebug("Entering DeleteCourse() method with course: " + vo);
            int rowsAffected = 0;

            try {
                DbCommand command = Database.GetSqlStringCommand(DELETE_COURSE);
                Database.AddInParameter(command, COURSE_ID, DbType.Int32, vo.CourseID);
                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Error deleting course from database: " + vo);
                throw new DBException("Error deleting course from database: " + vo, e);
            }

            if (rowsAffected == 0) {
                LogError("No row deleted from database for course: " + vo);
                throw new DBException("No row deleted from database for course: " + vo);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private CourseVO FillInCourseVO(IDataReader reader) {
            CourseVO vo = new CourseVO();
            vo.CourseID = reader.GetInt32(0);
            vo.Code = reader.GetString(1);
            vo.Title = reader.GetString(2);
            vo.Description = reader.GetString(3);
            return vo;
        }

        #endregion Private Methods


    } // end CourseDAO class definition
} // end namespace
