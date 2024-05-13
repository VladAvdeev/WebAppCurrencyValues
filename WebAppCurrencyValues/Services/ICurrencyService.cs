using WebAppCurrencyValues.Models;

namespace WebAppCurrencyValues.Services
{
    public interface ICurrencyService
    {
        public Task<IEnumerable<ValuteModel>> GetValuteModels();
        public Task<ValuteModel> GetValuteById(string id);
        //public Task<ValuteModel> GetValuteByCountry(string country);
    }
}
