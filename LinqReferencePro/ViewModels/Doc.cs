using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqReferencePro.ViewModels
{
    public class Doc
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string HTMLPath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int CategoryOrder { get; set; }

        public string Theme { get; set; }

    }
}
