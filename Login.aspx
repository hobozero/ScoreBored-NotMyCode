<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ScoreBoard.Login"  MasterPageFile="~/Centered.Master" Title="Login to Create Scoreboards and Pros and Cons agendas" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div runat="server" id="loginBox" style="width:300px;margin-left: auto; margin-right:auto; border: solid 1px #111; padding:12px ">
             <h2>Log in</h2>
            <asp:Login ID="Login1" runat="server" 
                TitleText=""
                OnLoggedIn="Login1_LoggedIn"
                OnLoggingIn="Login1_LoggingIn1"
                MembershipProvider="SqlProvider" 
                RememberMeSet="True" 
                VisibleWhenLoggedIn="False" 
                TextBoxStyle-Width="80"
                Width="266px">
            </asp:Login>
            <p><a href="RecoverPassword.aspx"">Forgot your password?</a></p>
        </div>
        
        <div runat="server" id="activateBox" visible="false" style="margin-left: auto; margin-right:auto; :300px;">
            <h2>Activate your ScoreBored Account</h2>
            <div>Hi 
                <asp:Label ID="lblFName" runat="server"  />.  
                <p>You're almost there.  You've created your account, but you still need to activate it.  An email has been sent to 
                <asp:Label ID="lblEmail" runat="server"  />
             that contains your confirmation link.  
             </p>
             <p>Check you email and use the link then let the learning begin!</p>
             <p></p>Please <a href="mailto:support@scorebored.net">mail</a> support if any problems arise.</p>
            </div>
        </div>
        
        <div style="margin: 50px"></div>
        <div class="outline centerText" style="width:300px;margin-left: auto; margin-right:auto; ">
            <div>Don't have an account yet?  Click <a class="button" href="/Signup.aspx">here</a> to get started creating your own 
            <b>pros and cons lists</b> or set <b>one competitor versus another</b>.</div>

            
        </div>
    </asp:Content>