namespace Gearz.Tests
{
    public class ComplexObjectViewModel
    {
        public object ChildObject { get; set; }
        public int Phone1 { get; set; }
        public int Phone2 { get; set; }
        public int Phone3 { get; set; }
        public OfficeViewModel Office { get; set; }
        public bool HasPhones { get; set; }
    }
}