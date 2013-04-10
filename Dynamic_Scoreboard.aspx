<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/LeftCol.Master" CodeBehind="Dynamic_Scoreboard.aspx.cs" Inherits="ScoreBoard.DynamicScoreboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" type="text/css" href="/CSS/board.css" media="screen" />
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHLeft" runat="server">
    <p>
        <a href="http://loogla.com">
		    Not an Ad
        </a><br />
        Advertising corrupts.  Stop reading this.  It is not sponsored by anyone.
    </p>
    <p>
        <a href="http://google.com">
		    This either
        </a><br />
        Does anyone really ever read the advertising they find on this site?
    </p>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHRight" runat="server">
    <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server" />
    <table width="812px">
        <tr><td class="h1" colspan="2"><asp:Label ID="lblBoardName" runat="server" /><asp:Label ID="lCreated" runat="server" CssClass="note" /></td></tr>
        <tr><td class="h2">
            <asp:Label ID="lblDesc" runat="server" /></td>
        <td align="right" valign="bottom" style=" white-space: nowrap;"><a ID="lnkEdit" runat="server" class="btn" href="" visible="false"></a><a runat="server" href="javascript:void(0);" ID="btnVote" class="btnBold voteBt">add line</a></td>
        </tr>
    </table>
    
    <asp:UpdatePanel runat="server" ID="upBoards" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnCon" />
    <asp:AsyncPostBackTrigger ControlID="btnPro" />
    </Triggers>
        <ContentTemplate>
    <table id="board" >
    <tr>
        <td colspan="2">
           <div class="status"><asp:Label ID="lblStatus" runat="server" /></div>    
        </td>
    </tr>
    <tr>
    <td align="right" class="col">
        <asp:GridView ID="grdPro" CssClass="comTbl" AutoGenerateColumns="False" runat="server" Width="400" onrowdatabound="grdPro_RowDataBound">
            <EmptyDataTemplate><div class="score"><%# GetColumnHeader(true) %></div></EmptyDataTemplate>
            <Columns>
                <asp:BoundField Visible="false" DataField="ID" />
                <asp:TemplateField>
                    <HeaderTemplate><div class="score"><%# GetScore(true)%></div></HeaderTemplate>
                    <ItemTemplate>
                        <a name='s<%# Eval("ID") %>'></a>
                        <div class="score"><%# Eval("RatingAdjusted")%></div>
                        <a  class="comCt" href='#<%# Eval("ID") %>'><%# Eval("Count") %></a>
                        <asp:LinkButton ID="btnDel" runat="server" EnableViewState="true" Visible="false" OnClick="deleteScore" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate><div class="score"><%# GetColumnHeader(true) %></div></HeaderTemplate>
                    <ItemTemplate>
                        <div class="le"><div class="entry"><div class="ieShell"><div class="ieCenter"><%# Eval("Text") %></div></div></div></div>
                        <div class="upDown">
                        <div id="btnUp" runat="server" class="up" title="agree" />
                        <div id="btnCom" runat="server" class="com" title="comment" />
                        <div id="btnDown" runat="server" class="down" title="disagree" />
                        </div></ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </td>
    <td class="col">
         <asp:GridView ID="grdCon" CssClass="comTbl" AutoGenerateColumns="False" runat="server" Width="400" onrowdatabound="grdPro_RowDataBound">
            <EmptyDataTemplate><table class="empty"><tr><td><div class="score"><%# GetColumnHeader(false) %></div></td>
                <td class="sc"><div class="score"><%# GetScore(false)%></div></td></tr></table></EmptyDataTemplate>
            <Columns>
                <asp:BoundField Visible="false" DataField="ID" />
                <asp:TemplateField>
                    <HeaderTemplate><div class="score"><%# GetColumnHeader(false) %></div></HeaderTemplate>
                    <ItemTemplate>
                        <div class="le"><div class="entry"><div class="ieShell"><div class="ieCenter"><%# Eval("Text") %></div></div></div></div>
                        <div class="upDown">
                        <div id="btnUp" runat="server" class="up" title="agree" />
                        <div id="btnCom" runat="server" class="com" title="comment" />
                        <div id="btnDown" runat="server" class="down" title="disagree" />
                        </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate><div class="score"><%# GetScore(false) %></div></HeaderTemplate>
                    <ItemTemplate>
                        <a name='s<%# Eval("ID") %>'></a>
                        <div class="score"><%# Eval("RatingAdjusted")%></div>
                        <a class="comCt" href='#<%# Eval("ID") %>'><%# Eval("Count") %></a>
                        <asp:LinkButton ID="btnDel" runat="server" EnableViewState="true" Visible="false" OnClick="deleteScore" /> 
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </td>
    </tr>
