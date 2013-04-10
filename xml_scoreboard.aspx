<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xml_scoreboard.aspx.cs" Inherits="BsAs._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>How's BA for you today?</title>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <div style="width:100%; text-align: center;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <contenttemplate>
<TABLE style="WIDTH: 100%; HEIGHT: 100%" cellSpacing=0 cellPadding=0 border=0><TR><td colSpan=2 style="height: 18px; background-color: paleturquoise;">
    </td></TR>
    <tr>
        <td colspan="2" style="height: 18px">
    <asp:Label ID="lblWinner" runat="server" Text="Label" Font-Bold="True" Font-Size="Large"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 18px; background-color: paleturquoise">
        </td>
    </tr>
    <tr>
        <td>
            </td>
        <td>
            </td>
    </tr>
    <TR><td align="right">
    <asp:GridView ID="grdPro" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSourcePro" Width="389px">
        <Columns>
            <asp:BoundField DataField="txt" HeaderText="Prose" SortExpression="txt" />
            <asp:BoundField DataField="rate" HeaderText="rate" SortExpression="rate" />
        </Columns>
        <HeaderStyle BackColor="#FFFFC0" />
    </asp:GridView>
        <asp:XmlDataSource ID="XmlDataSourcePro" runat="server" DataFile="~/Scores.xml" XPath='/ScoreBoard/Point[@type="pro"]'>
        </asp:XmlDataSource>
            <asp:Label ID="lblProPts" runat="server" Text="0"></asp:Label>
    </td><td>
        <asp:GridView ID="grdCon" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSourceCon"
            Width="390px">
            <Columns>
                <asp:BoundField DataField="txt" HeaderText="KAAAAAHNs!" SortExpression="txt" />
                <asp:BoundField DataField="rate" HeaderText="rate" SortExpression="rate" />
            </Columns>
            <HeaderStyle BackColor="#FFFFC0" />
        </asp:GridView>
        <asp:XmlDataSource ID="XmlDataSourceCon" runat="server" DataFile="~/Scores.xml" XPath='/ScoreBoard/Point[@type="con"]'>
        </asp:XmlDataSource>
            <asp:Label ID="lblconPts" runat="server" Text="0"></asp:Label>
    </td></TR>
    <tr>
        <td colspan="2" style="background-color: #ffffc0">
            <table>
                <tr>
                    <td style="width: 100px">
<asp:TextBox id="txtEntry" runat="server" Width="284px"></asp:TextBox></td>
                    <td style="width: 100px">
                        <cc1:rating 
                    id="Rating1" 
                    StarCssClass="ratingStar"
                    WaitingStarCssClass="savedRatingStar"
                    FilledStarCssClass="filledRatingStar"
                    EmptyStarCssClass="emptyRatingStar"
                    runat="server" CssClass="stars"></cc1:rating>
                    </td>
                    <td align="left" style="width: 99px">
                        <asp:RadioButton id="rdoPro" runat="server" AutoPostBack="True" OnCheckedChanged="rdoPro_CheckedChanged" Text="Bonus" Checked="True" Width="68px"></asp:RadioButton><br />
                        <asp:RadioButton id="rdoCon" runat="server" AutoPostBack="True" OnCheckedChanged="rdoCon_CheckedChanged" Text="Bummer"></asp:RadioButton></td>
                    <td style="width: 100px">
    <asp:Button ID="btnSubmit" runat="server" Text="2 Cent Maker" OnClick="btnSubmit_Click" /></td>
                </tr>
            </table>
        </td>
    </tr>
</TABLE>
</contenttemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
