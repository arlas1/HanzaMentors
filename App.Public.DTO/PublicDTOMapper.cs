using AutoMapper;
using Base.Public;

namespace App.Public.DTO;

public class PublicDTOMapper<TLeftObject, TRightObject> : IPublicDTOMapper<TLeftObject, TRightObject>
    where TLeftObject : class
    where TRightObject : class
{
    private readonly IMapper _mapper;

    public PublicDTOMapper(IMapper mapper)
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