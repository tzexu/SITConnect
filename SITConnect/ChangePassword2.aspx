<%@ Page Language="C#" Title="Change Password" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword2.aspx.cs" Inherits="SITConnect.ChangePassword2" %>

<asp:Content ID="Content10" ContentPlaceHolderID="MainContent" runat="server">
    
<br />


<h3>Change Password</h3>


<asp:Table id="Table1" runat="server"
                CellPadding="10">

   

             <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label11" runat="server" Text="Enter New Password"></asp:Label><span style="color: #FF0000">&nbsp;*</span>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_password" runat="server" Height="36px" Width="280px" keyup="" onkeyup="btn_checkpassword_click"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <img id="hide_show_password" src="Images/hide_password.svg" style="position:relative; left:-55px;" onclick="javascript:hide_or_show_pw()" width="25" height="25" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="lbl_pwdchecker" style="position:relative; left:-55px;" runat="server" Text="pwdchecker"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label12" runat="server" Text="Confirm New Password"></asp:Label><span style="color: #FF0000">&nbsp;*</span>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_confirmPassword" runat="server" Height="36px" Width="280px"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <img id="hide_show_cfm_password" src="Images/hide_password.svg" style="position:relative; left:-55px;" onclick="javascript:hide_or_show_pw()" width="25" height="25" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="lbl_testing" style="position:relative; left:-55px;" runat="server" Text="tester"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>


</asp:Table>

    

<script type="text/javascript">

    function hide_or_show_pw() {

        var img_link = document.getElementById("hide_show_password").src


        if (img_link.slice(img_link.length - 24) == "Images/show_password.svg") {
            document.getElementById("hide_show_password").src = "Images/hide_password.svg";
            document.getElementById('<%=tb_password.ClientID %>').type = 'password';

            document.getElementById("hide_show_cfm_password").src = "Images/hide_password.svg";
            document.getElementById('<%=tb_confirmPassword.ClientID %>').type = 'password';
        } else {
            document.getElementById("hide_show_password").src = "Images/show_password.svg";
            document.getElementById('<%=tb_password.ClientID %>').type = 'text';

            document.getElementById("hide_show_cfm_password").src = "Images/show_password.svg";
            document.getElementById('<%=tb_confirmPassword.ClientID %>').type = 'text';
        }



    }




</script>




<asp:Button ID="Button1" runat="server" OnClick="btn_changePassword_Click" Text="Change Password" Width="437px" />


</asp:Content>

