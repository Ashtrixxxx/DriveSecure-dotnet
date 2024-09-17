using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupportDocumentController : ControllerBase
    {
        private readonly ISupportDocumentServices _documentServices;

        public SupportDocumentController(ISupportDocumentServices supportDocument)
        {
            _documentServices =supportDocument;
        }


        [Authorize(Policy = "AdminAndUser")]

        [HttpGet]
        public async Task<IEnumerable<SupportDocuments>> GetAllDocuments()
        {
            return await _documentServices.GetAllSupportDocument();
        }


        [Authorize(Policy = "AdminAndUser")]

        [HttpGet("{Id}")]
        public async Task<SupportDocuments> GetDocuments(int Id)
        {
            return await _documentServices.GetSupportDocumnetsById(Id);
        }


        //[Authorize(Roles = "user")]

        [NonAction]
        public async Task<SupportDocuments> CreateSupportDocuments(SupportDocuments supportDocuments)
        {
            return await _documentServices.AddSupportDocument(supportDocuments);
        }


        [Authorize(Roles = "user")]

        [HttpPut]
        public async Task<SupportDocuments> UpdateSupportDocuments(SupportDocuments supportDocuments)
        {
            return await _documentServices.UpdateSupportDocuments(supportDocuments);
        }


        [Authorize(Roles = "admin")]

        [HttpDelete]
        public async Task DeletePaymentDetails(int Id)
            {
                await _documentServices.DeleteSupportDocument(Id);
        }

    }
}
