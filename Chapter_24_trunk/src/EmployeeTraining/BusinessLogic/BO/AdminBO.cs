using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Infrastructure;
using Infrastructure.Exceptions;
using Infrastructure.ValueObjects;
using DataAccess.DAO;
using BusinessLogic.Utils;

namespace BusinessLogic.BO {
    public class AdminBO : BaseBO {

     #region Constructors

        public AdminBO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }

     #endregion Constructors

     #region Methods

        public List<EmailTypeVO> GetEmailTypes() {
            EmailTypeDAO dao = new EmailTypeDAO();
            return dao.SelectAllEmailTypes();
        }

        public EmailTypeVO GetEmailType(int id) {
            EmailTypeDAO dao = new EmailTypeDAO();
            return dao.SelectEmailType(id);
        }

        public List<EmailTypeVO> InsertEmailType(EmailTypeVO vo) {
            EmailTypeDAO dao = new EmailTypeDAO();
            dao.InsertEmailType(vo);
            return dao.SelectAllEmailTypes();
        }

        public List<EmailTypeVO> UpdateEmailType(EmailTypeVO vo) {
            EmailTypeDAO dao = new EmailTypeDAO();
            dao.UpdateEmailType(vo);
            return dao.SelectAllEmailTypes();
        }

        public List<EmailTypeVO> DeleteEmailType(EmailTypeVO vo) {
            EmailTypeDAO dao = new EmailTypeDAO();
            dao.DeleteEmailType(vo.EmailTypeID);
            return dao.SelectAllEmailTypes();
        }

     #endregion Methods
    }
}
