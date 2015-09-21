// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Imager.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The imager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;

    /// <summary>
    /// The imager.
    /// </summary>
    public static class Imager
    {
        /// <summary>
        /// The thumbnail.
        /// </summary>
        /// <param name="originBitmap">
        /// The origin bitmap.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <param name="filePath">
        /// The file Path.
        /// </param>
        /// <param name="extension">
        /// The extension.
        /// </param>
        public static void Thumbnail(Bitmap originBitmap, int width, int height, string filePath, string extension)
        {
            using (var newImage = new Bitmap(width, height))
            {
                using (var graphic = GetGraphic(originBitmap, newImage))
                {
                    graphic.DrawImage(originBitmap, 0, 0, width, height);
                    using (var encoderParameters = new EncoderParameters(1))
                    {
                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                        newImage.Save(filePath, ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.FilenameExtension.Contains(extension.ToUpperInvariant())), encoderParameters);
                    }
                }
            }
        }

        /// <summary>
        /// The get graphic.
        /// </summary>
        /// <param name="originImage">
        /// The origin image.
        /// </param>
        /// <param name="newImage">
        /// The new image.
        /// </param>
        /// <returns>
        /// The <see cref="Graphics"/>.
        /// </returns>
        private static Graphics GetGraphic(Image originImage, Bitmap newImage)
        {
            newImage.SetResolution(originImage.HorizontalResolution, originImage.VerticalResolution);
            var graphic = Graphics.FromImage(newImage);
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            return graphic;
        }
    }
}