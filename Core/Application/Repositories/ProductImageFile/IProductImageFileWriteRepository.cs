using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = Domain.Entities;

namespace Application.Repositories.ProductImageFile
{
    public interface IProductImageFileWriteRepository : IWriteRepository<F::ProductImageFile>
    {
    }
}
