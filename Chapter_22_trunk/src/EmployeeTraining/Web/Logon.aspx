<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="Web.Logon"
   EnableTheming="true" Theme="DefaultTheme" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Training Logon Page</title>
   
</head>
<body>
    <form id="form1" runat="server">
    <div class="logonDiv">
       <table width="100%">
        <tr>
          <td colspan="2" align="center">
                <span class="logonTitleText">Employee Training Application Login</span></td>
        </tr>

        <tr>
          <td align="center">
           <asp:Login ID="passwordLogin" CssClass="logonControl"  runat="server" 
           ToolTip="Enter Username and Password" 
           DestinationPageUrl="~/default.aspx" OnAuthenticate="Authenticate"
           FailureText="Invalid Username and Password Combination!"
           InstructionText="Enter Username and Password"
           PasswordRequiredErrorMessage="Password Required!"  
           DisplayRememberMe="false" BorderPadding="30" TextBoxStyle-CssClass="logonTextBox"  >
            </asp:Login>
        
           </td>
         </tr>
       </table>
    </div>
    </form>
</body>
</html>
