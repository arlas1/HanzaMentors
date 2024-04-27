using Base.DAL.Contracts;

namespace Base.BLL.Contracts;

public interface IBLLMapper<TLeftObject, TRightObject> : IDalMapper<TLeftObject, TRightObject>
    where TLeftObject : class
    where TRightObject : class
{
}
