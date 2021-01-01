#nullable enable
using System.Collections.Generic;
using GovUk.Frontend.AspNetCore.HtmlGeneration;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUk.Frontend.AspNetCore.TagHelpers
{
    /// <summary>
    /// Generates a GDS select component.
    /// </summary>
    [HtmlTargetElement(TagName)]
    [RestrictChildren(SelectItemTagHelper.TagName, LabelTagName, HintTagName, ErrorMessageTagName)]
    [OutputElementHint(ComponentGenerator.FormGroupElement)]
    public class SelectTagHelper : FormGroupTagHelperBase2
    {
        internal const string ErrorMessageTagName = "govuk-select-error-message";
        internal const string HintTagName = "govuk-select-hint";
        internal const string LabelTagName = "govuk-select-label";
        internal const string TagName = "govuk-select";

        private const string AttributesPrefix = "select-";
        private const string DescribedByAttributeName = "described-by";
        private const string DisabledAttributeName = "disabled";
        private const string IdAttributeName = "id";
        private const string NameAttributeName = "name";

        /// <summary>
        /// Creates a new <see cref="SelectTagHelper"/>.
        /// </summary>
        public SelectTagHelper()
            : this(htmlGenerator: null, modelHelper: null)
        {
        }

        internal SelectTagHelper(IGovUkHtmlGenerator? htmlGenerator = null, IModelHelper? modelHelper = null)
            : base(
                  htmlGenerator ?? new ComponentGenerator(),
                  modelHelper ?? new DefaultModelHelper())
        {
        }

        [HtmlAttributeName(DescribedByAttributeName)]
        public new string? DescribedBy
        {
            get => base.DescribedBy;
            set => base.DescribedBy = value;
        }

        [HtmlAttributeName(DisabledAttributeName)]
        public bool Disabled { get; set; } = ComponentGenerator.SelectDefaultDisabled;

        [HtmlAttributeName(IdAttributeName)]
        public string? Id { get; set; }

        [HtmlAttributeName(NameAttributeName)]
        public string? Name { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = AttributesPrefix)]
        public IDictionary<string, string>? SelectAttributes { get; set; } = new Dictionary<string, string>();

        private protected override FormGroupContext2 CreateFormGroupContext() => new SelectContext();

        private protected override IHtmlContent GenerateFormGroupContent(
            TagHelperContext context,
            FormGroupContext2 formGroupContext,
            out bool haveError)
        {
            var selectContext = context.GetContextItem<SelectContext>();

            var contentBuilder = new HtmlContentBuilder();

            var label = GenerateLabel(formGroupContext);
            contentBuilder.AppendHtml(label);

            var hint = GenerateHint(formGroupContext);
            if (hint != null)
            {
                contentBuilder.AppendHtml(hint);
            }

            var errorMessage = GenerateErrorMessage(formGroupContext);
            if (errorMessage != null)
            {
                contentBuilder.AppendHtml(errorMessage);
            }

            haveError = errorMessage != null;

            var selectTagBuilder = GenerateSelect(haveError);
            contentBuilder.AppendHtml(selectTagBuilder);

            return contentBuilder;

            TagBuilder GenerateSelect(bool haveError)
            {
                var resolvedId = ResolveId();
                var resolvedName = ResolveName();

                return Generator.GenerateSelect(
                    haveError,
                    resolvedId,
                    resolvedName,
                    DescribedBy,
                    Disabled,
                    selectContext.Items,
                    SelectAttributes);
            }
        }

        private protected override string ResolveId()
        {
            if (Id != null)
            {
                return Id;
            }

            if (Name == null && AspFor == null)
            {
                throw ExceptionHelper.AtLeastOneOfAttributesMustBeProvided(
                    IdAttributeName,
                    NameAttributeName,
                    AspForAttributeName);
            }

            var resolvedName = ResolveName();

            return TagBuilder.CreateSanitizedId(resolvedName, Constants.IdAttributeDotReplacement);
        }

        private string ResolveName()
        {
            if (Name == null && AspFor == null)
            {
                throw ExceptionHelper.AtLeastOneOfAttributesMustBeProvided(
                    NameAttributeName,
                    AspForAttributeName);
            }

            return Name ?? ModelHelper.GetFullHtmlFieldName(ViewContext, AspFor!.Name);
        }
    }
}
