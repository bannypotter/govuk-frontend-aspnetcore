#nullable enable
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUk.Frontend.AspNetCore.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;a&gt; elements.
    /// </summary>
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-action")]
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-controller")]
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-area")]
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-page")]
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-page-handler")]
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-fragment")]
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-host")]
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-protocol")]
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-route")]
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-all-route-data")]
    [HtmlTargetElement(BackLinkTagHelper.TagName, Attributes = "asp-route-*")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-action")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-controller")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-area")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-page")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-page-handler")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-fragment")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-host")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-protocol")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-route")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-all-route-data")]
    [HtmlTargetElement(BreadcrumbsItemTagHelper.TagName, Attributes = "asp-route-*")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-action")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-controller")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-area")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-page")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-page-handler")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-fragment")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-host")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-protocol")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-route")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-all-route-data")]
    [HtmlTargetElement(ButtonLinkTagHelper.TagName, Attributes = "asp-route-*")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-action")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-controller")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-area")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-page")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-page-handler")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-fragment")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-host")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-protocol")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-route")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-all-route-data")]
    [HtmlTargetElement(ErrorSummaryItemTagHelper.TagName, Attributes = "asp-route-*")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-action")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-controller")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-area")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-page")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-page-handler")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-fragment")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-host")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-protocol")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-route")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-all-route-data")]
    [HtmlTargetElement(SummaryListRowActionTagHelper.TagName, Attributes = "asp-route-*")]
    public class AnchorTagHelper : Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper
    {
        /// <inheritdoc/>
        public AnchorTagHelper(IHtmlGenerator generator)
            : base(generator)
        {
        }
    }
}
