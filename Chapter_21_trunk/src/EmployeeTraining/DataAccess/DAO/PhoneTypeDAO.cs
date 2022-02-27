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
    public class PhoneTypeDAO : BaseDAO {

        #region SQL Command Constants
        // SQL command parameter constants
        private const string PHONE_TYPE_ID = "@phonetypeid";
        private const string DESCRIPTION = "@description";
        #endregion SQL Command Constants

        #region SQL Query String Constants

        public const string SELECT_ALL_COLUMNS =
            "SELECT PhoneTypeID, Description ";

        public const string SELECT_ALL_PHONE_TYPES =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Phone_Type_LU";

        public const string SELECT_PHONE_TYPE_BY_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Phone_Type_LU " +
            "WHERE PhoneTypeID = " + PHONE_TYPE_ID;

        #endregion SQL Query String Constants

        #region Constructor
        public PhoneTypeDAO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }
        #endregion Constructor


        #region Public Methods

        public List<PhoneTypeVO> SelectAllPhoneTypes() {
            LogDebug("Entering SelectAllPhoneTypes() method...");
            List<PhoneTypeVO> list = new List<PhoneTypeVO>();
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_PHONE_TYPES);
                reader = Database.ExecuteReader(command);
                while (reader.Read()) {
                    list.Add(FillInPhoneTypeVO(reader));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectAllPhoneTypes() method...", e);
                throw new DBException("Exception in SelectAllPhoneTypes() method...", e);
            }
            finally {
                CloseReader(reader);
            }
            return list;
        }


        public PhoneTypeVO SelectPhoneType(int phoneTypeID) {
            LogDebug("Entering SelectPhoneType() method for phoneTypeID = " + phoneTypeID);
            PhoneTypeVO vo = null;
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_PHONE_TYPE_BY_ID);
                Database.AddInParameter(command, PHONE_TYPE_ID, DbType.Int32, phoneTypeID);
                reader = Database.ExecuteReader(command);
                if (reader.Read()) {
                    vo = FillInPhoneTypeVO(reader);
                }
            }
            catch (Exception e) {
                LogError("Problem selecting PhoneType", e);
                throw new DBException("Problem selecting PhoneType", e);
            }
            finally {
                base.CloseReader(reader);
            }
            return vo;
        }



        #endregion Public Methods


        #region Private Methods

        private PhoneTypeVO FillInPhoneTypeVO(IDataReader reader) {
            PhoneTypeVO vo = new PhoneTypeVO();
            vo.PhoneTypeID = reader.GetInt32(0);
            vo.Description = reader.GetString(1);
            return vo;
        }

        #endregion Private Methods



    } // End PhoneTypeDAO class definition
} // end namespace
