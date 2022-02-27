using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Web.Security;
using System.Web.Configuration;

using Web.App_Code;
using BusinessLogic.Utils;
using Infrastructure.ValueObjects;

namespace Web.Controls {
    public partial class EditEmployeeControl : BaseControl {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                birthdayCalendar.PopulateDropDowns();
                hiredateCalendar.PopulateDropDowns();
                birthdayCalendar.SetSelectedAndVisibleDate(DateTime.Now);
                hiredateCalendar.SetSelectedAndVisibleDate(DateTime.Now);
                firstNameTextBox.MaxLength = GetMaxFieldLength();
                firstNameTextBox.Width = GetFieldWidth();
                middleNameTextBox.MaxLength = GetMaxFieldLength();
                middleNameTextBox.Width = GetFieldWidth();
                lastNameTextBox.MaxLength = GetMaxFieldLength();
                lastNameTextBox.Width = GetFieldWidth();
                address1TextBox.Width = GetFieldWidth();
                address1TextBox.MaxLength = GetMaxFieldLength();
                address2TextBox.Width = GetFieldWidth();
                address2TextBox.MaxLength = GetMaxFieldLength();
                cityTextBox.MaxLength = GetMaxFieldLength();
                cityTextBox.Width = GetFieldWidth();
                emailTextBox.MaxLength = GetMaxFieldLength();
                emailTextBox.Width = GetFieldWidth();
                phoneTextBox.MaxLength = GetMaxFieldLength();
                phoneTextBox.Width = GetFieldWidth();
                

            }
        }

        public EmployeeVO Employee {
            get {
                
                EmployeeVO vo = (EmployeeVO)Session[WebConstants.EMPLOYEE];
                if (vo == null) {
                    vo = new EmployeeVO();
                }
                vo.FirstName = firstNameTextBox.Text;
                vo.MiddleName = middleNameTextBox.Text;
                vo.LastName = lastNameTextBox.Text;
                vo.Birthday = birthdayCalendar.SelectedDate;
                vo.HireDate = hiredateCalendar.SelectedDate;
                vo.IsActive = isActiveCheckBox.Checked;
                vo.UserRole.RoleID = userRolesDropDown.SelectedIndex;
                vo.Username = usernameTextBox.Text;
                string hashstring = (vo.Username.Trim() + passwordTextBox.Text.Trim());
                vo.LoginHash = FormsAuthentication.HashPasswordForStoringInConfigFile(hashstring, FormsAuthPasswordFormat.MD5.ToString());


                if (vo.Addresses.Count > 0) {
                    vo.Addresses[0].Address1 = address1TextBox.Text;
                    vo.Addresses[0].Address2 = address2TextBox.Text;
                    vo.Addresses[0].City = cityTextBox.Text;
                    vo.Addresses[0].State = stateDropDown.SelectedValue;
                    vo.Addresses[0].Zip = zipTextBox.Text;
                }
                else {
                    AddressVO addressVO = new AddressVO();
                    addressVO.Address1 = address1TextBox.Text;
                    addressVO.Address2 = address2TextBox.Text;
                    addressVO.City = cityTextBox.Text;
                    addressVO.State = stateDropDown.SelectedValue;
                    addressVO.Zip = zipTextBox.Text;
                    vo.Addresses.Add(addressVO);
                    LogDebug("Address count: " + vo.Addresses.Count);
                }

                if (vo.PhoneNumbers.Count > 0) {
                    vo.PhoneNumbers[0].PhoneType.PhoneTypeID = phoneTypeDropDown.SelectedIndex;
                    vo.PhoneNumbers[0].PhoneNumber = phoneTextBox.Text;
                }
                else {
                    PhoneNumberVO phoneNumberVO = new PhoneNumberVO();
                    phoneNumberVO.PhoneType.PhoneTypeID = phoneTypeDropDown.SelectedIndex;
                    phoneNumberVO.PhoneNumber = phoneTextBox.Text;
                    vo.PhoneNumbers.Add(phoneNumberVO);
                    LogDebug("Phone count: " + vo.PhoneNumbers.Count);
                }

                if (vo.EmailAddresses.Count > 0) {
                    vo.EmailAddresses[0].EmailType.EmailTypeID = emailTypeDropDown.SelectedIndex;
                    vo.EmailAddresses[0].EmailAddress = emailTextBox.Text;
                }
                else {
                    EmailVO emailVO = new EmailVO();
                    emailVO.EmailType.EmailTypeID = emailTypeDropDown.SelectedIndex;
                    emailVO.EmailAddress = emailTextBox.Text;
                    vo.EmailAddresses.Add(emailVO);
                    LogDebug("Email count: " + vo.EmailAddresses.Count);
                }

                return vo;
            }

            set {
                Session.Add(WebConstants.EMPLOYEE, value);
                firstNameTextBox.Text = value.FirstName;
                middleNameTextBox.Text = value.MiddleName;
                lastNameTextBox.Text = value.LastName;
                birthdayCalendar.SetSelectedAndVisibleDate(value.Birthday);
                hiredateCalendar.SetSelectedAndVisibleDate(value.HireDate);
                isActiveCheckBox.Checked = value.IsActive;
                userRolesDropDown.SelectedIndex = value.UserRole.RoleID;
                usernameTextBox.Text = value.Username;
                passwordTextBox.Text = string.Empty;

                if (value.Addresses.Count > 0) {
                    address1TextBox.Text = value.Addresses[0].Address1;
                    address2TextBox.Text = value.Addresses[0].Address2;
                    cityTextBox.Text = value.Addresses[0].City;
                    stateDropDown.SelectedValue = value.Addresses[0].State;
                    zipTextBox.Text = value.Addresses[0].Zip;
                }

                if (value.PhoneNumbers.Count > 0) {
                    phoneTextBox.Text = value.PhoneNumbers[0].PhoneNumber;
                    phoneTypeDropDown.SelectedIndex = value.PhoneNumbers[0].PhoneType.PhoneTypeID;
                }

                if (value.EmailAddresses.Count > 0) {
                    emailTextBox.Text = value.EmailAddresses[0].EmailAddress;
                    emailTypeDropDown.SelectedIndex = value.EmailAddresses[0].EmailType.EmailTypeID;
                }
            }
        }


        public bool Enabled {
            set {
                firstNameTextBox.Enabled = value;
                middleNameTextBox.Enabled = value;
                lastNameTextBox.Enabled = value;
                birthdayCalendar.Enabled = value;
                hiredateCalendar.Enabled = value;
                address1TextBox.Enabled = value;
                address2TextBox.Enabled = value;
                cityTextBox.Enabled = value;
                stateDropDown.Enabled = value;
                zipTextBox.Enabled = value;
                phoneTypeDropDown.Enabled = value;
                phoneTextBox.Enabled = value;
                emailTypeDropDown.Enabled = value;
                emailTextBox.Enabled = value;
                userRolesDropDown.Enabled = value;
                usernameTextBox.Enabled = value;
                passwordTextBox.Enabled = value;
            }
        }


        public void Clear() {
            firstNameTextBox.Text = string.Empty;
            middleNameTextBox.Text = string.Empty;
            lastNameTextBox.Text = string.Empty;
            birthdayCalendar.SetSelectedAndVisibleDate(DateTime.Now);
            hiredateCalendar.SetSelectedAndVisibleDate(DateTime.Now);
            isActiveCheckBox.Checked = true;
            address1TextBox.Text = string.Empty;
            address2TextBox.Text = string.Empty;
            cityTextBox.Text = string.Empty;
            stateDropDown.PopulateControl();
            zipTextBox.Text = string.Empty;
            phoneTypeDropDown.PopulateControl();
            phoneTextBox.Text = string.Empty;
            emailTypeDropDown.PopulateControl();
            emailTextBox.Text = string.Empty;
            userRolesDropDown.PopulateControl();
            usernameTextBox.Text = string.Empty;
            passwordTextBox.Text = string.Empty;
        }

        private int GetMaxFieldLength() {
            return BusinessConstants.MAX_FIELD_LENGTH;
        }

        private int GetFieldWidth() {
            return BusinessConstants.MAX_FIELD_LENGTH * 8;
        }


        protected void ValidateBirthday(object sender, ServerValidateEventArgs args) {

            TimeSpan ts = DateTime.Now.Subtract(birthdayCalendar.SelectedDate);
            int employee_age = (ts.Days / BusinessConstants.DAYS_PER_YEAR);
            if (employee_age < BusinessConstants.MIN_AGE_OF_HIRE_IN_YEARS) {
                args.IsValid = false;
            }
        }


        protected void ValidateHireDate(object sender, ServerValidateEventArgs args) {
           
            if (hiredateCalendar.SelectedDate > DateTime.Now) {
                args.IsValid = false;
            }

         
            TimeSpan ts = hiredateCalendar.SelectedDate.Subtract(birthdayCalendar.SelectedDate);
            int years_between_birthday_and_hiredate = ts.Days / 365;
            if (years_between_birthday_and_hiredate < BusinessConstants.MIN_AGE_OF_HIRE_IN_YEARS) {
                args.IsValid = false;
            }
        }

    }
}