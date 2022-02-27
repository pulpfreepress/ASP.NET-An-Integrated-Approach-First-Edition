<%@ Page Title="List Employees Page" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="ListEmployees.aspx.cs" Inherits="Web.Pages.Employee.ListEmployees"
    Theme="DefaultTheme" %>

<%@ Register Src="~/controls/EmployeesGridViewControl.ascx" TagName="EmployeesGridView" TagPrefix="egv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="server">
    <egv:EmployeesGridView ID="employeesGridViewControl" runat="server" Visible="true"  />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContentHolder" runat="server">
</asp:Content>