</TABLE>
</ContentTemplate></asp:UpdatePanel>

<asp:Panel ID="popScore" runat="server" CssClass="popoutBox" style="width: 400px;" Visible="false">
    <div class="x-close" onclick="$find('mpeScore').hide();">&nbsp;</div> 
     <h2>Add a Score </h2>
        <div id="vtVote">
            <p>Change the score by adding points</p>
            <asp:UpdatePanel ID="upScore" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"><ContentTemplate>
             <asp:TextBox id="txtEntry" runat="server" Width="340px" MaxLength="50" /><br />(50 characters max)   
            </ContentTemplate></asp:UpdatePanel>
            <asp:RequiredFieldValidator ID="val1" runat="server" CssClass="req" ErrorMessage="*" ControlToValidate="txtEntry" ValidationGroup="vote" />
            <table width="100%"><tr><td>
                <p>How important is this vote to you from <strong>1-5</strong>?<br /><i>(1=noteworthy, 5=critical)</i></p>
                </td><td style="width:70px;">
                <cc1:rating  
                            id="Rating1"  CurrentRating="1"
                            StarCssClass="ratingStar"
                            WaitingStarCssClass="savedRatingStar"
                            FilledStarCssClass="filledRatingStar"
                            EmptyStarCssClass="emptyRatingStar"
                            runat="server" CssClass="stars" />
            </td></tr></table>
        <p>We're on the honor system here.  <strong>Don't be a tool</strong> and always vote 5.</p>
        <table width="100%"><tr>
        <td align="left"><asp:LinkButton ID="btnPro" runat="server" onclick="btnVote_Click" CssClass="btnBold" ValidationGroup="vote" OnClientClick="score();" /></td>
        <td align="center"><asp:LinkButton ID="cancelVote" runat="server" CssClass="btnBold btnBoldDark" CausesValidation="false" EnableViewState="false">Cancel</asp:LinkButton></td>
        <td align="right"><asp:LinkButton ID="btnCon" runat="server" onclick="btnVote_Click" CssClass="btnBold" ValidationGroup="vote" OnClientClick="score();" /></td>
        </tr></table>
        </div>
        <div id="vtWait" class="text16" style="padding: 14px; display: none;">You can only vote once every 5 seconds</div>
    </asp:Panel>

<table width="800"><tr>
<td>
<a ID="btnEmbed" runat="server" class="btn" href="javascript:void(0);">embed</a> in your page
</td><td>
<a class="DiggThisButton DiggCompact" href='http://digg.com/submit?related=no&title=<%= this.EncodeTitle %>'></a>
</td>
<td>
    <img src="http://static.delicious.com/img/delicious.small.gif" height="10" width="10" alt="Delicious" />
<a href="http://delicious.com/save" onclick="window.open('http://delicious.com/save?v=5&noui&jump=close&url=<%= this.EncodeUrl %>&title=<%= this.EncodeTitle %>, 'delicious','toolbar=no,width=550,height=550'); return false;">Del.icio.us</a>
</td>
<td>
<a target="_blank" href='http://www.stumbleupon.com/submit?url=<%= this.EncodeUrl %>&title=<%= this.EncodeTitle %>'>
<img border=0 src="/images/stUp.png" alt="StumbleUpon" height="16" width="16">Stumbleupon</a></td>
</tr>
</table>
<iframe src="http://www.facebook.com/plugins/like.php?href=<%= this.UrlParam %>&amp;layout=standard&amp;show_faces=false&amp;width=450&amp;action=like&amp;colorscheme=light&amp;height=35" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:450px; height:25px;" allowTransparency="true"></iframe>


<asp:Panel ID="pnEmbed" runat="server" CssClass="popoutBox" Width="420">
<div class="x-close" onclick="$find('bhEmbed').hidePopup();" ></div>
    <h2>Add this scoreboard to your page</h2>
    
    <asp:Literal runat="server" ID="embedSam" EnableViewState="false" />
    <br />
    Copy the code below and paste into your page<br />
    <div class="embed"><pre class="code"><asp:Label ID="lEmbed" runat="server" /></pre></div>
