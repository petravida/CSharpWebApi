namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AuthorsSpecification")]
    public partial class AuthorsSpecification
    {
        public Guid Id { get; set; }

        [Column("First Book")]
        [Required]
        [StringLength(50)]
        public string First_Book { get; set; }

        [Column("Numbers of published books")]
        public int Numbers_of_published_books { get; set; }

        [StringLength(50)]
        public string Prizes { get; set; }

        public Guid Author_Id { get; set; }

        public virtual Author Author { get; set; }
    }
}
