// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageEdit.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   缩略图/水印图编辑模式
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Picture
{
    using global::System;
    using global::System.Drawing;
    using global::System.Drawing.Drawing2D;
    using global::System.Drawing.Imaging;

    using IDAL;

    /// <summary>
    /// 缩略图/水印图编辑模式
    /// </summary>
    public enum HWMode
    {
        /// <summary>
        /// 指定高宽缩放（可能变形）
        /// </summary>
        HW,

        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        H,

        /// <summary>
        /// 指定宽，高按比例
        /// </summary>
        W,

        /// <summary>
        /// 指定高宽裁减（不变形）
        /// </summary>
        Cut,

        /// <summary>
        /// 自适应，按最大的为基准
        /// </summary>
        Auto,

        /// <summary>
        /// 固定大小，填充
        /// </summary>
        Fill,

        /// <summary>
        /// 默认
        /// </summary>
        Default
    }

    /// <summary>
    /// 水印位置
    /// </summary>
    public enum PositionMode
    {
        /// <summary>
        /// 左上
        /// </summary>
        TopLeft,

        /// <summary>
        /// 左中
        /// </summary>
        MiddleLeft,

        /// <summary>
        /// 左下
        /// </summary>
        BottomLeft,

        /// <summary>
        /// 上中
        /// </summary>
        TopCenter,

        /// <summary>
        /// 中间
        /// </summary>
        MiddleCenter,

        /// <summary>
        /// 下中
        /// </summary>
        BottomCenter,

        /// <summary>
        /// 右上
        /// </summary>
        TopRight,

        /// <summary>
        /// 右中
        /// </summary>
        MiddleRight,

        /// <summary>
        /// 右下
        /// </summary>
        BottomRight
    }

    /// <summary>
    /// 水印图
    /// </summary>
    public class ImageEdit : IImageEdit
    {
        /// <summary>
        /// 原始图片路径
        /// </summary>
        public string OriginalImagePath { get; set; }

        /// <summary>
        /// 生成图保存路径
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 生成图宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 生成图高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 缩放模式
        /// </summary>
        public HWMode Mode { get; set; }

        /// <summary>
        /// 生成图片水印路径
        /// </summary>
        public string MarkPath { get; set; }

        /// <summary>
        /// 水印透明度  只有在设置了MarkPath才有效
        /// </summary>
        public int Opacity { get; set; }

        /// <summary>
        /// 水印位置  只有在设置了MarkPath才有效
        /// </summary>
        public PositionMode Position { get; set; }

        /// <summary>
        /// 生成图片
        /// </summary>
        public void CreateNew()
        {
            Image originalImage = Image.FromFile(this.OriginalImagePath);

            int towidth = this.Width;
            int toheight = this.Height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (this.Mode)
            {
                case HWMode.HW: // 指定高宽缩放（可能变形）                
                    break;
                case HWMode.W: // 指定宽，高按比例                    
                    toheight = originalImage.Height * this.Width / originalImage.Width;
                    break;
                case HWMode.H: // 指定高，宽按比例
                    towidth = originalImage.Width * this.Height / originalImage.Height;
                    break;
                case HWMode.Cut: // 指定高宽裁减（不变形）                
                    if (originalImage.Width / (double)originalImage.Height > towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * this.Height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }

                    break;
                case HWMode.Auto:
                    if (originalImage.Height > originalImage.Width) 
                    {
                        // 指定高，宽按比例
                        towidth = originalImage.Width * this.Height / originalImage.Height;
                    }
                    else // 指定宽，高按比例 
                        toheight = originalImage.Height * this.Width / originalImage.Width;
                    break;
                case HWMode.Fill:
                    towidth = this.Width;
                    toheight = this.Height;
                    if (originalImage.Width > originalImage.Height)
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width;
                        x = 0;
                        y = (originalImage.Height - originalImage.Width) / 2;
                    }
                    else
                    {
                        ow = originalImage.Height;
                        oh = originalImage.Height;
                        x = (originalImage.Width - originalImage.Height) / 2;
                        y = 0;
                    }

                    break;
                default:
                    towidth = originalImage.Width;
                    toheight = originalImage.Height;
                    break;
            }

            // 新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            // 新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            // 设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;

            // 设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            // 清空画布并以透明背景色填充
            g.Clear(Color.White);

            // 在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(
                originalImage,
                new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            #region 填充水印

            if (!string.IsNullOrEmpty(this.MarkPath))
            {
                Image imgMark = new Bitmap(this.MarkPath);
                int intMarkWidth = imgMark.Width;
                int intMarkHeight = imgMark.Height;

                ImageAttributes imageAttributes = new ImageAttributes();

                float[][] colorMatrixElements =
                    {
                        new[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
                        new[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
                        new[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
                        new[] { 0.0f, 0.0f, 0.0f, this.Opacity / 100f, 0.0f },
                        new[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
                    };

                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                int intLeft = 0, intTop = 0;
                switch (this.Position)
                {
                    case PositionMode.TopLeft:
                        intLeft = 0;
                        intTop = 0;
                        break;
                    case PositionMode.TopCenter:
                        intLeft = towidth / 2 - intMarkWidth / 2;
                        intTop = 0;
                        break;
                    case PositionMode.TopRight:
                        intLeft = towidth - intMarkWidth;
                        intTop = 0;
                        break;
                    case PositionMode.MiddleLeft:
                        intLeft = 0;
                        intTop = toheight / 2 - intMarkHeight / 2;
                        break;
                    case PositionMode.MiddleCenter:
                        intLeft = towidth / 2 - intMarkWidth / 2;
                        intTop = toheight / 2 - intMarkHeight / 2;
                        break;
                    case PositionMode.MiddleRight:
                        intLeft = towidth - intMarkWidth;
                        intTop = toheight / 2 - intMarkHeight / 2;
                        break;
                    case PositionMode.BottomLeft:
                        intLeft = 0;
                        intTop = toheight - intMarkHeight;
                        break;
                    case PositionMode.BottomCenter:
                        intLeft = towidth / 2 - intMarkWidth / 2;
                        intTop = toheight - intMarkHeight;
                        break;
                    case PositionMode.BottomRight:
                        intLeft = towidth - intMarkWidth;
                        intTop = toheight - intMarkHeight;
                        break;
                }

                g.DrawImage(imgMark, new Rectangle(intLeft, intTop, intMarkWidth, intMarkHeight), 0, 0, intMarkWidth, intMarkHeight, GraphicsUnit.Pixel, imageAttributes);
            }
            #endregion

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == "image/jpeg")
                {
                    ici = codec;
                }
            }

            EncoderParameters ep = new EncoderParameters();
            ep.Param[0] = new EncoderParameter(Encoder.Quality, 95L);
            try
            {
                // 以jpg格式保存缩略图
                bitmap.Save(this.SavePath, ici, ep);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }
}
