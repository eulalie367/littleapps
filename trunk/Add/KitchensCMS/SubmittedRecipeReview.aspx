<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/RecipeAdmin.Master"
    AutoEventWireup="true" CodeBehind="SubmittedRecipeReview.aspx.cs" Inherits="Hershey.Web.UmbracoAddons.KitchensCMS.SubmittedRecipeReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:formview id="frmRecipeEdit" runat="server" datakeynames="recipeId" enablemodelvalidation="True"
        defaultmode="Edit" datasourceid="RecipeDataSource">
    <EditItemTemplate>
        <div id="General" class="tab-content">
            <ol>
                <li class="half">Recipe #:
                    <asp:Literal runat="server" ID="litrecipeId" Text='<%# Bind("recipeId") %>'></asp:Literal>
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="RecipeNameTextBox" Text="Name:"></asp:Label>
                    <asp:TextBox ID="RecipeNameTextBox" runat="server" size="50" MaxLength="100" Text='<%# Bind("RecipeName") %>' />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RecipeNameTextBox"
                        ErrorMessage="Recipe name is required.">*</asp:RequiredFieldValidator>
                </li>
                <li class="half">
                    <asp:Label runat="server" AssociatedControlID="chkRecipeCategories" Text="Categories:"></asp:Label>
                    <asp:DropDownList ID="chkRecipeCategories" runat="server" DataTextField="CategoryName" DataValueField="CategoryId" DataSource="<%# Categories %>"> 
                    </asp:DropDownList>
                </li>

                <li class="half" style="margin-left: 5px;">
                <label>
                    Serving Size:</label>
                <asp:TextBox ID="ServingSizeTextBox" runat="server" Text='<%# Bind("ServingSize") %>' />
                </li>
                <li class="enter_ingredient">Ingredients:
                    <asp:DataList ID="dlIngredients" CssClass="ingredients_table" DataSource='<%#Ingredients %>'
                        runat="server" DataKeyField="IngredientID">
                        <HeaderTemplate>
                            <tr>
                                <th>
                                    Quantity
                                </th>
                                <th>
                                    Unit
                                </th>
                                <th>
                                    Item
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" Style="width: 40px;" type="text" Text='<%# Bind("Quantity") %>'
                                        MaxLength="15" ID='txtQty' />
                                </td>                                                              
                                <td>
                                    <asp:TextBox runat="server" CssClass="ingredient-input" type="text" Text='<%# Bind("Unit") %>'
                                        MaxLength="255" ID='txtUnit' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" CssClass="ingredientinfo-input" type="text" Text='<%# Bind("Item") %>'
                                        MaxLength="255" ID='txtItem' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Button ID="btnAddIngredient" runat="server" Text="Add another ingredient" OnClick="btnAddIngredient_Click" />
                    <%--<a href="#" class="add"></a>--%>
                </li>
                <li>
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="InstructionsTextBox" Text="Instructions:"></asp:Label>
                    <asp:TextBox ID="InstructionsTextBox" runat="server" TextMode="MultiLine" Rows="10"
                        Text='<%# Bind("Instructions") %>' Columns="80" Style="width: 100%" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="InstructionsTextBox"
                        ErrorMessage="Please enter some instructions." ValidationGroup="SubmitRecipe">*</asp:RequiredFieldValidator>
                </li>
                <li><a class="preview" href="/Image.ashx?type=submittedphoto&id=<%= RecipeId %>&s=lg&missing=1"
                    target="_blank">
                    <img src="/Image.ashx?type=submittedphoto&id=<%= RecipeId %>&s=sm&missing=1" style="height: 40px;
                        width: 60px;" />
                </a>
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="uploadRecipe" Text="Recipe Image:"></asp:Label>
                    <asp:FileUpload ID="uploadRecipe" runat="server" ToolTip="Upload Recipe Photo" />
                </li>
      
              <li class="">
                    <asp:Label runat="server" AssociatedControlID="chkRecipeCategories" Text="Active:"></asp:Label>
                    <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Bind("Active") %>' > 
                    </asp:CheckBox>
                </li>
               
            </ol>
        </div>

      
       
        <div id="Save">
            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                Text="Save Changes" />
            &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False"
                Visible="false" CommandName="Cancel" Text="Cancel" />
        </div>
    </EditItemTemplate>
</asp:formview>
    <asp:objectdatasource id="RecipeDataSource" runat="server" dataobjecttypename="Hershey.DataLayer.Recipes.Recipe"
        selectmethod="SelectSubmittedRecipe" typename="Hershey.DataLayer.Recipes.RecipeDataSource"
        updatemethod="UpdateSubmitedRecipe" convertnulltodbnull="True">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="0" Name="sId" QueryStringField="Id" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="OriginDate" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="DietaryDate" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="TPRDate" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="ConsumerTestDate" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="KitchenFinalDate" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="PublicFinalDate" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="DateUpdated" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="CreateDate" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="TPRYear" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="TPRScore" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="TPRScale" ConvertEmptyStringToNull="true" />
    </UpdateParameters>
</asp:objectdatasource>
</asp:Content>
