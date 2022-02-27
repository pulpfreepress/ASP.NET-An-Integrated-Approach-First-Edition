using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ValueObjects {
    public class PhoneTypeVO {

       #region Public properties
        public int PhoneTypeID { get; set; }
        public string Description { get; set; }
       #endregion Public properties

        #region Constructors
        public PhoneTypeVO() : this(0, string.Empty) { }

        public PhoneTypeVO(int phoneTypeID, string description) {
            PhoneTypeID = phoneTypeID;
            Description = description;
        }

        public PhoneTypeVO(string description) {
            Description = description;
        }
        #endregion Constructors
    }
}
