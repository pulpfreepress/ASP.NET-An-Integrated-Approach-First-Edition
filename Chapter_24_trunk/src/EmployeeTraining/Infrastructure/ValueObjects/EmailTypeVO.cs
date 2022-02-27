using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Infrastructure.ValueObjects {
    public class EmailTypeVO {

      #region Public properties

        [DisplayName("Email Type ID")]
        public int EmailTypeID { get; set; }

       [Required(ErrorMessage="Description field is required!")]
       [RegularExpression(@"^[a-zA-Z ]{1,50}$", ErrorMessage="Invalid Characters!")]
        public string Description { get; set; }

       #endregion Public properties

       #region Constructors
        public EmailTypeVO() : this(0, string.Empty) { }

        public EmailTypeVO(int phoneTypeID, string description) {
           EmailTypeID = phoneTypeID;
           Description = description;
        }

        public EmailTypeVO(string description) {
           Description = description;
        }
        
        #endregion Constructors

        public override string ToString() {
            return EmailTypeID + " " + Description;
        }

       
    } // end class
} // end namespace
