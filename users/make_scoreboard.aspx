<%@ Page Language="C#" Title="Make a new Scoreboard or Pros and Cons list" MasterPageFile="~/Centered.Master" AutoEventWireup="True" CodeBehind="make_scoreboard.aspx.cs" Inherits="ScoreBoard.make_scoreboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" type="text/css" href="/CSS/make.css" media="screen" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">

    <div>
    <h1 runat="server" id="h1">Make a new ScoreBored</h1>
    <asp:Label runat="server" ID="editWarn" />
   
   <asp:UpdatePanel ID="upHead" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
   <ContentTemplate>
    <table width="800">
        <tr>
        <td><b>Choose a board type</b></td>
        <td>
            <asp:RadioButtonList ID="rdoType" runat="server" Repeatdirection="Vertical" 
                onselectedindexchanged="rdoType_SelectedIndexChanged" AutoPostBack="true" >
                <asp:ListItem Selected="True" Text="Pros and Cons List about <b>one</b> specific topic" Value="1" />
                <asp:ListItem Text="Match between <b>two</b> competitors." Value="2" />
            </asp:RadioButtonList> </td></tr>
        <tr runat="server" id="rowName">
        <td><b>Board Title</b> (what is your pros and cons list about?)</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" MaxLength="255" Width="399px"  Text="" onkeyup="proConPre();" />
                <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="txtName" Text="*" CssClass="req" />
            </td>
        </tr>
        <tr>
            <td><b>What&#39;s the Score Board about?</b>  (shown as board subtitle)</td>
            <td>
                <asp:TextBox ID="txtTheme" runat="server" MaxLength="500" Width="399px" />
            </td>
        </tr>
        </table>
        </ContentTemplate></asp:UpdatePanel>
        
        <asp:UpdatePanel ID="upForm" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
        <Triggers><asp:AsyncPostBackTrigger ControlID="rdoType" /></Triggers>
        <ContentTemplate>
        
            <!--  ProCon  -->
            <asp:Panel ID="pnlProCon" runat="server" Width="900">
            <h2>Personalize your status text</h2>
            <p>Add some text for the different status levels.  The status text on your board will change depending on how far ahead one side is.</p>
            
            <asp:UpdatePanel ID="upIsPrefix" runat="server" >
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtName" />
            </Triggers>
            <ContentTemplate>
            <asp:CheckBox ID="chkIs" runat="server" onclick="proConPre();" />
            if checked, your status will start out like this:  &nbsp;<span style="font-size: 1em" class="status"><span id="isPreName"></span> is...</span>
            </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <div >
            <div style="float:left;">
            <table class="scale" width="300px">
                <tr>
                    <th colspan="3">How much the leader is ahead (see points on the right)</th><th>Display Text</th>
                </tr>
                <tr>
                    <td align="right"><div class="conLev" style="width:10px">con</div></td>
                    <td><</td>
                    <td><div class="proLev" style="width:90px">pro</div></td>
                    <td>
                        <asp:TextBox ID="txtPro1" runat="server" Width="215px" onfocus="gotFoc(this, 90, 10)" onkeyup="proConPre();" />
                    </td>
                </tr>
                <tr>
                    <td align="right"><div class="conLev" style="width:25px">con</div></td>
                    <td><</td>
                    <td><div class="proLev" style="width:75px">pro</div></td>
                    <td>
                        <asp:TextBox ID="txtPro2" runat="server" Width="215px" onfocus="gotFoc(this, 75, 25)" onkeyup="proConPre();" />
                    </td>
                </tr>
                <tr>
                    <td align="right"><div class="conLev" style="width:40px">con</div></td>
                    <td><</td>
                    <td><div class="proLev" style="width:60px">pro</div></td>
                    <td><asp:TextBox ID="txtPro3" runat="server" Width="215px" onfocus="gotFoc(this, 60, 40)" onkeyup="proConPre();" />
                    </td>
                </tr>
                <tr>
                    <td align="right"><div class="conLev" style="width:50px">con</div></td>
                    <td>=</td>
                    <td><div class="proLev" style="width:50px">pro</div></td>
                    <td><asp:TextBox ID="txtConTie" runat="server" Width="215px" onfocus="gotFoc(this, 0, 0)" onkeyup="proConPre();" Text="Tied" /></td></tr>
                <tr>
                    <td align="right"><div class="conLev" style="width:60px">con</div></td>
                    <td>></td>
                    <td><div class="proLev" style="width:40px">pro</div></td>
                    <td><asp:TextBox ID="txtCon1" runat="server" Width="215px" onfocus="gotFoc(this, 40, 60)" onkeyup="proConPre();" /></td>
                </tr>
                <tr>
                    <td align="right"><div class="conLev" style="width:75px">con</div></td>
                    <td>></td>
                    <td><div class="proLev" style="width:25px">pro</div></td>
                    <td><asp:TextBox ID="txtCon2" runat="server" Width="215px"  onfocus="gotFoc(this, 25, 75)" onkeyup="proConPre();" /></td>
                </tr>
                <tr>
                    <td align="right"><div class="conLev" style="width:90px">con</div></td>
                    <td>></td>
                    <td><div class="proLev" style="width:10px">pro</div></td>
                    <td><asp:TextBox ID="txtCon3" runat="server" Width="215px" onfocus="gotFoc(this, 10, 90)" onkeyup="proConPre();" />
                    </td>
                </tr>
                </table>
             </div>
             
             <div style="float:right">
                <div class="h2" style="display: inline;">Preview</div>(adjust the boxes and view preview here)
                <table class="board">
                <tr>
                    <td colspan="2"><div id="pcStatus" class="status">no scores</div></td>
                </tr>
                <tr><td class="col">
                    <table><tr><th class="num"><div id="score1" class="score">00</div></th><th><div id="cont1" class="score">Pro</div></th></tr></table>
                    </td><td class="col">
                    <table><tr><th><div id="cont2" class="score">Con</div></th><th class="num"><div id="score2" class="score">00</div></th></tr></table>
                </td></tr>
                </table>
             </div>
             </div>
             <div style="clear: both;"></div>
            </asp:Panel>
            
            <!--  Matches  -->
            <asp:Panel ID="pnlMatch" runat="server" Visible="false" Width="900" Height="400">
            <h2>Name your contestants</h2>
            <table>
            <tr><td>
                <asp:TextBox ID="txtMatch1" runat="server" MaxLength="512" Text="Contestant 1" Width="230px" onkeyup="matchPre();" /></td>
                <td>vs. <asp:TextBox ID="txtMatch2" runat="server" width="230px" Text="Contestant 2" MaxLength="512"  onkeyup="matchPre();" /></td>
            </tr>
            </table>
            <h2>Personalize your status text</h2>
            Fill in the boxes below to determine what the status of your board is depending on how close the competition is.
            <div>
            <div style="float: left; margin-top:30px;">
            <table class="scale" width="300px">
                <tr>
                    <th colspan="3">How much the leader is ahead<br />(see points on the right)</th><th>Display Text</th>
                </tr>
                <tr>
                <tr><td align="right"><div class="conLev" style="width:30px">loser</div></td>
                    <td><</td>
                    <td><div class="proLev" style="width:130px">winner</div></td>
                    <td><asp:TextBox ID="txtVSphrase3" runat="server" Width="215px" onfocus="gotFoc(this, 90, 10, true);" onkeyup="matchPre();">obliterating</asp:TextBox></td></tr>
                <tr><td align="right"><div class="conLev" style="width:50px">loser</div></td>
                    <td><</td>
                    <td><div class="proLev" style="width:110px">winner</div></td>
                    <td ><asp:TextBox ID="txtVSphrase2" runat="server" Width="215px" onfocus="gotFoc(this, 75, 25, true);" onkeyup="matchPre();">spanking</asp:TextBox></td></tr>
                <tr><td align="right"><div class="conLev" style="width:70px">loser</div></td>
                    <td><</td>
                    <td><div class="proLev" style="width:90px">winner</div></td>
                    <td><asp:TextBox ID="txtVSphrase1" runat="server" Width="215px" onfocus="gotFoc(this, 60, 40, true);" onkeyup="matchPre();">beating</asp:TextBox></td></tr>
                <tr><td align="right"><div class="conLev" style="width:80px">contestant</div></td>
                    <td>=</td>
                    <td><div class="proLev" style="width:80px">contestant</div></td>
                    <td><asp:TextBox ID="txtVStie" runat="server" Width="215px" onfocus="gotFoc(this, 0, 0, true);" onkeyup="matchPre();">tied with</asp:TextBox></td></tr>
            </table>
            </div>
            
            <div style="float:right">
                <div class="h2">Preview</div>
                <table class="board">
                <tr>
                    <td colspan="2"><div id="mStatus" class="status">Contestant1 is beating Contestant1</div></td>
                </tr>
                <tr><td class="col">
                    <table><tr><th class="num"><div id="mScore1" class="score">00</div></th><th><div class="score"><span id="preCont1">Contestant 1</span></div></th></tr></table>
                    </td><td class="col">
                    <table><tr><th><div class="score"><span id="preCont2">Contestant 2</span></div></th><th class="num"><div id="mScore2" class="score">00</div></th></tr></table>
                </td></tr>
                </table>
             </div>
             </div>
             <div style="clear: both;"></div>
            </asp:Panel>
        </ContentTemplate></asp:UpdatePanel>

    <div style="text-align: center;"><asp:LinkButton ID="btnGo" runat="server" onclick="btnGo_Click" CssClass="btn">Go</asp:LinkButton>
    <asp:Label ID="lblFeedback" runat="server" /></div>
    
    <script type="text/javascript">
    <!--
    var focusBox;
    var pcStatus=null, pcName=null, chkIs=null, isPreName=null, mStatus=null, cont1=null, cont2=null, preCont1=null, preCont2=null;
    
    function gotFoc(e, s1, s2, isMatch) {
        focusBox = e;

        if (isMatch) {
            $get('mScore1').innerHTML = s1;
            $get('mScore2').innerHTML = s2;
            matchPre();
        }
        else {
            $get('score1').innerHTML = s1;
            $get('score2').innerHTML = s2;
            proConPre();
        }
    }

    function proConPre() {
        debugger;
        if (pcStatus == null) {
            pcName = $get('<%= txtName.ClientID %>');
            pcStatus = $get('pcStatus');
            focusBox = $get('<%= txtConTie.ClientID %>');
            isPreName = $get('isPreName');
            chkIs = $get('<%= chkIs.ClientID %>');
        }

        isPreName.innerHTML = pcName.value.replace(/[\?!]/g, "");

        if (chkIs.checked)
            pcStatus.innerHTML = pcName.value.replace (/[\?!]/g, "") + " is " + focusBox.value;
        else
            pcStatus.innerHTML = (focusBox.value == '') ? '&nbsp;' : focusBox.value;
    }

    function matchPre() {
        if (mStatus == null) {
            mStatus = $get('mStatus');
            cont1 = $get('<%= txtMatch1.ClientID %>');
            cont2 = $get('<%= txtMatch2.ClientID %>');
            preCont1 = $get('preCont1');
            preCont2 = $get('preCont2');
        }
        if (focusBox == null) focusBox = $get('<%= txtVStie.ClientID %>');

        mStatus.innerHTML = cont1.value.replace(/[\?!]/g, "") + " is " + focusBox.value + "  " + cont2.value;

        preCont1.innerHTML = cont1.value;
        preCont2.innerHTML = cont2.value;
    }
    -->
    </script>
    
    </div>
</asp:Content>

