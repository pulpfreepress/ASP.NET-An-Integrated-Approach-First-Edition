using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ValueObjects {
    public class PhoneNumberVO {
        #region Public Properties
        public int EmployeeID { get; set; }
        public PhoneTypeVO PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        #endregion Public Properties

        #region Constructors
        public PhoneNumberVO() : this(0, new PhoneTypeVO(), string.Empty) { }

        public PhoneNumberVO(int employeeID, PhoneTypeVO phoneType, string phoneNumber) {
            EmployeeID = employeeID;
            PhoneType = phoneType;
            PhoneNumber = phoneNumber;
        }
        #endregion Constructors
    }
}
