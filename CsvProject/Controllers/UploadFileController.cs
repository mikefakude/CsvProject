using CsvProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CsvProject.Controllers
{
    public class UploadFileController : Controller
    {
        public ActionResult Index()
        {
            return View(new List<UploadFileModel>());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            var readFile = new List<UploadFileModel>();
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/UploadedFiles/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        for (int i = 0; i < row.Length; i++)
                        {
                            if (row.Length > 0)
                            {
                                readFile.Add(new UploadFileModel
                                {
                                    ReadValues = row[i] - 1
                                });
                            }
                            else
                            {
                                for (int j = 0; j < row.Length; j++)
                                {
                                    readFile.Add(new UploadFileModel
                                    {
                                        ReadValues = row[i]
                                    });
                                }
                            }
                        }
                    }
                }
            }
            return View(readFile);
        }
    }
}