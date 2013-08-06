using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Js.BLL;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AppService" in code, svc and config file together.
    public class AppService : IAppService
    {
        string cnKey = "Label";
        public string GetLabelNoInfo(string labelNo)
        {
            Js.BLL.Label.LabelDal dal = new Js.BLL.Label.LabelDal(cnKey);
            DataTable dt = dal.GetLabelNoInfo(labelNo);
            string labelNoInfo = Js.Com.JsonHelper.Dtb2Json(dt);

            return labelNoInfo;
        }
    }
}
