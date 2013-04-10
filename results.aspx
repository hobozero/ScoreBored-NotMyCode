<%@ Page Title="" Language="C#" MasterPageFile="~/Centered.Master" AutoEventWireup="true" CodeBehind="results.aspx.cs" Inherits="ScoreBoard.results" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    You can't find unless you look
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">

    <h1>Search Results</h1>
    <asp:Label runat="server" ID="lblNoResults" />
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
</asp:Content>
