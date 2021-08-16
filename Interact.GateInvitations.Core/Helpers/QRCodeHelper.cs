using Interact.GateInvitations.Core.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace Interact.GateInvitations.Core.Helpers
{
    public class QRCodeHelper
    {
        private static readonly Random _random = new Random();
        // Generates a random string with a given size.    
        private static string GenerateRandomCodeString(int size, bool lowerCase = false)
        {

            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public static (string qrCode,string qrCodeImgUrl) GenerateQRCode(object id,string inviteName)
        {
            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            var qcode = GenerateRandomCodeString(20);
             var result = QCwriter.Write(qcode);
            var name = inviteName.Length > 10
                ? inviteName.Substring(0, 10)
                : inviteName;
            string fileName = $"{id}.{name}.jpg";
            string path =Utility.MapQRCodePath(fileName);
            var barcodeBitmap = new Bitmap(result);

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(path,
                   FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return (qcode,fileName);
        }
        public static string ReadQRCode(string fileName)
        {
            var QCreader = new BarcodeReader();
            var QCresult = QCreader.Decode(new Bitmap(Utility.MapQRCodePath(fileName)));
            return QCresult is null ? null : QCresult.Text;
        }
    }
}
