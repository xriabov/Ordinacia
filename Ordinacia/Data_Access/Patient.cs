namespace Ordinacia.Data_Access
{
    public class Patient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int InsuranceID { get; set; }
        public int DoctorID { get; set; }
    }
}