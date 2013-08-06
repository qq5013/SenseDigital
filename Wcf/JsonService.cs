using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wcf
{
    public class JsonService : IJsonService
    {
        public JsonResult GetJsonResult(string name, string address, string phone)
        {
            JsonResult result = new JsonResult(name, address, phone);
            return result;
        }
    }
}
