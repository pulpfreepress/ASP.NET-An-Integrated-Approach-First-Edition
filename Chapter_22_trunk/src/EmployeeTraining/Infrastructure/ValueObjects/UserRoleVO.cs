using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Infrastructure.ValueObjects {
    public class UserRoleVO {
      #region Public Properties

        public int RoleID { get; set; }
        public string RoleName { get; set; }

      #endregion Public Properties

      #region Constructors

        public UserRoleVO() {
            RoleID = 0;
            RoleName = string.Empty;
        }

      #endregion Constructors

        public override string ToString() {
            return RoleID + " " + RoleName;
        }

    }
}
