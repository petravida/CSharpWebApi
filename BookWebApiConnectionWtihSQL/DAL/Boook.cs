namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Boook")]
    public partial class Boook
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Boook()
        {
            BoookPublishingHousee = new HashSet<BoookPublishingHousee>();
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
        public virtual ICollection<BoookPublishingHousee> BoookPublishingHousee { get; set; }
    }
}
