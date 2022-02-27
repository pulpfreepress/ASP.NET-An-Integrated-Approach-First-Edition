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
    public class UserRoleDAO : BaseDAO {


        #region SQL Command Parameters
        //SQL command parameter constants
        private const string USER_ROLE_ID = "@UserRoleID";
        private const string ROLE_NAME = "@RoleName";

        #endregion

        #region SQL Query String Constants
        //SQL query string constants
        private const string SELECT_ALL_COLUMNS =
            "SELECT UserRoleID, RoleName ";

        private const string SELECT_ALL_USER_ROLES =
            SELECT_ALL_COLUMNS +
            "FROM tbl_UserRoles_LU";

        private const string SELECT_USER_ROLE_BY_ID =
           SELECT_ALL_COLUMNS +
           "FROM tbl_UserRoles_LU " +
           "WHERE UserRoleID = " + USER_ROLE_ID;

        private const string INSERT_USER_ROLE =
            "INSERT INTO tbl_UserRoles_LU " +
               "(RoleName) " +
             "VALUES (" + ROLE_NAME + ")" +
              "SELECT scope_identity()";

        private const String UPDATE_USER_ROLE =
            "UPDATE tbl_UserRoles_LU " +
            "SET RoleName = " + ROLE_NAME + ", " +
            "WHERE UserRoleID = " + USER_ROLE_ID;

        private const string DELETE_USER_ROLE =
            "DELETE FROM tbl_UserRoles_LU " +
            "WHERE UserRoleID = " + USER_ROLE_ID;
        #endregion




        #region Constructor

        public UserRoleDAO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }

        #endregion


        #region Public Methods

        public List<UserRoleVO> SelectAllUserRoles() {
            LogDebug("Entering SelectAllUserRoles() method...");
            List<UserRoleVO> list = new List<UserRoleVO>();
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_USER_ROLES);
                reader = Database.ExecuteReader(command);
                while (reader.Read()) {
                    list.Add(FillInUserRoleVO(reader));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectAllUserRoles() method.", e);
                throw new DBException("Exception in SelectAllUserRoles() method.", e, BaseException.Severity.ERROR);
            }
            finally {
                base.CloseReader(reader);
            }
            return list;
        }


        public UserRoleVO SelectUserRole(int id) {
            LogDebug("Entering SelectUserRole() method for RoleID = " + id);
            UserRoleVO vo = null;
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_USER_ROLE_BY_ID);
                Database.AddInParameter(command, USER_ROLE_ID, DbType.Int32, id);
                reader = Database.ExecuteReader(command);
                if (reader.Read()) {
                    vo = FillInUserRoleVO(reader);
                }
                else {
                    throw new DBException("No user role found for UserRoleID: " + id);
                }
            }
            catch (Exception e) {
                LogError("Error getting user role by UserRoleID " + id, e);
                throw new DBException("Error getting user role by UserRoleID " + id);
            }
            finally {
                base.CloseReader(reader);
            }
            return vo;
        }


        public UserRoleVO InsertUserRole(UserRoleVO vo) {
            LogDebug("Entering InsertUserRole() method with vo: " + vo);

            try {

                DbCommand command = Database.GetSqlStringCommand(INSERT_USER_ROLE);
                Database.AddInParameter(command, ROLE_NAME, DbType.String, vo.RoleName);
                vo.RoleID = Convert.ToInt32(Database.ExecuteScalar(command));
            }
            catch (Exception e) {
                LogError("Error inserting user role into database!", e);
                throw new DBException("Error inserting user role into database!", e, BaseException.Severity.ERROR);
            }
            return vo;
        }


        public UserRoleVO UpdateUserRole(UserRoleVO vo) {
            LogDebug("Entering UpdateUserRole() method with UserRoleVO: " + vo);

            int rowsAffected = 0;
            try {

                DbCommand command = Database.GetSqlStringCommand(UPDATE_USER_ROLE);
                Database.AddInParameter(command, ROLE_NAME, DbType.String, vo.RoleName);
                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Error updating user role record: " + e);
                throw new DBException("Error updating user role record: " + e);
            }

            if (rowsAffected == 0) {
                LogError("No rows updated for user role: " + vo);
                throw new DBException("No rows updated for user role: " + vo);
            }
            return vo;
        }


        public void DeleteUserRole(int id) {
            LogDebug("Entering DeleteUserRole() method with user role id = " + id);
            int rows_affected = 0;
            try {
                DbCommand command = Database.GetSqlStringCommand(DELETE_USER_ROLE);
                Database.AddInParameter(command, USER_ROLE_ID, DbType.Int32, id);
                rows_affected = Database.ExecuteNonQuery(command);

            }
            catch (Exception e) {
                LogError("Problem deleting user role with id = " + id, e);
                throw new DBException("Problem deleting user role with id = " + id, e);
            }

            if (rows_affected == 0) {
                LogError("No row deleted from database for user role id: " + id);
                throw new DBException("No row deleted from database for user rile id: " + id);
            }

        }


        #endregion Public Methods

        #region Private Methods

        private UserRoleVO FillInUserRoleVO(IDataReader reader) {
            UserRoleVO vo = new UserRoleVO();
            vo.RoleID = reader.GetInt32(0);
            vo.RoleName = reader.GetString(1);
            return vo;
        }

        #endregion Private Methods

    } // end class
} // end namespace
