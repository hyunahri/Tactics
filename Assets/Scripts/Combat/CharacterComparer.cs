using System.Collections.Generic;
using Characters;

namespace Combat
{
    //<summary> used to compare characters for turn order in combat
    public class CharacterComparer : IComparer<ICharacter>
    {
        public int Compare(ICharacter? x, ICharacter? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (y is null) return 1;
            if (x is null) return -1;
            
            //First by initiative
            var initiativeComparison = x.GetStat("initiative").CompareTo(y.GetStat("initiative"));
            if (initiativeComparison != 0) return initiativeComparison;
            
            //then by mobility
            var mobilityComparison = x.GetStat("mobility").CompareTo(y.GetStat("mobility"));
            if (mobilityComparison != 0) return mobilityComparison;
            
            //Then by level
            var levelComparison = x.Level.CompareTo(y.Level);
            if (levelComparison != 0) return levelComparison;
            
            //Then by name
            return string.Compare(x.GetName(), y.GetName(), System.StringComparison.Ordinal);
        }
    }
}