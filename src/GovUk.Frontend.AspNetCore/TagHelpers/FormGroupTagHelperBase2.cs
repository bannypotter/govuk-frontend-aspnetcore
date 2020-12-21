#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GovUk.Frontend.AspNetCore.HtmlGeneration;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUk.Frontend.AspNetCore.TagHelpers
{
    /// <summary>
    /// Base class for tag helpers that generate a form group.
    /// </summary>
    public abstract class FormGroupTagHelperBase2 : TagHelper
    {
        private protected const string AspForAttributeName = "asp-for";
        private protected const string IgnoreModelStateErrorsAttributeName = "ignore-modelstate-errors";

        private protected FormGroupTagHelperBase2(
            IGovUkHtmlGenerator htmlGenerator,
            IModelHelper modelHelper)
        {
            Generator = Guard.ArgumentNotNull(nameof(htmlGenerator), htmlGenerator);
            ModelHelper = Guard.ArgumentNotNull(nameof(modelHelper), modelHelper);
        }

        [HtmlAttributeName(AspForAttributeName)]
        public ModelExpression? AspFor { get; set; }

        [HtmlAttributeName(IgnoreModelStateErrorsAttributeName)]
        public bool IgnoreModelStateErrors { get; set; } = false;

        /// <summary>
        /// Gets the <see cref="ViewContext"/> of the executing view.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        [DisallowNull]
        public ViewContext? ViewContext { get; set; }

        private protected string? DescribedBy { get; set; }

        private protected IGovUkHtmlGenerator Generator { get; }

        private protected IModelHelper ModelHelper { get; }

        /// <inheritdoc/>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var formGroupContext = CreateFormGroupContext();

            IHtmlContent content;
            bool haveError;

            using (context.SetScopedContextItem(formGroupContext))
            using (context.SetScopedContextItem(formGroupContext.GetType(), formGroupContext))
            {
                await output.GetChildContentAsync();

                content = GenerateFormGroupContent(context, formGroupContext, out haveError);
            }

            var tagBuilder = Generator.GenerateFormGroup(
                haveError,
                content,
                output.Attributes.ToAttributesDictionary());

            output.TagName = tagBuilder.TagName;
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Clear();
            output.MergeAttributes(tagBuilder);
            output.Content.SetHtmlContent(tagBuilder.InnerHtml);
        }

        private protected static string? AppendToDescribedBy(string? describedBy, string? value)
        {
            if (value == null)
            {
                return describedBy;
            }

            if (describedBy == null)
            {
                return value;
            }
            else
            {
                return $"{describedBy} {value}";
            }
        }

        private protected abstract FormGroupContext2 CreateFormGroupContext();

        private protected abstract IHtmlContent GenerateFormGroupContent(
            TagHelperContext context,
            FormGroupContext2 formGroupContext,
            out bool haveError);

        private protected IHtmlContent? GenerateErrorMessage(FormGroupContext2 formGroupContext)
        {
            var visuallyHiddenText = formGroupContext.ErrorMessage?.VisuallyHiddenText ??
                ComponentGenerator.ErrorMessageDefaultVisuallyHiddenText;

            var content = formGroupContext.ErrorMessage?.Content;
            var attributes = formGroupContext.ErrorMessage?.Attributes;

            if (content == null && AspFor != null && IgnoreModelStateErrors != true)
            {
                var validationMessage = ModelHelper.GetValidationMessage(
                    ViewContext,
                    AspFor!.ModelExplorer,
                    AspFor.Name);

                if (validationMessage != null)
                {
                    content = new HtmlString(validationMessage);
                }
            }

            if (content != null)
            {
                var resolvedId = ResolveId();
                var errorId = resolvedId + "-error";
                DescribedBy = AppendToDescribedBy(DescribedBy, errorId);

                attributes ??= new Dictionary<string, string>();
                attributes["id"] = errorId;

                return Generator.GenerateErrorMessage(visuallyHiddenText, content, attributes);
            }
            else
            {
                return null;
            }
        }

        private protected IHtmlContent? GenerateHint(FormGroupContext2 builder)
        {
            if (builder.Hint != null)
            {
                var resolvedId = ResolveId();
                var hintId = resolvedId + "-hint";
                DescribedBy = AppendToDescribedBy(DescribedBy, hintId);
                return Generator.GenerateHint(hintId, builder.Hint.Value.Content, builder.Hint.Value.Attributes);
            }
            else
            {
                return null;
            }
        }

        private protected IHtmlContent GenerateLabel(FormGroupContext2 formGroupContext)
        {
            // We need some content for the label; if AspFor is null then label content must have been specified
            if (AspFor == null && (!formGroupContext.Label.HasValue || formGroupContext.Label.Value.Content == null))
            {
                throw new InvalidOperationException(
                    $"Label content must be specified when the '{AspForAttributeName}' attribute is not specified.");
            }

            var resolvedId = ResolveId();

            var isPageHeading = formGroupContext.Label?.IsPageHeading ?? false;
            var content = formGroupContext.Label?.Content;
            var attributes = formGroupContext.Label?.Attributes;

            var resolvedContent = content ??
                new HtmlString(ModelHelper.GetDisplayName(ViewContext, AspFor!.ModelExplorer, AspFor.Name));

            return Generator.GenerateLabel(resolvedId, isPageHeading, resolvedContent, attributes);
        }

        private protected abstract string ResolveId();
    }
}
