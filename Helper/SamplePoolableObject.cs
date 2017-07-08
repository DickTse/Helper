namespace Helper.Optimization
{
    public class SamplePoolableObject : IPoolableObject
    {
        private PoolManager poolManager;
        public byte[] Data { get; set; }
        public int UsableLength { get; set; }

        public int Size
        {
            get {return (Data == null)? Data.Length : 0;}
        }

        void IPoolableObject.Reset()
        {
            UsableLength = 0;
        }

        void IPoolableObject.SetPoolManager(PoolManager poolManager)
        {
            this.poolManager = poolManager;
        }

        public void Dispose()
        {
            this.poolManager.ReturnObject(this);
        }
    }
}