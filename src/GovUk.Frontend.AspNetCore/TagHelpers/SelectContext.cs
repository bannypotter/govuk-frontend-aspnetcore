#nullable enable
using System.Collections.Generic;
using GovUk.Frontend.AspNetCore.HtmlGeneration;
using Microsoft.AspNetCore.Html;

namespace GovUk.Frontend.AspNetCore.TagHelpers
{
    internal class SelectContext : FormGroupContext2
    {
        private readonly List<SelectItem> _items;

        public SelectContext()
        {
            _items = new List<SelectItem>();
        }

        public IReadOnlyCollection<SelectItem> Items => _items;

        protected override string ErrorMessageTagName => SelectTagHelper.ErrorMessageTagName;

        protected override string HintTagName => SelectTagHelper.HintTagName;

        protected override string LabelTagName => SelectTagHelper.LabelTagName;

        protected override string RootTagName => SelectTagHelper.TagName;

        public void AddItem(SelectItem item)
        {
            Guard.ArgumentNotNull(nameof(item), item);

            _items.Add(item);
        }

        public override void SetErrorMessage(
            string? visuallyHiddenText,
            IDictionary<string, string>? attributes,
            IHtmlContent? content)
        {
            if (_items.Count != 0)
            {
                throw ExceptionHelper.ChildElementMustBeSpecifiedBefore(ErrorMessageTagName, SelectItemTagHelper.TagName);
            }

            base.SetErrorMessage(visuallyHiddenText, attributes, content);
        }

        public override void SetHint(IDictionary<string, string>? attributes, IHtmlContent? content)
        {
            if (_items.Count != 0)
            {
                throw ExceptionHelper.ChildElementMustBeSpecifiedBefore(HintTagName, SelectItemTagHelper.TagName);
            }

            base.SetHint(attributes, content);
        }

        public override void SetLabel(bool isPageHeading, IDictionary<string, string>? attributes, IHtmlContent? content)
        {
            if (_items.Count != 0)
            {
                throw ExceptionHelper.ChildElementMustBeSpecifiedBefore(LabelTagName, SelectItemTagHelper.TagName);
            }

            base.SetLabel(isPageHeading, attributes, content);
        }
    }
}
