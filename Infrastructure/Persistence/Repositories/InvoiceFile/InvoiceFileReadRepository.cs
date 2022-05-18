using Application.Repositories;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class InvoiceFileReadRepository : ReadRepository<Domain.Entities.InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(APIDbContext context) : base(context)
        {
        }
    }
}
