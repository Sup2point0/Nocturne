using System;


class Program
{ 
  public static void Main()
  {
    Log("RUNNING!");
    Mandelbrot.Render(500);
    // Mandelbrot.Render(500, (-2.0, 2.0), (-2.0, 2.0));
    Log("FINISHED!");
  }

  public static void Log(string text)
  {
    Console.WriteLine($"STATUS: {text}");
  }

}
