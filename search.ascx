<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="search.ascx.cs" Inherits="ScoreBoard.search" %>
<asp:TextBox ID="txtSearch" runat="server" />
<asp:LinkButton ID="btnSearch" runat="server" onclick="btnSearch_Click" CssClass="search" CausesValidation="false">Search</asp:LinkButton>

<cc1:AutoCompleteExtender
    runat="server" 
    ID="autSearch" 
    TargetControlID="txtSearch"
    ServiceMethod="SearchBored"
    ServicePath="~/services/searchService.asmx"
    MinimumPrefixLength="4" 
    EnableCaching="true"
    CompletionSetCount="20" 
    CompletionListCssClass="sug" 
    CompletionListItemCssClass="itm" 
    CompletionListHighlightedItemCssClass="sel" />
