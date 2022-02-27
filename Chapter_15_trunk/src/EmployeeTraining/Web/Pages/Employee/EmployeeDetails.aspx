<%@ Page Title="Employee Details" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="Web.Pages.Employee.EmployeeDetails"
    Theme="DefaultTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="server">
    <div class="sectionDivider">
      <span class="sectionHeadingText">Employee Details</span>
    </div>
    <table class="centeredTable">
        <tr>
            <td class="dataEntryLabel">
                <asp:Label  ID="firstNameLabel" runat="server" Text="First Name:"></asp:Label>
            </td>
            <td class="dataEntryControl">
                <asp:TextBox ID="firstNameTextBox" runat="server" Width="125" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td class="dataEntryLabel">
            <asp:Label ID="middleNameLabel" runat="server" Text="Middle Name:"></asp:Label>
          </td>
          <td class="dataEntryControl">
            <asp:TextBox ID="middleNameTextBox" runat="server" Width="125" Enabled="false"></asp:TextBox>
          </td>
        </tr>


        <tr>
          <td class="dataEntryLabel">
            <asp:Label ID="lastNameLabel" runat="server" Text="Last Name:"></asp:Label>
          </td>
             
          <td class="dataEntryControl">
             <asp:TextBox ID="lastNameTextBox" runat="server" Width="125" Enabled="false" ></asp:TextBox>
          </td>
        </tr>

        <tr>
          <td class="dataEntryLabel">
            <asp:Label ID="birthdayLable" runat="server" Text="Date of Birth:"></asp:Label>
          </td>
             
          <td class="dataEntryControl">
             <asp:Calendar ID="birthdayCalendar" runat="server" Enabled="false"  ></asp:Calendar>
          </td>
        </tr>

        <tr>
          <td class="dataEntryLabel">
            <asp:Label ID="hiredateLable" runat="server" Text="Hire Date:"></asp:Label>
          </td>
             
          <td class="dataEntryControl">
             <asp:Calendar ID="hiredateCalendar" runat="server" Enabled="false" ></asp:Calendar>
          </td>
        </tr>

        <tr>
          <td class="dataEntryLabel">
            <asp:Label ID="isActiveLabel" runat="server" Text="Active:"></asp:Label>
          </td>
             
          <td class="dataEntryControl">
             <asp:CheckBox ID="isActiveCheckbox" runat="server" Enabled="false" />
          </td>
        </tr>



    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContentHolder" runat="server">
</asp:Content>
