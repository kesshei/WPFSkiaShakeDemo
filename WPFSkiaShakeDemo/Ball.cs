using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSkiaShakeDemo
{
public class Ball
{
    public double X { get; set; }
    public double Y { get; set; }
    public double VX { get; set; }
    public double VY { get; set; }
    public int R { get; set; }
    public SKColor sKColor { get; set; } = SKColors.Blue;
}
}
