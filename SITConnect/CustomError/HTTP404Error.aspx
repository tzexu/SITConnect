<%@ Page Language="C#" Title="Error" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HTTP404Error.aspx.cs" Inherits="SITConnect.CustomError.HTTP404Error" %>

<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    
<br />



    <div>
        <h1>Resource not found <small>404</small></h1>
        <p class="lead">The requested resource could not be found but may be available again in the future.</p>


        <asp:Button ID="btn_HomePage" runat="server" style="padding:15px;" >Return to Home Page</asp:Button>

    </div>

    

    <footer><p>Technical Contact: <a href="mailto:x@example.com">help@sitconnect.com</a></p></footer>



</asp:Content>
