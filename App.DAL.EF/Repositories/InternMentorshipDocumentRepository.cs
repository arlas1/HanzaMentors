using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternMentorshipDocumentRepository : BaseEntityRepository<InternMentorshipDocument, AppDbContext>, IInternMentorshipDocumentRepository
{
    public InternMentorshipDocumentRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}