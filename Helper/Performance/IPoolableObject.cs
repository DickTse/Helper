using System;

namespace Helper.Performance
{
    public interface IPoolableObject : IDisposable
    {
        int Size { get; }
        void Reset();
        void SetPoolManager(PoolManager poolManager);
    }
}