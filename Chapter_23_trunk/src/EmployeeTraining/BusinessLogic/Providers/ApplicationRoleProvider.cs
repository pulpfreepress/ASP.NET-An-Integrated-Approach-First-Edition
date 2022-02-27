using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Security;
using System.Web.Configuration;

using Infrastructure;
using Infrastructure.Exceptions;
using BusinessLogic.BO;


namespace BusinessLogic.Providers {
    public class ApplicationRoleProvider : RoleProvider {

       

        private string _appName;

        public override string ApplicationName {
            get {
                return _appName;
            }
            set {
                this._appName = ApplicationName;
            }
        }


        public override void Initialize(string providerName, NameValueCollection configSettings) {
           BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, 
                               "RoleProvider: Inititalize: Entering Method");


            if (configSettings == null) {
                BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                                  "Inititalize: Could not read the web.config settings for the roles");
                throw new ArgumentNullException("The web.config settings are incorrect. ");
            }

            if (String.IsNullOrEmpty(providerName)) {
                providerName = "BaseRoleProvider";
            }

            if (String.IsNullOrEmpty(configSettings["description"])) {
                BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                                    "Inititalize: setting description programatically.");
                configSettings.Remove("description");
                configSettings.Add("description", "Custom role Provider for <MY APP NAME>.");
            }

            if (String.IsNullOrEmpty(configSettings["applicationName"])) {
                _appName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else {
                _appName = configSettings["applicationName"];
            }
            if (_appName.Length > 255) {
                throw new ProviderException("Provider application name is too long, the max is 255.");
            }

            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                                "Inititalize: calling the base Initialize method.");
            base.Initialize(providerName, configSettings);



            //recommended to make sure there aren't any other attributes left.
            if (configSettings.Count > 0) {
                string attribUnrecognized = configSettings.GetKey(0);
                if (!String.IsNullOrEmpty(attribUnrecognized)) {
                    throw new ProviderException("Role Provider unrecognized attribute: " + attribUnrecognized);
                }
            }
        }


        public override string[] GetRolesForUser(string userName) {
            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                               "GetRolesForUser: Getting Role for User: " + userName);

            string[] user_roles = new string[1];
            String rolename = String.Empty;
            
            LoginBO bo = new LoginBO();

            lock (new object()) {
                try {
                    user_roles = bo.GetUserRoles(userName);
                    rolename = user_roles[0];
                }

                catch (Exception e) {
                    BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                                       "GetRolesForUser: Caught a generic Exception in executing the sql query.");
                    throw new BLException("Caught an exception when executing a Role Request.  ", e);
                }
            }

            if (String.IsNullOrEmpty(rolename)) {
               BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                                  "GetRolesForUser: The query didn't return any roles, and everyone needs a role. ");
                throw new BLException("Could not find a Role for the user: " + userName);
            }


            return user_roles;
        }



        #region Methods not Implemented in this base version - TO DO

        /// <summary>
        /// Override of RoleProvider.GetAllRoles class required by abstract parent. Currently not 
        /// implemented by the base application framework.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">This method is not implemented in the application</exception>
        public override string[] GetAllRoles() {
            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                               "Calling the RoleProvider:GetAllRoles method, which isn't implemented.");
            throw new NotImplementedException("GetAllRoles is not implemented for this project");
        }

        /// <summary>
        /// This method is an override of the interface method.  It is not currently 
        /// implemented in the base application framework</summary>
        /// <param name="roleName">The actual name of the role to find out which users belong to it.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">This method is not implemented in base application framework</exception>
        public override string[] GetUsersInRole(string roleName) {
            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, 
                "Calling the RoleProvider:GetUsersInRole method, which isn't implemented.");
            throw new NotImplementedException("GetUsersInRole is not implemented for this project");
        }

        /// <summary>
        /// This is an override of the required interface method.  It is not currently implemented
        /// in the base application framework
        /// </summary>
        /// <param name="roleName">The role name to search against</param>
        /// <param name="usernameToMatch">The username to search against using a Sql 'LIKE' command</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">This method is not implemented in the base application</exception>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, 
                "Calling the RoleProvider:FindUsersInRole method, which isn't implemented.");
            throw new NotImplementedException("FindUsersInRole is not implemented for this project");
        }

        /// <summary>
        /// Required override of RoleProvider base method in pravider framework. 
        /// Currently not implemented in the base application framework.
        /// </summary>
        /// <param name="username">The username to match up with the role</param>
        /// <param name="roleName">The Role to check and see if the user has it.</param>
        /// <returns>A Boolean value to determine if the user is in the role(true) or not (false)</returns>
        /// <exception cref="NotImplementedException">This method is not implemented in base application framework</exception>
        public override bool IsUserInRole(string username, string roleName) {
            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, 
                "Calling the RoleProvider:IsUserInRole method, which isn't implemented.");
            throw new NotImplementedException("IsUserInRole is not implemented for this project");
        }

        /// <summary>
        /// This method is the override of the parent method that handles adding multiple users to 
        /// multiple roles, but is not implemented in base application framework.
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roleNames"></param>
        /// <exception cref="NotImplementedException">This method is not implemented in base application framework</exception>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, 
                "Calling the RoleProvider:AddUsersToRoles method, which isn't implemented.");
            throw new NotImplementedException("AddUsersToRoles is not implemented for this project");
        }

        /// <summary>
        /// This method is the required implementation of the parent class (RoleProvider) and
        /// is not currently implemented in the base application framework.  
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">This method is not implemented in base application framework</exception>
        public override bool RoleExists(string roleName) {
            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, 
                "Calling the RoleProvider:RoleExists method, which isn't implemented.");
            throw new NotImplementedException("The RolePRovider:RoleExists method is not implemented yet");
        }

        /// <summary>
        /// This method is the override of the parent method that handles removing multiple users from 
        /// multiple roles (via the XREF table), but is not implemented in base application framework.
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roleNames"></param>
        /// <exception cref="NotImplementedException">This method is not implemented in base application framework</exception>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, 
                "Calling the RoleProvider:RemoveUsersFromRoles method, which isn't implemented.");
            throw new NotImplementedException("RemoveUsersFromRoles is not implemented for this project");
        }

        /// <summary>
        /// This method contains the signature of the Provider Framework override method, but is not
        /// implemented in base application framework.
        /// </summary>
        /// <param name="roleName">The name of the Role to create in the system.</param>
        /// <exception cref="NotImplementedException">This method is not implemented in base application framework</exception>
        public override void CreateRole(string roleName) {
            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, 
                "Calling the RoleProvider:CreateRole method, which isn't implemented.");
            throw new NotImplementedException("CreateRole is not implemented for this project");
        }

        /// <summary>
        /// This method is required by the RoleProvider framework, but is not
        /// implemented in base application framework.</summary>
        /// <param name="roleName">The name of the Role to delete.</param>
        /// <param name="throwOnPopulatedRole">A boolean to determine if the query should be executed, 
        /// and if an exception should be thrown if a role is populated.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">This method is not implemented in base application framework</exception>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
            BaseObject.LogDebug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, 
                "Calling the RoleProvider:DeleteRole method, which isn't implemented.");
            throw new NotImplementedException("DeleteRole is not implemented for this project");
        }

        #endregion

    }
}
