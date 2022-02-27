using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Infrastructure;
using DataAccess;


namespace BusinessLogic.BO {
    public class BaseBO : BaseObject {

        #region Constructors

        public BaseBO() : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }

        public BaseBO(Type loggerClassType) : base(loggerClassType) { }

        #endregion Constructors


    }  // end BaseBO class definition
} // end namespace
