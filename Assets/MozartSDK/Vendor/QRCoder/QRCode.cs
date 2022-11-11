using System;
using UnityEngine;
using static QRCoder.QRCodeGenerator;

namespace QRCoder
{
    public class QRCode : AbstractQRCode, IDisposable
    {
        /// <summary>
        /// Constructor without params to be used in COM Objects connections
        /// </summary>
        public QRCode() { }

        public QRCode(QRCodeData data) : base(data) { }

        public bool[,] GetArray(bool drawQuietZones = true)
        {
            int size = QrCodeData.ModuleMatrix.Count - 8;
            bool[,] result = new bool[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    result[x, y] = QrCodeData.ModuleMatrix[y][x];
                }
            }

            return result;
        }

        public Texture2D GetGraphic(int pixelsPerModule, Color darkColor, Color lightColor, bool drawQuietZones = true)
        {
            int size = (this.QrCodeData.ModuleMatrix.Count - (drawQuietZones ? 0 : 8)) * pixelsPerModule;
            int offset = drawQuietZones ? 0 : 4 * pixelsPerModule;

            Texture2D tex = new Texture2D(size, size);
            for (int x = 0; x < size + offset; x = x + pixelsPerModule)
            {
                for (int y = 0; y < size + offset; y = y + pixelsPerModule)
                {
                    bool module = QrCodeData.ModuleMatrix[(y + pixelsPerModule) / pixelsPerModule - 1][(x + pixelsPerModule) / pixelsPerModule - 1];

                    for (int ry = 0; ry < pixelsPerModule; ry++)
                    {
                        for (int rx = 0; rx < pixelsPerModule; rx++)
                        {
                            tex.SetPixel(x + rx, y + ry, module ? darkColor : lightColor);
                        }
                    }
                }
            }

            tex.Apply();

            return tex;
        }
    }

    public static class QRCodeHelper
    {
        public static Texture2D GetQRCode(string plainText, int pixelsPerModule, ECCLevel eccLevel = ECCLevel.Q, Color? darkColor = null, Color? lightColor = null, bool forceUtf8 = false, bool utf8BOM = false, EciMode eciMode = EciMode.Default, int requestedVersion = -1, bool drawQuietZones = true)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(plainText, eccLevel, forceUtf8, utf8BOM, eciMode, requestedVersion))
            using (QRCode qrCode = new QRCode(qrCodeData))
                return qrCode.GetGraphic(pixelsPerModule, darkColor ?? Color.black, lightColor ?? Color.white, drawQuietZones);
        }
        public static bool[,] GetQRArray(string plainText, ECCLevel eccLevel, bool forceUtf8 = false, bool utf8BOM = false, EciMode eciMode = EciMode.Default, int requestedVersion = -1)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(plainText, eccLevel, forceUtf8, utf8BOM, eciMode, requestedVersion))
            using (QRCode qrCode = new QRCode(qrCodeData))
                return qrCode.GetArray();
        }
    }
}