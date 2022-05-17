using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using McTotp.QRCode;
using System.Linq;
using System;

namespace McTotp.Test
{
    public class QRImageTests {
        public class QuoteQRTest {
            public int id { get; set; }
            public string type { get; set; }
            public string image { get; set; }
            public string text { get; set; }
        }

        IList<QuoteQRTest>? quotes;

        [SetUp]
        public void Setup() {
            string quoteData = System.Text.Encoding.UTF8.GetString(Properties.Resources.qrquotes);
            quotes = JsonConvert.DeserializeObject<List<QuoteQRTest>>(quoteData);

        }

        [Test]
        public void LoadingTestDataFromResourcesWorks() {
            Assert.NotNull(quotes);
            Assert.AreEqual(quotes?.Count, 4);
        }
        
        [Test]
        public void LoadingAndDecodingImageFromByteArray() {
            foreach (var q in quotes.Where(x => x.type == "single")) {
                var bytes = (byte[]?)Properties.Resources.ResourceManager.GetObject(q.image);
                var image = new QRImage(bytes);

                Assert.IsTrue(string.Equals(q.text, image.content, StringComparison.CurrentCulture));
                Assert.IsTrue(string.Equals(q.text, image.allContent.First(), StringComparison.CurrentCulture));
            }
        }


        [Test]
        public void ReadingFromNullOrEmptyByteArrayThrows() {
            Assert.Throws<ArgumentNullException>(() => { new QRImage(null); });
            Assert.Throws<ArgumentException>(() => { new QRImage(new byte[] { }); });
        }

        [Test]
        public void MultipleCodesFoundException() {

            foreach (var q in quotes.Where(x => x.type == "multiple")) {
                var bytes = (byte[]?)Properties.Resources.ResourceManager.GetObject(q.image);

                Assert.Throws<Exception>(() => { new QRImage(bytes); });
            }
        }

        [Test] 
        public void DetectMultipleCodes() {
            foreach (var q in quotes.Where(x => x.type == "multiple")) {
                var bytes = (byte[]?)Properties.Resources.ResourceManager.GetObject(q.image);
                var multi = new QRMultiImage(bytes);
                Assert.Greater(multi.allContent.Count(), 1);
                Assert.IsFalse(string.IsNullOrEmpty(multi.content));
            }
        }

        [Test]
        public void NotAQRCodeImage() {
            var bytes = Properties.Resources.pd_cat;

            Assert.Throws<Exception>(() => { new QRImage(bytes); });
        }

        [Test]
        public void EmptyQRCodeDoesNotThrow() {
            var bytes = Properties.Resources.empty;
            Assert.DoesNotThrow(() => { new QRImage(bytes); });
        }

        [Test]
        public void EmptyQRCodeEmptyContent() {
            var bytes = Properties.Resources.empty;
            QRImage image = new QRImage(bytes);
            Assert.IsNotNull(image.content);
            Assert.IsTrue(string.IsNullOrEmpty(image.content));
        }
    }
}