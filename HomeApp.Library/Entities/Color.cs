public struct Color 
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }

    public Color(int r, int g, int b, int a) 
    {
        R = (byte) r;
        G = (byte) g;
        B = (byte) b;
        A = (byte) a;
    }

    public Color(string htmlHex)
    {
        if (htmlHex.Length != 8 && htmlHex.Length != 6) 
            throw new ArgumentException("Color hex string must be either 6 or 8 digits characters long");

        R = Convert.ToByte(htmlHex.Substring(0, 2), 16);
        G = Convert.ToByte(htmlHex.Substring(2, 2), 16);
        B = Convert.ToByte(htmlHex.Substring(4, 2), 16);
        A = htmlHex.Length == 8 ? Convert.ToByte(htmlHex.Substring(6, 2), 16) : (byte) 0xFF;
    }

    public string ToHtmlHex() => $"#{R.ToString("X2")}{G.ToString("X2")}{B.ToString("X2")}{A.ToString("X2")}";


    public static implicit operator Color(string hex) => new Color(hex);

    public static Color Lerp(Color a, Color b, float t) 
    {
        return new Color
        (
            Convert.ToInt32(a.R + ((int) b.R - a.R) * t),
            Convert.ToInt32(a.G + ((int) b.G - a.G) * t),
            Convert.ToInt32(a.B + ((int) b.B - a.B) * t),
            Convert.ToInt32(a.A + ((int) b.A - a.A) * t)
        );
    }
}