namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            BookPublishingHouse = new HashSet<BookPublishingHouse>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Column("Number of pages")]
        public int Number_of_pages { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        public Guid Author_Id { get; set; }

        public virtual Author Author { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookPublishingHouse> BookPublishingHouse { get; set; }
    }
}
