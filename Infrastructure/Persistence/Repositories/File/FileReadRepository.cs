using Application.Repositories;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.File
{
    public class FileReadRepository : ReadRepository<Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(APIDbContext context) : base(context)
        {
        }
    }
}
