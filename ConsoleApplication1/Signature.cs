using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using System;
using System.IO;
using Org.BouncyCastle.Pkcs;

namespace ConsoleApplication1
{
    public class Signature
    {
        public static byte[] SignPdfFile(MemoryStream sourceDocument, Stream privateKeyStream, string keyPassword, string reason, string location)
        {
            var pk12 = new Pkcs12Store(privateKeyStream, keyPassword.ToCharArray());
            privateKeyStream.Dispose();

            string alias = pk12.Aliases.Cast<string>().FirstOrDefault(pk12.IsKeyEntry);
            var pk = pk12.GetKey(alias).Key;

            Byte[] result;

            var reader = new PdfReader(sourceDocument);
            using (var fout = new MemoryStream())
            {
                using (var stamper = PdfStamper.CreateSignature(reader, fout, '\0'))
                {
                    var systemPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\times.ttf";
                    var sylfaen = BaseFont.CreateFont(systemPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    var head = new Font(sylfaen, 7f, Font.NORMAL, BaseColor.BLACK);

                    var appearance = stamper.SignatureAppearance;
                    appearance.Reason = reason;
                    appearance.Location = location;
                    appearance.Layer2Font = head;
                    appearance.SetVisibleSignature(new Rectangle(20, 10, 270, 60), reader.NumberOfPages, "Icsi-Vendor");
                    var es = new PrivateKeySignature(pk, "SHA-512");
                    MakeSignature.SignDetached(appearance, es, new[] { pk12.GetCertificate(alias).Certificate },
                        null, null, null, 0, CryptoStandard.CMS);

                    result = fout.ToArray();
                    stamper.Close();
                }
            }
            return result;
        }
    }
}
