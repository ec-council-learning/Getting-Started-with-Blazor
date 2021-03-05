using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFundamentals.Pages
{
    public class QuerystringModel : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected string QueryStringValue { get; set; }

        protected override void OnInitialized()
        {
            var queryString = new Uri(NavigationManager.Uri).Query;

            if (QueryHelpers.ParseQuery(queryString)
                .TryGetValue("NavigationFrom", out var value))
            {
                QueryStringValue = value;
            }
        }
    }
}
