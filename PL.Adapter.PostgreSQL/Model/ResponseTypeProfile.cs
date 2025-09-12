using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("response_type")]
    internal class ResponseTypeProfile
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("Name")]
        public string? name { get; set; }
        [FilterIdentifier("Code")]
        public string? code { get; set; }
        [FilterIdentifier("Description")]
        public string? description { get; set; }
    }
}