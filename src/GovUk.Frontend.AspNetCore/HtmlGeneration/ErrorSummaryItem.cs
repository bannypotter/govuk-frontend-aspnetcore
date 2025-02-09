#nullable enable
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GovUk.Frontend.AspNetCore.HtmlGeneration
{
    public class ErrorSummaryItem
    {
        public IHtmlContent? Content { get; set; }
        public AttributeDictionary? Attributes { get; set; }
        public string? Href { get; set; }
        public AttributeDictionary? LinkAttributes { get; set; }
    }
}