</asp:Panel>
<cc1:PopupControlExtender ID="popEmbed" runat="server" PopupControlID="pnEmbed" TargetControlID="btnEmbed" BehaviorID="bhEmbed" OffsetX="20" OffsetY="-250" />


<asp:UpdatePanel runat="server" ID="upComs" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnCon" />
    <asp:AsyncPostBackTrigger ControlID="btnPro" />
    </Triggers>
        <ContentTemplate>
<div class="comment">
<h1>Score Comments</h1>
<asp:UpdatePanel ID="upOut" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" >
    <ContentTemplate>
    <asp:Repeater ID="rpProCom" runat="server" OnItemDataBound="rpProCom_ItemDataBound">
        <HeaderTemplate><div class="comment"></HeaderTemplate>
        <ItemTemplate>
            <div class="c-score"><a name='<%# Eval("ID") %>' href='#s<%# Eval("ID") %>'><%# Eval("Text") %></a><asp:Label ID="lScore" runat="server" CssClass="score mini" /></div>
            <asp:Repeater ID="rpVotes" runat="server" DataSource='<%# ((ScoreBoard.Score)Container.DataItem).Votes %>' OnItemDataBound="rpVotes_ItemDataBound">
                <HeaderTemplate><ul></HeaderTemplate>
                <ItemTemplate>
                <li>
                <div class="c-ctrl"><asp:Linkbutton ID="btnD" runat="server" EnableViewState="true" OnClick="DeleteComment" Text="delete" Visible="false" />
                    <a runat="server" id="reply" href="javascript:void(0);" enableviewstate="false">reply</a></div>
                <div class="c-box"></div>
                <div class="c-i"><div runat="server" ID="imgCom" class='<%# Eval("ImgClass") %>' /></div>
                <div class="c-head">
                    <strong><a href='../user.aspx?u=<%# Eval("UserName") %>'><%# Eval("UserName") %></a></strong>
                    <br /><%# Eval("Dt")%>
                </div>
                <div class="c-body"><%# Eval("Comment") %></div>
                </li></ItemTemplate><FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>
        </ItemTemplate> 
        <FooterTemplate></div></FooterTemplate>
    </asp:Repeater>
    </ContentTemplate></asp:UpdatePanel>
</div>
    
<asp:Panel runat="server" ID="pnlVote" Width="250" Height="226" CssClass="popoutBox" style="padding-top: 0;" Visible="false" >
    <div class="x-close" onclick="$find('comPopBhr').hidePopup();">&nbsp;</div>
    <asp:UpdatePanel ID="upComBox" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"><ContentTemplate>
        <h2 id="comHdr"></h2><span id="comDtl"></span>
        <asp:TextBox ID="txtComment" runat="server"  CssClass="comBox" TextMode="MultiLine" MaxLength="512" />
        <asp:HiddenField ID="comCd" runat="server" />
        <asp:HiddenField ID="comWt" runat="server" />
        </ContentTemplate></asp:UpdatePanel>
        <asp:LinkButton ID="btnAddComment" runat="server" CssClass="btn" OnClientClick="vote_click();" EnableViewState="true"
            ValidationGroup="com" onclick="comment_Click" >Go</asp:LinkButton>
        <asp:RequiredFieldValidator ID="valCom" runat="server" CssClass="req" ErrorMessage="Comment Required" ControlToValidate="txtComment" ValidationGroup="com" />

    </asp:Panel>
<asp:LinkButton ID="btnComDummy" runat="server" EnableViewState="false"/> 

