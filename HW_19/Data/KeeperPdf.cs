using DinkToPdf;
using HW_19.Models;
using HW_19.Services;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HW_19.Data
{
    internal class KeeperPdf : IAnimalsSave
    {
        public void SaveAnimals(IEnumerable<IAnimals> animals)
        {
            if (File.Exists("File.pdf"))
                File.Delete("File.pdf");

            var sb = new StringBuilder();

            #region Transport
            sb.AppendFormat(@"<table>");
            foreach (var animal in animals)
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>;</td>
                                    <td>{1}</td>
                                    <td>;</td>
                                    <td>{2}</td>
                                </tr>", animal.Id, animal.Name, animal.Type);
            sb.AppendFormat(@"</table>");
            #endregion

            var globalSetings = new GlobalSettings { Out = @"File.pdf" };

            var objectSettings = new ObjectSettings
            {
                HtmlContent = sb.ToString(),
                WebSettings = { DefaultEncoding = "utf-8" },
            };

            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = globalSetings,
                Objects = { objectSettings }
            };


            var converter = new BasicConverter(new PdfTools());

            converter.Convert(pdf);
        }
    }
}
