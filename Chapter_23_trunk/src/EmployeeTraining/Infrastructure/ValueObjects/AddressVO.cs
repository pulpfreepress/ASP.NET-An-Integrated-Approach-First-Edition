using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ValueObjects {
    public class AddressVO {
      #region Public Properties

        public int AddressID { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }

      #endregion Public Properties

      #region Constructors
        public AddressVO() : this(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

        public AddressVO(int addressID, string address1, string address2, string city, string state, string zip) {
            AddressID = addressID;
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            Zip = zip;
        }

        public AddressVO(string address1, string address2, string city, string state, string zip) {
            AddressID = 0;
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            Zip = zip;
        }

      #endregion Constructors

    }
}
