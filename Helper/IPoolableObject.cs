using System;

namespace Helper.Optimization
{
    public interface IPoolableObject : IDisposable
    {
        int Size { get; }
        void Reset();
        void SetPoolManager(PoolManager poolManager);
    }
}