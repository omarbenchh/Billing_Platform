<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="WebApplication1.Upload" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <h1>WEB Billing Platform for telecom Operators</h1>
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="Form1" runat="server" autocomplete="on">
        <div id="login" class="animate form">

            <h1>Global Bill</h1>
            <table>
                <tr>
                    <th style="text-align: left; font-family: 'Segoe UI'; font-weight: normal;">&nbsp;</th>
                    <th>
                        <asp:FileUpload ID="FileUpload1" runat="server" placeholder="Choose you FIle" />
                    </th>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="Button1" runat="server" Text="Upload and send PDF" Width="196px" OnClick="Button1_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="emptyupload" runat="server" Font-Names="Segoe UI" OnUnload="Page_Load" Text="Label" Visible="False" Style="text-align: center"></asp:Label>
                    </td>
                </tr>
            </table>
            <p></p>
            <p class="change_link">
                <a href="#toregister" class="to_register">Specific Bill</a>
            </p>
        </div>
        <div id="register" class="animate form">
            <h1>Specific Bill</h1>
            <table>
                <tr>
                    <th style="text-align: left; font-family: 'Segoe UI'; font-weight: normal;">&nbsp;</th>
                    <th style="text-align: left;">
                        <label for="username" class="uname" data-icon="u">Phone number </label>
                        <asp:TextBox ID="TxtPhone" runat="server" placeholder="Phone Number"></asp:TextBox>
                    </th>
                </tr>
                <tr>
                    <th style="text-align: left; font-family: 'Segoe UI'; font-weight: normal;">&nbsp;</th>
                    <th>
                        <asp:FileUpload ID="FileUpload2" runat="server" placeholder="Choose you file" />
                    </th>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="Button2" runat="server" Text="Upload and send PDF" Style="text-align: left" Width="196px" OnClick="Button2_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label1" runat="server" Font-Names="Segoe UI" OnUnload="Page_Load" Text="Label" Visible="False" Style="text-align: center"></asp:Label>
                    </td>
                </tr>
            </table>
            <p></p>
            <p class="change_link">
                <a href="#tologin" class="to_register">Global Bill </a>
            </p>
        </div>
    </form>

</asp:Content>





