using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BusinessLogic.Components {
    public abstract class BaseDropDown : DropDownList {

        #region Public Properties

        public String DefaultOption { get; set;  }

        #endregion Public Properties


        #region Constructors

        public BaseDropDown() {
            DefaultOption = String.Empty;
        }

        #endregion Constructors


        #region Public Methods

        public virtual void PopulateControl() { }

        protected void AddDefaultOption() {
            if ((!DefaultOption.Equals("")) && (DefaultOption != null)) {
                ListItem defaultItem = new ListItem(" - " + DefaultOption + " - ", String.Empty);
                this.Items.Insert(0, defaultItem);
            }
        }

        #endregion Public Methods

    } // end BaseDropDown class declaration
} // end namespace
