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
    public class EmailAddressDAO : BaseDAO {
        #region SQL Command Constants
        // SQL command parameter constants
        private const string FK_EMPLOYEE_ID = "@fkemployeeid";
        private const string FK_EMAIL_TYPE_ID = "@fkemailtypeid";
        private const string EMAIL_ADDRESS = "@emailaddress";
        private const string NEW_EMAIL_ADDRESS = "@newemailaddress";
        private const string NEW_FK_EMAIL_TYPE_ID = "@newfkphonetypeid";
        #endregion SQL Command Constants

        #region SQL Query String Constants

        private const string SELECT_ALL_COLUMNS =
            "SELECT FK_EmployeeID, FK_EmailTypeID, EmailAddress ";

        private const string SELECT_ALL_EMAIL_ADDRESSES =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Email_Addresses";

        private const string SELECT_ALL_EMAIL_ADDRESSES_FOR_EMPLOYEE =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Email_Addresses " +
            "WHERE FK_EmployeeID = " + FK_EMPLOYEE_ID;

        private const string INSERT_EMAIL_ADDRESS =
            "INSERT INTO tbl_Email_Addresses (FK_EmployeeID, FK_EmailTypeID, EmailAddress) " +
            "VALUES ( " + FK_EMPLOYEE_ID + ", " + FK_EMAIL_TYPE_ID + ", " + EMAIL_ADDRESS + ")";

        private const string UPDATE_EMAIL_ADDRESS =
            "UPDATE tbl_Email_Addresses " +
            "SET EmailAddress = " + NEW_EMAIL_ADDRESS + ", " +
                "FK_EmailTypeID = " + NEW_FK_EMAIL_TYPE_ID + " " +
            "WHERE FK_EmployeeID = " + FK_EMPLOYEE_ID + " AND FK_EmailTypeID = " + FK_EMAIL_TYPE_ID + " AND EmailAddress = " + EMAIL_ADDRESS;

        private const string DELETE_ALL_EMAIL_ADDRESSES_FOR_EMPLOYEE =
            "DELETE FROM tbl_Email_Addresses " +
            "WHERE FK_EmployeeID = " + FK_EMPLOYEE_ID;

        private const string DELETE_EMAIL_ADDRESS =
            "DELETE FROM tbl_Email_Addresses " +
            "WHERE FK_EmployeeID = " + FK_EMPLOYEE_ID + " AND FK_EmailTypeID = " + FK_EMAIL_TYPE_ID + " AND EmailAddress = " + EMAIL_ADDRESS;


        #endregion SQL Query String Constants

         #region Constructor
        public EmailAddressDAO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }
        #endregion Constructor

        #region Public Methods

        public List<EmailVO> SelectAllEmailAddresses() {
            LogDebug("Entering SelectAllEmailAddresses() method...");
            List<EmailVO> list = new List<EmailVO>();
            IDataReader reader = null;
            

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_EMAIL_ADDRESSES);
                reader = Database.ExecuteReader(command);
                EmailTypeDAO emailTypeDAO = new EmailTypeDAO();
                while (reader.Read()) {
                    list.Add(FillInEmailVO(reader, emailTypeDAO));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectAllEmailAddresses() method...", e);
                throw new DBException("Exception in SelectAllEmailAddresses() method...", e);
            }
            finally {
                CloseReader(reader);
                
            }
            return list;
        }


        public List<EmailVO> SelectEmailAddressesForEmployee(int employeeID) {
            LogDebug("Entering SelectEmailAddressesForEmployee() method for employee ID " + employeeID);
            List<EmailVO> list = new List<EmailVO>();
            IDataReader reader = null;
            
            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_EMAIL_ADDRESSES_FOR_EMPLOYEE);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, employeeID);
                reader = Database.ExecuteReader(command);
                EmailTypeDAO emailTypeDAO = new EmailTypeDAO();
                while (reader.Read()) {
                    list.Add(FillInEmailVO(reader, emailTypeDAO));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectEmailAddressesForEmployee() method...", e);
                throw new DBException("Exception in SelectEmailAddressesForEmployee() method...", e);
            }
            
            return list;
        }


        /************************************************************
         * Inserts an email address and returns a list of employee's
         * email addresses.
         * ********************************************************/
        public EmailVO InsertEmailAddress(EmailVO vo) {
            LogDebug("Entering InsertEmailAddress() method...");
            

            try {
                DbCommand command = Database.GetSqlStringCommand(INSERT_EMAIL_ADDRESS);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, vo.EmployeeID);
                Database.AddInParameter(command, FK_EMAIL_TYPE_ID, DbType.Int32, vo.EmailType.EmailTypeID);
                Database.AddInParameter(command, EMAIL_ADDRESS, DbType.String, vo.EmailAddress);
                Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Exception inserting email address.", e);
                throw new DBException("Exception inserting email address.", e);
            }
            
            return vo;
        }


        /***********************************************************
         * Updates an employee's email address and returns a list of
         * employee's email addresses.
         * **********************************************************/
        public EmailVO UpdateEmailAddress(EmailVO oldEmail, EmailVO newEmail) {
            LogDebug("Entering UpdateEmailAddress() method...");
            
            try {
                DbCommand command = Database.GetSqlStringCommand(UPDATE_EMAIL_ADDRESS);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, oldEmail.EmployeeID);
                Database.AddInParameter(command, FK_EMAIL_TYPE_ID, DbType.Int32, oldEmail.EmailType.EmailTypeID);
                Database.AddInParameter(command, EMAIL_ADDRESS, DbType.String, oldEmail.EmailAddress);
                Database.AddInParameter(command, NEW_FK_EMAIL_TYPE_ID, DbType.Int32, newEmail.EmailType.EmailTypeID);
                Database.AddInParameter(command, NEW_EMAIL_ADDRESS, DbType.String, newEmail.EmailAddress);
                Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Exception updating email address.", e);
                throw new DBException("Exception updating email address.", e);
            }
            
            return newEmail;
        }



        public void DeleteAllEmailAddressesForEmployee(int employeeID) {
            LogDebug("Entering DeleteAllEmailAddressesForEmployee() method for employeeID: " + employeeID);
           
            try {
                 DbCommand command = Database.GetSqlStringCommand(DELETE_ALL_EMAIL_ADDRESSES_FOR_EMPLOYEE);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.Int32, employeeID);
                Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Exception deleting all email addresses for employee.", e);
                throw new Exception("Exception deleting all email addresses for employee.", e);
            }
            
        }


        public void DeleteEmailAddress(EmailVO vo) {
            LogDebug("Entering DeleteEmailAddress() method...");
           
            try {
                 DbCommand command = Database.GetSqlStringCommand(DELETE_EMAIL_ADDRESS);
                Database.AddInParameter(command, FK_EMPLOYEE_ID, DbType.String, vo.EmployeeID);
                Database.AddInParameter(command, FK_EMAIL_TYPE_ID, DbType.Int32, vo.EmailType.EmailTypeID);
                Database.AddInParameter(command, EMAIL_ADDRESS, DbType.String, vo.EmailAddress);
                Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Exception deleting email address.", e);
                throw new DBException("Exception deleting email address.", e);
            }
        }

        #endregion Public Methods




        #region Private Methods

        private EmailVO FillInEmailVO(IDataReader reader, EmailTypeDAO emailTypeDAO) {
            EmailVO vo = new EmailVO();
            vo.EmployeeID = reader.GetInt32(0);
            vo.EmailType = emailTypeDAO.SelectEmailType(reader.GetInt32(1));
            vo.EmailAddress = reader.GetString(2);
            return vo;
        }

        #endregion Private Methods

    } // End EmailAddressDAO class definition
} // End namespace
