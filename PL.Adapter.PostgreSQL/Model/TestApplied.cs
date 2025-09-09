using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("test_applied")]
    internal class TestApplied
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("StatusId")]
        public int? status_id { get; set; }
        [FilterIdentifier("CompanyTestId")]
        public int? company_test_id { get; set; }
        [FilterIdentifier("PersonId")]
        public int? person_id { get; set; }
        [FilterIdentifier("DateBegin")]
        public DateTime? date_begin { get; set; }
        [FilterIdentifier("DateEnd")]
        public DateTime? date_end { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}