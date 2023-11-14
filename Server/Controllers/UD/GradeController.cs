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
    public class GradeController : BaseController, GenericRestController<GradeDTO>
    {
        public GradeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{GradeCodeOccurrence}")]
        public async Task<IActionResult> Delete(int GradeCodeOccurence)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades.Where(x => x.GradeCodeOccurrence == GradeCodeOccurence).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Grades.Remove(itm);
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

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Grades.Select(sp => new GradeDTO
                {
                    GradeCodeOccurrence = sp.GradeCodeOccurrence,
                    Comments = sp.Comments,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    GradeTypeCode = sp.GradeTypeCode,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    NumericGrade = sp.NumericGrade,
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                    StudentId = sp.StudentId,
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
        [Route("Get/{SchoolID}/{GradeCodeOccurrence}")]
        public async Task<IActionResult> Get(int SchoolID, int GradeCodeOccurrence)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeDTO? result = await _context
                    .Grades
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.GradeCodeOccurrence == GradeCodeOccurrence)
                     .Select(sp => new GradeDTO
                     {
                         GradeCodeOccurrence = sp.GradeCodeOccurrence,
                         SchoolId = sp.SchoolId,
                         SectionId = sp.SectionId,
                         StudentId = sp.StudentId,
                         Comments = sp.Comments,
                         CreatedBy = sp.CreatedBy,
                         CreatedDate = sp.CreatedDate,
                         GradeTypeCode = sp.GradeTypeCode,
                         ModifiedBy = sp.ModifiedBy,
                         ModifiedDate = sp.ModifiedDate,
                         NumericGrade = sp.NumericGrade
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
                                                GradeDTO _GradeDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades.Where(x => x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Grade g = new Grade
                    {
                        GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence,
                        NumericGrade = _GradeDTO.NumericGrade,
                        ModifiedDate = _GradeDTO.ModifiedDate,
                        ModifiedBy = _GradeDTO.ModifiedBy,
                        GradeTypeCode = _GradeDTO.GradeTypeCode,
                        CreatedBy = _GradeDTO.CreatedBy,
                        Comments = _GradeDTO.Comments,
                        StudentId = _GradeDTO.StudentId,
                        SchoolId = _GradeDTO.SchoolId,
                        CreatedDate = _GradeDTO.CreatedDate
                    };
                    _context.Grades.Add(g);
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

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody]
                                                GradeDTO _GradeDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades.Where(x => x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence).FirstOrDefaultAsync();

                itm.GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence;
                itm.NumericGrade = _GradeDTO.NumericGrade;
                itm.GradeTypeCode = _GradeDTO.GradeTypeCode;
                itm.SectionId = _GradeDTO.SectionId;

                _context.Grades.Update(itm);
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
    }
}
