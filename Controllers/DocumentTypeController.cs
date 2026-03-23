using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivigest_backend.Application.DTOs.DocumentType;
using Vivigest_backend.Application.Interfaces.IService;

namespace Vivigest_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeService _documentTypeService;

        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _documentTypeService.getAllDocumentTypeService();

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error.Description);
            }

            return Ok(result.Value);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] DocumentTypeRequestDto request)
        {
            var result = await _documentTypeService.addDocumentTypeService(request);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error.Description);
            }

            return Ok(result.Value); // By standard usually 201 Created is returned, but Ok works according to your Result pattern
        }
    }
}
