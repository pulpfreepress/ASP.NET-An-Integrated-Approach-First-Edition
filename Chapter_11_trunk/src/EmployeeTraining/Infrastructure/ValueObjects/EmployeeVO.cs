using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ValueObjects
{
    public class EmployeeVO
    {
        #region Public_Properties
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public byte[] Picture { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public List<CourseVO> Courses { get; set; }
        #endregion

        #region Constructors

        public EmployeeVO() { }

        public EmployeeVO(int employeeID, string first_name, string middle_name, string last_name,
                          DateTime birthday, byte[] picture, DateTime hiredate, bool is_active, List<CourseVO> courses)
        {
            EmployeeID = employeeID;
            FirstName = first_name;
            MiddleName = middle_name;
            LastName = last_name;
            Birthday = birthday;
            Picture = picture;
            HireDate = hiredate;
            IsActive = is_active;
            Courses = courses;
        }

        public EmployeeVO(string first_name, string middle_name, string last_name,
                          DateTime birthday, byte[] picture, DateTime hiredate, bool is_active, List<CourseVO> courses)
        {
            FirstName = first_name;
            MiddleName = middle_name;
            LastName = last_name;
            Birthday = birthday;
            Picture = picture;
            HireDate = hiredate;
            IsActive = is_active;
            Courses = courses;
        }

        public override string ToString()
        {
            return EmployeeID + " " + FirstName + " " + MiddleName + " " + LastName + " " + Birthday.ToShortDateString()
                   + " " + HireDate.ToShortDateString() + " " + IsActive;
        }

        #endregion

    } // end EmployeeVO class
}
