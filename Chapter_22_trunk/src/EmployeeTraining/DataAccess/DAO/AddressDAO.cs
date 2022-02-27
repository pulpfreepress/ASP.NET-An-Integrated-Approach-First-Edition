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
    public class AddressDAO : BaseDAO {

        #region SQL Command Constants
        // SQL command parameter constants
        private const string ADDRESS_ID = "@addressid";
        private const string ADDRESS_1 = "@address1";
        private const string ADDRESS_2 = "@address2";
        private const string CITY = "@city";
        private const string STATE = "@state";
        private const string ZIP = "@zip";
        private const string EMPLOYEE_ID = "@employeeid";
        #endregion SQL Command Constants

        #region SQL Query String Constants

        private const string SELECT_ALL_COLUMNS =
            "SELECT  AddressID, " +
                    "Address1, " +
                    "Address2, " +
                    "City, " +
                    "State, " +
                    "Zip ";
        private const string SELECT_ALL_ADDRESSES =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Employee_Addresses";

        private const string SELECT_ADDRESS_BY_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Employee_Addresses " +
            "WHERE AddressID = " + ADDRESS_ID;


        private const string INSERT_ADDRESS =
           "INSERT INTO tbl_Employee_Addresses " +
              "(Address1, Address2, City, State, Zip) " +
           "VALUES (" + ADDRESS_1 + ", " + ADDRESS_2 + ", " + CITY + ", " + STATE + ", " + ZIP + ") " +
           "SELECT scope_identity()";

        private const string UPDATE_ADDRESS =
            "UPDATE tbl_Employee_Addresses " +
            "SET Address1 = " + ADDRESS_1 + ", " +
                 "Address2 = " + ADDRESS_2 + ", " +
                 "City = " + CITY + ", " +
                 "State = " + STATE + ", " +
                 "Zip = " + ZIP + " " +
            "WHERE AddressID = " + ADDRESS_ID;


        private const string DELETE_ADDRESS =
            "DELETE FROM tbl_Employee_Addresses " +
            "WHERE AddressID = " + ADDRESS_ID;

        /*******************************************************
         * Queries that deal with addresses related to employees
         * via the tbl_Employee_Address_XREF table.
         * *****************************************************/

        private const string SELECT_EMPLOYEES_ADDRESSES =
           SELECT_ALL_COLUMNS +
           "FROM tbl_Employee_Address_XREF, tbl_Employee_Addresses " +
           "WHERE FK_EmployeeID = " + EMPLOYEE_ID + " AND FK_EmployeeAddressID = AddressID";

        private const string INSERT_EMPLOYEE_ADDRESS_XREF =
            "INSERT INTO tbl_EMPLOYEE_ADDRESS_XREF (FK_EmployeeID, FK_EmployeeAddressID) " +
            "VALUES (" + EMPLOYEE_ID + ", " + ADDRESS_ID + ")";

        private const string DELETE_ADDRESS_XREFS_FOR_EMPLOYEE_ID =
            "DELETE FROM tbl_Employee_Address_XREF " +
            "WHERE FK_EmployeeID = " + EMPLOYEE_ID;

        #endregion SQL Query String Constants


        #region Constructor
        public AddressDAO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }
        #endregion Constructor


        #region Public Methods

        public List<AddressVO> SelectAllAddresses() {
            LogDebug("Entering SelectAllAddress() method...");
            List<AddressVO> list = new List<AddressVO>();
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_ADDRESSES);
                reader = Database.ExecuteReader(command);
                while (reader.Read()) {
                    list.Add(FillInAddressVO(reader));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectAllAddresses() method...", e);
                throw new DBException("Exception in SelectAllAddresses() method...", e);
            }
            finally {
                CloseReader(reader);
            }
            return list;
        }


        public AddressVO SelectAddress(int id) {
            LogDebug("Entering SelectAddress() method with AddressID = " + id);
            AddressVO vo = null;
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ADDRESS_BY_ID);
                Database.AddInParameter(command, ADDRESS_ID, DbType.Int32, id);
                reader = Database.ExecuteReader(command);
                if (reader.Read()) {
                    vo = FillInAddressVO(reader);
                }
                else {
                    throw new DBException("No address found for AddressID: " + id);
                }
            }
            catch (Exception e) {
                LogError("Error getting address by AddressID " + id, e);
                throw new DBException("Error getting address by AddressID " + id);
            }
            finally {
                base.CloseReader(reader);
            }

            return vo;
        }


        public AddressVO InsertAddress(AddressVO vo) {
            LogDebug("Entering InsertAddress() method for address: " + vo);
            

            try {
                DbCommand command = Database.GetSqlStringCommand(INSERT_ADDRESS);
                Database.AddInParameter(command, ADDRESS_1, DbType.String, vo.Address1);
                Database.AddInParameter(command, ADDRESS_2, DbType.String, vo.Address2);
                Database.AddInParameter(command, CITY, DbType.String, vo.City);
                Database.AddInParameter(command, STATE, DbType.String, vo.State);
                Database.AddInParameter(command, ZIP, DbType.String, vo.Zip);
                vo.AddressID = Convert.ToInt32(Database.ExecuteScalar(command));
            }
            catch (Exception e) {
                LogError("Exception inserting address into database!", e);
                throw new DBException("Exception inserting address into database!", e);
            }

            return vo;
        }


        public AddressVO UpdateAddress(AddressVO vo) {
            LogDebug("Entering the UpdateAddress() method with address: " + vo);
            int rowsAffected = 0;
            
            try {
               DbCommand command = Database.GetSqlStringCommand(UPDATE_ADDRESS);
                Database.AddInParameter(command, ADDRESS_1, DbType.String, vo.Address1);
                Database.AddInParameter(command, ADDRESS_2, DbType.String, vo.Address2);
                Database.AddInParameter(command, CITY, DbType.String, vo.City);
                Database.AddInParameter(command, STATE, DbType.String, vo.State);
                Database.AddInParameter(command, ZIP, DbType.String, vo.Zip);

                Database.AddInParameter(command, ADDRESS_ID, DbType.Int32, vo.AddressID);
                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Error updating address record: " + e);
                throw new DBException("Error updating address record: " + e);
            }
            

            if (rowsAffected == 0) {
                LogError("No rows updated for address: " + vo);
                throw new DBException("No rows updated for address: " + vo);
            }
            return vo;
        }


        public void DeleteAddress(AddressVO vo) {
            LogDebug("Entering DeleteAddress() method with address: " + vo);
            int rowsAffected = 0;
           
            
            try {
                DbCommand command = Database.GetSqlStringCommand(DELETE_ADDRESS);
                Database.AddInParameter(command, ADDRESS_ID, DbType.Int32, vo.AddressID);
                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Error deleting address from database: " + vo);
                throw new DBException("Error deleting address from database: " + vo, e);
            }

            if (rowsAffected == 0) {
                LogError("No row deleted from database for address: " + vo);
                throw new DBException("No row deleted from database for address: " + vo);
            }
        }


        /************************************************************
         * Methods that deal with addresses related to employees.
         * 
         * ********************************************************/

        public List<AddressVO> SelectEmployeeAddresses(int employeeID) {
            LogDebug("Entering SelectEmployeeAddresses() method with employeeID = " + employeeID);
            AddressVO vo = null;
            List<AddressVO> addressList = new List<AddressVO>();
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_EMPLOYEES_ADDRESSES);
                Database.AddInParameter(command, EMPLOYEE_ID, DbType.Int32, employeeID);
                reader = Database.ExecuteReader(command);
                while (reader.Read()) {
                    vo = FillInAddressVO(reader);
                    addressList.Add(vo);
                }
            }
            catch (Exception e) {
                LogError("Error getting employee addresses by employeeID " + employeeID, e);
                throw new DBException("Error getting employee addresses by employeeID " + employeeID);
            }
            finally {
                base.CloseReader(reader);
            }
            return addressList;
        }


        public void InsertEmployeeAddressXrefs(int employeeID, List<AddressVO> addresses) {
            LogDebug("Entering InsertEmployeeAddressXrefs() method for employeeID " + employeeID);

            try {
               
                foreach (AddressVO address in addresses) {
                    this.InsertEmployeeAddressXref(employeeID, address.AddressID);
                }

            }
            catch (Exception e) {
                LogError("Error inserting employee address XREFS" + e);
                throw new DBException("Error inserting employee address XREFS" + e);
            }
        }

        #endregion Public Methods


        #region Private Methods

        private AddressVO FillInAddressVO(IDataReader reader) {
            AddressVO vo = new AddressVO();
            vo.AddressID = reader.GetInt32(0);
            vo.Address1 = reader.GetString(1);
            if (!reader.IsDBNull(2)) {
                vo.Address2 = reader.GetString(2);
            }
            else {
                vo.Address2 = string.Empty;
            }
            vo.City = reader.GetString(3);
            vo.State = reader.GetString(4);
            vo.Zip = reader.GetString(5);
            return vo;
        }


        private void InsertEmployeeAddressXref(int employeeID, int addressID) {
            LogDebug("Entering InsertEmployeeAddressXref() method...");

            try {
                DbCommand command = Database.GetSqlStringCommand(INSERT_EMPLOYEE_ADDRESS_XREF);
                Database.AddInParameter(command, EMPLOYEE_ID, DbType.String, employeeID);
                Database.AddInParameter(command, ADDRESS_ID, DbType.String, addressID);
                Database.ExecuteScalar(command);
            }
            catch (Exception e) {
                LogError("Problem inserting employee_address XREF", e);
                throw new DBException("Problem inserting employee_address XREF", e);
            }
        }


        public void DeleteEmployeeAddressXrefs(int employeeID) {
            LogDebug("Entering DeleteEmployeeAddressXrefs() method for employeeID " + employeeID);

            try {
                DbCommand command = Database.GetSqlStringCommand(DELETE_ADDRESS_XREFS_FOR_EMPLOYEE_ID);
                Database.AddInParameter(command, EMPLOYEE_ID, DbType.Int32, employeeID);
                Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Error deleting employee address XREFS" + e);
                throw new DBException("Error deleting employee address XREFS" + e);
            }
        }

        #endregion Private Methods
    } // end AddressDAO class definition
} // end namespace
