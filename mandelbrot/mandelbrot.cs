using System;
using System.Drawing;
using System.Drawing.Imaging;


public static class Mandelbrot
{
  public static void Render(
    int size,
    (double start, double stop) boundsX,
    (double start, double stop) boundsY
  ) {
    Bitmap plane = new Bitmap(size, size);
    Graphics graphics = Graphics.FromImage(plane);

    for (int x = 0; x < size; x++)
    {
      for (int y = 0; y < size; y++)
      {
        Complex z = new Complex(
          boundsX.start + (boundsX.stop - boundsX.start) * (double)x / (double)size,
          boundsY.start + (boundsY.stop - boundsY.start) * (double)y / (double)size
        );
        int iterations = z.Diverge().Count - 1;
        
        double frac;
        frac = (double)iterations / (double)Complex.DivergeIterations;
        frac = Math.Sqrt(frac);
        Color colour = Colourise.Apply(frac, "inferno");

        plane.SetPixel((int)x, (int)y, colour);
      }
    }

    plane.Save($"render ({size}x{size}).png""", ImageFormat.Png);

    graphics.Dispose();
    plane.Dispose();
  }

  public static void Render(int size)
  {
    Render(size, (-2.0, 2.0), (-2.0, 2.0));
  }
}
