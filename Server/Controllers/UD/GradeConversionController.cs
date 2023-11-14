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
    public class GradeConversionController : BaseController, GenericRestController<CourseDTO>
    {
        public GradeConversionController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{LetterGrade}")]
        public async Task<IActionResult> Delete(string LetterGrade)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions.Where(x => x.LetterGrade == LetterGrade).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeConversions.Remove(itm);
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

                var result = await _context.GradeConversions.Select(sp => new GradeConversionDTO
                {
                    LetterGrade = sp.LetterGrade,
                     ModifiedDate = DateTime.Now,
                      ModifiedBy = sp.ModifiedBy,
                       CreatedBy = sp.CreatedBy,
                        CreatedDate = DateTime.Now,
                         GradePoint = sp.GradePoint,
                          MaxGrade = sp.MaxGrade,
                           MinGrade = sp.MinGrade,
                            SchoolId = sp.SchoolId
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
        [Route("Get/{SchoolID}/{LetterGrade}")]
        public async Task<IActionResult> Get(int SchoolID, string LetterGrade)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeConversionDTO? result = await _context
                    .GradeConversions
                    .Where(x => x.LetterGrade == LetterGrade)
                    .Where(x => x.SchoolId == SchoolID)
                     .Select(sp => new GradeConversionDTO
                     {
                         LetterGrade = sp.LetterGrade,
                          SchoolId = sp.SchoolId,
                           MinGrade = sp.MinGrade,
                            MaxGrade = sp.MaxGrade,
                             GradePoint = sp.GradePoint,
                              CreatedBy = sp.CreatedBy,
                                CreatedDate = sp.CreatedDate,
                                ModifiedBy = sp.ModifiedBy,
                                 ModifiedDate = sp.ModifiedDate,
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
                                                GradeConversionDTO _GradeConversionDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions.Where(x => x.LetterGrade == _GradeConversionDTO.LetterGrade).FirstOrDefaultAsync();

                if (itm == null)
                {
                    GradeConversion c = new GradeConversion
                    {
                        LetterGrade = _GradeConversionDTO.LetterGrade,
                        SchoolId = _GradeConversionDTO.SchoolId,
                        CreatedBy = _GradeConversionDTO.CreatedBy,
                        CreatedDate = _GradeConversionDTO.CreatedDate,
                        GradePoint = _GradeConversionDTO.GradePoint,
                        MaxGrade = _GradeConversionDTO.MaxGrade,
                        MinGrade = _GradeConversionDTO.MinGrade,
                        ModifiedBy = _GradeConversionDTO.ModifiedBy,
                        ModifiedDate = _GradeConversionDTO.ModifiedDate
                    };
                    _context.GradeConversions.Add(c);
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
                                                GradeConversionDTO _GradeConversionDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions.Where(x => x.LetterGrade == _GradeConversionDTO.LetterGrade).FirstOrDefaultAsync();

                itm.LetterGrade = _GradeConversionDTO.LetterGrade;
                itm.MaxGrade = _GradeConversionDTO.MaxGrade;
                itm.MinGrade = _GradeConversionDTO.MinGrade;
                itm.GradePoint = _GradeConversionDTO.GradePoint;

                _context.GradeConversions.Update(itm);
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
