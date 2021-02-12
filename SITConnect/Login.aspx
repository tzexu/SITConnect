<%@ Page Language="C#" Title="SITConnect - Login" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SITConnect.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<br />


<h3>Login</h3>


<script src="https://www.google.com/recaptcha/api.js?render=6LdSPlAaAAAAAOPX4PnJOC1tcvuBtr9-PivgpheA"></script>

<br />
<br />
    
<div class="container">
    <div class="row">
        <div class="col-6">

            <asp:Table id="Table1" runat="server"
                CellPadding="10">

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_email_Login" runat="server" Height="36px" Width="280px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label7" runat="server" Text="Password"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_password_Login" runat="server" Height="36px" Width="280px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>


            </asp:Table>

            <asp:Label ID="lbl_errorMsg" runat="server" ></asp:Label>

            <asp:Button ID="btn_recoverAccount" runat="server" OnClick="btn_recoverAccount_Click" Text="Recover Account" Width="437px" />
            
        </div>
    </div>
</div>






            









<br />
<br />

    <script type="text/javascript">

        function thefun() {
            tb_lastName.Text = "yes"
        }

    </script>
        
<p>
    <asp:Button ID="Button1" runat="server" OnClick="btn_Login_Click" Text="Login" Width="437px" />
    <%--<asp:Button ID="Button2" runat="server"  Text="Register" OnClick="thefun()" Width="430px" />--%>

    
    <asp:Label ID="lbl_test" runat="server" Text="thelabel"></asp:Label>
</p>

    <p>Don't have an account?</p>
    <p style="text-decoration:underline;">Sign Up here</p>

    <%--<asp:Button ID="Button2" runat="server" OnClick="btn_Register_Click" Text="Register" Width="437px" />--%>


    <%--<div class="g-recaptcha" data-sitekey="6LdSPlAaAAAAAOPX4PnJOC1tcvuBtr9-PivgpheA"></div>--%>

    <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>

    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('', { action: 'Login' }).then(function (token)) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>

</asp:Content>
