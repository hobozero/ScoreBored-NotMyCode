<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="embedded.aspx.cs" Inherits="ScoreBoard.embedded" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ScoreBored.net</title>
    <style type="text/css">
    <!--
body {
color:#666;
font:0.75em/1 "Trebuchet MS",Verdana,Arial,Helvetica,sans-serif;
}
    
#tblList
{
    padding: 2px 2px 2px 6px;
	color:#FFF;
    float: left;
    background: #466E4A;
    width:100%
}
#tblList td
{
    vertical-align: top;
}
#tblList td.lnk
{
    color:#FFA21E;
    text-align:center;
}
#tblList .txt
{
    padding:2px 6px;
    border:4px solid #FFF;
    border-collapse:collapse;
    width: 100%;
}
#tblList .to
{
    padding:10px 0;
}
#tblList .title{
    text-align: center;
    font: bold 100% Lucida Console, Monaco;
}
#tblList a:link { color:#FFF; text-decoration:none; }
#tblList a:visited { color:#FFF; text-decoration:none; }
#tblList a:hover { color:#FFF; text-decoration:underline; }
#tblList a:active { color:#FFF; text-decoration:none; }

#tblList .score
{
    font: 140%/1 Impact;
    margin:1px;
    padding: 2px 4px 2px 0;
    text-align: right;
    color: #FFA21E;
    background: #232623;
    width: 40px;
    border: solid 1px #ddd;
}
    -->
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tblList">
            <tr>
            <td class="txt" rowspan="2"><div class="title"><a href='<%= GetUrl() %>' target="_blank"><asp:Label ID="ttl" runat="server" /></a></div>
                <div><asp:Label ID="desc" runat="server" /></div>
            </td>
            <td><div class="score"><asp:Label ID="pro" runat="server" /></div></td>
            <td class="to">to</td>
            <td><div class="score"><asp:Label ID="con" runat="server" /></div></td>
            </tr>
            <tr><td colspan="3" class="lnk"><a href='<%= GetUrl() %>' target="_blank">add your vote</a></td></tr>
        </table>
    </div>
    </form>
</body>
</html>
