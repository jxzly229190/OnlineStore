namespace V5.Library.Security
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static class SecurityCode
    {
        #region Constants and Fields

        private static readonly List<Color> BackgroundColors = new List<Color>
                                                                   {
                                                                       Color.Snow,
                                                                       Color.Gainsboro,
                                                                       Color.DarkRed,
                                                                       Color.Teal,
                                                                       Color.Silver,
                                                                       Color.BlanchedAlmond,
                                                                       Color.Navy,
                                                                       Color.PaleTurquoise
                                                                   };

        private static readonly List<Color> FontColors = new List<Color>
                                                             {
                                                                 Color.DimGray,
                                                                 Color.Green,
                                                                 Color.FloralWhite,
                                                                 Color.White,
                                                                 Color.Sienna,
                                                                 Color.Black,
                                                                 Color.MintCream,
                                                                 Color.OrangeRed
                                                             };

        private static readonly List<Color> NoiseLineColors = new List<Color>
                                                             {
                                                                 Color.Thistle,
                                                                 Color.Turquoise,
                                                                 Color.Silver,
                                                                 Color.Tan,
                                                                 Color.AliceBlue,
                                                                 Color.Azure,
                                                                 Color.CornflowerBlue,
                                                                 Color.Honeydew,
                                                                 Color.LightSkyBlue,
                                                                 Color.MintCream,
                                                                 Color.PaleGoldenrod,
                                                                 Color.PaleTurquoise,
                                                                 Color.PapayaWhip,
                                                                 Color.Pink,
                                                                 Color.MediumVioletRed,
                                                                 Color.MidnightBlue,
                                                                 Color.Linen,
                                                                 Color.Gray,
                                                                 Color.Gold,
                                                                 Color.LightPink
                                                             };

        private static readonly List<Font> Fonts = new List<Font>
                                                       {
                                                           new Font(
                                                               "Comic Sans MS",
                                                               16,
                                                               (FontStyle.Bold | FontStyle.Italic)),
                                                           new Font(
                                                               "Arial Black",
                                                               18,
                                                               (FontStyle.Regular | FontStyle.Italic)),
                                                           new Font(
                                                               "Consolas",
                                                               16,
                                                               (FontStyle.Bold | FontStyle.Italic)),
                                                           new Font(
                                                               "Corbel",
                                                               18,
                                                               (FontStyle.Regular | FontStyle.Italic)),
                                                           new Font(
                                                               "Courier New",
                                                               16,
                                                               (FontStyle.Bold | FontStyle.Italic))
                                                       };

        #endregion

        #region Public Methods and Operators

        public static string CreateSecurityCode()
        {
            string strsvali = "0,1,2,3,4,5,6,7,8,9";    //定义一个字符串  
            string[] ValiArray = strsvali.Split(',');   //定义一个字符数组  
            string ReturnNum = "";                          //定义一个返回验证码的字符串  
            //记录随机数，避免产生同样的随机数  
            int nums = -1;
            //确保产生不同的随机数  
            Random vrand = new Random();                //声明一个Random对象  
            for (int n = 1; n < 6; n++)
            {
                if (nums != -1)
                {
                    vrand = new Random(n * nums * unchecked((int)DateTime.Now.Ticks));
                }
                int t = vrand.Next(10);                     //由Next()方法获取随机数  
                nums = t;
                ReturnNum += ValiArray[t];                  //生成随机数  
            }
            return ReturnNum;                              //返回生成的随机数

            /*
            var temporaries = Guid.NewGuid().ToString().Split(new[] { '-' });

            var stringBuilder = new StringBuilder();

            foreach (var temporary in temporaries)
            {
                stringBuilder.Append(temporary.Substring(0, 1));
            }

            return stringBuilder.ToString();
            */
        }

        public static MemoryStream CreateSecurityCode(out string securityCode)
        {
            var memoryStream = new MemoryStream();

            var bitmap = new Bitmap(90, 30);
            var graphics = Graphics.FromImage(bitmap);

            try
            {
                var random = new Random();

                // 获取安全码图片背景色，并填充到图面
                var index = random.Next(8);
                graphics.Clear(BackgroundColors[index]);

                // 获取安全码字符串
                securityCode = CreateSecurityCode();
                var securityCodeArray = securityCode.ToArray();

                var linearGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, bitmap.Width, bitmap.Height), FontColors[index], FontColors[index], 1.3f, true);
                
                // 绘制安全码字符
                for (var i = 0; i < 5; i++)
                {
                    var x = (i + 1) * (i + 9);
                    var y = random.Next(0, 3);

                    graphics.DrawString(securityCodeArray[i].ToString(CultureInfo.InvariantCulture), Fonts[i], linearGradientBrush, x, y);
                }

                // 绘制噪线
                ////for (int i = 0; i < 15; i++)
                ////{
                ////    var x1 = random.Next(bitmap.Width);
                ////    var x2 = random.Next(bitmap.Width);
                ////    var y1 = random.Next(bitmap.Height);
                ////    var y2 = random.Next(bitmap.Height);

                ////    graphics.DrawLine(new Pen(NoiseLineColors[i], 1.5f), x1, x2, y1, y2);
                ////}

                // 绘制噪点
                for (var i = 0; i < 100; i++)
                {
                    var x = random.Next(bitmap.Width);
                    var y = random.Next(bitmap.Height);

                    bitmap.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                // 绘制边框
                graphics.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1);

                // 保存图片
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Gif);

                return memoryStream;
            }
            finally
            {
                bitmap.Dispose();
                graphics.Dispose();
            }
        }

        #endregion
    }
}