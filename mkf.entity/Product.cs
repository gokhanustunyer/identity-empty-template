using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mkf.entity
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public IQueryable<Label>? Label { get; set; }
        public IQueryable<Category>? Categories { get; set; }
    }

}
