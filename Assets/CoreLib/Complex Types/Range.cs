using CoreLib.Utilities;

namespace CoreLib.Complex_Types
{
    public struct Range
    {
        private string DiceString;
        public readonly int DiceCount;
        public readonly int DiceSize;
        public override string ToString() => $"{DiceCount} - {DiceCount * DiceSize}";

        public int Roll()
        {
            string proxy = "";
            return Roll(out proxy);
        }
        public int Roll(out string rollResult)
        {
            int sum = 0;
            rollResult = $"Roll [{DiceString}]: ";
            for (int i = 0; i < DiceCount; i++)
            {
                int roll = RNG.rng.Next(1, DiceSize + 1);
                rollResult += $"[{roll}]+";
                sum += roll;
            }

            rollResult = rollResult.Remove(rollResult.Length - 1, 1);

            rollResult += $" = {sum}.";
            FLog.Log($"<color=white>{rollResult}</color>");
            return sum;
        }
    
        public Range(string diceString)
        {
            DiceString = diceString;
            var split = diceString.Split('d'); //1d6, 1 six sided dice
            DiceCount = int.Parse(split[0]);
            DiceSize = int.Parse(split[1]);
        }
    }
    
}