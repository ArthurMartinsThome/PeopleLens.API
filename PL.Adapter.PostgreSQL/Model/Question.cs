using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("question")]
    internal class Question
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("ResponseTypeId")]
        public int? response_type_id { get; set; }
        [FilterIdentifier("QuestionText")]
        public string? question_text { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}