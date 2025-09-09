using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("person_company")]
    internal class PersonCompany
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("PersonId")]
        public int? person_id { get; set; }
        [FilterIdentifier("CompanyId")]
        public int? company_id { get; set; }
        [FilterIdentifier("StatusId")]
        public int? status_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}