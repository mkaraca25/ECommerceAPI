using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class File : BaseEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        [NotMapped]//BaseEntity de istenilmeyen column override edilmistir.
        public override DateTime UpdatedDate { get=>base.UpdatedDate; set=>base.UpdatedDate=value; }
    }
}
