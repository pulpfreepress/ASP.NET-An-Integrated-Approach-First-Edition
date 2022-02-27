using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Web.App_Code;

namespace Web.Controls {
    public partial class EditUpdateSaveCancelControl : BaseControl {
        protected void Page_Load(object sender, EventArgs e) {

        }

        #region Delegate Properties

        private Delegate _saveMethod;
        public Delegate SaveMethod {
            get { return _saveMethod; }
            set { _saveMethod = value; }

        }



        private Delegate _updateMethod;
        public Delegate UpdateMethod {
            get { return _updateMethod; }
            set { _updateMethod = value; }
        }


        private Delegate _cancelMethod;
        public Delegate CancelMethod {
            get { return _cancelMethod; }
            set { _cancelMethod = value; }
        }

        private Delegate _editMethod;
        public Delegate EditMethod {
            get { return _editMethod; }
            set { _editMethod = value; }
        }

        #endregion


        public void CallSaveHandlerMethod(object sender, EventArgs e) {
            _saveMethod.DynamicInvoke();
        }


        public void CallUpdateHandlerMethod(object sender, EventArgs e) {
            _updateMethod.DynamicInvoke();
        }


        public void CallCancelHandlerMethod(object sender, EventArgs e) {
            _cancelMethod.DynamicInvoke();
        }


        public void CallEditHandlerMethod(object sender, EventArgs e) {
            _editMethod.DynamicInvoke();
        }



        public void ShowUpdateButton() {
            editButton.Visible = false;
            updateButton.Visible = true;
            saveButton.Visible = false;
            cancelButton.Text = "Cancel";
        }

        public void ShowSaveButton() {
            saveButton.Visible = true;
            editButton.Visible = false;
            updateButton.Visible = false;
            cancelButton.Text = "Cancel";
        }

        public void HideSaveAndUpdateButtons() {
            editButton.Visible = false;
            saveButton.Visible = false;
            updateButton.Visible = false;
            cancelButton.Text = "Close";
        }

        public void ShowEditButton() {
            editButton.Visible = true;
            updateButton.Visible = false;
            saveButton.Visible = false;
            cancelButton.Visible = true;
        }



    }
}