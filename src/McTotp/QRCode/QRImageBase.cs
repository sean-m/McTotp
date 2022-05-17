using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using ZXing;
using ZXing.Common;
using ZXing.ImageSharp;
using ZXing.Multi.QrCode;

namespace McTotp.QRCode {
    public abstract class QRImageBase {

        internal byte[] bytes = default(byte[]);

        internal Image<Rgba32> image;

        public QRImageBase(byte[] source) {
            bytes = source;

            if (source == null) throw new ArgumentNullException("Argument source cannot be null. It must contain the bytes for the image to decode.");
            if (source.Length == 0) throw new ArgumentException("Argument source cannot be empty. It must contain the bytes for the image to decode.");

            var encoder = new PngEncoder();
            encoder.ColorType = PngColorType.RgbWithAlpha;

            var tmp = Image.Load(bytes);
            image = tmp.CloneAs<Rgba32>();
        }

        internal Result[] GetDecodeResults() {
            using (var ms = new MemoryStream(bytes)) {
                if (null == ms) return null;

                var reader = new QRCodeMultiReader();
                var ls = new ImageSharpLuminanceSource<Rgba32>(image);
                var bzer = new HybridBinarizer(ls);
                var bm = new BinaryBitmap(bzer);
                return reader.decodeMultiple(bm);
            }
        }
    }
}
