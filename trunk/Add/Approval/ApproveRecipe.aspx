<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveRecipe.aspx.cs"
    MasterPageFile="/Umbraco/masterpages/umbracoPage.Master" Inherits="UmbracoAddons.ApproveRecipe" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>
<%@ Register Src="~/usercontrols/recipes/Details/Kitchens.ascx" TagName="Details"
    TagPrefix="Recipe" %>
<%@ Import Namespace="Hershey.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <cc1:TabView ID="TabView1" runat="server" Width="552px" Height="392px"></cc1:TabView>
    <cc1:Pane ID="pane_Details" runat="server">
        <div>
            <asp:Label AssociatedControlID="cbRecipeApproved" Text="Approved" runat="server"></asp:Label>
            <asp:CheckBox ID="cbRecipeApproved" runat="server" />
        </div>
        <div class="recipe-header">
            <h1 id="dvTitle" runat="server">
            </h1>
            <p class="header-tag">
                <img src="/assets/images/kitchens/tags/light.gif" alt="Light Recipe" id="imgLight"
                    runat="server" /></p>
        </div>
        <div class="recipe-category">
            <img src="/assets/images/kitchens/categories/cakes.png" alt="cakes" id="imgCategories"
                runat="server" />
        </div>
        <div class="recipe-photo" id="dvPhotoMain" runat="server">
        </div>
        <div class="ingredients">
            <h2>
                Ingredients</h2>
            <ul>
                <asp:Repeater ID="rptIngredients" runat="server">
                    <ItemTemplate>
                        <li>
                            <%# DataBinder.Eval(Container.DataItem, "quantity")%>
                            <%# DataBinder.Eval(Container.DataItem, "unit")%>
                            <%# DataBinder.Eval(Container.DataItem, "Ingredient")%>
                        </li>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <li class="odd">
                            <%# DataBinder.Eval(Container.DataItem, "quantity")%>
                            <%# DataBinder.Eval(Container.DataItem, "unit")%>
                            <%# DataBinder.Eval(Container.DataItem, "Ingredient")%>
                        </li>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="directions">
            <h2>
                Directions</h2>
            <div id="dvInstructions" runat="server">
            </div>
            <ol style="display: none;">
                <li>Heat oven to 350°F. Grease and flour one 9-inch round baking pan.</li>
                <li>Stir together flour, sugar, cocoa and baking soda in large bowl. Add butter, milk,
                    egg and vanilla. Beat on low speed of mixer until all ingredients are moistened.
                    Beat on medium speed 2 minutes. Pour batter into prepared pan.</li>
                <li>Bake 30 to 35 minutes or until wooden pick inserted in center comes out clean. Cool
                    10 minutes; remove from pan to wire rack. Cool completely. Prepare CHOCOLATE FILLING.
                    Cut cake into two thin layers. Place one layer on serving plate; spread filling
                    over layer. Top with remaining layer. </li>
                <li>Prepare SATINY CHOCOLATE GLAZE. Pour onto top of cake, allowing some to drizzle
                    down sides. Refrigerate until serving time. Cover; refrigerate leftover cake.</li>
            </ol>
            <h3 style="display: none;">
                Chocolate Filling</h3>
            <ul style="display: none;">
                <li>1/2 cup sugar </li>
                <li>1/4 cup HERSHEY'S Cocoa</li>
                <li>2 tablespoons cornstarch</li>
                <li>1-1/2 cups light cream </li>
                <li>1 tablespoon butter or margarine </li>
                <li>1 teaspoon vanilla extract</li>
            </ul>
            <p style="display: none;">
                Stir together sugar, cocoa and cornstarch in medium saucepan; gradually stir in
                light cream. Cook over medium heat, stirring constantly, until mixture thickens
                and begins to boil. Boil 1 minute, stirring constantly; remove from heat. Stir in
                butter and vanilla. Press plastic wrap directly onto surface. Cool completely.</p>
        </div>
    </cc1:Pane>
    <cc1:Pane ID="pane_Comments" runat="server">
        <ul>
            <asp:Repeater runat="server" ID="rptReviews">
                <ItemTemplate>
                    <li>
                        <p class="group_wrap">
                            <span class="approved">
                                <label for="cbRating<%# Eval("RecipeRatingsID") %>">Approved</label>
                                <input type="checkbox" name="cbRating<%# Eval("RecipeRatingsID") %>" id="cbRating<%# Eval("RecipeRatingsID") %>" <%# IsChecked(Container) %> />
                            </span>
                            <span class="username><%# Eval("UserName") %></span>
                            <span class="tip-date r"><%# Eval("Created")%>
                        </p>
                        <p>
                            <%# Eval("Comment")%>
                        </p>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </cc1:Pane>
</asp:Content>
