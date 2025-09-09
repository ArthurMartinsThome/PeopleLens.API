using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("response")]
    internal class Response
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("TestAppliedId")]
        public int? test_applied_id { get; set; }
        [FilterIdentifier("QuestionId")]
        public int? question_id { get; set; }
        [FilterIdentifier("Value")]
        public string? value { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}