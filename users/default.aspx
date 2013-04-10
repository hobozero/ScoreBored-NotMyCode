<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ScoreBoard.users._default"  MasterPageFile="~/Centered.Master" Title="ScoreBored Personal Page" %>


<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
        <asp:Panel runat="server" ID="pnlHowTo" Visible="false" EnableViewState="false">
        <h1>Getting Started</h1>
        Since you haven't created any boards yet, here's a rundown of how to get going.
        <ul style="padding: 12px; font-size:120%;">
            <li>Click the <a href="make_scoreboard.aspx" class="btn" style="margin-left:0; left:2px;">New</a>button to make a new board.</li>
            <li>Decide if it should be a <span class="iBold">One on One Competition</span> or a <span class="iBold">Pros and Cons List</span>.</li>
            <li>Decide what the board will say depending on who's winning.</li>
            <li>Scores are created by other users who put the reason for the score and weight the value from 1-5.</li>
            <li>Other users can vote scores up or down, which affects the overall impact of the score.</li>
            <li>All votes should include a comment to justify your reasoning.</li>
            <li>Discover the truths to life's mysteries and make life altering decisions based on the capricious whims of other internet users!  What could go wrong?</li>
        </ul>
            <p>You can always click on your user name at the top of the page to take you back here to your home page.</p>
        </asp:Panel>
        
        <div style="float:right;"><a href="PasswordManagement.aspx" class="btn">Change Password</a></div>
        <h2>My Scoreboards</h2>
        
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