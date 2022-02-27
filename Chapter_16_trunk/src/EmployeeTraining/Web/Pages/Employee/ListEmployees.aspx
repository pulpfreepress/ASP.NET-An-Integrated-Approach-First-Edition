<%@ Page Title="List Employees Page" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="ListEmployees.aspx.cs" Inherits="Web.Pages.Employee.ListEmployees"
    Theme="DefaultTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="server">
    <table id="table1" class="centeredTable">
        <tr>
            <td align="center">
                <asp:LinkButton ID="exportLink" runat="server" Text="Export to Excel" OnClick="ExportDataGrid" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="ListEmployeesGridView" SkinID="ListEmployeesGridViewSkin" runat="server"
                    AutoGenerateColumns="false" AutoGenerateSelectButton="false" DataKeyNames="EmployeeID"
                    OnRowEditing="EditingRow_Handler" OnRowUpdating="UpdatingRow_Handler" OnRowCancelingEdit="EditCanceling_Handler"
                    AllowPaging="true" OnPageIndexChanging="PageIndexChanging_Handler" OnRowDataBound="RowDataBound_Handler"
                    AllowSorting="true" OnSorting="OnSorting_Handler">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="EmployeeID" SortExpression="EmployeeID"
                            Visible="false" />
                        <asp:BoundField HeaderText="First Name" DataField="FirstName" SortExpression="FirstName"
                            ItemStyle-CssClass="col" />
                        <asp:BoundField HeaderText="Middle Name" DataField="MiddleName" SortExpression="MiddleName"
                            ItemStyle-CssClass="col" />
                        <asp:BoundField HeaderText="LastName" DataField="LastName" SortExpression="LastName"
                            ItemStyle-CssClass="col" />
                        <asp:TemplateField HeaderText="Birthday" SortExpression="Birthday" ItemStyle-CssClass="col">
                            <ItemTemplate>
                                <%# GetBirthday(Container.DataItem)%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Calendar ID="birthdayCalendar" runat="server"></asp:Calendar>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hire Date" SortExpression="HireDate" ItemStyle-CssClass="col">
                            <ItemTemplate>
                                <%# GetHiredate(Container.DataItem) %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Calendar ID="hiredateCalendar" runat="server"></asp:Calendar>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:CheckBox ID="isActiveCheckBox" runat="server" Checked='<%#GetIsActive(Container.DataItem) %>'
                                    Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="editIsActiveCheckBox" runat="server" Checked='<%#GetIsActive(Container.DataItem) %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" EditText="Edit" ShowEditButton="true" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
               
                <asp:GridView ID="ExportOnlyGridView" SkinID="ListEmployeesGridViewSkin" runat="server"
                    AutoGenerateColumns="false" Visible="false">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="EmployeeID" SortExpression="EmployeeID" />
                        <asp:BoundField HeaderText="First Name" DataField="FirstName" SortExpression="FirstName"
                            ItemStyle-CssClass="col" />
                        <asp:BoundField HeaderText="Middle Name" DataField="MiddleName" SortExpression="MiddleName"
                            ItemStyle-CssClass="col" />
                        <asp:BoundField HeaderText="LastName" DataField="LastName" SortExpression="LastName"
                            ItemStyle-CssClass="col" />
                        <asp:TemplateField HeaderText="Birthday" SortExpression="Birthday" ItemStyle-CssClass="col">
                            <ItemTemplate>
                                <%# GetBirthday(Container.DataItem)%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hire Date" SortExpression="HireDate" ItemStyle-CssClass="col">
                            <ItemTemplate>
                                <%# GetHiredate(Container.DataItem) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <%#GetIsActive(Container.DataItem) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContentHolder" runat="server">
</asp:Content>
