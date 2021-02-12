<%@ Page Language="C#" Title="SITConnect - Account Recovery" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountRecovery.aspx.cs" Inherits="SITConnect.AccountRecovery" %>

<asp:Content ID="Content9" ContentPlaceHolderID="MainContent" runat="server">
    
<br />


<h3>Account Recovery</h3>


<asp:Table id="Table1" runat="server"
                CellPadding="10">

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label><span style="color: #FF0000">&nbsp;*</span>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_firstName" runat="server" Height="36px" Width="280px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label7" runat="server" Text="Last Name"></asp:Label><span style="color: #FF0000">&nbsp;*</span>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_lastName" runat="server" Height="36px" Width="280px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label10" runat="server" Text="Email Address"></asp:Label><span style="color: #FF0000">&nbsp;*</span>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_email" runat="server" Height="36px" Width="280px" TextMode="Email"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>



</asp:Table>


<asp:Button ID="btn_accountRecovery_Next" runat="server" OnClick="btn_sendAccountRecoveryEmail" Text="Next" />



</asp:Content>
