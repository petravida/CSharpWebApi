namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BookPublishingHouse")]
    public partial class BookPublishingHouse
    {
        public Guid Id { get; set; }

        public Guid Book_Id { get; set; }

        public Guid PublishingHouse_Id { get; set; }

        public virtual Book Book { get; set; }

        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
