using PrimeTech.DataAccess.Model;

namespace PrimeTech.ViewModels
{
    public class VmCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual List<VmAttributesValue>? VmAttributesValue { get; set; }
    }
}
