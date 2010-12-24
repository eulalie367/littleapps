<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/RecipeAdmin.Master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="RecipeProductManagement.aspx.cs"
    Inherits="Hershey.Web.UmbracoAddons.KitchensCMS.RecipeProductManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Products</h3>
    <asp:panel id="Panel1" runat="server" backcolor="#CCCCCC" width="600px" defaultbutton="btnAddProduct"
        groupingtext="Add New Product">
        <asp:Label ID="Label1" runat="server" Text="Product Name" AssociatedControlID="txtProductName"></asp:Label>
        <asp:TextBox runat="server" ID="txtProductName" Width="100%"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Category:" 
            AssociatedControlID="ddProductCategory"></asp:Label>
        <asp:DropDownList ID="ddProductCategory" runat="server" DataTextField="Category"
            DataValueField="CategoryID" DataSourceID="sqlProductCategory">
        </asp:DropDownList>
        
        &nbsp;<asp:Label ID="Label3" runat="server" AssociatedControlID="txtProductImage" 
            Text="Image (Product Node Id)"></asp:Label>
        <asp:TextBox runat="server" ID="txtProductImage"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Description" AssociatedControlID="txtDescription"></asp:Label>
        <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" 
            Width="100%" Rows="4"></asp:TextBox>
        <br />
        <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" />
        <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
    </asp:panel>
    <p>
    </p>
    <asp:gridview id="GridView1" runat="server" autogeneratecolumns="False" cellpadding="4"
        datakeynames="ProductID" datasourceid="sqlProducts" enablemodelvalidation="True"
        forecolor="#333333" gridlines="None" allowsorting="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="Product ID" InsertVisible="False"
                ReadOnly="True" SortExpression="ProductID" />
            <asp:TemplateField HeaderText="Category" SortExpression="CategoryID">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddProductCategory" runat="server" DataTextField="Category" DataValueField="CategoryID" DataSourceID="sqlProductCategory" SelectedValue='<%# Bind("CategoryID") %>'>
                    </asp:DropDownList>                    
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
            <asp:BoundField DataField="ProductImage" HeaderText="Product Number" SortExpression="ProductImage" />
            <asp:TemplateField HeaderText="ProductDescription" SortExpression="ProductDescription">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Columns="100" Text='<%# Bind("ProductDescription") %>'
                        TextMode="MultiLine" Rows="5"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductDescription") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="rank" HeaderText="Sort Order" SortExpression="rank" />
            <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
            <asp:CommandField ShowDeleteButton="False" ShowEditButton="True" HeaderText="Edit"
                ButtonType="Button" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:gridview>
    <asp:sqldatasource id="sqlProducts" runat="server" connectionstring="<%$ ConnectionStrings:KitchenDB %>"
        deletecommand="DELETE FROM [Products] WHERE [ProductID] = @ProductID" insertcommand="INSERT INTO [Products] ([CategoryID], [ProductName], [ProductImage], [ProductDescription], [rank], [IsActive]) VALUES (@CategoryID, @ProductName, @ProductImage, @ProductDescription, @rank, @IsActive)"
        selectcommand="SELECT Products.*, Category FROM [Products] 
join ProductCategories pc ON pc.CategoryId = Products.CategoryId
ORDER BY Products.[rank]" updatecommand="UPDATE [Products] SET [CategoryID] = @CategoryID, [ProductName] = @ProductName, [ProductImage] = @ProductImage, [ProductDescription] = @ProductDescription, [rank] = @rank, [IsActive] = @IsActive WHERE [ProductID] = @ProductID">
        <DeleteParameters>
            <asp:Parameter Name="ProductID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="CategoryID" Type="Int32" />
            <asp:Parameter Name="ProductName" Type="String" />
            <asp:Parameter Name="ProductImage" Type="String" />
            <asp:Parameter Name="ProductDescription" Type="String" />
            <asp:Parameter Name="rank" Type="Int32" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CategoryID" Type="Int32" />
            <asp:Parameter Name="ProductName" Type="String" />
            <asp:Parameter Name="ProductImage" Type="String" />
            <asp:Parameter Name="ProductDescription" Type="String" />
            <asp:Parameter Name="rank" Type="Int32" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
            <asp:Parameter Name="ProductID" Type="Int32" />
        </UpdateParameters>
    </asp:sqldatasource>
    <asp:sqldatasource id="sqlProductCategory" runat="server" connectionstring="<%$ ConnectionStrings:KitchenDB %>"
        selectcommand="SELECT [CategoryID], [Category] FROM [ProductCategories] ORDER BY [Rank]">
    </asp:sqldatasource>
</asp:Content>
