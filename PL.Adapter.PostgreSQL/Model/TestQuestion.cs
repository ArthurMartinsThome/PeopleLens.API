using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("test_question")]
    internal class TestQuestion
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("TestId")]
        public int? test_id { get; set; }
        [FilterIdentifier("QuestionId")]
        public int? question_id { get; set; }
    }
}