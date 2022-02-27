<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Web.Default" EnableViewState="true"   %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="table1" width="80%">
        <tr>
            <td colspan="3">
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="textbox1" runat="server" TextMode="MultiLine" Width="840" 
                             Height="180" EnableViewState="false" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="button1" runat="server" Text="Click Me!"  OnClick="OnButton1Click"
                            ToolTip="Click the button to display request data in text area above!" />
            </td>
            <td>
            <asp:Button ID="button2" runat="server" Text="Change Label Message"  OnClick="OnButton2Click"
                            ToolTip="Click the button to demonstrate ViewState" />
            </td>
            <td>
             <asp:Label ID="label1" runat="server" Text="Label" ></asp:Label>
            </td>
        </tr>
        <tr>
         <td colspan="3">
           <asp:TextBox ID="textbox2" runat="server" TextMode="MultiLine" Width="840"
                        Height="300" ></asp:TextBox>
         </td>
        </tr>
        <tr>
         <td>
           <asp:Button ID="decodeViewStateBtn" runat="server" Text="Decode View State" OnClick="DecodeViewStateBtnClick"
                       ToolTip="Click the button to decode the viewstate pasted in the textbox above..." />
         </td>
        </tr>
    </table>
</asp:Content>
