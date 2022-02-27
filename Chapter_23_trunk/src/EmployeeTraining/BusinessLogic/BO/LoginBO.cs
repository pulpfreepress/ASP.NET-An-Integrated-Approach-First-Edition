using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Security;
using System.Web.Configuration;

using Infrastructure;
using Infrastructure.Exceptions;
using Infrastructure.ValueObjects;
using DataAccess.DAO;
using BusinessLogic.Utils;

namespace BusinessLogic.BO {
    public class LoginBO : BaseBO {

        #region Constructors

        public LoginBO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }

        #endregion Constructors

        #region Methods

        public EmployeeVO LoginUser(string username, string password) {
            LogUserAccess("Attempting to login user with username: " + username);
            LogDebug("Attempting to login user with username: " + username);
            EmployeeVO vo = null;
            EmployeeDAO dao = new EmployeeDAO();

            try {
                vo = dao.SelectEmployee(username);

            }
            catch (Exception e) {

                LogDebug("Problem logging in user with username: " + username, e);
                throw new UserLoginException("Problem logging in user with username: " + username, e);
            }

            if (!vo.IsActive) {

                LogUserAccess("Could not login user with username: " + username + " because they were deactivated!");
                throw new UserAuthorizationException("Could not login user with username: " + username + " because they were deactivated!");
            }

            if(!ValidateLoginHash(vo, password)){
                 LogUserAccess("Could not login user with username: " + username + " because of invalid password!");
                throw new UserAuthorizationException("Could not login user with username: " + username + " because of invalid password!");
            }

            return vo;
        }


        public EmployeeVO AuthenticateUser(string username) {
            LogUserAccess("Attempting to authenticate user with username: " + username);
            LogDebug("Attempting to authenticate user with username: " + username);
            EmployeeVO vo = null;
            EmployeeDAO dao = new EmployeeDAO();

            try {
                vo = dao.SelectEmployee(username);

            }
            catch (Exception e) {

                LogDebug("Problem authenticating  user with username: " + username, e);
                throw new UserLoginException("Problem suthenticating user with username: " + username, e);
            }

            if (!vo.IsActive) {

                LogUserAccess("Could not authenticate user with username: " + username + " because they were deactivated!");
                throw new UserAuthorizationException("Could not authenticate user with username: " + username + " because they were deactivated!");
            }


            return vo;
        }


        public String[] GetUserRoles(string username) {
            string[] user_roles = new string[1];
            user_roles[0] = string.Empty;

            EmployeeVO vo = null;
            EmployeeDAO dao = new EmployeeDAO();

            try {
                vo = dao.SelectEmployee(username);
                if (vo.IsActive) {
                    user_roles[0] = vo.UserRole.RoleName;
                }


            }
            catch (Exception e) {

                LogDebug("Problem retrieving user with username: " + username);
                LogError("Problem retrieving user with username: " + username);
                throw new BLException("Problem retrieving user with username: " + username, e);

            }

            return user_roles;
        }


        private bool ValidateLoginHash(EmployeeVO vo, string password) {

            string employee_login_hash = vo.LoginHash;
            string attempted_login_hash =
                   FormsAuthentication.HashPasswordForStoringInConfigFile((vo.Username + password),
                                                                        FormsAuthPasswordFormat.MD5.ToString());
            if(employee_login_hash.Equals(attempted_login_hash)){
                return true;
            }else{
                return false;
            }

        }


        #endregion Methods
    }
}

