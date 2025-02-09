using GovUk.Frontend.AspNetCore.TestCommon;
using Xunit;

namespace GovUk.Frontend.AspNetCore.ConformanceTests
{
    public partial class ComponentTests
    {
        [Theory]
        [ComponentFixtureData("warning-text", typeof(OptionsJson.WarningText))]
        public void WarningText(ComponentTestCaseData<OptionsJson.WarningText> data) =>
            CheckComponentHtmlMatchesExpectedHtml(
                data,
                (generator, options) =>
                {
                    var iconFallbackText = options.IconFallbackText;
                    var content = TextOrHtmlHelper.GetHtmlContent(options.Text, options.Html) ?? _emptyContent;

                    var attributes = options.Attributes.ToAttributesDictionary()
                        .MergeAttribute("class", options.Classes);

                    return generator.GenerateWarningText(iconFallbackText, content, attributes)
                        .RenderToString();
                });
    }
}
