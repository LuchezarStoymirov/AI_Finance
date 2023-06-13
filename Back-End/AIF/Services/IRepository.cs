namespace AIF.Services.Implementation
{
    internal interface IRepository<T>
    {
        object FindByCondition(Func<object, bool> value);
    }
}