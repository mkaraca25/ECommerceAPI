using Application.Repositories.ProductImageFile;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ProductImageFileWriteRepository : WriteRepository<Domain.Entities.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(APIDbContext context) : base(context)
        {
        }
    }
}
