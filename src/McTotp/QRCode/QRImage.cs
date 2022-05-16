using McTotp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.ImageSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using ZXing.Common;
using SixLabors.ImageSharp.PixelFormats;
using ZXing.Multi.QrCode;
using SixLabors.ImageSharp.Formats.Png;

namespace McTotp.QRCode
{
    public class QRImage : QRImageBase, QRContent
    {

        public string content { get; private set; } = String.Empty;

        public IEnumerable<string> allContent => new List<string> { content };

        public QRImage(byte[] source)
            : base(source) {
            Decode();
        }

        private void Decode() {

            var result = GetDecodeResults();
            if (null == result) throw new Exception("No QR code found in image.");
            if (result.Length > 1) throw new Exception("Multiple QRCodes detected. Use multi code reader instead.");

            if (!String.IsNullOrEmpty(result.First()?.Text)) {
                content = result.First()?.Text;
            }
            
        }
    }
}
