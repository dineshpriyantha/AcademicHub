using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BusinessLogic.Services
{
    public static class EnumHelper
    {
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, string optionLabel = null, object htmlAttributes = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;
            var values = Enum.GetValues(enumType).Cast<TEnum>();

            var items = from value in values
                        select new SelectListItem
                        {
                            Text = value.ToString(),
                            Value = value.ToString(),
                            Selected = value.Equals(metadata.Model)
                        };

            if (!string.IsNullOrEmpty(optionLabel))
            {
                items = new[] { new SelectListItem { Text = optionLabel, Value = "" } }.Concat(items);
            }

            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }
    }

}
