using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

using Web.App_Code;
using BusinessLogic.BO;
using Infrastructure.ValueObjects;

namespace Web.Pages.Admin {
    public partial class Test : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                EmployeeManagementBO bo = new EmployeeManagementBO();
                GridView1.DataSource = bo.GetAllEmployees();
                GridView1.DataBind();

            }
        }

        protected void OnButton1Click(object sender, EventArgs e) {
            StringBuilder sb = new StringBuilder();
            sb.Append("              URL: " + Request.Url + "\n");
            sb.Append("       User Agent: " + Request.UserAgent + "\n");
            sb.Append("User Host Address: " + Request.UserHostAddress + "\n");
            sb.Append("   User Host Name: " + Request.UserHostName + "\n");
            sb.Append("      Total Bytes: " + Request.TotalBytes + "\n");
            sb.Append("     Request Type: " + Request.RequestType + "\n");
            sb.Append("    Physical Path: " + Request.PhysicalPath + "\n");
            sb.Append("   User Languages: ");

            foreach (string s in Request.UserLanguages) {
                sb.Append(s + " ");
            }

            sb.Append("\n");
            textbox1.Text = sb.ToString();
        }


        protected void OnButton2Click(object sender, EventArgs e) {
            label1.Text = "Text has changed!";
        }


        public void DecodeViewStateBtnClick(object sender, EventArgs e) {
            LosFormatter formatter = new LosFormatter();
            Object viewstate_obj = formatter.Deserialize(textbox2.Text);
            textbox1.Text = string.Empty;
            textbox1.Text = ParseViewState(viewstate_obj, 0, "VIEW STATE: ");
        }



        protected string ParseViewState(object node, int depth, string label) {
            StringBuilder sb = new StringBuilder();
            ParseViewState(sb, node, depth, label);
            return sb.ToString();
        }


        private string ParseViewState(StringBuilder sb, object node, int depth, string label) {
            if (node == null) {
                sb.Append(Indent(depth) + label + "Null Node\n");
            }
            else if (node is Triplet) {
                sb.Append(Indent(depth) + label + "TRIPLET\n");
                ParseViewState(sb, ((Triplet)node).First, depth + 1, "First: ");
                ParseViewState(sb, ((Triplet)node).Second, depth + 1, "Second: ");
                ParseViewState(sb, ((Triplet)node).Third, depth + 1, "Third: ");

            }
            else if (node is Pair) {
                sb.Append(Indent(depth) + label + "PAIR:\n");
                ParseViewState(sb, ((Pair)node).First, depth + 1, "First: ");
                ParseViewState(sb, ((Pair)node).Second, depth + 1, "Second: ");

            }
            else if (node is ArrayList) {
                sb.Append(Indent(depth) + label + "ARRAYLIST:\n");
                for (int i = 0; i < ((ArrayList)node).Count; i++) {
                    ParseViewState(sb, ((ArrayList)node)[i], depth + 1, String.Format("[{0}]", i));
                }
            }
            else if (node.GetType().IsArray) {
                sb.Append(Indent(depth) + label + "ARRAY: ");
                sb.Append("(" + node.GetType().ToString() + ")\n");
                int count = 0;
                foreach (object o in (Array)node) {
                    ParseViewState(sb, o, depth + 1, String.Format("[{0}]", count++));
                }
            }
            else if (node is HybridDictionary) {
                sb.Append(Indent(depth) + label + "HYBRIDDICTIONARY: ");
                int count = 0;
                foreach (string key in ((HybridDictionary)node).Keys) {
                    sb.Append(Indent(depth) + key.ToString() + ": " + ((HybridDictionary)node)[key].ToString() + "\n");
                    ParseViewState(sb, ((HybridDictionary)node)[key], depth + 1, String.Format("({0})", count++));
                }
            }
            else if (node.GetType().IsPrimitive || node is string) {
                sb.Append(Indent(depth) + label);
                sb.Append(node.GetType().ToString() + " (" + node.ToString() + ")\n");
            }

            else {
                sb.Append(Indent(depth) + label + "Other - ");
                sb.Append(node.ToString() + "\n");
            }

            return sb.ToString();
        } // end ParseViewState() method



        private string Indent(int depth) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < depth; i++) {
                sb.Append("  ");
            }
            return sb.ToString();
        }
    }
}