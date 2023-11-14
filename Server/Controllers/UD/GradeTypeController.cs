using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using System.Diagnostics;

namespace OCTOBER.Server.Controllers.UD
{
    public class GradeTypeController : BaseController, GenericRestController<CourseDTO>
    {
        public GradeTypeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{GradeTypeCode}")]
        public async Task<IActionResult> Delete(string GradeTypeCode)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypes.Where(x => x.GradeTypeCode == GradeTypeCode).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeTypes.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.GradeTypes.Select(sp => new GradeTypeDTO
                {
                    CreatedDate = DateTime.Now,
                     GradeTypeCode = sp.GradeTypeCode,
                      SchoolId = sp.SchoolId,
                       CreatedBy = sp.CreatedBy,
                        Description = sp.Description,
                         ModifiedBy = sp.ModifiedBy,
                          ModifiedDate = DateTime.Now,
                })
                .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get/{SchoolID}/{GradeTypeCode}")]
        public async Task<IActionResult> Get(int SchoolID, string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeTypeDTO? result = await _context
                    .GradeTypes
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Where(x => x.SchoolId == SchoolID)
                     .Select(sp => new GradeTypeDTO
                     {
                         GradeTypeCode = sp.GradeTypeCode,
                          ModifiedDate = sp.ModifiedDate,
                           ModifiedBy = sp.ModifiedBy,
                            Description= sp.Description,
                             SchoolId = sp.SchoolId
                     })
                .SingleOrDefaultAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody]
                                                GradeTypeDTO _GradeTypeDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypes.Where(x => x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode).FirstOrDefaultAsync();

                if (itm == null)
                {
                    GradeType g = new GradeType
                    {
                        GradeTypeCode = _GradeTypeDTO.GradeTypeCode,
                         SchoolId = _GradeTypeDTO.SchoolId,
                          CreatedDate = DateTime.Now,
                           CreatedBy = _GradeTypeDTO.CreatedBy,
                            Description = _GradeTypeDTO.Description,
                             ModifiedBy = _GradeTypeDTO.ModifiedBy,
                               ModifiedDate = _GradeTypeDTO.ModifiedDate
                    };
                    _context.GradeTypes.Add(g);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Post([FromBody] CourseDTO _T)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody]
                                                GradeTypeDTO _GradeTypeDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypes.Where(x => x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode).FirstOrDefaultAsync();

                itm.GradeTypeCode = _GradeTypeDTO.GradeTypeCode;
                itm.SchoolId = _GradeTypeDTO.SchoolId;
                itm.Description = _GradeTypeDTO.Description;


                _context.GradeTypes.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Put([FromBody] CourseDTO _T)
        {
            throw new NotImplementedException();
        }
    }
}
