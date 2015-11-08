using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tohow.Domain.Database
{
    [Table("tblImage")]
    public class tblImage
    {
        [Key]
        public Guid Id { get; set; }
        [Column("UploaderUserId")]
        public int userId { get; set; }
        public bool IsDeleted { get; set; }
        public string Uri { get; set; }
    }
}
