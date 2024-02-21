using System;
using System.Collections.Generic;


public class Complex
{
  public double Real;
  public double Imag;

  public const int DivergeIterations = 255;

  // PROPERTIES
  public double Norm {
    get => this.Real * this.Real + this.Imag * this.Imag;
  }
  
  public double Mag {
    get => Math.Sqrt(this.Norm);
  }

  public double Arg {
    get => Math.Atan2(this.Imag, this.Real);
  }

  public Complex Conjugate {
    get => new Complex(this.Real, -this.Imag);
  }

  // CONSTRUCTORS
  public Complex(int real, int imag)
  {
    this.Real = real;
    this.Imag = imag;
  }

  public Complex(double real, double imag)
  {
    this.Real = real;
    this.Imag = imag;
  }

  public Complex(int real)
  {
    this.Real = real;
    this.Imag = 0.0f;
  }

  public Complex(double real)
  {
    this.Real = real;
    this.Imag = 0.0f;
  }

  // OPERATORS
  public static Complex operator +(Complex z, Complex c)
    => new Complex(z.Real + c.Real, z.Imag + c.Imag);

  public static Complex operator -(Complex z, Complex c)
    => new Complex(z.Real - c.Real, z.Imag - c.Imag);

  public static Complex operator *(Complex z, int n)
    => new Complex(z.Real * n, z.Imag * n);

  public static Complex operator *(Complex z, Complex c)
    => new Complex(
      z.Real * c.Real - z.Imag * c.Imag,
      z.Real * c.Imag + z.Imag * c.Real
    );

  public static Complex operator /(Complex z, int n)
  {
    if (n == 0) {
      throw new DivideByZeroException();
    }
    return new Complex(z.Real / n, z.Imag / n);
  }

  public static Complex operator /(Complex z, double n)
  {
    if (n == 0.0f) {
      throw new DivideByZeroException();
    }
    return new Complex(z.Real / n, z.Imag / n);
  }

  public static Complex operator /(int n, Complex z)
    => z.Conjugate / z.Norm;

  public static Complex operator /(Complex z, Complex c)
    => z * c.Conjugate / c.Norm;

  public Complex Pow(int n)
  {
    if (n == 0) {
      return new Complex(1);
    } else if (n > 0) {
      return this * this.Pow(n - 1);
    } else if (n < 0) {
      return 1 / this.Pow(-n);
    } else {
      throw new ArgumentException("Invalid exponent provided");
    }
  }

  // METHODS
  public override string ToString()
  {
    return $"{this.Real} + {this.Imag}i";
  }
  
  public List<Complex> Diverge()
  {
    List<Complex> trace = new();
    Complex z = new Complex(this.Real, this.Imag);
    trace.Add(z);

    int i = 0;
    while (i < DivergeIterations && z.Mag < 2) {
      z = trace[i].Pow(2) + this;
      trace.Add(z);
      i++;
    }

    return trace;
  }

}
