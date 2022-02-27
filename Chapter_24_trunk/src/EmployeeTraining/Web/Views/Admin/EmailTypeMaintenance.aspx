<%@ Page Title="Email Type Maintenance Page" Language="C#" MasterPageFile="~/Views/Shared/MasterViewPage.Master" AutoEventWireup="true"
    CodeBehind="~/Views/Admin/EmailTypeMaintenance.aspx.cs" Inherits="Web.Views.Admin.EmailTypeMaintenance"
    Theme="DefaultTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="server">
    <div class="formTitle">
        Email Type Maintenance</div>
        
    <table width="60%" rules="all" class="griddedTable">
        <tr>
            <th class="griddedTableHeader">
                Email Type ID
            </th>
            <th class="griddedTableHeader">
                Description
            </th>
        </tr>
        <% foreach (EmailTypeVO vo in (List<EmailTypeVO>)ViewData["EmailTypes"]) { %>
        <tr>
            <td class="griddedTable">
                <%: vo.EmailTypeID %>
            </td>
            <td class="griddedTable">
                <%: vo.Description %>
            </td>
            <td>
             <%: Html.ActionLink("Edit", "Edit", new { id=vo.EmailTypeID  }) %>
            </td>
        </tr>
       
        <% } %>
         <tr>
          <td class="griddedTable" align="center" colspan="2">
            <%: Html.ActionLink("New", "New") %>
          </td> 
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContentHolder" runat="server">
</asp:Content>
