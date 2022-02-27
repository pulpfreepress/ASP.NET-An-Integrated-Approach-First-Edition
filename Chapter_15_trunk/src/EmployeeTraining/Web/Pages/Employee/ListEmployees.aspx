<%@ Page Title="List Employees Page" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="ListEmployees.aspx.cs" Inherits="Web.Pages.Employee.ListEmployees"
    Theme="DefaultTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="server">
    <table id="table1" class="centeredTable">
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="true"
                              OnSelectedIndexChanging="IndexChanged_Handler" DataKeyNames="EmployeeID"   >

                </asp:GridView>
            </td>
        </tr>
       
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContentHolder" runat="server">
</asp:Content>
