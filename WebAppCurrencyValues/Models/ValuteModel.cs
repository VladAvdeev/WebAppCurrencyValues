namespace WebAppCurrencyValues.Models
{
    public class ValuteModel
    {
        public string ID { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
        public float Previous { get; set; }
    }
}
