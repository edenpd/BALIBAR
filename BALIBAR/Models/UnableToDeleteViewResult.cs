using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

namespace BALIBAR.Models
{
    public class UnableToDeleteViewResult : ViewResult
    {
        public UnableToDeleteViewResult(string viewName, string message)
        {
            this.ViewName = viewName;
            this.StatusCode = (int)HttpStatusCode.InternalServerError;
            this.ViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new ModelStateDictionary());
            ViewData["Message"] = message;
        }
    }
}
