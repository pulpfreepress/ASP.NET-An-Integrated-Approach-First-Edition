using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ValueObjects {
    public class EmailTypeVO {

      #region Public properties
        public int EmailTypeID { get; set; }
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
    }
}
