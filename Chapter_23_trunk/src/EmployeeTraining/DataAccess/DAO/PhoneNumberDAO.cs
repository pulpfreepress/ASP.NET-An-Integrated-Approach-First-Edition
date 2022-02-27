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
    public class PhoneNumberDAO : BaseDAO {
        #region SQL Command Constants
        // SQL command parameter constants
        private const string FK_EMPLOYEE_ID = "@fkemployeeid";
        private const string FK_PHONE_TYPE_ID = "@fkphonetypeid";
        private const string PHONE_NUMBER = "@phonenumber";
        private const string NEW_PHONE_NUMBER = "@newphonenumber";
        private const string NEW_FK_PHONE_TYPE_ID = "@newfkphonetypeid";
        #endregion SQL Command Constants

        #region SQL Query String Constants

        private const string SELECT_ALL_COLUMNS =
            "SELECT FK_EmployeeID, FK_PhoneTypeID, PhoneNumber ";

        private const string SELECT_ALL_PHONE_NUMBERS =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Phone_Numbers";

        private const string SELECT_ALL_PHONE_NUMBERS_FOR_EMPLOYEE =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Phone_Numbers " +
            "WHERE FK_EmployeeID = " + FK_EMPLOYEE_ID;

        private const string INSERT_PHONE_NUMBER =
            "INSERT INTO tbl_Phone_Numbers (FK_EmployeeID, FK_PhoneTypeID, PhoneNumber) " +
            "VALUES ( " + FK_EMPLOYEE_ID + ", " + FK_PHONE_TYPE_ID + ", " + PHONE_NUMBER + ")";

        private const string UPDATE_PHONE_NUMBER =
            "UPDATE tbl_Phone_Numbers " +
            "SET PhoneNumber = " + NEW_PHONE_NUMBER + ", " +
                "FK_PhoneTypeID = " + NEW_FK_PHONE_TYPE_ID + " " +
            "WHERE FK_EmployeeID = " + FK_EMPLOYEE_ID + " AND FK_PhoneTypeID = " + FK_PHONE_TYPE_ID +
                                   " AND PhoneNumber = " + PHONE_NUMBER;

        private const string DELETE_ALL_PHONE_NUMBERS_FOR_EMPLOYEE =
            "DELETE FROM tbl_Phone_Numbers " +
            "WHERE FK_EmployeeID = " + FK_EMPLOYEE_ID;

        private const string DELETE_PHONE_NUMBER =
            "DELETE FROM tbl_Phone_Numbers " +
            "WHERE FK_EmployeeID = " + FK_EMPLOYEE_ID + " AND FK_PhoneTypeID = " + FK_PHONE_TYPE_ID + 
                                   " AND PhoneNumber = " + PHONE_NUMBER;


        #endregion SQL Query String Constants


         #region Constructor
        public PhoneNumberDAO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }
        #endregion Constructor



        #region Public Methods

        public List<PhoneNumberVO> SelectAllPhoneNumbers() {
            LogDebug("Entering SelectAllPhoneNumbers() method...");
            List<PhoneNumberVO> list = new List<PhoneNumberVO>();
            IDataReader reader = null;
            

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_PHONE_NUMBERS);
                reader = Database.ExecuteReader(command);
                PhoneTypeDAO phoneTypeDAO = new PhoneTypeDAO();
                while (reader.Read()) {
                    list.Add(FillInPhoneNumberVO(reader,phoneTypeDAO));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectAllPhoneNumbers() method...", e);
                throw new DBException("Exception in SelectAllPhoneNumbers() method...", e);
            }
            finally {
                CloseReader(reader);
                
            }
            return list;
        }


        public List<PhoneNumberVO> SelectPhoneNumbersForEmployee(int employeeID) {
            LogDebug("Entering SelectPhoneNumbersForEmployee() method for employee ID " + employeeID);
            List<PhoneNumberVO> list = new List<PhoneNumberVO>();
            IDataReader reader = null;
            
            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_PHONE_NUMBERS_FOR_EMPLOYEE);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, employeeID);
                reader = Database.ExecuteReader(command);
                PhoneTypeDAO phoneTypeDAO = new PhoneTypeDAO();
                while (reader.Read()) {
                    list.Add(FillInPhoneNumberVO(reader, phoneTypeDAO));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectPhoneNumbersForEmployee() method...", e);
                throw new DBException("Exception in SelectPhoneNumbersForEmployee() method...", e);
            }
            
            return list;
        }


        /************************************************************
         * Inserts a phone number and returns a list of employee's
         * phone numbers.
         * ********************************************************/
        public PhoneNumberVO InsertPhoneNumber(PhoneNumberVO vo) {
            LogDebug("Entering InsertPhoneNumber() method...");
            

            try {
                DbCommand command  = Database.GetSqlStringCommand(INSERT_PHONE_NUMBER);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, vo.EmployeeID);
                Database.AddInParameter(command, FK_PHONE_TYPE_ID, DbType.Int32, vo.PhoneType.PhoneTypeID);
                Database.AddInParameter(command, PHONE_NUMBER, DbType.String, vo.PhoneNumber);
                Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Exception inserting phone number.", e);
                throw new DBException("Exception inserting phone number.", e);
            }


            return vo;
        }

        /***********************************************************
         * Updates an employee's phone number and returns a list of
         * employee's phone numbers.
         * **********************************************************/
        public PhoneNumberVO UpdatePhoneNumber(PhoneNumberVO oldNumber, PhoneNumberVO newNumber) {
            LogDebug("Entering UpdatePhoneNumber() method...");
           
            try {
                 DbCommand command = Database.GetSqlStringCommand(UPDATE_PHONE_NUMBER);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, oldNumber.EmployeeID);
                Database.AddInParameter(command, FK_PHONE_TYPE_ID, DbType.Int32, oldNumber.PhoneType.PhoneTypeID);
                Database.AddInParameter(command, PHONE_NUMBER, DbType.String, oldNumber.PhoneNumber);
                Database.AddInParameter(command, NEW_FK_PHONE_TYPE_ID, DbType.Int32, newNumber.PhoneType.PhoneTypeID);
                Database.AddInParameter(command, NEW_PHONE_NUMBER, DbType.String, newNumber.PhoneNumber);
                Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Exception updating phone number.", e);
                throw new DBException("Exception updating phone number.", e);
            }

            return newNumber;
        }


        public void DeleteAllPhoneNumbersForEmployee(int employeeID) {
            LogDebug("Entering DeleteAllPhoneNumbersForEmployee() method for employeeID: " + employeeID);
            
            try {
               DbCommand command = Database.GetSqlStringCommand(DELETE_ALL_PHONE_NUMBERS_FOR_EMPLOYEE);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, employeeID);
                Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Exception deleting all phone numbers for employee.", e);
                throw new Exception("Exception deleting all phone numbers for employee.", e);
            }
            
        }


        public void DeletePhoneNumber(PhoneNumberVO vo) {
            LogDebug("Entering DeletePhoneNumber() method...");
           
            try {
                 DbCommand command = Database.GetSqlStringCommand(DELETE_PHONE_NUMBER);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.String, vo.EmployeeID);
                Database.AddInParameter(command, FK_PHONE_TYPE_ID, DbType.Int32, vo.PhoneType.PhoneTypeID);
                Database.AddInParameter(command, PHONE_NUMBER, DbType.String, vo.PhoneNumber);
                Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Exception deleting phone number.", e);
                throw new DBException("Exception deleting phone number.", e);
            }
            
        }

        #endregion Public Methods


        #region Private Methods

        private PhoneNumberVO FillInPhoneNumberVO(IDataReader reader, PhoneTypeDAO phoneTypeDAO) {
            PhoneNumberVO vo = new PhoneNumberVO();
            vo.EmployeeID = reader.GetInt32(0);
            vo.PhoneType = phoneTypeDAO.SelectPhoneType(reader.GetInt32(1));
            vo.PhoneNumber = reader.GetString(2);
            return vo;
        }

        #endregion Private Methods
    } // End PhoneNumberDAO class definition
} // End namespace
