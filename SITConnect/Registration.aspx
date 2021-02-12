<%@ Page  Language="C#" Title="SITConnect - Registration Form" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="SITConnect.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<br />


<h3>Registration</h3>




<br />
<br />
    
<div class="container">
    <div class="row">
        <div class="col-6">

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
                        <asp:Label ID="Label8" runat="server" Text="Date of Birth"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_DOB" runat="server" Height="36px" Width="280px" TextMode="Date"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label9" runat="server" Text="Credit Card Number"></asp:Label><span style="color: #FF0000">&nbsp;*</span>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_creditCardNum" runat="server" Height="36px" Width="280px" TextMode="Number"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label2" runat="server" Text="Card Expiry Date"></asp:Label><span style="color: #FF0000">&nbsp;*</span>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_cardExpiry" runat="server" Height="36px" Width="80px" MinLength="4" MaxLength="5"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server" Text="CVC/CVV"></asp:Label><span style="color: #FF0000">&nbsp;*</span>&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="tb_CVCorCVV" runat="server" Height="36px" Width="80px" MinLength="3" MaxLength="4" TextMode="Number"></asp:TextBox>
                    </asp:TableCell>

                    <%--<asp:TableCell>
                        
                    </asp:TableCell>
                    <asp:TableCell>
                        
                    </asp:TableCell>--%>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label10" runat="server" Text="Email Address"></asp:Label><span style="color: #FF0000">&nbsp;*</span>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_email" runat="server" Height="36px" Width="280px" TextMode="Email"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label11" runat="server" Text="Password"></asp:Label><span style="color: #FF0000">&nbsp;*</span>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_password" runat="server" Height="36px" Width="280px" keyup="" onkeyup="btn_checkpassword_click"></asp:TextBox>
                    </asp:TableCell> 
                    <%--<asp:TableCell>
                        <input id="tb_password" type="password" size="50" onKeyUp="btn_checkPassword_Click" >
                    </asp:TableCell> --%>
                    <asp:TableCell>
                        <img id="hide_show_password" src="Images/hide_password.svg" style="position:relative; left:-55px;" onclick="javascript:hide_or_show_pw()" width="25" height="25" />
                    </asp:TableCell>
                    <%--<asp:TableCell>
                        <asp:Image ID="hide_show_password" runat="server" ImageUrl="Images/hide_password.svg" style="position:relative; left:-55px;" onclick="javascript:hide_or_show_pw()" width="25" height="25" />
                    </asp:TableCell>--%>
                    <asp:TableCell>
                        <asp:Label ID="lbl_pwdchecker" style="position:relative; left:-55px;" runat="server" Text="pwdchecker"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label12" runat="server" Text="Confirm Password"></asp:Label><span style="color: #FF0000">&nbsp;*</span>
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

               <%-- <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label4" runat="server" Text="Verification Code"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Image ID="Image10" runat="server" Height="55px" ImageUrl="~/Captcha.aspx" Width="186px" />
                        <br />
                        <asp:Label runat="server" ID="lbl_CaptchaMessage"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label5" runat="server" Text="Enter Verification Code"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_VerificationCode" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>--%>
                

            </asp:Table>

            <script type="text/javascript">

                function hide_or_show_pw() {

                    //<%--document.getElementById('<%=hide_show_password.ClientID %>').ImageUrl =  -->

                    //alert("hi");

                    <%--alert(document.getElementById('<%=hide_show_password.ClientID %>').src)

                    //alert(document.getElementById('<%=hide_show_password.ClientID %>').src == "Images/show_password.svg");


                    if (document.getElementById('<%=hide_show_password.ClientID %>').src == "Images/show_password.svg") {
                        document.getElementById('<%=hide_show_password.ClientID %>').src = "Images/hide_password.svg";
                        //document.getElementById("tb_password").TextMode = 
                        alert("yo");

                        //document.getElementById("hide_show_cfm_password").src = "Images/hide_password.svg";
                    } else {
                        document.getElementById('<%=hide_show_password.ClientID %>').src = "Images/show_password.svg";
                        //document.getElementById("hide_show_cfm_password").src = "Images/show_password.svg";
                    }--%>

                    

                    //alert(document.getElementById("hide_show_password").src);
                    //alert((document.getElementById("hide_show_password").src).substr(-1));

                    //alert(yz.slice(yz.length - 24));

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

                function validate() {
                    //btn_checkPassword_Click()

                    //FIGURE OUT HOW TO ACCESS tb_password.Text 

                    document.getElementById("min12char_icon").style.display = "inline";
                    document.getElementById("min1lowercase_icon").style.display = "inline";
                    document.getElementById("min1uppercase_icon").style.display = "inline";
                    document.getElementById("min1num_icon").style.display = "inline";
                    document.getElementById("min1symbol_icon").style.display = "inline";

                   // pw = password
                    var pw = document.getElementById('<%=tb_password.ClientID %>').value;

                    var score = 0;

                    //var char = "#";

                    //alert(char.search(/[a-z]/) != -1);

                    for (i in pw) {
                        var char = pw[i];

                        if (!isNaN(char)) {
                            //alert("it's a num");
                            score += 0.15;
                        } else if (char.search(/[A-Z]/) != -1) {
                            //alert("upper");
                            score += 0.15;
                        } else if (char.search(/[a-z]/) != -1) {
                            //alert("lower");
                            score += 0.1;
                        } else {
                            //alert("special");
                            score += 0.4;
                        }

                        //alert('char is: ' + char);
                    }

                    //alert('pw is:' + pw);

                    if (pw.search(/[a-z]/) != -1) {
                        score += 0.6;
                    }
                    if (pw.search(/[A-Z]/) != -1) {
                        score += 0.6;
                    }
                    if (pw.search(/[0-9]/) != -1) {
                        score += 0.6;
                    }
                    if (pw.search(/[^a-zA-Z0-9]/) != -1) {
                        score += 0.6;
                    }


                    if (pw.search(/[a-z]/) == -1) {
                        document.getElementById("min1lowercase_icon").src = "Images/cross.svg";

                        if (score > 3.95) {
                            score = 3.95;
                        }

                    } else {
                        document.getElementById("min1lowercase_icon").src = "Images/check.svg";
                    }


                    if (pw.search(/[A-Z]/) == -1) {
                        document.getElementById("min1uppercase_icon").src = "Images/cross.svg";

                        if (score > 3.95) {
                            score = 3.95;
                        }

                    } else {
                        document.getElementById("min1uppercase_icon").src = "Images/check.svg";
                    }


                    if (pw.search(/[0-9]/) == -1) {
                        document.getElementById("min1num_icon").src = "Images/cross.svg";

                        if (score > 3.95) {
                            score = 3.95;
                        }

                    } else {
                        document.getElementById("min1num_icon").src = "Images/check.svg";
                    }


                    if (pw.search(/[^a-zA-Z0-9]/) == -1) {
                        document.getElementById("min1symbol_icon").src = "Images/cross.svg";

                        if (score > 3.95) {
                            score = 3.95;
                        }

                    } else {
                        document.getElementById("min1symbol_icon").src = "Images/check.svg";
                    }

                    if (pw.length < 12) {
                        document.getElementById("min12char_icon").src = "Images/cross.svg";

                        if (score > 3.95) {
                            score = 3.95;
                        }

                    } else {
                        document.getElementById("min12char_icon").src = "Images/check.svg";
                    }

                    //alert("before score: " + score);

                    score = Math.round(score * 100) / 100;



                    //alert("new score: " + score);
                    //document.getElementById("pwStrength").innerHTML = score.toFixed(2);
                    //document.getElementById("pwStrength").innerHTML = score;


                    if (score < 1) {
                        document.getElementById("pwStatus").innerHTML = score + " - Extremely Weak";
                    } else if (score < 2) {
                        //alert(score);
                        document.getElementById("pwStatus").innerHTML = score + " - Very Weak";
                    } else if (score < 3) {
                        document.getElementById("pwStatus").innerHTML = score + " - Weak";
                    } else if (score < 4) {
                        document.getElementById("pwStatus").innerHTML = score + " - Medium";
                    } else if (score < 5) {
                        document.getElementById("pwStatus").innerHTML = score + " - Strong";
                    } else if (score < 6) {
                        document.getElementById("pwStatus").innerHTML = score + " - Very Strong";
                    } else if (score >= 6) {
                        document.getElementById("pwStatus").innerHTML = score + " - Extremely Strong";
                    }


                    if (score < 4) {
                        document.getElementById("pwStatus").style.color = "red";
                    } else {
                        document.getElementById("pwStatus").style.color = "green";
                    }


                }

            </script>
            


        </div>
        <div class="col-6">

            <div style="border: 1px solid black; border-radius: 10px 10px 10px 10px; padding:12px;">

                <h4>Password Complexity Requirements</h4>

                <style>

                    /*ul {
                        list-style-image: url('Images/check.svg');
                    }*/

                    ul {
                        list-style-type:none;
                    }

                    /*li.complexity_requirements {
                        list-style: none;
                    }
                    li.complexity_requirements::before{
                        content: '';
                        display: inline-block;
                        height: 25px;
                        width: 25px;
                        background-image: url(tickorcross);
                    }*/

                </style>
                <%--<img src="Images/check.svg" width="20" height="20"/>
                <img src="Images/check.svg" width="25" height="25"/>
                <img src="Images/cross.svg" width="20" height="20"/>
                <img src="Images/cross.svg" width="25" height="25"/>--%>

                <%--<img src="Images/hide_password.svg" width="25" height="25" />
                <img src="Images/show_password.svg" width="25" height="25" />--%>

                <ul>
                    <li id="min12char" class="complexity_requirements">
                        <img id="min12char_icon" src="Images/check.svg" width="25" height="25" style="position:relative; top:-2px;display:none;"/>
                        &nbsp;&nbsp;Minimum 12 Characters
                    </li>

                    <li id="min1lowercase" class="complexity_requirements">
                        <img id="min1lowercase_icon" src="Images/check.svg" width="25" height="25" style="position:relative; top:-2px;display:none;"/>
                        &nbsp;&nbsp;Minimum 1 Lowercase letter
                    </li>

                    <li id="min1uppercase" class="complexity_requirements">
                        <img id="min1uppercase_icon" src="Images/check.svg" width="25" height="25" style="position:relative; top:-2px;display:none;"/>
                        &nbsp;&nbsp;Minimum 1 Uppercase letter
                    </li>

                    <li id="min1num" class="complexity_requirements">
                        <img id="min1num_icon" src="Images/check.svg" width="25" height="25" style="position:relative; top:-2px;display:none;"/>
                        &nbsp;&nbsp;Minimum 1 Number
                    </li>

                    <li id="min1symbol" class="complexity_requirements">
                        <img id="min1symbol_icon" src="Images/check.svg" width="25" height="25" style="position:relative; top:-2px;display:none;"/>
                        &nbsp;&nbsp;Minimum 1 Symbol
                    </li>
                </ul>

                <%--<script type="text/javascript">

                    function validate() {
                        //btn_checkPassword_Click()

                        document.getElementById("min12char_icon").style.display = block;
                        alert("yo");

                    }

                </script>--%>

                <br />


                <script type="text/javascript">



                </script>


                <h6>Password Strength: </h6><h5 id="pwStatus" style="display:inline;"></h5>

                <%--<h6>Password Strength: <p id="pwStrength" style="display:inline;" ></p> - <p id="pwStatus" style="display:inline;"></p></h6>--%>


            </div>

            

            <%--<asp:Label ID="Label2" runat="server" Text="Confirm Password"></asp:Label>--%>
            

        </div>
    </div>
</div>






            










<%--<table class="style1">
            <tr>
                <td class="style3">
        <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_userid" runat="server" Height="36px" Width="280px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
        <asp:Label ID="Label3" runat="server" Text="Last Name"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_pwd" runat="server" Height="32px" Width="281px">mypassword</asp:TextBox>
                </td>
            </tr>
                        <tr>
                <td class="style3">
        <asp:Label ID="Label4" runat="server" Text="Date of Birth"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_cfpwd" runat="server" Height="32px" Width="281px"></asp:TextBox>
                </td>
            </tr>
                        <tr>
                <td class="style6">
        <asp:Label ID="Label5" runat="server" Text="Credit Card Info"></asp:Label>
                </td>
                <td class="style7">
                    <asp:TextBox ID="tb_nric" runat="server" Height="32px" Width="281px">S8511018B</asp:TextBox>
                </td>
            </tr>
                        <tr>
                <td class="style3">
        <asp:Label ID="Label6" runat="server" Text="Mobile"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(+65)
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_mobile" runat="server" Height="32px" Width="281px">81888188</asp:TextBox>
                </td>
            </tr>
                        <tr>
                <td class="style4">
       
                </td>
                <td class="style5">
    <asp:Button ID="btn_Submit" runat="server" Height="48px" 
        onclick="btn_Submit_Click" Text="Submit" Width="288px" />
                </td>
            </tr>
    </table>--%>


<br />
<br />

    <script type="text/javascript">

        function thefun() {
            tb_lastName.Text = "yes"
        }

    </script>
        
<p>
    <asp:Button ID="Button1" runat="server" OnClick="btn_checkPassword_Click" Text="Check Password" Width="437px" />
    <%--<asp:Button ID="Button2" runat="server"  Text="Register" OnClick="thefun()" Width="430px" />--%>

    
    <asp:Label ID="lbl_test" runat="server" Text="thelabel"></asp:Label>
</p>


    <asp:Button ID="Button2" runat="server" OnClick="btn_Register_Click" Text="Register" Width="437px" />


</asp:Content>
