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
    public class TrainingDAO : BaseDAO {
        #region SQL Command Parameters
        //SQL command parameter constants
        private const string FK_EMPLOYEE_ID = "@fkEmployeeId";
        private const string FK_COURSE_ID = "@fkCourseID";
        private const string DATE_COMPLETED = "@dateCompleted";
        private const string GRADE = "@grade";
        #endregion

        #region SQL Query String Constants
        //SQL query string constants
        private const string SELECT_ALL_COLUMNS =
            "SELECT FK_EmployeeID, FK_CourseID, DateCompleted, Grade ";

        private const string SELECT_ALL_TRAINING_COMPLETED =
            SELECT_ALL_COLUMNS +
            "FROM tbl_EmployeeCourse_XREF";

        private const string SELECT_COURSES_COMPLETED_BY_EMPLOYEE_ID =
           SELECT_ALL_COLUMNS +
           "FROM tbl_EmployeeCourse_XREF " +
           "WHERE tbl_EmployeeCourse_XREF.FK_EmployeeID = " +FK_EMPLOYEE_ID;

        private const string INSERT_COMPLETED_TRAINING =
            "INSERT INTO tbl_EmployeeCourse_XREF " +
               "(FK_EmployeeID, FK_CourseID, DateCompleted, Grade) " +
             "VALUES (" + FK_EMPLOYEE_ID + ", " + FK_COURSE_ID + ", " + DATE_COMPLETED + ", " + GRADE + ")";


        private const string DELETE_EMPLOYEE_TRAINING =
            "DELETE FROM tbl_EmployeeCourse_XREF " +
            "WHERE FK_EmployeeID = " + FK_EMPLOYEE_ID;
        #endregion

        #region Constructor

        public TrainingDAO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }

        #endregion

        #region Public Methods
        public List<CompletedCourseVO> SelectAllCompletedTraining() {
            LogDebug("Entering SelectAllCompletedTraining() method...");
            List<CompletedCourseVO> list = new List<CompletedCourseVO>();
            IDataReader reader = null;
            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_TRAINING_COMPLETED);
                reader = Database.ExecuteReader(command);
                CourseDAO courseDAO = new CourseDAO();
                while (reader.Read()) {
                    list.Add(FillInCompletedCourseVO(reader, courseDAO));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectAllCompletedTraining() method...", e);
                throw new DBException("Exception in SelectAllCompletedTraining() method...", e);
            }
            finally {
                CloseReader(reader);
            }
            return list;
        }


        public List<CompletedCourseVO> InsertCompletedTraining(CompletedCourseVO vo) {
            LogDebug("Entering InsertCompletedTraining() method with CompletedCourseVO = " + vo);
            List<CompletedCourseVO> list = new List<CompletedCourseVO>();
            try {
                DbCommand command = Database.GetSqlStringCommand(INSERT_COMPLETED_TRAINING);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, vo.EmployeeID);
                Database.AddInParameter(command, FK_COURSE_ID, DbType.Int32, vo.Course.CourseID);
                Database.AddInParameter(command, DATE_COMPLETED, DbType.DateTime, vo.DateCompleted);
                Database.AddInParameter(command, GRADE, DbType.Double, vo.Grade);
                Database.ExecuteScalar(command);
            }
            catch (Exception e) {
                LogError("Error inserting completed training record!", e);
                throw new DBException("Error inserting completed training record!", e);
            }
            return this.GetTrainingCompletedByEmployee(vo.EmployeeID);
        }


        public List<CompletedCourseVO> InsertEmployeeTrainingRecords(EmployeeVO employeeVO) {
            LogDebug("Entering InsertEmployeeTrainingRecords() method with employeeVO = " + employeeVO);
            foreach (CompletedCourseVO completedCourse in employeeVO.CompletedCourses) {
                this.InsertCompletedTraining(completedCourse);
            }
            return this.GetTrainingCompletedByEmployee(employeeVO.EmployeeID);
        }


        public void DeleteEmployeeTrainingRecords(int employeeID) {
            LogDebug("Entering DeleteEmployeeTraining() method for employeeID = " + employeeID);
            try {
                DbCommand command = Database.GetSqlStringCommand(DELETE_EMPLOYEE_TRAINING);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, employeeID);
                Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Error deleting employee training records!", e);
                throw new DBException("Error deleting employee training records!", e);
            }
        }


        public List<CompletedCourseVO> GetTrainingCompletedByEmployee(int employeeID) {
            LogDebug("Entering GetTrainingCompletedByEmployee() method for employeeID = " + employeeID);
            List<CompletedCourseVO> list = new List<CompletedCourseVO>();
            IDataReader reader = null;
            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_COURSES_COMPLETED_BY_EMPLOYEE_ID);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, employeeID);
                reader = Database.ExecuteReader(command);
                CourseDAO courseDAO = new CourseDAO();
                while (reader.Read()) {
                    list.Add(FillInCompletedCourseVO(reader, courseDAO));
                }
            }
            catch (Exception e) {
                LogError("Error selecting courses completed for employeeID = " + employeeID, e);
                throw new DBException("Error selecting courses completed for employeeID = " + employeeID, e);
            }
            finally {
                CloseReader(reader);
            }
            return list;
        }

        #endregion Public Methods

        #region Private Methods

        private CompletedCourseVO FillInCompletedCourseVO(IDataReader reader, CourseDAO courseDAO) {
            CompletedCourseVO vo = new CompletedCourseVO();
            vo.EmployeeID = reader.GetInt32(0);
            vo.Course = courseDAO.SelectCourse(reader.GetInt32(1));
            vo.DateCompleted = reader.GetDateTime(2);
            vo.Grade = reader.GetDouble(3);

            return vo;
        }

        #endregion Private Methods

    } // end TrainingDAO class definition
} // end namespace
