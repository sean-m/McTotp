using McTotp.Interface;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.ImageSharp;
using ZXing.Multi.QrCode;

namespace McTotp.QRCode {
    public class QRMultiImage : QRImageBase 
    {
        public string content => allContent.FirstOrDefault() ?? String.Empty;

        public IEnumerable<string> allContent { get; private set; } = new List<string>();


        public QRMultiImage(byte[] source) 
            : base(source) {
            Decode();
        }

        private void Decode() {

            var result = GetDecodeResults();

            if (null == result) throw new Exception("No QR code found in image.");
            if (allContent is List<string> ac) {
                foreach (var r in result) {
                    if (null != r && !String.IsNullOrEmpty(r?.Text)) {
                        ac.Add(r?.Text);
                    }
                }
            }
        }
    }
}
