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
    public class EmployeeDAO : BaseDAO {
        #region SQL Command Parameter Constants
        // SQL Command Parameter Constants
        private const String EMPLOYEE_ID = "@employeeID";
        private const String FIRST_NAME = "@firstName";
        private const String MIDDLE_NAME = "@middleName";
        private const String LAST_NAME = "@lastName";
        private const String BIRTHDAY = "@birthday";
        private const String PICTURE = "@picture";
        private const String HIRE_DATE = "@hireDate";
        private const String IS_ACTIVE = "@isactive";
        #endregion SQL Command Parameter Constants

        #region SQL Query String Constants
        // SQL Query String Constants
        private const String SELECT_ALL_COLUMNS =
            "SELECT tbl_Employee.EmployeeID, " +
                    "FirstName, " +
                    "MiddleName, " +
                    "LastName, " +
                    "Birthday, " +
                    "HireDate, " +
                    "IsActive, " + 
                    "Picture ";      // now retrieving Picture column

        private const String SELECT_ALL_EMPLOYEES =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Employee ";

        private const String SELECT_ALL_ACTIVE_EMPLOYEES =
            SELECT_ALL_EMPLOYEES +
            "WHERE IsActive = " + IS_ACTIVE;


        private const String SELECT_EMPLOYEE_BY_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Employee " +
            "WHERE EmployeeID = " + EMPLOYEE_ID;

        private const String INSERT_EMPLOYEE =
            "INSERT INTO tbl_Employee " +
                    "(FirstName, MiddleName, LastName, Birthday, Picture, HireDate, IsActive) " +
            "VALUES (" + FIRST_NAME + ", " + MIDDLE_NAME + ", " + LAST_NAME + ", " + BIRTHDAY + "," +
                         PICTURE + ", " + HIRE_DATE + ", " + IS_ACTIVE + ") " +
            "SELECT scope_identity()";

        private const String UPDATE_EMPLOYEE =
            "UPDATE tbl_Employee " +
            "SET FirstName = " + FIRST_NAME + ", " +
                 "MiddleName = " + MIDDLE_NAME + ", " +
                 "LastName = " + LAST_NAME + ", " +
                 "Birthday = " + BIRTHDAY + ", " +
                 "Picture = " + PICTURE + ", " +
                 "HireDate = " + HIRE_DATE + ", " +
                 "IsActive = " + IS_ACTIVE + " " +
            "WHERE EmployeeID = " + EMPLOYEE_ID;


        private const String DELETE_EMPLOYEE =
            "DELETE FROM tbl_Employee " +
            "WHERE EmployeeID = " + EMPLOYEE_ID;

        private const String UPDATE_ISACTIVE_COLUMN =
            "UPDATE tbl_Employee " +
            "SET IsActive = " + IS_ACTIVE + " " +
            "WHERE EmployeeID = " + EMPLOYEE_ID;

        #endregion SQL Query String Constants

        #region Constructor
        public EmployeeDAO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }
        #endregion Constructor

        #region Public Methods

        public List<EmployeeVO> SelectAllEmployees() {
            LogDebug("Entering SelectAllEmployees() method...");

            List<EmployeeVO> list = new List<EmployeeVO>();
            IDataReader reader = null;
            TrainingDAO trainingDAO = new TrainingDAO();
            AddressDAO addressDAO = new AddressDAO();
            EmailAddressDAO emailAddressDAO = new EmailAddressDAO();
            PhoneNumberDAO phoneNumberDAO = new PhoneNumberDAO();

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_EMPLOYEES);
                reader = Database.ExecuteReader(command);
                while (reader.Read()) {
                    list.Add(FillInEmployeeVO(reader, trainingDAO, addressDAO, emailAddressDAO, phoneNumberDAO));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectAllEmployees() method.", e);
                throw new DBException("Exception in SelectAllEmployees() method.", e, BaseException.Severity.ERROR);
            }
            finally {
                base.CloseReader(reader);
            }

            return list;
        }


        public List<EmployeeVO> SelectAllActiveEmployees() {
            LogDebug("Entering SelectAllActiveEmployees() method...");

            List<EmployeeVO> list = new List<EmployeeVO>();
            IDataReader reader = null;
            TrainingDAO trainingDAO = new TrainingDAO();
            AddressDAO addressDAO = new AddressDAO();
            EmailAddressDAO emailAddressDAO = new EmailAddressDAO();
            PhoneNumberDAO phoneNumberDAO = new PhoneNumberDAO();

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_ACTIVE_EMPLOYEES);
                Database.AddInParameter(command, IS_ACTIVE, DbType.Boolean, true);
                reader = Database.ExecuteReader(command);
                while (reader.Read()) {
                    list.Add(FillInEmployeeVO(reader, trainingDAO, addressDAO, emailAddressDAO, phoneNumberDAO));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectAllActiveEmployees() method.", e);
                throw new DBException("Exception in SelectAllActiveEmployees() method.", e, BaseException.Severity.ERROR);
            }
            finally {
                base.CloseReader(reader);
            }

            return list;
        }


        public EmployeeVO SelectEmployee(int id) {
            LogDebug("Entering SelectEmployee() method for EmployeeID = " + id);
            EmployeeVO vo = null;
            IDataReader reader = null;
            TrainingDAO trainingDAO = new TrainingDAO();
            AddressDAO addressDAO = new AddressDAO();
            EmailAddressDAO emailAddressDAO = new EmailAddressDAO();
            PhoneNumberDAO phoneNumberDAO = new PhoneNumberDAO();

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_EMPLOYEE_BY_ID);
                Database.AddInParameter(command, EMPLOYEE_ID, DbType.Int32, id);
                reader = Database.ExecuteReader(command);
                if (reader.Read()) {
                    vo = FillInEmployeeVO(reader, trainingDAO, addressDAO, emailAddressDAO, phoneNumberDAO);
                }
                else {
                    throw new DBException("No employee found for EmployeeID: " + id);
                }
            }
            catch (Exception e) {
                LogError("Error getting employee by EmployeeID " + id, e);
                throw new DBException("Error getting employee by EmployeeID " + id);
            }
            finally {
                base.CloseReader(reader);
            }

            return vo;
        }


        public EmployeeVO InsertEmployee(EmployeeVO vo) {
            LogDebug("Entering InsertEmployee() method with employee: " + vo.ToString());
            
            try {
                
                DbCommand command = Database.GetSqlStringCommand(INSERT_EMPLOYEE);
                Database.AddInParameter(command, FIRST_NAME, DbType.String, vo.FirstName);
                Database.AddInParameter(command, MIDDLE_NAME, DbType.String, vo.MiddleName);
                Database.AddInParameter(command, LAST_NAME, DbType.String, vo.LastName);
                Database.AddInParameter(command, BIRTHDAY, DbType.DateTime, vo.Birthday);
                Database.AddInParameter(command, PICTURE, DbType.Binary, vo.Picture);
                Database.AddInParameter(command, HIRE_DATE, DbType.DateTime, vo.HireDate);
                Database.AddInParameter(command, IS_ACTIVE, DbType.Boolean, vo.IsActive);
                vo.EmployeeID = Convert.ToInt32(Database.ExecuteScalar(command)); 
            }
            catch (Exception e) {
                LogError("Error inserting employee into database!", e);
                throw new DBException("Error inserting employee into database!", e, BaseException.Severity.ERROR);
            }
            return vo;
        }


        public EmployeeVO UpdateEmployee(EmployeeVO vo) {
            LogDebug("Entering the UpdateEmployee() method with Employee: " + vo);
            int rowsAffected = 0;
            try {
                
                DbCommand command = Database.GetSqlStringCommand(UPDATE_EMPLOYEE);
                Database.AddInParameter(command, FIRST_NAME, DbType.String, vo.FirstName);
                Database.AddInParameter(command, MIDDLE_NAME, DbType.String, vo.MiddleName);
                Database.AddInParameter(command, LAST_NAME, DbType.String, vo.LastName);
                Database.AddInParameter(command, BIRTHDAY, DbType.DateTime, vo.Birthday);
                Database.AddInParameter(command, PICTURE, DbType.Binary, vo.Picture);
                Database.AddInParameter(command, HIRE_DATE, DbType.DateTime, vo.HireDate);
                Database.AddInParameter(command, IS_ACTIVE, DbType.Boolean, vo.IsActive);
                Database.AddInParameter(command, EMPLOYEE_ID, DbType.Int32, vo.EmployeeID);
                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Error updating employee record: " + e);
                throw new DBException("Error updating employee record: " + e);
            }
            

            if (rowsAffected == 0) {
                LogError("No rows updated for employee: " + vo);
                throw new DBException("No rows updated for employee: " + vo);
            }
            return vo;
        }


        public void DeleteEmployee(EmployeeVO vo) {
            LogDebug("Entering DeleteEmployee() method with employee: " + vo);
            int rowsAffected = 0;

            try {
                DbCommand command = Database.GetSqlStringCommand(DELETE_EMPLOYEE);
                Database.AddInParameter(command, EMPLOYEE_ID, DbType.Int32, vo.EmployeeID);
                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Error deleting employee from database: " + vo);
                throw new DBException("Error deleting employee from database: " + vo, e);
            }

            if (rowsAffected == 0) {
                LogError("No row deleted from database for employee: " + vo);
                throw new DBException("No row deleted from database for employee: " + vo);
            }
        }


        public void SetEmployeeIsActiveState(int id, bool state) {
            LogDebug("Entering SetEmployeeIsActiveState() for EmployeeID = " + id + " and state = " + state);
            int rowsAffected = 0;

            try {
                DbCommand command = Database.GetSqlStringCommand(UPDATE_ISACTIVE_COLUMN);
                Database.AddInParameter(command, IS_ACTIVE, DbType.Boolean, state);
                Database.AddInParameter(command, EMPLOYEE_ID, DbType.Int32, id);
                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("No rows updated for EmpoyeeID = " + id, e);
                throw new DBException("No rows updated for EmployeeID = " + id, e);
            }

            if (rowsAffected == 0) {
                LogError("EmployeeID " + id + " does not exist in the database.");
                throw new DBException("EmployeeID " + id + " does not exist in the database.");
            }
        }

        #endregion Public Methods


        #region Private Methods

        private EmployeeVO FillInEmployeeVO(IDataReader reader, TrainingDAO trainingDAO,
                                            AddressDAO addressDAO, EmailAddressDAO emailAddressDAO,
                                            PhoneNumberDAO phoneNumberDAO) {
            EmployeeVO vo = new EmployeeVO();

            vo.EmployeeID = reader.GetInt32(0);
            vo.FirstName = reader.GetString(1);
            vo.MiddleName = reader.GetString(2);
            vo.LastName = reader.GetString(3);
            vo.Birthday = reader.GetDateTime(4);
            vo.HireDate = reader.GetDateTime(5);
            vo.IsActive = reader.GetBoolean(6);

            if (!reader.IsDBNull(7)) {
                vo.Picture = (byte[])reader.GetValue(7);
            }

            vo.CompletedCourses = trainingDAO.GetTrainingCompletedByEmployee(vo.EmployeeID);
            vo.Addresses = addressDAO.SelectEmployeeAddresses(vo.EmployeeID);
            vo.EmailAddresses = emailAddressDAO.SelectEmailAddressesForEmployee(vo.EmployeeID);
            vo.PhoneNumbers = phoneNumberDAO.SelectPhoneNumbersForEmployee(vo.EmployeeID);

            return vo;
        }
        #endregion Private Methods

    } // end EmployeeDAO class
} // end namespace
