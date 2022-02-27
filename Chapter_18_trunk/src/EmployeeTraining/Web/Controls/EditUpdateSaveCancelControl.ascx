<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditUpdateSaveCancelControl.ascx.cs"
            Inherits="Web.Controls.EditUpdateSaveCancelControl" %>

<!-- Edit Button -->
<asp:LinkButton runat="server" ID="editButton"
    CssClass="pageProcessingLink" Text="Edit" 
    Visible="true" CausesValidation="false" 
    OnClick="CallEditHandlerMethod" />

<!-- Update Button -->
<asp:LinkButton runat="server" ID="updateButton"
    CssClass="pageProcessingLink" Text="Update" 
    Visible="true" CausesValidation="true" 
    OnClick="CallUpdateHandlerMethod" />

<!-- Save Button -->
<asp:LinkButton runat="server" ID="saveButton"
    CssClass="pageProcessingLink" Text="Save" 
    Visible="true" CausesValidation="true" 
    OnClick="CallSaveHandlerMethod" />

<!-- Cancel Button -->
<asp:LinkButton runat="server" ID="cancelButton"
    CssClass="pageProcessingLink" Text="Cancel" 
    Visible="true" CausesValidation="false"
    OnClick="CallCancelHandlerMethod" /> 
