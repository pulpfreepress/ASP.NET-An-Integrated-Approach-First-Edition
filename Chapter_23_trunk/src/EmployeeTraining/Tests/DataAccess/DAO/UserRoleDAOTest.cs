using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


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


namespace Tests.DataAccess.DAO {

     [TestFixture]
    public class UserRoleDAOTest {

         private UserRoleDAO _dao;

         [SetUp]
         public void SetUp() {
             _dao = new UserRoleDAO();

         }


         [Test]
         public void SelectAllUserRolesTest() {

             List<UserRoleVO> list = _dao.SelectAllUserRoles();

             Assert.AreEqual(list.Count, 2);
         }


         [Test]
         public void SelectUserRoleByIDTest() {
             UserRoleVO vo = _dao.SelectUserRole(1);
             Assert.AreEqual(vo.RoleID, 1);
         }


         [Test]
         public void InsertAndDeleteUserRoleTest() {
             UserRoleVO vo = new UserRoleVO();
             vo.RoleName = "SuperUser";
             vo =  _dao.InsertUserRole(vo);
             UserRoleVO retrieved_vo = _dao.SelectUserRole(vo.RoleID);
             Assert.AreEqual(vo.RoleName, retrieved_vo.RoleName);
             _dao.DeleteUserRole(retrieved_vo.RoleID);
             List<UserRoleVO> list = _dao.SelectAllUserRoles();
             Assert.AreEqual(list.Count, 2);
             


         }






    }
}
