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
    public class EmailTypeDAO : BaseDAO {
        #region SQL Command Constants
        // SQL command parameter constants
        private const string EMAIL_TYPE_ID = "@emailtypeid";
        private const string DESCRIPTION = "@description";
        #endregion SQL Command Constants

        #region SQL Query String Constants

        public const string SELECT_ALL_COLUMNS =
            "SELECT EmailTypeID, Description ";

        public const string SELECT_ALL_EMAIL_TYPES =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Email_Type_LU";

        public const string SELECT_EMAIL_TYPE_BY_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Email_Type_LU " +
            "WHERE EmailTypeID = " + EMAIL_TYPE_ID;

        #endregion SQL Query String Constants

        #region Constructor
        public EmailTypeDAO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }
        #endregion Constructor


        #region Public Methods

        public List<EmailTypeVO> SelectAllEmailTypes() {
            LogDebug("Entering SelectAllEmailTypes() method...");
            List<EmailTypeVO> list = new List<EmailTypeVO>();
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_EMAIL_TYPES);
                reader = Database.ExecuteReader(command);
                while (reader.Read()) {
                    list.Add(FillInEmailTypeVO(reader));
                }
            }
            catch (Exception e) {
                LogError("Exception in SelectAllEmailTypes() method...", e);
                throw new DBException("Exception in SelectAllEmailTypes() method...", e);
            }
            finally {
                CloseReader(reader);
            }
            return list;
        }


        public EmailTypeVO SelectEmailType(int emailTypeID) {
            LogDebug("Entering SelectEmailType() method for emailTypeID = " + emailTypeID);
            EmailTypeVO vo = null;
            IDataReader reader = null;

            try {
                DbCommand command = Database.GetSqlStringCommand(SELECT_EMAIL_TYPE_BY_ID);
                Database.AddInParameter(command, EMAIL_TYPE_ID, DbType.Int32, emailTypeID);
                reader = Database.ExecuteReader(command);
                if (reader.Read()) {
                    vo = FillInEmailTypeVO(reader);
                }
            }
            catch (Exception e) {
                LogError("Problem selecting EmailType", e);
                throw new DBException("Problem selecting EmailType", e);
            }
            finally {
                base.CloseReader(reader);
            }


            return vo;
        }



        #endregion Public Methods


        #region Private Methods

        private EmailTypeVO FillInEmailTypeVO(IDataReader reader) {
            EmailTypeVO vo = new EmailTypeVO();
            vo.EmailTypeID = reader.GetInt32(0);
            vo.Description = reader.GetString(1);
            return vo;
        }

        #endregion Private Methods





    } // End EmailTypeDAO class definition
} // end namespace
