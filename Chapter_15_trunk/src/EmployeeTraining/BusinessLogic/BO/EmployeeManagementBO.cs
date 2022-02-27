using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Infrastructure;
using Infrastructure.Exceptions;
using Infrastructure.ValueObjects;
using DataAccess.DAO;

namespace BusinessLogic.BO {
    public class EmployeeManagementBO : BaseBO {

        #region Constructors

        public EmployeeManagementBO() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }

        #endregion Constructors


        #region Public Methods

        public EmployeeVO GetEmployee(int employeeID) {
            LogDebug("Entering GetEmployee() method with employeeID = " + employeeID);
            EmployeeVO vo = null;
            try {
                EmployeeDAO empDAO = new EmployeeDAO();
                vo = empDAO.SelectEmployee(employeeID);
            }
            catch (Exception e) {
                LogError("Problem getting employee with employeeID = " + employeeID, e);
                throw new BLException("Probem getting employee with employeeID = " + employeeID, e);
            }
            return vo;
        }


        public List<EmployeeVO> GetAllEmployees() {
            LogDebug("Entering GetAllEmployees() method");
            List<EmployeeVO> employeeList = null;

            try {
                EmployeeDAO empDAO = new EmployeeDAO();
                employeeList = empDAO.SelectAllEmployees();
            }
            catch (Exception e) {
                LogError("Problem getting all employees!", e);
                throw new BLException("Problem getting all employees!", e);
            }
            return employeeList;
        }


        public List<EmployeeVO> GetAllActiveEmployees() {
            LogDebug("Entering GetAllAciveEmployees() method");
            List<EmployeeVO> employeeList = null;
            try {
                EmployeeDAO empDAO = new EmployeeDAO();
                employeeList = empDAO.SelectAllActiveEmployees();
            }
            catch (Exception e) {
                LogError("Problem getting all employees!", e);
                throw new BLException("Problem getting all employees!", e);
            }
            return employeeList;
        }



        public EmployeeVO CreateEmployee(EmployeeVO vo) {
            LogDebug("Entering CreateEmployee() method with EmployeeVO = " + vo);

            try {
                EmployeeDAO empDAO = new EmployeeDAO();
                vo = empDAO.InsertEmployee(vo);
            }
            catch (Exception e) {
                LogError("Problem creating employee!", e);
                throw new BLException("Problem creating employee!", e);
            }
            return vo;
        }


        public EmployeeVO UpdateEmployee(EmployeeVO vo) {
            LogDebug("Entering UpdateEmployee() method with EmployeeVO = " + vo);

            try {
                EmployeeDAO empDAO = new EmployeeDAO();
                vo = empDAO.UpdateEmployee(vo);
            }
            catch (Exception e) {
                LogError("Problem updating employee!", e);
                throw new BLException("Problem updating employee!", e);
            }
            return vo;
        }


        public void InactivateEmployee(EmployeeVO vo) {
            LogDebug("Entering InactivateEmployee() method for EmployeeVO = " + vo);

            try {
                EmployeeDAO empDAO = new EmployeeDAO();
                empDAO.SetEmployeeIsActiveState(vo.EmployeeID, false);
            }
            catch (Exception e) {
                LogError("Problem inactivating employee!", e);
                throw new BLException("Problem inactivating employee!", e);
            }
        }


        public void ActivateEmployee(EmployeeVO vo) {
            LogDebug("Entering ActivateEmployee() method for EmployeeVO = " + vo);

            try {
                EmployeeDAO empDAO = new EmployeeDAO();
                empDAO.SetEmployeeIsActiveState(vo.EmployeeID, true);
            }
            catch (Exception e) {
                LogError("Problem activating employee!", e);
                throw new BLException("Problem activating employee!", e);
            }
        }


        public List<CompletedCourseVO> GetEmployeeTrainingRecords(EmployeeVO vo) {
            LogDebug("Entering GetEmployeeTrainingRecords() method with EmployeeVO " + vo);
            List<CompletedCourseVO> list = null;
            try {
                TrainingDAO trainingDAO = new TrainingDAO();
                list = trainingDAO.GetTrainingCompletedByEmployee(vo.EmployeeID);
            }
            catch (Exception e) {
                LogError("Problem getting employee training records!", e);
                throw new BLException("Problem getting employee training records!", e);
            }
            return list;
        }


        public List<CompletedCourseVO> InsertCompletedTrainingRecord(CompletedCourseVO vo) {
            LogDebug("Entering InsertCompletedTrainingRecord() method with CompletedCourseVO = " + vo);
            List<CompletedCourseVO> list = null;
            try {
                TrainingDAO trainingDAO = new TrainingDAO();
                list = trainingDAO.InsertCompletedTraining(vo);
            }
            catch (Exception e) {
                LogError("Problem inserting completed training record!", e);
                throw new BLException("Problem inserting completed training record!", e);
            }
            return list;
        }


        public EmployeeVO InsertEmployeeTrainingRecords(EmployeeVO empVO) {
            LogDebug("Entering InsertEmployeeTrainingRecords() method with EmployeeVO = " + empVO);

            using (TransactionScope ts = new TransactionScope()) {
                try {
                    TrainingDAO trainingDAO = new TrainingDAO();
                    trainingDAO.DeleteEmployeeTrainingRecords(empVO.EmployeeID);
                   empVO.CompletedCourses = trainingDAO.InsertEmployeeTrainingRecords(empVO);
                    ts.Complete();

                }
                catch (Exception e) {
                    LogError("Problem inserting employee's training records!", e);
                    throw new BLException("Problem inserting employee's training records!", e);
                }
                finally {
                    ts.Dispose();
                }
            } // end TransactionScope
            return empVO;
        } 


        #endregion Public Methods
    } // end EmployeeManagementBO class definition
} // end namespace
