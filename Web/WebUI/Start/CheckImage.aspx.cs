using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Js.Com;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


public partial class Login_CheckImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RandImage randImage = new RandImage();
        System.Drawing.Image image = randImage.GetImage();
        System.IO.MemoryStream memoryStream = new MemoryStream();
        image.Save(memoryStream, ImageFormat.Jpeg);
        Response.ClearContent();
        Response.ContentType = "image/gif";
        Response.BinaryWrite(memoryStream.ToArray());
        image.Dispose();
        Response.End();       
    }
}