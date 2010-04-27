using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrandmasRecipes
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (RecipeDBDataContext lr = new RecipeDBDataContext())
            {
                Recipe r = new Recipe
                {
                    Name = "Macoroni and Cheese",
                    Ingredients = GetIngredients(new EntitySet<Ingredient>(), .25, "Cup", .236588236, 2, "Butter", "Land O'Lakes"),
                    Source = new Source { Name = "Label" },
                    Steps = GetSteps(new EntitySet<Step>(), 1, "Boil Some Water")
                };
                lr.Recipes.InsertOnSubmit(r);
                lr.SubmitChanges();
            }
        }
        #region Steps
        protected EntitySet<Step> GetSteps(EntitySet<Step> currentSteps, int order, string instructions)
        {
            currentSteps.Add
            (new Step
                {
                    StepOrder = order,
                    Instruction = instructions
                }
            );
            return currentSteps;
        }
        #endregion
        #region Ingredients
        protected EntitySet<Ingredient> GetIngredients(EntitySet<Ingredient> currentList, double ammount, string unit, double conversionRatio, int SSIUnit, string ingredientName, string ingredientCommonBrand)
        {
            currentList.Add(new Ingredient
            {
                Ammount = ammount,
                MeasurementUnit = GetUnit(unit, conversionRatio, SSIUnit),
                CommonIngredient = GetCommonIngredient(ingredientName, ingredientCommonBrand)
            }
            );
            return currentList;
        }
        protected CommonIngredient GetCommonIngredient(string name, string commonBrand)
        {
            using (RecipeDBDataContext r = new RecipeDBDataContext())
            {
                IQueryable<CommonIngredient> ingr =
                    from i in r.CommonIngredients
                    where
                    (
                        i.Name.Contains(name) && i.CommonBrand.Equals(commonBrand)
                    )
                    select i;
                if (ingr.Count() > 0)
                {
                    return ingr.First();
                }
                else
                {
                    ingr = from i in r.CommonIngredients
                           where (i.Name.Contains(name))
                           select i;
                    if (ingr.Count() > 0)
                    {
                        return ingr.First();
                    }
                    else
                    {
                        return new CommonIngredient
                        {
                            CommonBrand = commonBrand,
                            Name = name
                        };
                    }
                }

            }
        }
        protected MeasurementUnit GetUnit(string name, double conversionRatio, int ssiUnit)
        {
            using (RecipeDBDataContext r = new RecipeDBDataContext())
            {
                IQueryable<MeasurementUnit> mus =
                    from mu in r.MeasurementUnits
                    where
                    (
                        mu.Name.Contains(name)
                    )
                    select mu;
                if (mus.Count() > 0)
                {
                    return mus.First();
                }
                else
                {
                    return new MeasurementUnit
                    {
                        Name = name,
                        SSIConversion = conversionRatio,
                        SSIUnit = ssiUnit
                    };
                }
            }

        }
        #endregion
    }
}
