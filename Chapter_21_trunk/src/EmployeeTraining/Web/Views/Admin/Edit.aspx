﻿<%@ Page Title="Edit Email Type Page" Language="C#" MasterPageFile="~/Views/Shared/MasterViewPage.Master" 
Inherits="System.Web.Mvc.ViewPage<Infrastructure.ValueObjects.EmailTypeVO>" 
Theme="DefaultTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="server">

     <div class="formTitle">
        Edit Email Type</div>

    <% using (Html.BeginForm("Save", "Admin")) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EmailTypeID) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EmailTypeID, 
                                    new { @readonly="false", @class="readonlyInputField" })%>
                <%: Html.ValidationMessageFor(model => model.EmailTypeID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Description) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Description, new { size = "50", maxlength = "50" })%>
                <%: Html.ValidationMessageFor(model => model.Description, null, new { @class = "required" })%>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "EmailTypeMaintenance") %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterContentHolder" runat="server">
</asp:Content>

