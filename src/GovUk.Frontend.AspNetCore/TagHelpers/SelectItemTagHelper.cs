#nullable enable
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GovUk.Frontend.AspNetCore.HtmlGeneration;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUk.Frontend.AspNetCore.TagHelpers
{
    /// <summary>
    /// Represents an item in a GDS select component.
    /// </summary>
    [HtmlTargetElement(TagName, ParentTag = SelectTagHelper.TagName)]
    public class SelectItemTagHelper : TagHelper
    {
        internal const string TagName = "govuk-select-item";

        private const string DisabledAttributeName = "disabled";
        private const string SelectedAttributeName = "selected";
        private const string ValueAttributeName = "value";

        private string _value = ComponentGenerator.SelectItemDefaultValue;

        /// <summary>
        /// The <c>disabled</c> attribute for the item.
        /// </summary>
        [HtmlAttributeName(DisabledAttributeName)]
        public bool Disabled { get; set; } = ComponentGenerator.SelectItemDefaultDisabled;

        /// <summary>
        /// The <c>selected</c> attribute for the item.
        /// </summary>
        [HtmlAttributeName(SelectedAttributeName)]
        public bool Selected { get; set; } = ComponentGenerator.SelectItemDefaultSelected;

        /// <summary>
        /// The <c>value</c> attribute for the item.
        /// </summary>
        [HtmlAttributeName(ValueAttributeName)]
        [DisallowNull]
        public string Value
        {
            get => _value;
            set => _value = Guard.ArgumentNotNull(nameof(value), value);
        }

        /// <inheritdoc/>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var selectContext = context.GetContextItem<SelectContext>();

            var content = await output.GetChildContentAsync();

            selectContext.AddItem(new SelectItem()
            {
                Attributes = output.Attributes.ToAttributesDictionary(),
                Content = content.Snapshot(),
                Disabled = Disabled,
                Selected = Selected,
                Value = Value
            });

            output.SuppressOutput();
        }
    }
}
