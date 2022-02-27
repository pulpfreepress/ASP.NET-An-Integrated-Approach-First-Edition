using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ValueObjects {
    public class CompletedCourseVO {

        #region Public Properties
        public int      EmployeeID      { get; set; }
        public CourseVO Course          { get; set; }
        public DateTime DateCompleted   { get; set; }
        public double Grade              { get; set; }
        #endregion 

        #region Constructors
        public CompletedCourseVO() : this(0, new CourseVO(), DateTime.Now, 0.0) { }
        
        
        public CompletedCourseVO(int employeeID, CourseVO course, DateTime dateCompleted, double grade) {
            EmployeeID = employeeID;
            Course = course;
            DateCompleted = dateCompleted;
            Grade = grade;
        }
        #endregion

        public override string ToString() {
            return EmployeeID + " " + Course + " " + DateCompleted + " " + Grade;
        }
    } // end CompletedCourseVO class definition
} // end namespace
