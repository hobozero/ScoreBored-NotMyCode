<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScoreBoard.aspx.cs" Inherits="BsAs.ScoreBoard" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ScoreBoard</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
        <td>
            <asp:GridView ID="GrdScores" runat="server">
            
            </asp:GridView>
        </td>
    </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
