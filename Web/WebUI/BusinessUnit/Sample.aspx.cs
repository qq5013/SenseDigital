using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_BusinessUnit_Sample : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //根目錄
        string strRoot = ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js");
        //InitLoading();
    }
    protected void btnMultiSearch_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    public void InitLoading()
    {
        HttpContext hc = HttpContext.Current;
        //创建一个页面居中的div
        hc.Response.Write("<style>");
        hc.Response.Write("#loader_container {text-align:center; position:absolute; top:40%; width:100%; left: 0;}");
        hc.Response.Write("#loader {font-family:Tahoma, Helvetica, sans; font-size:11.5px; color:#000000; background-color:#FFFFFF; padding:10px 0 16px 0; margin:0 auto; display:block; width:320px; border:1px solid #5a667b; text-align:left; z-index:2;}");
        hc.Response.Write("#progress {height:8px; font-size:1px; width:34px; position:relative; top:1px; left:0px; background-color:#8894a8;}");
        hc.Response.Write("#loader_bg {background-color:#e4e7eb; position:relative; top:8px; left:22px; height:10px; width:270px; font-size:1px;}");
        hc.Response.Write("</style>");
        hc.Response.Write("<div id=loader_container>");
        hc.Response.Write("<div id=loader>");
        hc.Response.Write("<div align=center style='font-size:20px'>页面正在加载中 ...</div>");
        hc.Response.Write("<div id=loader_bg><marquee direction='right' scrollamount='10'><div id=progress> </div></marquee></div>");
        hc.Response.Write("</div></div>");
        //hc.Response.Response.Write("<script>mydiv.innerText = '';</script>");
        hc.Response.Write("<script type=text/javascript>");
        //最重要是这句了,重写文档的onreadystatechange事件,判断文档是否加载完毕
        hc.Response.Write("function document.onreadystatechange()");
        hc.Response.Write(@"{ try  
                                   {
                                    if (document.readyState == 'complete') 
                                    {
                                         delNode('loader_container');
                                        
                                    }
                                   }
                                 catch(e)
                                    {
                                        alert('页面加载失败');
                                    }
                                                        } 

                            function delNode(nodeId)
                            {   
                                try
                                {   
                                      var div =document.getElementById(nodeId); 
                                      if(div !==null)
                                      {
                                          div.parentNode.removeChild(div);   
                                          div=null;    
                                          CollectGarbage(); 
                                      } 
                                }
                                catch(e)
                                {   
                                   alert('删除ID为'+nodeId+'的节点出现异常');
                                }   
                            }

                            ");

        hc.Response.Write("</script>");
        hc.Response.Flush();
    }
}