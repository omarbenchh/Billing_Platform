<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="login" class="animate form">
        <form runat="server" autocomplete="on">
            <h1>Log in</h1>
            <p>
                <label for="username" class="uname" data-icon="u">Username </label>
                <asp:TextBox ID="TxtIdentifiant" runat="server"></asp:TextBox>
            </p>
            <p>
                <label for="password" class="youpasswd" data-icon="p">Your password </label>
                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </p>
            <p class="keeplogin">
                <input type="checkbox" name="loginkeeping" id="loginkeeping" value="loginkeeping" />
                <label for="loginkeeping">Keep me logged in</label>
            </p>
            <p class="login button">
                <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click1" />
            </p>
        </form>
    </div>

</asp:Content>


