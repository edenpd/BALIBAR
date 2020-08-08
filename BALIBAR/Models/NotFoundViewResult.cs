using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

namespace BALIBAR.Models
{
    public class NotFoundViewResult : ViewResult
    {
        public NotFoundViewResult(string viewName, string message)
        {
            this.ViewName = viewName;
            this.StatusCode = (int)HttpStatusCode.NotFound;
            this.ViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new ModelStateDictionary());
            this.ViewData["Message"] = message;
        }
    }
}
