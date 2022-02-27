using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

using Infrastructure.ValueObjects;
using Infrastructure.Exceptions;
using DataAccess.DAO;


namespace Tests.DataAccess.DAO
{
    [TestFixture]
    public class EmployeeDAOTest
    {
        private EmployeeDAO _employeeDAO;

        [SetUp]
        public void SetUp()
        {
            _employeeDAO = new EmployeeDAO();
        }

        [Test]
        public void SelectAllEmployeesTest(){
            List<EmployeeVO> list = _employeeDAO.SelectAllEmployees();

            Assert.IsTrue(list.Count > 0);
        }

    }
}
