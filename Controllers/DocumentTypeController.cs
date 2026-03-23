using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivigest_backend.Application.DTOs.DocumentType;
using Vivigest_backend.Application.Interfaces.IService;

namespace Vivigest_backend.Controllers
{
    /// <summary>
    /// Controller for managing document types.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeService _documentTypeService;

        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }

        /// <summary>
        /// Retrieves all document types.
        /// </summary>
        /// <returns>An IActionResult containing the list of document types.</returns>
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

        /// <summary>
        /// Creates a new document type.
        /// </summary>
        /// <param name="request">The request body containing document type details.</param>
        /// <returns>An IActionResult indicating the creation result.</returns>
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] DocumentTypeRequestDto request)
        {
            var result = await _documentTypeService.addDocumentTypeService(request);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error.Description);
            }

            return Created();
        }

        /// <summary>
        /// Retrieves a document type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the document type.</param>
        /// <returns>An IActionResult containing the document type details.</returns>
        [HttpGet("get/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _documentTypeService.getDocumentTypeByIdService(id);

            // Return 404 if the document type doesn't exist
            if (!result.IsSuccess)
            {
                return NotFound(result.Error.Description);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Updates an existing document type.
        /// </summary>
        /// <param name="id">The unique identifier of the document type to update.</param>
        /// <param name="request">The request body containing the updated details.</param>
        /// <returns>An IActionResult containing the updated document type.</returns>
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] DocumentTypeUpdateRequestDto request)
        {
            var result = await _documentTypeService.updateDocumentTypeService(id, request);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error.Description);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Deletes a document type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the document type to delete.</param>
        /// <returns>An IActionResult containing the deletion result.</returns>
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _documentTypeService.deleteDocumentTypeService(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error.Description);
            }

            return Ok(result.Value);
        }
    }
}
