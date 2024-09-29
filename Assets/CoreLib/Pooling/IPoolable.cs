namespace CoreLib.Complex_Types
{
    public interface IPoolable
    {
        void OnPoolCreate();
        void OnPoolDeploy();
        void OnPoolReturn();
        void OnPoolDestroy();
    }
}