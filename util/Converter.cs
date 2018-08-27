using System;


namespace util
{
    public class Converter
    {
        public static int PixelInCm(int pixel, int dpis = 300)
        {
            return Convert.ToInt32(pixel / dpis * 2.5);
        }

        public static int CmInPixel(int centimeter, int dpis = 300)
        {
            return Convert.ToInt32(centimeter * dpis / 2.5);
        }

        
        
    }
}
