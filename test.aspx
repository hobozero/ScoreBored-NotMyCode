<%@ Page Title="" Language="C#" MasterPageFile="~/Centered.Master" AutoEventWireup="True" CodeBehind="test.aspx.cs" Inherits="ScoreBoard.test1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Panel runat="server" ID="parent">
    <a runat="server" href="javascript:void(0);" ID="btnVote">vote</a>
    <asp:Panel ID="popScore" runat="server">Test Text</asp:Panel>
      
    </asp:Panel>
    
</asp:Content>
