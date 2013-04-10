<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordManagement.aspx.cs" Inherits="ScoreBoard.users.PasswordManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--using Encrypted passwords so can't send--%>
            <asp:ChangePassword ID="ChangePassword1" runat="server">
            </asp:ChangePassword>
    </div>
    </form>
</body>
</html>
