using Model.Variant;

namespace Model.Sensors
{
    public static class Environent
    {
        public static double X(double t)
        {
            return VariantData.A + VariantData.B * t + VariantData.D + Math.Sin(VariantData.C * t);
        }

        public static double F(double t) 
        {
            return VariantData.F * Math.Sin(VariantData.G * t);
        }        
    }
}