</ContentTemplate>
</asp:UpdatePanel>
    
    <asp:UpdatePanel runat="server" ID="upSign">
     <ContentTemplate>
        <asp:Panel runat="server" ID="pnlSignup" Width="300" Height="280" CssClass="popoutBox" >
        <div class="x-close" onclick="$find('mpbSignup').hide();">&nbsp;</div>
            You can help settle this debate after you
            <a href="/signup.aspx" class="btn">join</a>
            <br /><br />
            It's quick and easy and we won't use your email for anything.  We just need to make sure you're not an evil spammer.
            <br /><br />
            Or you can log in below:
            <br />
            <asp:Login ID="Login1" runat="server"  OnLoginError="Login1_LoginError"
            PasswordRecoveryUrl="RecoverPassword.aspx" PasswordRecoveryText="forgot your password?" />
        </asp:Panel>
        <asp:LinkButton ID="btnDummy" runat="server"  EnableViewState="false" /> 
        <cc1:ModalPopupExtender ID="MPESignup" BehaviorID="mpbSignup" runat="server" PopupControlID="pnlSignup" TargetControlID="btnDummy" BackgroundCssClass="modalBackground" />
     </ContentTemplate>
    </asp:UpdatePanel>
   <!-- spacer for popups -->
   <div style="height:200px"></div> 
</asp:Content>

<asp:Content ContentPlaceHolderID="CPEend" ID="CPEend" runat="server" >
<script type="text/javascript">
    <!--
        
       var lastClick;

       function showCom(e, id, wt, sender) {
           //debugger;
           var bhr = $find('comPopBhr');
           if (bhr) {
               //Kludge:  set hidden fields
               $get('<%= comCd.ClientID %>').value = id;
               $get('<%= comWt.ClientID %>').value = wt;
               var str1 = "Your vote will ", str2 = " the weight of this score by half a point";
               var dtl, hd = "Why do you ";
               if (wt==1) {
                   hd += "agree?";
                   dtl = str1+"raise "+str2;
               }
               else if (wt==-1) {
                   hd += "disagree?";
                   dtl = str1+"lower "+str2;
               }
               else {
                   hd="Add Comment"
                   dtl='';
               }

               $get('comHdr').innerHTML = hd;
               $get('comDtl').innerHTML = dtl;

               bhr._popupBehavior.set_parentElement(sender);
               //need to set the dynamic Key.
               //bhr.set_dynamicContextKey(id);

               //highlight parent
               //"highlight"

               //stop event bubbling
               var ev = (e) ? e : window.event;
               //e.cancelBubble is supported by IE - this will kill the bubbling process.
               ev.cancelBubble = true;
               ev.returnValue = false;
               //e.stopPropagation works only in Firefox.
               if (ev.stopPropagation) {
                   ev.stopPropagation();
                   e.preventDefault();
               }

               bhr.showPopup();

               lastClick = sender;

               return false;
               //Need to remove handler and change background to prevent a double post.
           }
       }

       function vote_click() {
           if (lastClick && (lastClick.className != "com")) {
                $("#"+lastClick.id).parent().children("div.up,div.down")
                .each( 
                function() {
                    var $this = $(this);
                    $this.unbind('click');
                    var c = $this.attr('class');
                    $this.removeClass(c).addClass(c + '-ya');
                });
                
                lastClick = null;
            }
            
            $find('comPopBhr').hidePopup();
        }

        function score(s) {
            var vt = $get('vtVote').style;
            var wt = $get('vtWait').style;

            if (s) {
                $get('<%= txtEntry.ClientID %>').value = vt.display = '';
                wt.display = 'none';
                vt.display = '';
            }
            else {
                vt.display = 'none';
                wt.display = '';
                setTimeout("score(true)", 5000);
                $find('mpeScore').hide();
            }

        }

//        //auto-pad
//        ; (function($) {
//            $.fn.textfill = function(options) {
//                var fontSize = options.maxFontPixels;
//                var ourText = $('.ieCenter', this);
//                var maxHeight = $(this).height();
//                var maxWidth = $(this).width();
//                var textHeight;
//                var textWidth;
//                do {
//                    ourText.css('font-size', fontSize);
//                    textHeight = ourText.height();
//                    textWidth = ourText.width();
//                    fontSize = fontSize - 1;
//                } while (textHeight > maxHeight || textWidth > maxWidth && fontSize > 3);
//                return this;
//            }
//        })(jQuery);

//        $(document).ready(function() {
//            $('.entry').textfill({ maxFontPixels: 36 });
//        });

    -->
    </script>
<script type="text/javascript">
    (function() {
        var s = document.createElement('SCRIPT'), s1 = document.getElementsByTagName('SCRIPT')[0];
        s.type = 'text/javascript';
        s.async = true;
        s.src = 'http://widgets.digg.com/buttons.js';
        s1.parentNode.insertBefore(s, s1);
    })();
</script>

</asp:Content>
