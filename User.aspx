<%@ Page Title="" Language="C#" MasterPageFile="~/Centered.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="ScoreBoard.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
<h2>Scoreboards by <asp:Label runat="server" ID="lblUser" Text="Nobody" /></h2>
        
        <hr />
        <p>
        <asp:Repeater ID="rptNewBoards" runat="server" >
        <HeaderTemplate><table id="tblList"></HeaderTemplate>
        <ItemTemplate>
            <tr>
            <td class="txt"><div class="title"><a href='/bored/<%# Eval("Name").ToString().URLEncodeName() %>'><%# Eval("Name") %></a></div>
                <div><%# Eval("DESCRIPTION") %></div>
            </td>
            <td><div class="score"><%# Eval("PRO") %></div></td>
            <td class="to">to</td>
            <td><div class="score"><%# Eval("CON") %></div></td>
            </tr>
            
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
        </asp:Repeater>
        </p>
</asp:Content>
