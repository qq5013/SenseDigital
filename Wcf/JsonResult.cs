using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf
{
    [DataContract]
 
    public class JsonResult
    {
        /// <summary>
       /// Construct
       /// </summary>
       public JsonResult(string name, string address, string phone)
       {
           _name = string.Format("Name:{0}", name);
          _address = string.Format("Address:{0}", address);
          _phoneNumber = string.Format("PhoneNubmer:{0}", phone);
      }
  
      private string _name;
      /// <summary>
      /// Name
      /// </summary>
      [DataMember]
      public string Name
      {
          get { return _name; }
          set { _name = value; }
      }
      private string _address;
      /// <summary>
      /// Address
      /// </summary>
      [DataMember]
      public string Address
      {
          get { return _address; }
          set { _address = value; }
      }
      private string _phoneNumber;
      /// <summary>
      /// PhoneNumber
      /// </summary>
      [DataMember]
      public string PhoneNumber
      {
          get { return _phoneNumber; }
          set { _phoneNumber = value; }
      }
    }
}
