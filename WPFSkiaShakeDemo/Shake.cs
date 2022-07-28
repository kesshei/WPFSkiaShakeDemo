using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSkiaShakeDemo
{
    /// <summary>
    /// 弹动特效 
    /// </summary>
    public class Shake
    {
        public SKPoint centerPoint;
        public int Radius = 0;
        /// <summary>
        /// 弹动系数
        /// </summary>
        public double Spring = 0.02;
        public double TargetX;
        /// <summary>
        /// 摩擦系数
        /// </summary>
        public double Friction = 0.95;
        public Ball Ball;
        public void Init(SKCanvas canvas, SKTypeface Font, int Width, int Height)
        {
            if (Ball == null)
            {
                Ball = new Ball();
                Ball.X = 60;
                Ball.Y = Height / 2;
                Ball.R = 30;
            }
        }
        /// <summary>
        /// 渲染
        /// </summary>
        public void Render(SKCanvas canvas, SKTypeface Font, int Width, int Height)
        {
            Init(canvas, Font, Width, Height);
            centerPoint = new SKPoint(Width / 2, Height / 2);
            this.Radius = 30;
            this.TargetX = Width / 2;
            canvas.Clear(SKColors.White);

            var ax = (TargetX - Ball.X) * Spring;

            Ball.VX += ax;
            Ball.VX *= Friction;
            Ball.X += Ball.VX;


            DrawCircle(canvas, Ball);

            using var paint = new SKPaint
            {
                Color = SKColors.Blue,
                IsAntialias = true,
                Typeface = Font,
                TextSize = 24
            };
            string by = $"by 蓝创精英团队";
            canvas.DrawText(by, 600, 400, paint);
        }
        /// <summary>
        /// 画一个圆
        /// </summary>
        public void DrawCircle(SKCanvas canvas, Ball ball)
        {
            using var paint = new SKPaint
            {
                Color = SKColors.Blue,
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                StrokeWidth = 2
            };
            canvas.DrawCircle((float)ball.X, (float)ball.Y, ball.R, paint);
        }
        public void ReSet()
        {
            Ball.X = 60;
        }
    }
}
