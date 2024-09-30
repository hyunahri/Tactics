using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using CoreLib;
using CoreLib.Complex_Types;
using CoreLib.Extensions;
using Game;
using Unity.Mathematics;

namespace Units
{
    /// <summary>
    /// A group of ICharacters arranged in a Formation.
    /// The Unit is the basic building block of combat and the smallest movable entity on the tactical map.
    /// Combat will always be between two units.
    /// </summary>
    public class Unit
    {
        public Unit()
        {
            CharactersInFormation = new ICharacter?[Globals.UnitColumns, Globals.UnitRows];
        }
        
        //Events
        public readonly CoreEvent OnChange = new CoreEvent();
        
        //----------------
        protected ICharacter?[,] CharactersInFormation;
        public IReadOnlyDictionary<int2,ICharacter> GetReadonlyFormation() => CharactersInFormation.ToDict();
        
        //Getters
        public bool IsEmpty => GetICharacters().TrueForAll(x => x is null);
        public ICharacter? GetICharacter(int i, int j)
        {
            return CharactersInFormation[i, j];
        }

        public List<ICharacter> GetRow(int i)
        {
            List<ICharacter> ICharacters = new List<ICharacter>();
            for (int j = 0; j < Globals.UnitColumns; j++)
                if (CharactersInFormation[i, j] != null) ICharacters.Add(CharactersInFormation[i, j]!);
            return ICharacters;
        }
        public List<ICharacter> GetICharacters(bool cullNulls = true)
        {
           List<ICharacter> ICharacters = new List<ICharacter>();
           for (int i = 0; i < Globals.UnitColumns; i++)
           {
               for (int j = 0; j < Globals.UnitRows; j++)
               {
                   if (CharactersInFormation[i, j] != null || !cullNulls)
                       ICharacters.Add(CharactersInFormation[i, j]!);
               }
           }
           return ICharacters;
        }
        
        public int GetRowOf(ICharacter c) => GetICharacters(false).IndexOf(c) / Globals.UnitColumns;
        public int GetColumnOf(ICharacter c) => GetICharacters(false).IndexOf(c) % Globals.UnitColumns;

        //Setters
        public void SetOrReplaceICharacter(ICharacter c, int column, int row)
        {
            RemoveAt(column,row);
            
            CharactersInFormation[column, row] = c;
            c.SetUnit(this);
            
            OnChange.Invoke();
        }
        
        public bool TrySetICharacter(ICharacter c, int column, int row)
        {
            if (CharactersInFormation[column, row] != null) return false;
            
            CharactersInFormation[column, row] = c;
            c.SetUnit(this);
            
            OnChange.Invoke();
            return true;
        }

        public void RemoveICharacter(ICharacter c)
        {
            int2 pos = new int2(GetColumnOf(c), GetRowOf(c));
            RemoveAt(pos.x, pos.y);
        }
        
        public void RemoveAt(int column, int row)
        {
            var c = CharactersInFormation[column, row];
            if (c == null) return;
            
            c.SetUnit(null);
            CharactersInFormation[column, row] = null;
            OnChange.Invoke();
        }

        //Generate a seed for every enemy unit that shows up on the tactical map. Reroll after attacking that enemy or using a skill against it.
        public Dictionary<Unit, int> Seeds = new Dictionary<Unit, int>();
        public int GenerateSeed() => RNG.rng.Next(0, Int32.MaxValue);
    }


}