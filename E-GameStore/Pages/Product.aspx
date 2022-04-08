<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Pages_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 1029px;
            height: 395px;
        }
        .auto-style2 {
            height: 175px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td rowspan="4">
                <asp:image ID="imgProduct" runat="server" CssClass="detailsImage"></asp:image></td>
            <td><h2>
                <asp:label ID="lblTitle" runat="server" text="Label"></asp:label>
                <hr />
                </h2></td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:label ID="lblDescription" runat="server" CssClass="detailsDescription"></asp:label></td>
            <td class="auto-style2"><asp:label ID="lblPrice" runat="server" CssClass="detailsPrice"></asp:label>
                <br />
                <br />
                Quantity :
            <asp:dropdownlist ID="ddlAmount" runat="server"></asp:dropdownlist><br />
                <asp:Button ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" Text="Add Product" />
                <br />
            <asp:label ID ="lblResult" runat="server" text=""></asp:label>
            </td><br />
        </tr>
        <tr>
            <td>Product Number: <asp:label ID="lblItemNr" runat="server" text="Label"></asp:label></td>
        </tr>
        <tr>
            <td>
                <asp:label ID="Label1" runat="server" text="Available" CssClass="productPrice"></asp:label></td></tr>
    </table>
</asp:Content>

