using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ValueObjects
{
    public class CourseVO
    {
        #region Public_Properties
        public int CourseID { get; set; }
        public String Code { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        #endregion

        #region Constructors
        public CourseVO(): this(0, string.Empty, string.Empty, string.Empty) { }

        public CourseVO(int courseID, string code, string title, string description){
            CourseID = courseID;
            Code = code;
            Title = title;
            Description = description;
        }

        public CourseVO(string code, string title, string description){
            Code = code;
            Title = title;
            Description = description;
        }
        #endregion

        public override string ToString(){
            return CourseID + " " + Code + " " + Title + " " + Description;
        }
    } // end CourseVO class
} // end namespace
