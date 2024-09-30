using System.Collections.Generic;
using Characters;
using Units;

namespace Economy
{

    public interface IService // : ITransactable //todo implement later
    {
        bool EligibleForService(object input);
        void PerformService(object input);
    }

    //Covariant interface for IService
    public interface IService<T> : IService
    {
        bool EligibleForService(T input);
        void PerformService(T input);
    }

    public abstract class CharacterService : IService<Character>
    {
        public abstract bool EligibleForService(Character input);
        public abstract void PerformService(Character input);

        bool IService.EligibleForService(object input) => EligibleForService((Character)input);

        void IService.PerformService(object input) => PerformService((Character)input);
    }

    public abstract class UnitService : IService<Unit>
    {
        public abstract void PerformService(Unit input);

        public abstract bool EligibleForService(Unit input);
        
        bool IService.EligibleForService(object input) => EligibleForService((Unit)input);
        void IService.PerformService(object input) => PerformService((Unit)input);
    }

    public abstract class MultiCharacterService : IService<List<Character>>
    {
        public abstract void PerformService(List<Character> input);
        public abstract  bool EligibleForService(List<Character> input);
        
        bool IService.EligibleForService(object input) => EligibleForService((List<Character>)input);
        void IService.PerformService(object input) => PerformService((List<Character>)input);
    }

    public abstract class MultiUnitService : IService<List<Unit>>
    {
        public abstract void PerformService(List<Unit> input);
        public abstract bool EligibleForService(List<Unit> input);
        
        bool IService.EligibleForService(object input) => EligibleForService((List<Unit>)input);
        void IService.PerformService(object input) => PerformService((List<Unit>)input);
    }
}