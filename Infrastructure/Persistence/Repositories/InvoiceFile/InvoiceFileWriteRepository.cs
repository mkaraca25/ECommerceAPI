using Application.Repositories;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class InvoiceFileWriteRepository : WriteRepository<Domain.Entities.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(APIDbContext context) : base(context)
        {
        }
    }
}
