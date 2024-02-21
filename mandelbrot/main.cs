using System;
using System.Drawing;
using System.Drawing.Imaging;
// using System.Drawing.Common;


class Program
{
  public const double PlaneX = 1000d;
  public const double PlaneY = 1000d;
  public const double RenderStep = 1d;
  
  public static void Main()
  {
    Console.WriteLine("STATUS: RUNNING!");

    Bitmap plane = new Bitmap((int)PlaneX, (int)PlaneY);
    Graphics graphics = Graphics.FromImage(plane);

    for (double x = 0; x < PlaneX; x += RenderStep)
    {
      for (double y = 0; y < PlaneY; y += RenderStep)
      {
        Complex z = new Complex(
          4 * x / PlaneX - 2,
          4 * y / PlaneY - 2
        );
        int iterations = z.Diverge().Count - 1;
        
        double frac;
        frac = (double)iterations / (double)Complex.DivergeIterations;
        frac = Math.Sqrt(frac);
        Color colour = Colourise.Apply(frac, "inferno");

        plane.SetPixel((int)x, (int)y, colour);

        if (x == PlaneX/2 && y == PlaneY/2) {
          Console.WriteLine("STATUS: HALFWAY...");
        }
      }
    }

    plane.Save("render.png", ImageFormat.Png);

    graphics.Dispose();
    plane.Dispose();

    Console.WriteLine("STATUS: FINISHED!");
  }

}
