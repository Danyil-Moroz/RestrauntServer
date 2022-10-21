namespace RestrauntServer.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RestrauntServer.Models;

    public static class JsonHelper
    {
        const string mediaType= "application/json";
        private static ContentResult ContentResult;

        static JsonHelper()
        {
            ContentResult = new ContentResult();
            ContentResult.ContentType = mediaType;
        }
        public static ContentResult ConvertToJsonString (IEnumerable<BaseModel> inputData)
        {          
            if (inputData == null || inputData.Count() == 0)
            {
                ContentResult.Content = string.Empty;
            }
            else
            {
                var jObject = JsonConvert.SerializeObject(inputData);
                ContentResult.Content = jObject.ToString();
            }
            return ContentResult;
        }

        public static ContentResult ConvertToJsonString(BaseModel inputData)
        {
            if (inputData == null)
            {
                ContentResult.Content = string.Empty;
            }
            else
            {
                var jObject = JsonConvert.SerializeObject(inputData);
                ContentResult.Content = jObject.ToString();
            }
          
            return ContentResult;
        }
    }
}
