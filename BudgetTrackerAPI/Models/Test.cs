using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTrackerAPI.Models
{
    public class Test
    {
        [Key]
        [Column("testId")]
        public int testId {  get; set; }

        [Column("Name")]
        public string name { get; set; } = "";

        [Column("Value")]
        public float value { get; set; }

        [Column("Type")]
        public string type { get; set; } = "";
    }

    public class TestCreateDto {
        public string name { get; set; } = "";
        public float value { get; set; }
        public string type { get; set; } = "";
    }

    public class TestDeleteDto
    {
        public int testId { get; set; }
    }
}
