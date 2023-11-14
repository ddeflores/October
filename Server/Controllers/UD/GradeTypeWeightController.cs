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
    public class GradeTypeWeightController : BaseController, GenericRestController<CourseDTO>
    {
        public GradeTypeWeightController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SchoolID}/{SectionID}/{GradeTypeCode}")]
        public async Task<IActionResult> Delete(int SchoolID, int SectionID, string GradeTypeCode)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.SchoolId == SchoolID).Where(x => x.SectionId == SectionID).Where(x => x.GradeTypeCode == GradeTypeCode).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeTypeWeights.Remove(itm);
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

                var result = await _context.GradeTypeWeights.Select(sp => new GradeTypeWeightDTO
                {
                    GradeTypeCode = sp.GradeTypeCode,
                     SectionId = sp.SectionId,
                      SchoolId = sp.SchoolId,
                       CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                         DropLowest = sp.DropLowest,
                          ModifiedBy = sp.ModifiedBy,
                           ModifiedDate = sp.ModifiedDate,
                            NumberPerSection = sp.NumberPerSection,
                             PercentOfFinalGrade = sp.PercentOfFinalGrade
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
        [Route("Get/{SchoolID}/{CourseNo}")]
        public async Task<IActionResult> Get(int SchoolID, int SectionID, string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeTypeWeightDTO? result = await _context
                    .GradeTypeWeights
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                     .Select(sp => new GradeTypeWeightDTO
                     {
                         GradeTypeCode = sp.GradeTypeCode,
                          SectionId = sp.SectionId,
                           PercentOfFinalGrade=sp.PercentOfFinalGrade,
                            CreatedBy = sp.CreatedBy,
                             CreatedDate = sp.CreatedDate,
                              DropLowest = sp.DropLowest,
                               ModifiedBy = sp.ModifiedBy,
                                ModifiedDate = sp.ModifiedDate,
                                 NumberPerSection = sp.NumberPerSection,
                                  SchoolId = SchoolID,
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
                                                GradeTypeWeightDTO _GradeTypeWeightDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.SchoolId == _GradeTypeWeightDTO.SchoolId).Where(x => x.SectionId == _GradeTypeWeightDTO.SectionId).Where(x => x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode).FirstOrDefaultAsync();

                if (itm == null)
                {
                    GradeTypeWeight g = new GradeTypeWeight
                    {
                        GradeTypeCode = _GradeTypeWeightDTO.GradeTypeCode,
                        DropLowest = _GradeTypeWeightDTO.DropLowest,
                        PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade,
                        NumberPerSection = _GradeTypeWeightDTO.NumberPerSection
                    };
                    _context.GradeTypeWeights.Add(g);
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
                                                GradeTypeWeightDTO _GradeTypeWeightDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.SchoolId == _GradeTypeWeightDTO.SchoolId).Where(x => x.SectionId == _GradeTypeWeightDTO.SectionId).Where(x => x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode).FirstOrDefaultAsync();

                itm.NumberPerSection = _GradeTypeWeightDTO.NumberPerSection;
                itm.PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade;
                itm.DropLowest = _GradeTypeWeightDTO.DropLowest;

                _context.GradeTypeWeights.Update(itm);
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
