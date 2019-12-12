using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
using WebApplication4.Security;

namespace WebApplication4.Controllers
{
    public class QRCoderController : Controller
    {
        public IActionResult Index()
        {
            var userId = 135;
            var pass = "123@abc";
            var encrypt = StringCipher.Encrypt(userId.ToString(), pass);
            var url = $"http://localhost:50001/QRCoder/OTP?md5={encrypt}";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return View(BitmapToBytes(qrCodeImage));
        }

        public IActionResult OTP(string md5)
        {
            var pass = "123@abc";
            var decrypt = StringCipher.Decrypt(md5, pass);

            int.TryParse(decrypt, out int userId);
            ViewBag.OTP = TCISOTP.OTP(userId, OTPMinuteTypes.EXTRA_SMALL);
            return View();
        }
        //[HttpPost]
        //public IActionResult Index(string qrText)
        //{
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
        //    QRCode qrCode = new QRCode(qrCodeData);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(20);

        //    return View(BitmapToBytes(qrCodeImage));
        //}

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        //public IActionResult GenerateFile()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult GenerateFile(string qrText)
        //{
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
        //    string fileGuid = Guid.NewGuid().ToString().Substring(0, 4);
        //    qrCodeData.SaveRawData("wwwroot/qrr/file-" + fileGuid + ".qrr", QRCodeData.Compression.Uncompressed);

        //    QRCodeData qrCodeData1 = new QRCodeData("wwwroot/qrr/file-" + fileGuid + ".qrr", QRCodeData.Compression.Uncompressed);
        //    QRCode qrCode = new QRCode(qrCodeData1);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(20);
        //    return View(BitmapToBytes(qrCodeImage));
        //}

        //public IActionResult ViewFile()
        //{
        //    List<KeyValuePair<string, Byte[]>> fileData = new List<KeyValuePair<string, byte[]>>();
        //    KeyValuePair<string, Byte[]> data;

        //    string[] files = Directory.GetFiles("wwwroot/qrr");
        //    foreach (string file in files)
        //    {
        //        QRCodeData qrCodeData = new QRCodeData(file, QRCodeData.Compression.Uncompressed);
        //        QRCode qrCode = new QRCode(qrCodeData);
        //        Bitmap qrCodeImage = qrCode.GetGraphic(20);

        //        Byte[] byteData = BitmapToBytes(qrCodeImage);
        //        data = new KeyValuePair<string, Byte[]>(Path.GetFileName(file), byteData);
        //        fileData.Add(data);
        //    }

        //    return View(fileData);
        //}
    }




}