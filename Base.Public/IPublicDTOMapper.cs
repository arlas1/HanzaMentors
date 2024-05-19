using Base.DAL.Contracts;

namespace Base.Public;

public interface IPublicDTOMapper<TLeftObject, TRightObject> : IDalMapper<TLeftObject, TRightObject>
    where TLeftObject : class
    where TRightObject : class
{
}
