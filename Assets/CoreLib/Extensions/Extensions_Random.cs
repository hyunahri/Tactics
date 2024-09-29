namespace CoreLib.Extensions
{
    public static class Extensions_Random
    {
        public static float Next(this System.Random random, float a, float b) => (float)(random.NextDouble() * (b - a) + a);
        public static float NextFloat(this System.Random random, int a, int b) => (float)(random.NextDouble() * (b - a) + a);
        public static float NextFloat(this System.Random random, float a, float b) => (float)(random.NextDouble() * (b - a) + a);
        public static float NextFloat(this System.Random random, double a, double b) => (float)(random.NextDouble() * (b - a) + a);
        public static bool NextBool(this System.Random random) => random.Next(0, 2) == 0;
        
   
        

        public static void Randomize(this int i, int ExclusiveMax) => i = RNG.rng.Next(0, ExclusiveMax);
        public static void Randomize(this float i, float ExclusiveMax) => i = RNG.rng.NextFloat(0, ExclusiveMax);
        public static void Randomize(this int i, int InclusiveMin, int ExclusiveMax) => i = RNG.rng.Next(InclusiveMin, ExclusiveMax);
        public static void Randomize(this float i, float InclusiveMin, float ExclusiveMax) => i = RNG.rng.NextFloat(InclusiveMin, ExclusiveMax);
    }
}