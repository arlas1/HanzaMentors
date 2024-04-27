using Base.DAL.Contracts;
using App.Domain;
using AutoMapper;

namespace App.DAL.EF;

public class DalDummyMapper<TLeftObject, TRightObject> : IDalMapper<TLeftObject, TRightObject>
    where TLeftObject : class 
    where TRightObject : class
{
    private readonly IMapper _mapper;
    public DalDummyMapper(IMapper mapper)
    {
        _mapper = mapper;
    }
    public TLeftObject? Map(TRightObject? inObject)
    {
        return _mapper.Map<TLeftObject>(inObject);
    }

    public TRightObject? Map(TLeftObject? inObject)
    {
        return _mapper.Map<TRightObject>(inObject);    
    }
}
