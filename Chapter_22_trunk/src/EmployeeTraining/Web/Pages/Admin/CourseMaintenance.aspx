<%@ Page Title="Course Maintenance Page" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="CourseMaintenance.aspx.cs" 
    Inherits="Web.Pages.Admin.CourseMaintenance"
    Theme="DefaultTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="server">
    <div class="formTitle">
        Course Maintenance Page</div>
    <%-------------------------------- DataSources  ------------------------------------%>
    <asp:ObjectDataSource ID="CoursesDataSource" runat="server" DeleteMethod="Delete"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="Web.DataSets.CoursesTableAdapters.tbl_Course_LUTableAdapter" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_CourseID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Code" Type="String" DefaultValue="None" />
            <asp:Parameter Name="Title" Type="String" DefaultValue="None" />
            <asp:Parameter Name="Description" Type="String" DefaultValue="None" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Code" Type="String" DefaultValue="None" />
            <asp:Parameter Name="Title" Type="String" DefaultValue="None" />
            <asp:Parameter Name="Description" Type="String" DefaultValue="None" />
            <asp:Parameter Name="Original_CourseID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="CourseDetailsViewDataSource" runat="server" DeleteMethod="Delete"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataBy"
        TypeName="Web.DataSets.CoursesTableAdapters.tbl_Course_LUTableAdapter" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_CourseID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Code" Type="String" DefaultValue="None" />
            <asp:Parameter Name="Title" Type="String" DefaultValue="None" />
            <asp:Parameter Name="Description" Type="String" DefaultValue="None" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="CoursesGridView" Name="CourseID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Code" Type="String" DefaultValue="None" />
            <asp:Parameter Name="Title" Type="String" DefaultValue="None" />
            <asp:Parameter Name="Description" Type="String" DefaultValue="None" />
            <asp:Parameter Name="Original_CourseID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <%--------------------------------- End DataSources ---------------------------------%>
    <%--------------------------------- GridViews ---------------------------------%>
    <asp:GridView ID="CoursesGridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="CourseID" DataSourceID="CoursesDataSource"
        OnSelectedIndexChanged="SelectedIndexChangedHandler" Width="1095px" CellPadding="4"
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="CourseID" InsertVisible="False"
                ReadOnly="True" SortExpression="CourseID" />
            <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:CommandField ShowEditButton="True" ShowSelectButton="True" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <table class="commandTable">
        <tr>
            <td align="center">
                <asp:LinkButton CssClass="centerControls" runat="server" ID="newButton" 
                OnClick="NewCourseHandler">New</asp:LinkButton>
            </td>
        </tr>
    </table>
    <%--------------------------------- End GridViews ---------------------------------%>
    <%---------------------------------- Details View ----------------------------------%>
    <div id="detailsViewDiv" runat="server" class="formTitle">
        Courses Details</div>
    <asp:DetailsView ID="CourseDetailsView" runat="server" HorizontalAlign="Center" Height="50px"
        Width="1090px" AutoGenerateRows="False" DataKeyNames="CourseID" DataSourceID="CourseDetailsViewDataSource"
        OnItemInserted="ItemInsertedHandler" OnItemUpdated="ItemUpdatedHandler" CellPadding="4"
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="CourseID" HeaderText="CourseID" InsertVisible="False"
                ReadOnly="True" SortExpression="CourseID" />
            <asp:TemplateField HeaderText="Code" SortExpression="Code">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="6" Text='<%# Bind("Code") %>'>
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="codeRequiredValidator1" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox1" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="codeRegExpressionValidator1" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox1" ValidationExpression="^[A-Z]{3}[0-9]{3}$" 
                        ErrorMessage="Invalid Format!"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="6" Text='<%# Bind("Code") %>'>
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="codeRequiredValidator2" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox1"
                        ErrorMessage="Required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="codeRegExpressionValidator2" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox1" ValidationExpression="^[A-Z]{3}[0-9]{3}$" 
                        ErrorMessage="Invalid Format!"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="100px" />
                <ItemStyle Width="700px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title" SortExpression="Title">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="200" Text='<%# Bind("Title") %>'>
                    </asp:TextBox>
                     <asp:RequiredFieldValidator ID="titleRequiredValidator1" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox2" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="titleRegExpressionValidator1" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox2" ValidationExpression="^[a-zA-Z0-9 ]{200}$" 
                        ErrorMessage="Invalid Format!"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="200" Text='<%# Bind("Title") %>'>
                    </asp:TextBox>
                     <asp:RequiredFieldValidator ID="titleRequiredValidator2" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox2" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="titleRegExpressionValidator2" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox2" ValidationExpression="^[a-zA-Z0-9]{200}$" 
                        ErrorMessage="Invalid Format!"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="600px" />
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="1000" Text='<%# Bind("Description") %>'>
                    </asp:TextBox>
                     <asp:RequiredFieldValidator ID="descriptionRequiredValidator1" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox3" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="descriptionRegExpressionValidator1" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox3" ValidationExpression="^[a-zA-Z0-9 ]{1000}$" 
                        ErrorMessage="Invalid Format!"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="1000" Text='<%# Bind("Description") %>'>
                    </asp:TextBox>
                     <asp:RequiredFieldValidator ID="descriptionRequiredValidator2" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox3" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="descriptionRegExpressionValidator2" runat="server" 
                        CssClass="errorMessage"
                        ControlToValidate="TextBox3" ValidationExpression="^[a-zA-Z0-9 ]{1000}$" 
                        ErrorMessage="Invalid Format!"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="600px" />
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ShowInsertButton="True" />
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>
    <%----------------------------------End Details View  ------------------------------%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContentHolder" runat="server">
</asp:Content>
