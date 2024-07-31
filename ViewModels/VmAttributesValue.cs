using PrimeTech.DataAccess.Model;
using System.Text.Json.Serialization;

namespace PrimeTech.ViewModels
{
    public class VmAttributesValue
    {
        public int Id { get; set; }
        public string AttributeValue { get; set; } = string.Empty;
        public int AttributesId { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        public VmCompany? Company { get; set; }
        public VmAttributes? Attributes { get; set; }
    }
}
