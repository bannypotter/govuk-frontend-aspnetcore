#nullable enable
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GovUk.Frontend.AspNetCore.HtmlGeneration;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUk.Frontend.AspNetCore.TagHelpers
{
    /// <summary>
    /// Generates a GDS input component.
    /// </summary>
    [HtmlTargetElement(TagName)]
    [RestrictChildren(LabelTagName, HintTagName, ErrorMessageTagName, InputPrefixTagHelper.TagName, InputSuffixTagHelper.TagName)]
    [OutputElementHint(ComponentGenerator.FormGroupElement)]
    public class InputTagHelper : FormGroupTagHelperBase2
    {
        internal const string ErrorMessageTagName = "govuk-input-error-message";
        internal const string HintTagName = "govuk-input-hint";
        internal const string LabelTagName = "govuk-input-label";
        internal const string TagName = "govuk-input";

        private const string AttributesPrefix = "input-";
        private const string AutocompleteAttributeName = "autocomplete";
        private const string DescribedByAttributeName = "described-by";
        private const string DisabledAttributeName = "disabled";
        private const string IdAttributeName = "id";
        private const string InputModeAttributeName = "inputmode";
        private const string NameAttributeName = "name";
        private const string PatternAttributeName = "pattern";
        private const string SpellcheckAttributeName = "spellcheck";
        private const string TypeAttributeName = "type";
        private const string ValueAttributeName = "value";

        /// <summary>
        /// Creates a <see cref="InputTagHelper"/>.
        /// </summary>
        public InputTagHelper()
            : this(htmlGenerator: null, modelHelper: null)
        {
        }

        internal InputTagHelper(IGovUkHtmlGenerator? htmlGenerator = null, IModelHelper? modelHelper = null)
            : base(
                  htmlGenerator ?? new ComponentGenerator(),
                  modelHelper ?? new DefaultModelHelper())
        {
        }

        [HtmlAttributeName(AutocompleteAttributeName)]
        public string? Autocomplete { get; set; }

        [HtmlAttributeName(DescribedByAttributeName)]
        public new string? DescribedBy
        {
            get => base.DescribedBy;
            set => base.DescribedBy = value;
        }

        [HtmlAttributeName(DisabledAttributeName)]
        public bool Disabled { get; set; } = ComponentGenerator.InputDefaultDisabled;

        [HtmlAttributeName(IdAttributeName)]
        public string? Id { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = AttributesPrefix)]
        public IDictionary<string, string>? InputAttributes { get; set; } = new Dictionary<string, string>();

        [HtmlAttributeName(NameAttributeName)]
        public string? Name { get; set; }

        [HtmlAttributeName(InputModeAttributeName)]
        public string? InputMode { get; set; }

        [HtmlAttributeName(PatternAttributeName)]
        public string? Pattern { get; set; }

        [HtmlAttributeName(SpellcheckAttributeName)]
        public bool? Spellcheck { get; set; }

        [HtmlAttributeName(TypeAttributeName)]
        [DisallowNull]
        public string? Type { get; set; } = ComponentGenerator.InputDefaultType;

        [HtmlAttributeName(ValueAttributeName)]
        public string? Value { get; set; }

        private protected override FormGroupContext2 CreateFormGroupContext() => new InputContext();

        private protected override IHtmlContent GenerateFormGroupContent(
            TagHelperContext context,
            FormGroupContext2 formGroupContext,
            out bool haveError)
        {
            var inputContext = context.GetContextItem<InputContext>();

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

            var inputTagBuilder = GenerateInput(haveError);
            contentBuilder.AppendHtml(inputTagBuilder);

            return contentBuilder;

            TagBuilder GenerateInput(bool haveError)
            {
                var resolvedId = ResolveId();
                var resolvedName = ResolveName();
                var resolvedType = Type ?? ComponentGenerator.InputDefaultType;

                var resolvedValue = Value ??
                    (AspFor != null ? ModelHelper.GetModelValue(ViewContext, AspFor.ModelExplorer, AspFor.Name) : null);

                return Generator.GenerateInput(haveError, resolvedId, resolvedName, resolvedType, resolvedValue, DescribedBy,
                    Autocomplete,
                    Pattern,
                    InputMode,
                    Spellcheck,
                    Disabled,
                    InputAttributes,
                    inputContext.Prefix?.Content,
                    inputContext.Prefix?.Attributes,
                    inputContext.Suffix?.Content,
                    inputContext.Suffix?.Attributes);
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
