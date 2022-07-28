>没想到粉丝对界面效果这么喜欢，接下来就尽量多来点特效，当然，特效也算是动画的一部分了。WPF里面已经包含了很多动画特效的功能支持了，但是，还是得自己实现，我这边就来个自绘实现的。

# 弹动小球
弹动小球是一个很常见的页面特效，类似于，拖动物体或者窗体，实现了抖动效果一样。还是值得学习一二的，实际上，也很简单，只需要一个弹动系数和摩擦系数即可。

## Wpf 和 SkiaSharp
新建一个WPF项目，然后，Nuget包即可
要添加Nuget包
```csharp
Install-Package SkiaSharp.Views.WPF -Version 2.88.0
```
其中核心逻辑是这部分，会以我设置的60FPS来刷新当前的画板。
```csharp
skContainer.PaintSurface += SkContainer_PaintSurface;
_ = Task.Run(() =>
{
    while (true)
    {
        try
        {
            Dispatcher.Invoke(() =>
            {
                skContainer.InvalidateVisual();
            });
            _ = SpinWait.SpinUntil(() => false, 1000 / 60);//每秒60帧
        }
        catch
        {
            break;
        }
    }
});
```
## 弹球实体代码 (Ball.cs)
```csharp
public class Ball
{
    public double X { get; set; }
    public double Y { get; set; }
    public double VX { get; set; }
    public double VY { get; set; }
    public int R { get; set; }
    public SKColor sKColor { get; set; } = SKColors.Blue;
}
```

## 弹动核心类 (Shake.cs)
```csharp
/// <summary>
///  弹动特效
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
```

## 效果如下:
![](https://tupian.wanmeisys.com/markdown/1659017407757-d771c74b-270c-437b-8e93-7c13fffd4d79.gif)


这个特效原理是可以运用到任何物体上的，会产生很神奇的效果，后面可以再搞一些案例。

## 总结
第一次实现特效，当初学FLash，就见到过很多特效的效果，现在自己用敲出来特效感觉是挺不一样的。

## 代码地址
https://github.com/kesshei/WPFSkiaShakeDemo.git
 
https://gitee.com/kesshei/WPFSkiaShakeDemo.git

# 阅
一键三连呦！，感谢大佬的支持，您的支持就是我的动力!

# 版权
蓝创精英团队（公众号同名，CSDN同名，CNBlogs同名）




