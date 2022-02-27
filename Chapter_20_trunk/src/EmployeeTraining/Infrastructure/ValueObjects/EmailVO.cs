using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ValueObjects {
    public class EmailVO {
        #region Public Properties
        public int EmployeeID { get; set; }
        public EmailTypeVO EmailType { get; set; }
        public string EmailAddress { get; set; }
        #endregion Public Properties

        #region Constructors
        public EmailVO() : this(0, new EmailTypeVO(), string.Empty) { }

        public EmailVO(int employeeID, EmailTypeVO emailType, string emailAddress) {
            EmployeeID = employeeID;
            EmailType = emailType;
            EmailAddress = emailAddress;
        }
        #endregion Constructors
    }
}
