using Application.Repositories;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.File
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File>,IFileWriteRepository
    {
        public FileWriteRepository(APIDbContext context) : base(context)
        {
        }
    }
}
