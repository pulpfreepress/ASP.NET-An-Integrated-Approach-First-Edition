using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Infrastructure;
using Infrastructure.ValueObjects;

namespace BusinessLogic.Utils {
    
    public class GenericItemComparer<T> : IComparer<object> {
        #region Fields
        private string _sortColumn;
        private bool _reverse;
        #endregion

        #region Constructors
        
        public GenericItemComparer(string sortExpression, string sortDirection) {
            _reverse = sortDirection.ToLowerInvariant().Equals(BusinessConstants.SORT_DESCENDING);
            _sortColumn = sortExpression;
        }
        #endregion

        #region GenericItemComparer<T> Members

        public int Compare(object x, object y) {

            int retVal = 0;

            // Figure out what type of objects we are comparing
            Type type = x.GetType();
            PropertyInfo prop = type.GetProperty(_sortColumn);

            // Figure out the type of the expression we are comparing
            Type valueType = prop.GetValue(x, null).GetType();

            // Get the value of the first object we need to compare
            object oX = prop.GetValue(x, null);

            // Get the value of the second object we need to compare
            object oY = prop.GetValue(y, null);

            // Do the comparison based upon the type
            switch (valueType.ToString()) {
                case "System.String":
                    retVal = String.Compare((string)oX, (string)oY);
                    BaseObject.LogDebug(x.GetType(), "Sorting strings...");
                    break;
                case "System.DateTime":
                    retVal = DateTime.Compare((DateTime)oX, (DateTime)oY);
                    BaseObject.LogDebug(x.GetType(), "Sorting DateTime objects...");
                    break;
                case "System.Guid":
                    Guid guidx = (Guid)oX;
                    retVal = guidx.CompareTo((Guid)oY);
                    break;
                case "System.Int16":
                    Int16 shortx = (Int16)oX;
                    retVal = shortx.CompareTo((Int16)oY);
                    break;
                case "System.Int32":
                    Int32 intx = (Int32)oX;
                    retVal = intx.CompareTo((Int32)oY);
                    break;
                case "System.Int64":
                    Int64 longx = (Int64)oX;
                    retVal = longx.CompareTo((Int64)oY);
                    break;
                case "System.Boolean":
                    Boolean boolx = (Boolean)oX;
                    retVal = boolx.CompareTo((Boolean)oY);
                    break;
                case "System.Decimal":
                    Decimal decx = (Decimal)oX;
                    retVal = decx.CompareTo((Decimal)oY);
                    break;

                /*******************************************************************
                case "Infrastructure.ValueObjects.EmployeeVO":
                    EmployeeVO personVO = (EmployeeVO)oX;
                    retVal = personVO.LastName.CompareTo(((EmployeeVO)oY).LastName);
                    break;
                 * ****************************************************************/
            }

            return (retVal * (_reverse ? -1 : 1));
        }

        #endregion
    }
}
