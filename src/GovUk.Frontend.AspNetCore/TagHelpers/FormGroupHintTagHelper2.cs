#nullable enable
using System.Threading.Tasks;
using GovUk.Frontend.AspNetCore.HtmlGeneration;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUk.Frontend.AspNetCore.TagHelpers
{
    /// <summary>
    /// Represents the hint in a GDS form group component.
    /// </summary>
    [OutputElementHint(ComponentGenerator.HintElement)]
    public class FormGroupHintTagHelper2 : TagHelper
    {
        /// <summary>
        /// Creates a <see cref="FormGroupHintTagHelper2"/>.
        /// </summary>
        public FormGroupHintTagHelper2()
        {
        }

        /// <inheritdoc/>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = output.TagMode == TagMode.StartTagAndEndTag ?
                await output.GetChildContentAsync() :
                null;

            var formGroupContext = context.GetContextItem<FormGroupContext2>();

            formGroupContext.SetHint(output.Attributes.ToAttributesDictionary(), childContent?.Snapshot());

            output.SuppressOutput();
        }
    }
}
