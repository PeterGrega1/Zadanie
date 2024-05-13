using Zadanie.Models;
using Zadanie.Services;
using Microsoft.AspNetCore.Mvc;

namespace Zadanie.Controllers
{
    public class HomeController : Controller
    {

        public readonly IServices _services;

        public HomeController(IServices services)
        {
            _services = services;
        }

        [HttpPost]
        public bool SaveData([FromBody] List<DataModelDbo> data)
        {
            bool Success;
            if (data.Equals(null))
                throw new ArgumentNullException("Chyba pri poslaní formulárov");

            try
            {
                Success = _services.Generate(data);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Chyba pri spracovaní ", ex);
            }

            return Success;
        }
    }
}
