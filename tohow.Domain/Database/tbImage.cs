﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tohow.Domain.Database
{
    [Table("tbImage")]
    public class tbImage
    {
        [Key]
        public Guid Id { get; set; }
        [Column("UploaderUserId")]
        public long userId { get; set; }
        public bool IsDeleted { get; set; }
        public string Uri { get; set; }
    }
}
