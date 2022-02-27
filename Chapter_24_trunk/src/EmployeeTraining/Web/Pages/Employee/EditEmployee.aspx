<%@ Page Title="Edit Employee Page" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" 
         CodeBehind="EditEmployee.aspx.cs" Inherits="Web.Pages.Employee.EditEmployee" Theme="DefaultTheme" %>

<%@ Register Src="~/Controls/EditEmployeeControl.ascx" TagPrefix="ee" TagName="EditEmployee" %>
<%@ Register Src="~/Controls/EditUpdateSaveCancelControl.ascx" TagPrefix="eu" TagName="EditUpdate" %>
<%@ Register TagPrefix="bl" Namespace="BusinessLogic.Components" Assembly="BusinessLogic" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="server">
<table class="centeredTable">
  <tr>
    <td>
      <bl:EmployeeDropDown ID="employeeDropDown" runat="server" 
          DefaultOption="Select Employee" OnSelectedIndexChanged="EmployeeIndexChanged_Handler"
           AutoPostBack="true"></bl:EmployeeDropDown>
    </td>
   
  </tr>
</table>
<ee:EditEmployee ID="editEmployeeControl" runat="server"  />
    <table class="centeredTable">
        <tr>
            <td>
                <eu:EditUpdate ID="editUpdateCancelControl" runat="server"/>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContentHolder" runat="server">
</asp:Content>
