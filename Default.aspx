<%@ Page Title="" Language="C#" MasterPageFile="~/Centered.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ScoreBoard.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    Unscientifically answering life's nagging questions.
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    
    <div class="text16" style="padding:0 20px;">
    <h1>Life is full of decisions</h1>

    <p>Who would make a better frisbee salesman?  A kangaroo or a meth head?
      Should I tell my husband the truth about that thing?   Do these jeans make me look fat?   Should I move to Buenos Aires?</p>
    
    <p>Who better to help you solve the conumdrum than total strangers?  Create and publish your own Pros and Cons lists and competitor scoreboards then let the community compete.</p>
    <p>We'll keep score.</p>
    </div>
    
    <h2>Recently created Boards</h2>
    <asp:Repeater ID="rptNewBoards" runat="server" 
        onitemdatabound="rptNewBoards_ItemDataBound">
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
