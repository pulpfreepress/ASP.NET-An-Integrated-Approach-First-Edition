<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterViewPage.Master" AutoEventWireup="true" 
CodeBehind="About.aspx.cs" Inherits="Web.Views.Help.About" Theme="DefaultTheme"    %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="server">

 <h1><%: ViewData["Message"] %></h1>

 <h2>
 <asp:Label ID="MessageLabel" runat="server"></asp:Label>
 </h2>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContentHolder" runat="server">
</asp:Content>
