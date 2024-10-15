using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml;
using WsCepdi;

namespace CepdiRetoApi.Controllers
{
    public class UploadController : Controller
    {


        private readonly ILogger<UploadController> _logger;

        public UploadController(ILogger<UploadController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FileReader()
        { return View(); }


        [HttpPost]
        public async Task<IActionResult> FileReader(IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // Upload File
            if (file != null && file.Length > 0 && file.FileName.Contains(".xml"))
            {
                var uploadDirectory = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads";

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var filePath = Path.Combine(uploadDirectory,"xmlSubido.xml");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                TempData["Success"] = "Archivo cargado exitosamente!";
            }
            else 
            {
                TempData["Error"] = "Archivo no valido.!";
                
            }

            return View();
        }

        public async Task<IActionResult> getPDF()
        {
            //Read Xml
            var uuid = "";
            var filePath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\xmlsubido.xml";
            XmlReader xmlReader = XmlReader.Create(filePath);
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "tfd:TimbreFiscalDigital"))
                {
                    if (xmlReader.HasAttributes)
                    {
                        uuid = xmlReader.GetAttribute("UUID");
                    }
                }
            }
            if (uuid != "" && uuid != null)
            {
                string usuario = "demo1@mail.com";
                string password = "Demo123#";
                WsCepdi.WSClient wSClient = new WSClient();

                var response = await wSClient.ObtenerPDFAsync(usuario, password, uuid.ToString());
                xmlReader.Close();
                xmlReader.Dispose();
                var contentType = "application/pdf;base64";
                return File(response.@return.PDF, contentType);
            }
            return View();
        }
    }

}
