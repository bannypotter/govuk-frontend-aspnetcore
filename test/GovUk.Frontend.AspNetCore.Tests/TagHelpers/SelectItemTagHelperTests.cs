using System.Collections.Generic;
using System.Threading.Tasks;
using GovUk.Frontend.AspNetCore.TagHelpers;
using GovUk.Frontend.AspNetCore.TestCommon;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace GovUk.Frontend.AspNetCore.Tests.TagHelpers
{
    public class SelectItemTagHelperTests
    {
        [Fact]
        public async Task ProcessAsync_AddItemsToContext()
        {
            // Arrange
            var selectContext = new SelectContext();

            var context = new TagHelperContext(
                tagName: "govuk-select-item",
                allAttributes: new TagHelperAttributeList(),
                items: new Dictionary<object, object>()
                {
                    { typeof(SelectContext), selectContext }
                },
                uniqueId: "test");

            var output = new TagHelperOutput(
                "govuk-select-item",
                attributes: new TagHelperAttributeList(),
                getChildContentAsync: (useCachedResult, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.AppendHtml(new HtmlString("Item text"));
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });

            var tagHelper = new SelectItemTagHelper()
            {
                Disabled = true,
                Selected = true,
                Value = "value"
            };

            // Act
            await tagHelper.ProcessAsync(context, output);

            // Assert
            Assert.Collection(
                selectContext.Items,
                item =>
                {
                    Assert.Equal("Item text", item.Content.RenderToString());
                    Assert.True(item.Disabled);
                    Assert.True(item.Selected);
                    Assert.Equal("value", item.Value);
                });
        }
    }
}
