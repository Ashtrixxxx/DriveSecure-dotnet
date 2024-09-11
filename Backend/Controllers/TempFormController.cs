using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
//    [Route("api/[controller]/[action]")]
 //   [ApiController]
    public class TempFormController : ControllerBase
    {
        private readonly ITempFormDataServices _tempFormServices;

        public TempFormController(ITempFormDataServices tempFormServices)
        {
            _tempFormServices = tempFormServices;
        }

        [HttpGet]
        public async Task<IEnumerable<TempFormData>> GetAllTempForm()
        {
            return await _tempFormServices.GetAllTempFormData();
        }

        [HttpGet("{Id}")]
        public async Task<TempFormData> GetTempForm(int Id)
        {
            return await _tempFormServices.GetTempFormDataById(Id);
        }

        [HttpPost]
        public async Task<TempFormData> CreateTempForm(TempFormData tempForm)
        {
            return await _tempFormServices.AddTempFormData(tempForm);
        }

        [HttpPut]
        public async Task<TempFormData> UpdateTempForm(TempFormData tempForm)
        {
            return await _tempFormServices.UpdateTempFormData(tempForm);
        }

        [HttpDelete]
        public async Task DeleteTempForm(int Id)
        {
            await _tempFormServices.DeleteTempFormData(Id);
        }

    }
}
