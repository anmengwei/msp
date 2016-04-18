using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;

namespace Spread.Web
{
    /// <summary>
    /// ImgCode 的摘要说明
    /// </summary>
    public class ImgCode : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            //设置不缓存此页
            context.Response.AppendHeader("pragma", "no-cache");
            context.Response.AppendHeader("Cache-Control", "no-cache, must-revalidate");
            context.Response.AppendHeader("expires", "0");

            Random rand = new Random();

            //获取随机字符
            string str = GetRandString(4);
            //写session 方便验证
            context.Session["imgcode"] = str.ToLower();
            //创建画板
            Bitmap image = new Bitmap(80, 26);
            Graphics g = Graphics.FromImage(image);
            g.InterpolationMode = InterpolationMode.Low;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //绘制渐变背景
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            Brush brushBack = new LinearGradientBrush(rect, Color.FromArgb(rand.Next(150, 256), 255, 255), Color.FromArgb(255, rand.Next(150, 256), 255), rand.Next(90));
            g.FillRectangle(brushBack, rect);

            //绘制干扰曲线
            for (int i = 0; i < 2; i++)
            {
                Point p1 = new Point(0, rand.Next(image.Height));
                Point p2 = new Point(rand.Next(image.Width), rand.Next(image.Height));
                Point p3 = new Point(rand.Next(image.Width), rand.Next(image.Height));
                Point p4 = new Point(image.Width, rand.Next(image.Height));
                Point[] p = { p1, p2, p3, p4 };
                Pen pen = new Pen(Color.Gray, 1);
                g.DrawBeziers(pen, p);
            }

            //逐个绘制文字
            for (int i = 0; i < str.Length; i++)
            {
                string strChar = str.Substring(i, 1);
                int deg = rand.Next(-15, 15);
                float x = (image.Width / str.Length / 2) + (image.Width / str.Length) * i;
                float y = image.Height / 2;
                //随机字体大小
                Font font = new Font("Consolas", rand.Next(16, 24), FontStyle.Regular);
                SizeF size = g.MeasureString(strChar, font);
                Matrix m = new Matrix();
                //旋转
                m.RotateAt(deg, new PointF(x, y), MatrixOrder.Append);
                //扭曲
                m.Shear(rand.Next(-10, 10) * 0.03f, 0);
                g.Transform = m;
                //随机渐变画笔
                Brush brushPen = new LinearGradientBrush(rect, Color.FromArgb(rand.Next(0, 256), 0, 0), Color.FromArgb(0, 0, rand.Next(0, 256)), rand.Next(90));
                g.DrawString(str.Substring(i, 1), font, brushPen, new PointF(x - size.Width / 2, y - size.Height / 2));

                g.Transform = new Matrix();
            }

            g.Save();
            context.Response.ContentType = "image/jpeg";
            context.Response.Clear();
            context.Response.BufferOutput = true;
            image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            g.Dispose();
            image.Dispose();
            context.Response.Flush();
        }

        //获取随机数
        private string GetRandString(int len)
        {
            string s = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string str = "";
            Random r = new Random();
            for (int i = 0; i < len; i++)
            {
                str += s.Substring(r.Next(s.Length), 1);
            }
            return str;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}