<%@ Page Title="Create Employee Page" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="CreateEmployee.aspx.cs" Theme="DefaultTheme"
    Inherits="Web.Pages.Employee.CreateEmployee" %>

<%@ Register Src="~/Controls/EditEmployeeControl.ascx" TagPrefix="ee" TagName="EditEmployee" %>
<%@ Register Src="~/Controls/EditUpdateSaveCancelControl.ascx" TagPrefix="eu" TagName="EditUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="server">
    <ee:EditEmployee ID="editEmployeeControl" runat="server" />
    <table class="centeredTable">
        <tr>
            <td>
                <eu:EditUpdate ID="editUpdateCancelControl" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContentHolder" runat="server">
</asp:Content>
