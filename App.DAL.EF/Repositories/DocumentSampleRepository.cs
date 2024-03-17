using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class DocumentSampleRepository : BaseEntityRepository<DocumentSample, AppDbContext>, IDocumentSampleRepository
{
    public DocumentSampleRepository(AppDbContext dataContext) : base(dataContext)
    {
    }

}