<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmartCalendar.ascx.cs" 
            Inherits="Web.Controls.SmartCalendar" %>

<table cellspacing="0" cellpadding="0" width="20%" border="0">
    <tr>
        <td align="left" class="calTitle">
            <asp:DropDownList ID="monthList" runat="server" AutoPostBack="true" 
                              OnSelectedIndexChanged="SetCalendar" CssClass="calTitle">
            </asp:DropDownList>
        </td>
        <td align="right" class="calTitle">
            <asp:DropDownList ID="yearList" runat="server" AutoPostBack="true" 
                              OnSelectedIndexChanged="SetCalendar" CssClass="calTitle">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Calendar OtherMonthDayStyle-BackColor="White" DayStyle-BackColor="LightYellow"
                ID="smartCalendar" runat="server" CssClass="calBody" DayHeaderStyle-BackColor="#eeeeee"
                 OnSelectionChanged="SetDropDowns" OnDayRender="DayRender" Width="100%"></asp:Calendar>
        </td>
    </tr>
</table>