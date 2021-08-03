using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Xsl;
using System.Web;
using System.Web.Mvc;
using System.Xml.XPath;
using System.Text;

namespace TinkuLCS.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
            
        }


        [HttpPost]
        [Obsolete]
        public ActionResult UploadFile(HttpPostedFileBase file, HttpPostedFileBase xslfile)
        {

            try
            {

                if (file.ContentLength > 0 & xslfile.ContentLength > 0)
                {

                    var supportedTypes = new[] { "txt", "xml", "xsl", "doc", "docx", "pdf", "xls", "xlsx" };
                    var fileExtxml = Path.GetExtension(file.FileName).Substring(1);
                    var fileExtxsl = Path.GetExtension(file.FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExtxml) || !supportedTypes.Contains(fileExtxsl))
                    {
                        ViewBag.message = "File Extension Is InValid - Only Upload WORD/PDF/EXCEL/TXT File";
                        return ViewBag.message;
                    }
                    else
                    {
                        string _filename = Path.GetFileName(file.FileName);
                        string _fileextn = Path.GetExtension(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _filename);
                        file.SaveAs(_path);
                        string _xslfilefilename = Path.GetFileName(xslfile.FileName);
                        string _xslpath = Path.Combine(Server.MapPath("~/UploadedFiles"), _xslfilefilename);
                        file.SaveAs(_xslpath);

                        string _pathout = Path.Combine(Server.MapPath("~/UploadedFiles"), "output.xml");
                        XslTransform xslt1 = new XslTransform();
                        xslt1.Load(_xslpath);
                        XPathDocument mydata = new XPathDocument(_path);

                        XmlWriter writer = new XmlTextWriter(Console.Out);

                        xslt1.Transform(mydata, null, writer, null);


//                        XslCompiledTransform xslt = new XslCompiledTransform();

  //                      xslt.Load(_xslpath);

    //                    xslt.Transform(_path, "Output.xml");
                    }
                }

                ViewBag.message = "File Uploaded Successfully!!";
                return View();
            }

            catch(Exception e)
            {
                { }
                ViewBag.message = e.Message;

                return View();

            }
        
        }

        [HttpGet]
        public ActionResult Xmlwelform()
        {
            return View();
        
        }




        [HttpPost]
        public ActionResult Xmlwelform(HttpPostedFileBase xmlfile)
        {


            XmlDocument doc = new XmlDocument();


          string  _xfilename = Path.GetFileName(xmlfile.FileName);
          string _xmlpath = Path.Combine(Server.MapPath("~/UploadedFiles"), _xfilename);

            xmlfile.SaveAs(_xmlpath);

            try
            {

                StreamReader sreader = new StreamReader(_xmlpath, Encoding.UTF8);

                    string content = sreader.ReadToEnd();

                doc.LoadXml(content);

                ViewBag.Message = "Your xml file is wellform!!!";

                return View();

            }

            catch(Exception e)
            {
                ViewBag.Message = e.Message;

                return View();
            }


/*
            using (StreamReader streamReader = new StreamReader(path_name, Encoding.UTF8))
            {
                contents = streamReader.ReadToEnd();
            }
            doc.LoadXml(contents);



            string xmlStr = "<ROOT><Employee Id='100'/><Employee Id='134'/><Employee Id='178'/></ROOT>";
*/



        }


    }
}