using PrimeTech.DataAccess.Model;
using System.Text.Json.Serialization;

namespace PrimeTech.ViewModels
{
    public class VmAttributes
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }        
    }
}
