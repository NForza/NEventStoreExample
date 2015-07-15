namespace NEventStoreExample.Test
{
    class TestDataBuilder<T>
    {
        public virtual T Build()
        {
            return default(T);
        }        
    }
}
