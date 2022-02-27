using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ValueObjects {
    public class EmployeeVO {
        #region Public Properties
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public byte[] Picture { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public List<CompletedCourseVO> CompletedCourses { get; set; }
        public List<PhoneNumberVO> PhoneNumbers { get; set; }
        public List<EmailVO> EmailAddresses { get; set; }
        public List<AddressVO> Addresses { get; set; }
        #endregion Public Properties

        #region Constructors

        public EmployeeVO()
            : this(0, string.Empty, string.Empty, string.Empty, DateTime.MinValue, null,
                  DateTime.MinValue, true, new List<CompletedCourseVO>(), new List<PhoneNumberVO>(),
                  new List<EmailVO>(), new List<AddressVO>()) { }


        public EmployeeVO(string first_name, string middle_name, string last_name,
                          DateTime birthday, byte[] picture, DateTime hiredate, bool is_active, 
                          List<CompletedCourseVO> completedCourses, List<PhoneNumberVO> phoneNumbers, 
                          List<EmailVO> emailAddresses, List<AddressVO> addresses)
            : this(0, first_name, middle_name, last_name, birthday, picture, hiredate, is_active, 
                   completedCourses, phoneNumbers, emailAddresses, addresses) { }



        public EmployeeVO(int employeeID, string first_name, string middle_name, string last_name,
                          DateTime birthday, byte[] picture, DateTime hiredate, bool is_active,
                          List<CompletedCourseVO> completedCourses, List<PhoneNumberVO> phoneNumbers,
                          List<EmailVO> emailAddresses, List<AddressVO> addresses) {
            EmployeeID = employeeID;
            FirstName = first_name;
            MiddleName = middle_name;
            LastName = last_name;
            Birthday = birthday;
            Picture = picture;
            HireDate = hiredate;
            IsActive = is_active;
            CompletedCourses = completedCourses;
            PhoneNumbers = phoneNumbers;
            EmailAddresses = emailAddresses;
            Addresses = addresses;
        }

        #endregion Constructors

        #region Overridden Object Methods

        public override string ToString() {
            return EmployeeID + " " + FirstName + " " + MiddleName + " " + LastName + " " 
                              + Birthday.ToShortDateString() + " " 
                              + HireDate.ToShortDateString() + " " + IsActive;
        }

        #endregion Overridden Object Methods

    } // end EmployeeVO class
}
