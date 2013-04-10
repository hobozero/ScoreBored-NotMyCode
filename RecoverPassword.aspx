<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" Inherits="ScoreBoard.RecoverPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div runat="server" id="loginBox" class="center " style="width:300px;">
             
           <%--using Encrypted passwords so can't send--%>
          <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" MailDefinition-BodyFileName="~/PsswordRecovery.htm"
                MailDefinition-From="admin@loogla.com" MailDefinition-Subject="Loogla Password Recovery"
                 OnVerifyingUser="PasswordRecovery1_VerifyingUser" OnVerifyingAnswer="PasswordRecovery1_VerifyingAnswer"
                OnSendingMail="PasswordRecovery1_SendingMail">
            </asp:PasswordRecovery>
            <br /><br /><br />
            <asp:Panel ID="pnlLockedOut" runat="server" Visible="false" >
                <table cellpadding="3" cellspacing="10">
                    <tr><td colspan="2">Your account has been locked because of to many failed attempts.  <br />To reset your password, enter your username below:</td></tr>
                    <tr><td>UserName</td><td><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td></tr>
                    <tr><td colspan="2">Answer the Security Question:</td></tr>
                    <tr runat="server" id="trSecQues"><td><asp:Label ID="lblSecQues" runat="server"></asp:Label></td><td><asp:TextBox ID="txtSecQuesAns" runat="server"></asp:TextBox></td></tr>
                    <tr><td colspan="2" align="center"><asp:Button ID="btnUnlock" runat="server" Text="Unlock" onclick="btnUnlock_Click" /></td></tr>
                    <tr><td colspan="2"><asp:Label ID="lblPWError" runat="server" Width="200" />
                        <asp:Label ID="lblPass" runat="server" Width="300" /></td></tr>
                </table>
                
            </asp:Panel>
        </div>
    </form>
</body>
</html>
