using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("response_option")]
    internal class ResponseOption
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("ResponseId")]
        public int? response_id { get; set; }
        [FilterIdentifier("QuestionResponseOptionId")]
        public int? question_response_option_id { get; set; }
    }
}