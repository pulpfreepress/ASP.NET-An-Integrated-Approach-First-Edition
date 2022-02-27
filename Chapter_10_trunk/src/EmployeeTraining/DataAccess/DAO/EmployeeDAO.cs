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

namespace DataAccess.DAO
{
    public class EmployeeDAO : BaseDAO
    {
        // Column identifiers
        private const String EMPLOYEE_ID = "@employeeID";
        private const String FIRST_NAME = "@firstName";
        private const String MIDDLE_NAME = "@middleName";
        private const String LAST_NAME = "@lastName";
        private const String BIRTHDAY = "@birthday";
        private const String PICTURE = "@picture";        // will ignore Picture column for now
        private const String HIRE_DATE = "@hireDate";

        // SQL String Constants
        private const String SELECT_ALL_COLUMNS =
            "SELECT tbl_Employee.EmployeeID, " +
                    "FirstName, " +
                    "MiddleName, " +
                    "LastName, " +
                    "Birthday, " +
                    "HireDate ";

        private const String SELECT_ALL_EMPLOYEES =
            SELECT_ALL_COLUMNS +
            "FROM tbl_EMPLOYEE ";


        public EmployeeDAO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }


        public List<EmployeeVO> SelectAllEmployees()
        {
            LogDebug("Entering SelectAllEmployees() method...");

            List<EmployeeVO> list = new List<EmployeeVO>();
            IDataReader reader = null;

            DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_EMPLOYEES);

            try
            {
                reader = Database.ExecuteReader(command);
                while (reader.Read())
                {
                    list.Add(FillInEmployeeVO(reader));
                }
            }
            catch (Exception e)
            {
                LogError("Exception in SelectAllEmployees() method.", e);
                throw new DBException("Exception in SelectAllEmployees() method.", e, BaseException.Severity.ERROR);
            }
            finally
            {
                base.CloseReader(reader);
            }

            return list;
        }


        private EmployeeVO FillInEmployeeVO(IDataReader reader)
        {
            EmployeeVO vo = new EmployeeVO();

            vo.EmployeeID = reader.GetInt32(0);
            vo.FirstName = reader.GetString(1);
            vo.MiddleName = reader.GetString(2);
            vo.LastName = reader.GetString(3);
            vo.Birthday = reader.GetDateTime(4);
            vo.HireDate = reader.GetDateTime(5);

            return vo;
        }

    } // end EmployeeDAO class
} // end namespace
