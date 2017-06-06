namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Page
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public int UrlId { get; set; }

        public int CategoryId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreationDate { get; set; }

        public virtual Category Category { get; set; }

        public virtual Seo Seo { get; set; }
    }
}
