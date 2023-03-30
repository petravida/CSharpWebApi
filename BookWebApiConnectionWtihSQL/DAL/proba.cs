namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("proba")]
    public partial class proba
    {
        public Guid Id { get; set; }

        [Column("Proba")]
        public bool? Proba1 { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        [Column(TypeName = "money")]
        public decimal PRICE { get; set; }
    }
}
