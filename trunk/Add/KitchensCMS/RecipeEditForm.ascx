<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecipeEditForm.ascx.cs"
    Inherits="Hershey.Web.UmbracoAddons.KitchensCMS.RecipeEditForm" %>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SubmitRecipe" />
<asp:Label ID="lblError" runat="server" Text="" CssClass="error" ForeColor="Red"
    EnableViewState="false"></asp:Label>
<asp:FormView ID="frmRecipeEdit" runat="server" DataKeyNames="recipe_ID" EnableModelValidation="True"
    DefaultMode="Edit" DataSourceID="RecipeDataSource">
    <EditItemTemplate>
        <div id="General" class="tab-content">
            <ol>
                <li class="half">Recipe #:
                    <asp:Literal runat="server" ID="litRecipe_ID" Text='<%# Bind("recipe_ID") %>'></asp:Literal>
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="RecipeNameTextBox" Text="Name:"></asp:Label>
                    <asp:TextBox ID="RecipeNameTextBox" runat="server" size="50" MaxLength="100" Text='<%# Bind("RecipeName") %>' />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RecipeNameTextBox"
                        ErrorMessage="Recipe name is required.">*</asp:RequiredFieldValidator>
                </li>
                <li class="half">
                    <asp:Label runat="server" AssociatedControlID="ddRecipeCategory" Text="Category:"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddRecipeCategory" DataTextField="category_name"
                        SelectedValue='<%# Bind("category_ID") %>' DataSource="<%# RecipeCategories %>"
                        DataValueField="category_ID">
                    </asp:DropDownList>
                    <asp:Label runat="server" AssociatedControlID="ddSkillLevel" Text="Skill Level"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddSkillLevel" SelectedValue='<%# Bind("SkillLevel") %>'>
                        <asp:ListItem>Beginner</asp:ListItem>
                        <asp:ListItem>Intermediate</asp:ListItem>
                        <asp:ListItem>Advanced</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="half">
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="LeadLineTextBox" Text="Lead Line:"></asp:Label>
                    <asp:TextBox ID="LeadLineTextBox" runat="server" Columns="80" TextMode="MultiLine"
                        Text='<%# Bind("LeadLine") %>' />
                </li>
                <%--<li class="half" style="margin-left: 5px;">
                <label>
                    Serving Size:</label>
                <asp:TextBox ID="ServingSizeTextBox" runat="server" 
                Text='<%# Bind("ServingSize") %>' /><asp:requiredfieldvalidator
                    id="RequiredFieldValidator2" runat="server" controltovalidate="ServingSizeTextBox"
                    errormessage="Serving size is required." validationgroup="SubmitRecipe">*</asp:requiredfieldvalidator>
            </li>        --%>
                <li class="enter_ingredient">Ingredients:
                    <asp:DataList ID="dlIngredients" CssClass="ingredients_table" DataSource='<%#Ingredients %>'
                        runat="server" DataKeyField="ingredient_ID">
                        <HeaderTemplate>
                            <tr>
                                <th>
                                    QTY
                                </th>
                                <th>
                                    UNIT
                                </th>
                                <th>
                                    UNIT INFO
                                </th>
                                <th>
                                    INGREDIENT
                                </th>
                                <th>
                                    INGREDIENT INFO
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" Style="width: 40px;" type="text" Text='<%# Bind("quantity") %>'
                                        MaxLength="15" ID='txtQty' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" Style="width: 60px;" type="text" Text='<%# Bind("unit") %>'
                                        MaxLength="25" ID='txtUnit' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" Style="width: 60px;" type="text" Text='<%# Bind("ingredient_info") %>'
                                        MaxLength="100" ID='txtUnitInfo' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" CssClass="ingredient-input" type="text" Text='<%# Bind("Ingredient") %>'
                                        MaxLength="255" ID='txtName' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" CssClass="ingredientinfo-input" type="text" Text='<%# Bind("ingredient_info2") %>'
                                        MaxLength="255" ID='txtIngredientInfo' />
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
                <li>                
                    <asp:CheckBox ID="chkActive" runat="server" Text="Active" Checked='<%# Bind("IsActive") %>' Enabled='<%# RecipeObject.CanPublishRecipes %>' />
                </li>
            </ol>
        </div>
        <div id="Search" class="tab-content">
            <h3>
                Search Criteria
            </h3>
            <table class="style1">
                <tr>
                    <td>
                        <asp:CheckBox ID="QuickAndEasyCheckBox" runat="server" Checked='<%# Bind("QuickAndEasy") %>'
                            Text="Quick And Easy" />
                        <br />
                        <asp:CheckBox ID="LessFatRecipeCheckBox" runat="server" Checked='<%# Bind("LessFatRecipe") %>'
                            Text="Less Fat Recipe" />
                        <br />
                        <asp:CheckBox ID="FunWithKidsCheckBox" runat="server" Checked='<%# Bind("FunWithKids") %>'
                            Text="Fun With Kids" />
                        <br />
                        <asp:CheckBox ID="NewRecipeCheckBox" runat="server" Checked='<%# Bind("NewRecipe") %>'
                            Text=" New Recipe" />
                        <br />
                        <asp:CheckBox ID="OurFavoritesCheckBox" runat="server" Checked='<%# Bind("OurFavorites") %>'
                            Text="Our Favorites" />
                        <br />
                        <asp:CheckBox ID="SensibleCheckBox" runat="server" Checked='<%# Bind("Sensible") %>'
                            Text="Sensible" />
                    </td>
                    <td>
                        <asp:CheckBox ID="MicrowaveRecipeCheckBox" runat="server" Checked='<%# Bind("MicrowaveRecipe") %>'
                            Text="Microwave Recipe" />
                        <br />
                        <asp:CheckBox ID="HolidayRecipeCheckBox" runat="server" Checked='<%# Bind("HolidayRecipe") %>'
                            Text="Holiday Recipe" />
                        <br />
                        <asp:CheckBox ID="ClassicsRecipeCheckBox" runat="server" Checked='<%# Bind("ClassicsRecipe") %>'
                            Text="Classics Recipe" />
                        <br />
                        <asp:CheckBox ID="CelebrationsRecipeCheckBox" runat="server" Checked='<%# Bind("CelebrationsRecipe") %>'
                            Text="Celebrations Recipe" />
                        <br />
                        <asp:CheckBox ID="CookieExchangeRecipeCheckBox" runat="server" Checked='<%# Bind("CookieExchangeRecipe") %>'
                            Text="Cookie Exchange Recipe" />
                    </td>
                </tr>
            </table>
            <%-- <asp:CheckBoxList ID="cblRecipeIdeas" runat="server" DataTextField="category_name"
            DataSource="<%# Ideas %>" DataValueField="category_ID" RepeatColumns="3">
        </asp:CheckBoxList>--%>
            <hr />
            <h3>
                Occasions</h3>
            <asp:CheckBoxList ID="cblOccasions" runat="server" DataTextField="Occasion1" DataValueField="Occasion_ID"
                DataKeyNames="Occasion_ID" DataSource="<%# Occasions %>" RepeatColumns="3">
            </asp:CheckBoxList>
            <hr />
            <h3>
                Products</h3>
            <asp:CheckBoxList ID="cblProducts" runat="server" DataTextField="ProductName" DataValueField="ProductID"
                DataSource="<%# Products %>" RepeatColumns="3">
            </asp:CheckBoxList>
            <hr />
            <%-- <h3>
            Product Variations</h3>
        <asp:checkboxlist id="chkFilterByIdea" runat="server" datatextfield="category_name"
            datavaluefield="category_ID">
                        </asp:checkboxlist>
        <hr />--%>
        </div>
        <div id="Nutrition" class="tab-content">
            <table>
                <tr>
                    <td align="right" width="25%">
                        Serving Size
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="ServingSizeTextBox" size="15" MaxLength="30" runat="server" Text='<%# Bind("ServingSize") %>' />
                    </td>
                    <td align="right" width="25%">
                        # of Servings
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="NumOfServingsTextBox" size="15" MaxLength="30" runat="server" Text='<%# Bind("NumOfServings") %>' />
                    </td>
                </tr>
                <tr>
                    <td align="right" width="25%">
                        Calories
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="CaloriesTextBox" runat="server" size="15" MaxLength="10" Text='<%# Bind("Calories") %>' />
                    </td>
                    <td align="right" width="25%">
                        Calories From Fat
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="CaloriesFatTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("CaloriesFat") %>' />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Total Fat
                    </td>
                    <td>
                        <asp:TextBox ID="TotalFatTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("TotalFat") %>' />
                    </td>
                    <td align="right">
                        % Fat
                    </td>
                    <td>
                        <asp:TextBox ID="_FatTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("_Fat") %>' />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Saturated Fat G
                    </td>
                    <td>
                        <asp:TextBox ID="SaturatedFatGTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("SaturatedFatG") %>' />g
                    </td>
                    <td align="right">
                        % Saturated Fat
                    </td>
                    <td>
                        <asp:TextBox ID="_SaturatedFatTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("_SaturatedFat") %>' />%
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Cholesterol MG
                    </td>
                    <td>
                        <asp:TextBox ID="CholesterolMGTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("CholesterolMG") %>' />mg
                    </td>
                    <td align="right">
                        % Cholesterol
                    </td>
                    <td>
                        <asp:TextBox ID="_CholesterolTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("_Cholesterol") %>' />%
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Sodium MG
                    </td>
                    <td>
                        <asp:TextBox ID="SodiumMGTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("SodiumMG") %>' />mg
                    </td>
                    <td align="right">
                        % Sodium
                    </td>
                    <td>
                        <asp:TextBox ID="_SodiumTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("_Sodium") %>' />%
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Total Carbohydrates
                    </td>
                    <td>
                        <asp:TextBox ID="TotalCarbohydratesTextBox" size="15" MaxLength="10" runat="server"
                            Text='<%# Bind("TotalCarbohydrates") %>' />g
                    </td>
                    <td align="right">
                        % Carbohydrates
                    </td>
                    <td>
                        <asp:TextBox ID="_CarbohydratesTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("_Carbohydrates") %>' />%
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Dietary Fiber G
                    </td>
                    <td>
                        <asp:DropDownList ID="ddDietaryFiberOperator" runat="server" SelectedValue='<%# Bind("DietaryFiberOperator") %>'>
                            <asp:ListItem Value="=" Text="="></asp:ListItem>
                            <asp:ListItem Value="<" Text="&lt;"></asp:ListItem>
                        </asp:DropDownList>
                        <%-- <select id="ddlDietaryFiberOperator" runat="server">
                        <option value="=" selected="selected">=</option>
                        <option value="<">&lt;</option>
                    </select>--%>
                        <asp:TextBox ID="DietaryFiberTextBox" size="8" MaxLength="10" runat="server" Text='<%# Bind("DietaryFiber") %>' />
                    </td>
                    <td align="right">
                        % Fiber
                    </td>
                    <td>
                        <asp:TextBox ID="_FiberTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("_Fiber") %>' />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Sugars G
                    </td>
                    <td>
                        <asp:TextBox ID="SugarsGTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("SugarsG") %>' />g
                    </td>
                    <td align="right">
                        Protein G
                    </td>
                    <td>
                        <asp:TextBox ID="ProteinGTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("ProteinG") %>' />g
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        % Vitamin A
                    </td>
                    <td>
                        <asp:TextBox ID="_VitaminATextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("_VitaminA") %>' />%
                    </td>
                    <td align="right">
                        % Vitamin C
                    </td>
                    <td>
                        <asp:TextBox ID="_VitaminCTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("_VitaminC") %>' />%
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        % Calcium
                    </td>
                    <td>
                        <asp:TextBox ID="_CalciumTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("_Calcium") %>' />%
                    </td>
                    <td align="right">
                        MG Calcium
                    </td>
                    <td>
                        <asp:TextBox ID="CalciumMGTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("CalciumMG") %>' />mg
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        % Iron
                    </td>
                    <td>
                        <asp:TextBox ID="_IronTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("_Iron") %>' />%
                    </td>
                    <td align="right">
                        Total Ingred. Weight
                    </td>
                    <td>
                        <asp:TextBox ID="IngredientWeightGTextBox" size="15" MaxLength="10" runat="server"
                            Text='<%# Bind("IngredientWeightG") %>' />g
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Weight/Cup
                    </td>
                    <td>
                        <asp:TextBox ID="WeightPerCupTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("WeightPerCup") %>' />g
                    </td>
                    <td align="right">
                        Weight/Serving
                    </td>
                    <td>
                        <asp:TextBox ID="WeightPerServingTextBox" size="15" MaxLength="10" runat="server"
                            Text='<%# Bind("WeightPerServing") %>' />g
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Yield
                    </td>
                    <td>
                        <asp:TextBox ID="YieldTextBox" size="20" MaxLength="20" runat="server" Text='<%# Bind("Yield") %>' />
                    </td>
                    <td align="right">
                        Dietary Date
                    </td>
                    <td>
                        <asp:TextBox ID="DietaryDateTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("DietaryDate","{0:d}") %>'
                            CssClass="datepicker" />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Salatrim
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="SalatrimTextBox" runat="server" TextMode="MultiLine" Columns="60"
                            Rows="2" Text='<%# Bind("Salatrim") %>' />
                        <%--<textarea id="txtSalatrim" cols="60" rows="2" runat="server"></textarea>--%>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="3%" align="right">
                        <asp:CheckBox ID="LowCalorieCheckBox" runat="server" Checked='<%# Bind("LowCalorie") %>' />
                    </td>
                    <td width="30%">
                        Low Calorie
                    </td>
                    <td width="3%">
                        <asp:CheckBox ID="LowFatCheckBox" runat="server" Checked='<%# Bind("LowFat") %>' />
                    </td>
                    <td width="30%">
                        Low Fat
                    </td>
                    <td width="3%">
                        <asp:CheckBox ID="LowSodiumCheckBox" runat="server" Checked='<%# Bind("LowSodium") %>' />
                    </td>
                    <td width="30%">
                        Low Sodium
                    </td>
                </tr>
                <tr>
                    <td width="3%" align="right">
                        <asp:CheckBox ID="LowCholesterolCheckBox" runat="server" Checked='<%# Bind("LowCholesterol") %>' />
                    </td>
                    <td width="30%">
                        Low Cholesterol
                    </td>
                    <td width="3%">
                        <asp:CheckBox ID="FatFreeCheckBox" runat="server" Checked='<%# Bind("FatFree") %>' />
                    </td>
                    <td width="24%">
                        Fat Free
                    </td>
                    <td width="3%">
                        <asp:CheckBox ID="EggFreeCheckBox" runat="server" Checked='<%# Bind("EggFree") %>' />
                    </td>
                    <td width="30%">
                        Egg Free
                    </td>
                </tr>
                <tr>
                    <td width="3%" align="right">
                        <asp:CheckBox ID="SugarFreeCheckBox" runat="server" Checked='<%# Bind("SugarFree") %>' />
                    </td>
                    <td width="30%">
                        Sugar Free
                    </td>
                    <td width="3%">
                        <asp:CheckBox ID="MilkFreeCheckBox" runat="server" Checked='<%# Bind("MilkFree") %>' />
                    </td>
                    <td width="30%">
                        Milk Free
                    </td>
                </tr>
            </table>
        </div>
        <div id="Additional" class="tab-content">
            <table style="width: 100%; text-align: right">
                <tr>
                    <td colspan="2">
                        <em>(H:MM where H=hours and M=minutes)</em> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Prep Time &nbsp;
                        <asp:TextBox ID="PrepTimeTextBox" size="8" MaxLength="8" runat="server" Text='<%# Bind("PrepTimeSpan", "{0:HH:mm}") %>' />
                    </td>
                    <td>
                        Cool Time &nbsp;
                        <asp:TextBox ID="CoolTimeTextBox" size="8" MaxLength="8" runat="server" Text='<%# Bind("CoolTimeSpan", "{0:HH:mm}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Bake Time &nbsp;
                        <asp:TextBox ID="BakeTimeTextBox" size="8" MaxLength="8" runat="server" Text='<%# Bind("BakeTimeSpan", "{0:HH:mm}") %>' />
                    </td>
                    <td>
                        Freeze Time &nbsp;
                        <asp:TextBox ID="FreezeTimeTextBox" size="8" MaxLength="8" runat="server" Text='<%# Bind("FreezeTimeSpan", "{0:HH:mm}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Cook Time &nbsp;
                        <asp:TextBox ID="CookTimeTextBox" size="8" MaxLength="8" runat="server" Text='<%# Bind("CookTimeSpan", "{0:HH:mm}") %>' />
                    </td>
                    <td>
                        Chill Time &nbsp;
                        <asp:TextBox ID="ChillTimeTextBox" size="8" MaxLength="8" runat="server" Text='<%# Bind("ChillTimeSpan", "{0:HH:mm}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Start to Finish Time &nbsp;
                        <asp:TextBox ID="StartFinishTimeTextBox" size="8" MaxLength="8" runat="server" Text='<%# Bind("StartFinishTimeSpan", "{0:HH:mm}") %>' />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="right">
                        Origination Date
                    </td>
                    <td>
                        <asp:TextBox ID="OriginDateTextBox" size="16" MaxLength="16" runat="server" Text='<%# Bind("OriginDate","{0:d}") %>'
                            CssClass="datepicker" />
                    </td>
                    <td align="right">
                        TPR Year
                    </td>
                    <td>
                        <asp:TextBox ID="TPRYearTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("TPRYear") %>' />
                        <%--<input type="text" id="txtTPRYear" value="0" runat="server" size="15" maxlength="10"
                        onchange="document.frmRecipe.hdnTPRUpdated.value = 'true'">--%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Recipe Owner
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRecipeOwner" runat="server" SelectedValue='<%# Bind("RecipeOwner") %>'>
                            <asp:ListItem Text="-- select --" Value="0">    </asp:ListItem>
                            <asp:ListItem Value="57" Text="Chocolate">    </asp:ListItem>
                            <asp:ListItem Value="58" Text="FoodServices">    </asp:ListItem>
                            <asp:ListItem Value="59" Text="Linda">    </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        TPR Score
                    </td>
                    <td>
                        <asp:TextBox ID="TPRScoreTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("TPRScore") %>' />
                        <%--<input type="text" id="txtTPRScore" size="15" maxlength="10" value="0" runat="server"
                        onchange="document.frmRecipe.hdnTPRUpdated.value = 'true'">--%>
                    </td>
                </tr>
                <!-- TPRScoreHistory:
            <asp:TextBox ID="TPRScoreHistoryTextBox" runat="server" 
                Text='<%# Bind("TPRScoreHistory") %>' />
                -->
                <tr>
                    <td align="right">
                        Update
                    </td>
                    <td>
                        <select id="ddlUpdateId" runat="server" size="1">
                            <option value="">-- select -- </option>
                            <option value="60">Added</option>
                            <option value="61">Edit</option>
                            <option value="62">Pending</option>
                            <option value="63">Review</option>
                            <option value="64">Revised</option>
                            <option value="65">Update</option>
                        </select>
                    </td>
                    <td align="right">
                        TPR Scale
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="TPRScaleTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("TPRScale") %>' />
                        <%--<input type="text" id="txtTPRScale" value="0" runat="server" size="15" maxlength="10"
                        onchange="document.frmRecipe.txtTPRUpdated.value = 'true'">--%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Date Updated
                    </td>
                    <td>
                        <%--<input type="text" id="txtDateUpdated" value="" runat="server">--%>
                        <asp:TextBox ID="DateUpdatedTextBox" runat="server" size="16" MaxLength="16" Text='<%# Bind("DateUpdated","{0:d}") %>'
                            CssClass="datepicker"></asp:TextBox>
                    </td>
                    <td align="right">
                        TPR Date
                    </td>
                    <td>
                        <asp:TextBox ID="TPRDateTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("TPRDate","{0:d}") %>'
                            CssClass="datepicker" />
                        <%--<asp:TextBox ID="TPRDateHistoryTextBox" size="15" MaxLength="10" runat="server" Text='<%# Bind("TPRDateHistory") %>' />--%>
                        <%--<input type="text" id="txtTPRDate" size="15" maxlength="10" value="" runat="server"
                        onchange="document.frmRecipe.hdnTPRUpdated.value = 'true'">&nbsp;--%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Last Modified By
                    </td>
                    <td>
                        <asp:TextBox ID="ModifiedByTextBox" runat="server" size="20" MaxLength="30" Text='<%# Bind("ModifiedBy") %>' />
                    </td>
                    <td align="right">
                        Skill Level
                    </td>
                    <td>
                        <select id="ddlSkillLevel" size="1" runat="server">
                            <option value="">-- select -- </option>
                            <option value="Beginner">Beginner</option>
                            <option value="Intermediate">Intermediate</option>
                            <option value="Advanced">Advanced</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Consumer Test Date
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="ConsumerTestDateTextBox" size="15" MaxLength="10" runat="server"
                            CssClass="datepicker" Text='<%# Bind("ConsumerTestDate","{0:d}") %>' />
                        <%-- <input type="text" id="txtConsumerTestDate" size="15" maxlength="10" value="" runat="server">&nbsp;<a
                        href="javascript:show_calendar('document.frmRecipe.txtConsumerTestDate', document.frmRecipe.txtConsumerTestDate.value);"
                        tabindex="-1"><img src="/recipes/cm/lib/img/calendar.gif" width="16" height="16"
                            align="middle" border="0" alt="Select a Date..."> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Kitchen Final
                    </td>
                    <td valign="top" colspan="3">
                        <asp:CheckBox ID="KitchenFinalCheckBox" runat="server" Checked='<%# Bind("KitchenFinal") %>' />
                    </td>
                </tr>
                <!-- 
            <asp:TextBox ID="KitchenFinalDateTextBox" runat="server" 
                Text='<%# Bind("KitchenFinalDate") %>' CssClass="datepicker" />
            -->
                <tr>
                    <td align="right">
                        Public Final
                    </td>
                    <td valign="top" colspan="3">
                        <asp:CheckBox ID="PublicFinalCheckBox" runat="server" Checked='<%# Bind("PublicFinal") %>' />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Recipe Uses
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="RecipeUsesTextBox" runat="server" TextMode="MultiLine" Columns="70"
                            Rows="4" Text='<%# Bind("RecipeUses") %>' />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Former Names
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="FormerNamesTextBox" runat="server" TextMode="MultiLine" Columns="70"
                            Rows="2" Text='<%# Bind("FormerNames") %>' />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Related Recipe
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="RelatedRecipeTextBox" runat="server" TextMode="MultiLine" Columns="70"
                            Rows="2" Text='<%# Bind("RelatedRecipe") %>' />
                        <%--<textarea id="txtRelatedRecipe" cols="70" rows="2" runat="server"></textarea>--%>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Comments
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="CommentsTextBox" runat="server" TextMode="MultiLine" Columns="70"
                            Rows="2" Text='<%# Bind("Comments") %>' />
                        <%--<textarea id="txtComments" cols="70" rows="2" runat="server"></textarea>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div id="Save">
            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Enabled='<%# RecipeObject.CanEditRecipes %>'
                Text="Save Changes" />
            &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False"
                Visible="false" CommandName="Cancel" Text="Cancel" />
        </div>
    </EditItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="RecipeDataSource" runat="server" DataObjectTypeName="Hershey.DataLayer.Recipes.Recipe"
    SelectMethod="Select" TypeName="Hershey.DataLayer.Recipes.RecipeDataSource" UpdateMethod="Update"
    ConvertNullToDBNull="True">
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
</asp:ObjectDataSource>
