using UC.Core.Models.FormData;

namespace backend.Helper.Untils
{
    public static class SearchQueryStringUntil
    {
        public static OSearch ConvertQueryStringToOSearch(HttpContext? context)
        {
            var search = new OSearch
            {
                fields = []
            };
            if (context is not null)
            {
                search.fields.AddRange(context.Request.Query.Select(x => new Field() { code = x.Key, value = x.Value }));
            }
            return search;
        }
    }
}
