namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BoookPublishingHousee")]
    public partial class BoookPublishingHousee
    {
        public Guid Id { get; set; }

        public Guid Boook_Id { get; set; }

        public Guid PublishingHousee_Id { get; set; }

        public virtual Boook Boook { get; set; }

        public virtual PublishingHousee PublishingHousee { get; set; }
    }
}
