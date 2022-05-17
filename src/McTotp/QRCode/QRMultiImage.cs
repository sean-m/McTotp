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
