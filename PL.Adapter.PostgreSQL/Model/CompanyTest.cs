using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("company_test")]
    internal class CompanyTest
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("TestId")]
        public int? test_id { get; set; }
        [FilterIdentifier("CompanyId")]
        public int? company_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}