using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GrpcServiceTest20230128.Models
{
    [Table("Tbl_Blog")]
    public class BlogModel
    {
        [Key]
        [Column("Blog_Id")]
        public int BlogId { get; set; }
        [Column("Blog_Title")]
        public string BlogTitle { get; set; }
        [Column("Blog_Author")]
        public string BlogAuthor { get; set; }
        [Column("Blog_Content")]
        public string BlogContent { get; set; }
    }
}