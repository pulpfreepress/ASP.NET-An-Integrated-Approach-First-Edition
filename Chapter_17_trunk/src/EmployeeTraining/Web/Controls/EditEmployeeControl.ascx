<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditEmployeeControl.ascx.cs" Inherits="Web.Controls.EditEmployeeControl" %>

<%@ Register Src="~/Controls/SmartCalendar.ascx"  TagPrefix="sc" TagName="SmartCalendar" %>

<table class="centeredTable">
  <tr>
    <td class="dataEntryLabel">First Name:</td>
    <td class="dataEntryControl">
       <asp:TextBox SkinID="DataEntryTextBox" ID="firstNameTextBox" runat="server" ></asp:TextBox>
    </td>
  </tr>

  <tr>
    <td class="dataEntryLabel">Middle Name:</td>
    <td class="dataEntryControl"> 
       <asp:TextBox SkinID="DataEntryTextBox" ID="middleNameTextBox" runat="server" ></asp:TextBox>
    </td>
  </tr>

   <tr>
    <td class="dataEntryLabel">Last Name: </td>
    <td class="dataEntryControl"> 
       <asp:TextBox SkinID="DataEntryTextBox" ID="lastNameTextBox" runat="server" ></asp:TextBox>
    </td>
  </tr>

   <tr>
    <td class="dataEntryLabel">Birthday: </td>
    <td class="dataEntryControl"> 
      <sc:SmartCalendar  ID="birthdayCalendar" runat="server" />
    </td>
  </tr>

   <tr>
    <td class="dataEntryLabel">Hiredate: </td>
    <td class="dataEntryControl"> 
      <sc:SmartCalendar  ID="hiredateCalendar" runat="server" />
    </td>
  </tr>

   <tr>
    <td class="dataEntryLabel">Active:  </td>
    <td class="dataEntryControl"> 
      <asp:CheckBox ID="isActiveCheckBox" runat="server" Checked="true" />
    </td>
  </tr>

</table>