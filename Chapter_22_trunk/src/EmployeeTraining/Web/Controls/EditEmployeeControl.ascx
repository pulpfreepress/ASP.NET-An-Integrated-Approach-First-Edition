<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditEmployeeControl.ascx.cs"
    Inherits="Web.Controls.EditEmployeeControl" %>
<%@ Register Src="~/Controls/SmartCalendar.ascx" TagPrefix="sc" TagName="SmartCalendar" %>
<%@ Register TagPrefix="bl" Namespace="BusinessLogic.Components" Assembly="BusinessLogic" %>
<table class="centeredTable">
    <tr>
        <td class="dataEntryLabel">
            First Name:
        </td>
        <td class="dataEntryControl">
            <asp:TextBox SkinID="DataEntryTextBox" ID="firstNameTextBox" 
            runat="server"></asp:TextBox>
            <span class="required">*</span>
            <asp:RequiredFieldValidator ID="firstNameReqFieldValidator" runat="server" 
            CssClass="errorMessage" ControlToValidate="firstNameTextBox"
             ErrorMessage="Required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="firstNameRegularExpressionValidator" 
            runat="server" CssClass="errorMessage" ControlToValidate="firstNameTextBox" 
             ValidationExpression="^[A-Z]{1}([a-zA-Z]{1,48})?([-]{1})?([a-zA-Z]{1,48})?[a-z]{1}$"
             ErrorMessage="Invalid characters!"></asp:RegularExpressionValidator>
        </td>
        <td class="dataEntryLabel">
        User Role
        </td>
        <td class="dataEntryControl">
          <bl:UserRolesDropDown ID="userRolesDropDown" runat="server" DefaultOption="Select User Role"
           ToolTip="Select user role from drop down list"></bl:UserRolesDropDown>
           <asp:requiredfieldvalidator ID="userRoleRequiredFieldValidator" runat="server"
            ControlToValidate="userRolesDropDown" 
            ErrorMessage="User Role is Required!"
            CssClass="errorMessage"></asp:requiredfieldvalidator>
        </td>
    </tr>
    <tr>
        <td class="dataEntryLabel">
            Middle Name:
        </td>
        <td class="dataEntryControl" >
            <asp:TextBox SkinID="DataEntryTextBox" ID="middleNameTextBox" 
            runat="server"></asp:TextBox>
            <span class="required">*</span>
            <asp:RequiredFieldValidator ID="middleNameReqFieldValidator" runat="server" 
            CssClass="errorMessage" ControlToValidate="middleNameTextBox" 
            ErrorMessage="Required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="middleNameRegularExpressionValidator" 
            runat="server" CssClass="errorMessage"
             ControlToValidate="middleNameTextBox" 
             ValidationExpression="^[A-Z]{1}([a-zA-Z]{1,48})?([-]{1})?([a-zA-Z]{1,48})?[a-z]{1}$"
             ErrorMessage="Invalid characters!"></asp:RegularExpressionValidator>
        </td>
        <td class="dataEntryLabel">
        Username
        </td>
        <td class="dataEntryControl">
        <asp:TextBox ID="usernameTextBox" runat="server" MaxLength="50" Width="100"
         ToolTip="Enter username"></asp:TextBox>
         <asp:RequiredFieldValidator ID="usernameRequiredFieldValidator" runat="server"
          ControlToValidate="usernameTextBox" 
          ErrorMessage="Username required!" 
          CssClass="errorMessage"></asp:RequiredFieldValidator>
          <asp:RegularExpressionValidator ID="usernameRegularExpressionValidator" 
            runat="server" CssClass="errorMessage" 
            ControlToValidate="usernameTextBox" 
            ValidationExpression="([0-9a-zA-Z ]{1,48})"
            ErrorMessage="Invalid Characters!"></asp:RegularExpressionValidator>
        
        </td>
    </tr>
    <tr>
        <td class="dataEntryLabel">
            Last Name:
        </td>
        <td class="dataEntryControl">
            <asp:TextBox SkinID="DataEntryTextBox" ID="lastNameTextBox" 
            runat="server"></asp:TextBox>
            <span class="required">*</span>
            <asp:RequiredFieldValidator ID="lastNameReqFieldValidator" runat="server" 
            CssClass="errorMessage" ControlToValidate="lastNameTextBox" 
            ErrorMessage="Required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="lastNameRegularExpressionValidator" 
            runat="server" CssClass="errorMessage" 
            ControlToValidate="lastNameTextBox"
            ValidationExpression="^[A-Z]{1}([a-zA-Z]{1,48})?([-]{1})?([a-zA-Z]{1,48})?[a-z]{1}$"
            ErrorMessage="Invalid characters!"></asp:RegularExpressionValidator>
        </td>
        <td class="dataEntryLabel">
        Password
        </td>
        <td class="dataEntryControl">
          <asp:TextBox ID="passwordTextBox" runat="server" MaxLength="50"
          Width="100"  ToolTip="Enter password"></asp:TextBox>
          <asp:RequiredFieldValidator ID="passwordRequiredFieldValidator" runat="server"
           ControlToValidate="passwordTextBox" ErrorMessage="Password Required!"
           CssClass="errorMessage"></asp:RequiredFieldValidator>
          <asp:RegularExpressionValidator ID="passwordRegularExpressionValidator" 
            runat="server" CssClass="errorMessage" 
            ControlToValidate="passwordTextBox" 
            ValidationExpression="([0-9a-zA-Z)(*&^%$#@!]{1,48})"
            ErrorMessage="Invalid Characters!"></asp:RegularExpressionValidator>
        </td>


    </tr>
    <tr>
        <td class="dataEntryLabel">
            Address 1:
        </td>
        <td class="dataEntryControl" colspan="4">
            <asp:TextBox SkinID="DataEntryTextBox" ID="address1TextBox" 
            runat="server"></asp:TextBox>
            <span class="required">*</span>
            <asp:RequiredFieldValidator ID="address1ReqFieldValidator" runat="server"
            CssClass="errorMessage" ControlToValidate="address1TextBox" 
            ErrorMessage="Required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="address1RegularExpressionValidator" 
            runat="server" CssClass="errorMessage" 
            ControlToValidate="address1TextBox" 
            ValidationExpression="([0-9a-zA-Z ]{1,48})"
            ErrorMessage="Invalid characters!"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="dataEntryLabel">
            Address 2:
        </td>
        <td class="dataEntryControl" colspan="4">
            <asp:TextBox SkinID="DataEntryTextBox" ID="address2TextBox" 
            runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="address2RegularExpressionValidator" 
            runat="server" CssClass="errorMessage" ControlToValidate="address2TextBox" 
            ValidationExpression="([0-9a-zA-Z ]{1,48})"
            ErrorMessage="Invalid characters!"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="dataEntryLabel">
            City:
        </td>
        <td class="dataEntryControl" colspan="4">
            <asp:TextBox SkinID="DataEntryTextBox" ID="cityTextBox" 
            runat="server"></asp:TextBox>
            <span class="required">*</span>
            <asp:RequiredFieldValidator ID="cityReqFieldValidator" runat="server" 
            CssClass="errorMessage" ControlToValidate="cityTextBox" 
            ErrorMessage="Required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="cityRegularExpressionValidator" 
            runat="server" CssClass="errorMessage" 
            ControlToValidate="cityTextBox" 
            ValidationExpression="([0-9a-zA-Z ]{1,48})"
            ErrorMessage="Invalid Characters!"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="dataEntryLabel">
            State:
        </td>
        <td class="dataEntryControl" colspan="3">
            <bl:StateDropDown ID="stateDropDown" runat="server" DefaultOption="Select State">
            </bl:StateDropDown>
            <span class="required">*</span>
            <asp:RequiredFieldValidator ID="stateDropDownValidator" runat="server" 
            CssClass="errorMessage" ControlToValidate="stateDropDown" 
            ErrorMessage="Required"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="dataEntryLabel">
            Zip Code:
        </td>
        <td class="dataEntryControl" colspan="3">
            <asp:TextBox SkinID="DataEntryTextBox" ID="zipTextBox" runat="server" 
            ToolTip="Format: nnnnn OR nnnnn-nnnn"></asp:TextBox>
            <span class="required">*</span>
            <asp:RequiredFieldValidator ID="zipRequiredFieldValidator" runat="server" 
            CssClass="errorMessage" ControlToValidate="zipTextBox"
             ErrorMessage="Required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="zipcodeRegularExpressionValidator" 
            runat="server" CssClass="errorMessage" ControlToValidate="zipTextBox" 
            ValidationExpression="^[0-9]{5}([-]{1}[0-9]{4})?$"
            ErrorMessage="Invalid Characters!"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="dataEntryLabel">
            PhoneType:
        </td>
        <td class="dataEntryControl" colspan="3">
            <bl:PhoneTypeDropDown ID="phoneTypeDropDown" runat="server" 
            DefaultOption="Select Phone Type">
            </bl:PhoneTypeDropDown>
            <asp:RequiredFieldValidator ID="phoneTypeRequiredFieldValidator" 
            runat="server" CssClass="errorMessage"
                ControlToValidate="phoneTypeDropDown" 
                ErrorMessage="Required"></asp:RequiredFieldValidator>
            <span class="dataEntryLabel">Phone: </span><span class="dataEntryControl">
                <asp:TextBox SkinID="DataEntryTextBox" ID="phoneTextBox" runat="server" 
                ToolTip="Format: (nnn) nnn-nnnn"></asp:TextBox>
                <span class="required">*</span>
                <asp:RequiredFieldValidator ID="phoneReqFieldValidator" runat="server" 
                CssClass="errorMessage" ControlToValidate="phoneTextBox" 
                ErrorMessage="Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="phoneNumberRegularExpressionValidator" runat="server"
                CssClass="errorMessage" ControlToValidate="phoneTextBox" 
                ValidationExpression="^[(]{1}[0-9]{3}[)]{1}[ ]{1}[0-9]{3}[-][0-9]{4}$"
                ErrorMessage="Invalid Characters!"></asp:RegularExpressionValidator>
            </span>
        </td>
    </tr>
    <tr>
        <td class="dataEntryLabel">
            Email Type:
        </td>
        <td class="dataEntryControl" colspan="3">
            <bl:EmailTypeDropDown ID="emailTypeDropDown" runat="server" 
            DefaultOption="Select Email Type">
            </bl:EmailTypeDropDown>
            <asp:RequiredFieldValidator ID="emailTypeRequiredFieldValidator" 
            runat="server" CssClass="errorMessage"
            ControlToValidate="emailTypeDropDown" 
            ErrorMessage="Required"></asp:RequiredFieldValidator>
            <span class="dataEntryLabel">Email: </span><span class="dataEntryControl">
                <asp:TextBox SkinID="DataEntryTextBox" ID="emailTextBox" 
                runat="server"></asp:TextBox>
                <span class="required">*</span>
                <asp:RequiredFieldValidator ID="emailReqFieldValidator" runat="server" 
                CssClass="errorMessage" ControlToValidate="emailTextBox" 
                ErrorMessage="Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="emailRegularExpressionValidator" 
                runat="server" CssClass="errorMessage" ControlToValidate="emailTextBox" 
                ValidationExpression="^[a-zA-Z]+((\.(?![.-]))|(\-(?![-.]))|([a-zA-Z0-9]+))*([a-zA-Z0-9](?![.-]))[@]{1}[a-zA-Z]+[.]{1}[a-zA-Z]+$"
                ErrorMessage="Invalid Characters!"></asp:RegularExpressionValidator>
                
            </span>
        </td>
    </tr>
    <tr>
        <td class="dataEntryLabel">
            Birthday:
        </td>
        <td class="dataEntryControl">
            <sc:SmartCalendar ID="birthdayCalendar" runat="server" />
            <asp:CustomValidator ID="birthdayCustomValidator" runat="server"
             CssClass="errorMessage" 
             ErrorMessage="Invalid birthday Must be 16 years or older!"
             OnServerValidate="ValidateBirthday"></asp:CustomValidator>
        </td>
        
    </tr>
    <tr>
        <td class="dataEntryLabel">
            Hiredate:
        </td>
        <td class="dataEntryControl">
            <sc:SmartCalendar ID="hiredateCalendar" runat="server" />
            <asp:CustomValidator ID="hireDateCustomValidator" runat="server"
             CssClass="errorMessage" 
             ErrorMessage="Invalid HireDate! Employee must be 16 years or older!"
             OnServerValidate="ValidateHireDate"></asp:CustomValidator>
        </td>
        
    </tr>
    <tr>
        <td class="dataEntryLabel">
            Active:
        </td>
        <td class="dataEntryControl">
            <asp:CheckBox ID="isActiveCheckBox" runat="server" Checked="true" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3" class="required">
            * Indicates Required Field
        </td>
    </tr>
</table>
