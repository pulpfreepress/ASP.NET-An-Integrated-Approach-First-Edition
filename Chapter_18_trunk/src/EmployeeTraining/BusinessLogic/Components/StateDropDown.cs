using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace BusinessLogic.Components {
    public class StateDropDown : BaseDropDown {

        public override void PopulateControl() {
            this.Items.Clear();

            String procName = "appSP_GetStatesLU";

            using (DataSet ds = DatabaseFactory.CreateDatabase().ExecuteDataSet(CommandType.StoredProcedure, procName)) {

                this.DataSource = ds;
                this.DataValueField = ds.Tables[0].Columns[0].ToString();
                this.DataTextField = ds.Tables[0].Columns[1].ToString();
                this.DataBind();
            }
            AddDefaultOption();
        }
    } // End StateDropDown class definition
} // End namespace
