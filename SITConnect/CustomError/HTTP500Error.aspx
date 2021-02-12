<%@ Page Language="C#" Title="Error" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HTTP500Error.aspx.cs" Inherits="SITConnect.CustomError.HTTP500Error" %>

<asp:Content ID="Content12" ContentPlaceHolderID="MainContent" runat="server">
    
<br />



    <div>
        <h1>Webservice currently unavailable <small>500</small></h1>
        <p class="lead">An unexpected condition was encountered.</p>


        <asp:Button ID="btn_HomePage" runat="server" style="padding:15px;" Text="Return to Home Page"></asp:Button>

    </div>

    <br />

    <footer><p>Technical Contact: <a href="mailto:x@example.com">help@sitconnect.com</a></p></footer>



</asp:Content>

