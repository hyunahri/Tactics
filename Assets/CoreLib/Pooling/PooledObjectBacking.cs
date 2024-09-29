using System;

namespace CoreLib.Complex_Types
{
    public struct PooledObjectBacking<T> : IDisposable where T : class
    {
        private readonly T m_ToReturn;
        private readonly ObjectPoolBacking<T> m_Pool;

        internal PooledObjectBacking(T value, ObjectPoolBacking<T> pool)
        {
            this.m_ToReturn = value;
            this.m_Pool = pool;
        }

        void IDisposable.Dispose() => this.m_Pool.Release(this.m_ToReturn);
    }
}