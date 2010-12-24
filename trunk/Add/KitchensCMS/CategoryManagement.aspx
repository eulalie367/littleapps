<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/RecipeAdmin.Master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="CategoryManagement.aspx.cs"
    Inherits="Hershey.Web.UmbracoAddons.KitchensCMS.CategoryManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Changes to this pages affect the navigation on the public facing website when active
        is true.
    </p>
    <h3>
        Recipe Categories</h3>
    <asp:gridview id="gvRecipeCategories" runat="server" allowsorting="True" autogeneratecolumns="False"
        cellpadding="4" datakeynames="category_ID" datasourceid="SqlDataSource1" enablemodelvalidation="True"
        enablesortingandpagingcallbacks="True" forecolor="#333333" gridlines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="category_ID" HeaderText="category_ID" 
                InsertVisible="False" ReadOnly="True" SortExpression="category_ID" 
                Visible="False" />
            <asp:BoundField DataField="category_pid" HeaderText="category_pid" 
                SortExpression="category_pid" Visible="False" />
            <asp:BoundField DataField="category_name" HeaderText="Name" 
                SortExpression="category_name" />
            <asp:BoundField DataField="category_description" 
                HeaderText="category_description" SortExpression="category_description" 
                Visible="False" />
            <asp:BoundField DataField="table_name" HeaderText="table_name" 
                SortExpression="table_name" Visible="False" />
            <asp:BoundField DataField="active" HeaderText="Active" 
                SortExpression="active" />
            <asp:BoundField DataField="rank" HeaderText="Sort Order" 
                SortExpression="rank" />
            <asp:CommandField ShowEditButton="True" 
                HeaderText="Edit" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:gridview>
    <asp:panel runat="server" backcolor="#CCCCCC" Width="500">
        <asp:TextBox runat="server" ID="txtCategory"></asp:TextBox>        
        <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" 
                onclick="btnAddCategory_Click" />
        </asp:panel>
    <br />
    <h3>
        Product Categories:</h3>
    <asp:gridview id="GridView1" runat="server" autogeneratecolumns="False" cellpadding="4"
        datakeynames="Category" datasourceid="sqlProductCategories" enablemodelvalidation="True"
        forecolor="#333333" gridlines="None" AllowSorting="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="CategoryID" HeaderText="CategoryID" 
                InsertVisible="False" ReadOnly="True" SortExpression="CategoryID" 
                Visible="False" />
            <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="True" 
                SortExpression="Category" />
            <asp:CheckBoxField DataField="IsActive" HeaderText="Active" 
                SortExpression="IsActive" />
            <asp:BoundField DataField="Rank" HeaderText="Sort Order" 
                SortExpression="Rank" />
            <asp:CommandField HeaderText="Edit" 
                ShowEditButton="True" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:gridview>
    <asp:panel runat="server" backcolor="#CCCCCC" Width="500">
        <asp:TextBox runat="server" ID="txtProductCategory"></asp:TextBox>        
        <asp:Button ID="btnAddProductCategory" runat="server" Text="Add Product Category" 
                onclick="btnAddProductCategory_Click" />
        </asp:panel>
    <br />
    <h3>
        Occasions</h3>
    <asp:gridview id="GridView2" runat="server" autogeneratecolumns="False" cellpadding="4"
        datakeynames="Occasion_ID" datasourceid="sqlOccasions" enablemodelvalidation="True"
        forecolor="#333333" gridlines="None" AllowSorting="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Occasion_ID" HeaderText="Occasion_ID" 
                InsertVisible="False" ReadOnly="True" SortExpression="Occasion_ID" 
                Visible="False" />
            <asp:BoundField DataField="Occasion" HeaderText="Occasion" 
                SortExpression="Occasion" />
            <asp:BoundField DataField="active" HeaderText="Active" 
                SortExpression="active" />
            <asp:BoundField DataField="rank" HeaderText="Sort Order" 
                SortExpression="rank" />
            <asp:CommandField HeaderText="Edit" 
                ShowEditButton="True" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:gridview>
    <asp:panel runat="server" backcolor="#CCCCCC" Width="500">
        <asp:TextBox runat="server" ID="txtOccasion"></asp:TextBox>        
        <asp:Button ID="btnAddOccasion" runat="server" Text="Add Occasion" 
                onclick="btnAddOccasion_Click" />
        </asp:panel>
    <asp:sqldatasource id="sqlOccasions" runat="server" connectionstring="<%$ ConnectionStrings:KitchenDB %>"
        deletecommand="DELETE FROM [Occasions] WHERE [Occasion_ID] = @Occasion_ID" insertcommand="INSERT INTO [Occasions] ([Occasion], [rank], [active], [int]) VALUES (@Occasion, @rank, @active, @int)"
        selectcommand="SELECT * FROM [Occasions] ORDER BY [rank]" updatecommand="UPDATE [Occasions] SET [Occasion] = @Occasion, [rank] = @rank, [active] = @active, [int] = @int WHERE [Occasion_ID] = @Occasion_ID">
        <DeleteParameters>
            <asp:Parameter Name="Occasion_ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Occasion" Type="String" />
            <asp:Parameter Name="rank" Type="Int32" />
            <asp:Parameter Name="active" Type="String" />
            <asp:Parameter Name="int" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Occasion" Type="String" />
            <asp:Parameter Name="rank" Type="Int32" />
            <asp:Parameter Name="active" Type="String" />
            <asp:Parameter Name="int" Type="Int32" />
            <asp:Parameter Name="Occasion_ID" Type="Int32" />
        </UpdateParameters>
    </asp:sqldatasource>
    <br />
    <asp:sqldatasource id="sqlProductCategories" runat="server" connectionstring="<%$ ConnectionStrings:KitchenDB %>"
        deletecommand="DELETE FROM [ProductCategories] WHERE [Category] = @Category"
        insertcommand="INSERT INTO [ProductCategories] ([Category], [Rank], [IsActive]) VALUES (@Category, @Rank, @IsActive)"
        selectcommand="SELECT * FROM [ProductCategories] ORDER BY Rank" updatecommand="UPDATE [ProductCategories] SET [CategoryID] = @CategoryID, [Rank] = @Rank, [IsActive] = @IsActive WHERE [Category] = @Category">
        <DeleteParameters>
            <asp:Parameter Name="Category" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Category" Type="String" />
            <asp:Parameter Name="Rank" Type="Int32" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CategoryID" Type="Int32" />
            <asp:Parameter Name="Rank" Type="Int32" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
            <asp:Parameter Name="Category" Type="String" />
        </UpdateParameters>
    </asp:sqldatasource>
    <br />
    <br />
    <asp:sqldatasource id="SqlDataSource1" runat="server" connectionstring="<%$ ConnectionStrings:KitchenDB %>"
        deletecommand="DELETE FROM [Category] WHERE [category_ID] = @category_ID" insertcommand="INSERT INTO [Category] ([category_pid], [category_name], [category_description], [table_name], [active], [rank]) VALUES (@category_pid, @category_name, @category_description, @table_name, @active, @rank)"
        selectcommand="SELECT * FROM [Category] WHERE ([table_name] = @table_name) ORDER BY [rank]"
        updatecommand="UPDATE [Category] SET [category_pid] = @category_pid, [category_name] = @category_name, [category_description] = @category_description, [table_name] = @table_name, [active] = @active, [rank] = @rank WHERE [category_ID] = @category_ID">
        <DeleteParameters>
            <asp:Parameter Name="category_ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="category_pid" Type="Int32" />
            <asp:Parameter Name="category_name" Type="String" />
            <asp:Parameter Name="category_description" Type="String" />
            <asp:Parameter Name="table_name" Type="String" />
            <asp:Parameter Name="active" Type="String" />
            <asp:Parameter Name="rank" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="recipes" Name="table_name" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="category_pid" Type="Int32" />
            <asp:Parameter Name="category_name" Type="String" />
            <asp:Parameter Name="category_description" Type="String" />
            <asp:Parameter Name="table_name" Type="String" />
            <asp:Parameter Name="active" Type="String" />
            <asp:Parameter Name="rank" Type="Int32" />
            <asp:Parameter Name="category_ID" Type="Int32" />
        </UpdateParameters>
    </asp:sqldatasource>
</asp:Content>
