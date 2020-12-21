#nullable enable
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;

namespace GovUk.Frontend.AspNetCore.TagHelpers
{
    internal class FormGroupFieldsetContext2
    {
        private readonly string _rootTagName;
        private readonly string _fieldsetTagName;

        public FormGroupFieldsetContext2(
            string rootTagName,
            string fieldsetTagName,
            IDictionary<string, string> attributes,
            string? describedBy)
        {
            _rootTagName = Guard.ArgumentNotNull(nameof(rootTagName), rootTagName);
            _fieldsetTagName = Guard.ArgumentNotNull(nameof(fieldsetTagName), fieldsetTagName);
            Attributes = Guard.ArgumentNotNull(nameof(attributes), attributes);
            DescribedBy = describedBy;
        }

        public IDictionary<string, string> Attributes { get; set; }

        public string? DescribedBy { get; }

        public (bool IsPageHeading, IDictionary<string, string> Attributes, IHtmlContent Content)? Legend { get; private set; }

        public void SetLegend(bool isPageHeading, IDictionary<string, string> attributes, IHtmlContent content)
        {
            Guard.ArgumentNotNull(nameof(content), content);

            if (Legend != null)
            {
                throw ExceptionHelper.OnlyOneElementIsPermittedIn(_fieldsetTagName, _rootTagName);
            }

            Legend = (isPageHeading, attributes, content);
        }
    }
}
