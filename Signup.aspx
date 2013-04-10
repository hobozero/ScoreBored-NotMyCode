<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="ScoreBoard.Signup" MasterPageFile="~/Centered.Master" Title="Signup to weigh the options" %>

<asp:Content ID="content1" ContentPlaceHolderID="cphTitle" runat="server">
    Decisions are hard.  Stop making them.
</asp:Content> 
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div style="text-align: center;">
    
        <div style="width:600px; margin: 12px auto auto auto; ">
        
        <br />
        <asp:LoginView ID="LoginView1" runat="server">
            <LoggedInTemplate>
                <asp:LoginName FormatString="<p>Welcome {0}!</p>" ID="LoginName1" runat="server" />
                <asp:LoginStatus ID="LoginStatus1" runat="server" LogoutText="Logout" CssClass="btn"  />
            </LoggedInTemplate>
        </asp:LoginView>

        
        
         <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
        CompleteSuccessText="" OnCreatingUser="CreateUserWizard1_CreatingUser" 
        OnCreatedUser="CreateUserWizard1_CreatedUser" LoginCreatedUser="False"  
        CreateUserButtonStyle-CssClass="btn" CreateUserButtonType="Link" NavigationButtonStyle-CssClass="btn" CancelButtonType="Link"
        MembershipProvider="SqlProvider" DisableCreatedUser="True" >
             <HeaderTemplate>
                 <div class="h2 slate">ScoreBored Enrollment</div>
             </HeaderTemplate>
            <WizardSteps>
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <h2>So close you can almost taste the chalk.  Signup and you're ready to go.</h2>
                    <br />
                    <p>We promise we won't use your email for anything evil or willingly let anyone else do it.  We just need to verify identities to filter out spammers.  You're not an eeeevil spammer, are you?</p>
                    <p>Once you complete the form, you will receive a confirmation email.</p>
                    <table border="0" style="width: 580px">
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">
                                    User Name:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">
                                    Password:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">
                                    Confirm Password:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                    ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">
                                    E-mail:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                    ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">
                                    Security Question:<br /><i>You will be asked this question if you forget your password.</i></asp:Label></td>
                            <td>
                                <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                                    ErrorMessage="Security question is required." ToolTip="Security question is required."
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">
                                    Security Answer:<br /><i>The answer you must give to the question above.</asp:Label></td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                                    ErrorMessage="Security answer is required." ToolTip="Security answer is required."
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                    ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                    ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color: red">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            <ContentTemplate>
                <h2 class="slate" style="padding:10px;">Almost Done</h2>
                <div style="margin-left: auto; margin-right: auto;">
                <p>Your account has been created.</p>
                <p>An email has been sent with your confirmation code.  Once you
                verify the email, you can begin creating boards and commenting on others.</p>

                </div>
                
                </ContentTemplate>
            </asp:CompleteWizardStep>
            </WizardSteps>
              <SideBarStyle BackColor="#5D7B9D" BorderWidth="0px" VerticalAlign="Top" />
              <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="White" />
            
              <FinishNavigationTemplate>
                
             </FinishNavigationTemplate>
              <ContinueButtonStyle  CssClass="btnBold" />
              <StepStyle BorderWidth="0px" />
              <StepNavigationTemplate>
                  <asp:Button ID="StepPreviousButton" runat="server"  CssClass="btnBold" CausesValidation="False" CommandName="MovePrevious" Text="Previous" />
                  <asp:Button ID="StepNextButton" runat="server"  CssClass="btnBold" CommandName="MoveNext" Text="Next" />
             </StepNavigationTemplate>
              <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        </asp:CreateUserWizard>
    
    <asp:Label ID="lblError" runat="server" /> 
    </div>
    
    </div>
    </asp:Content>