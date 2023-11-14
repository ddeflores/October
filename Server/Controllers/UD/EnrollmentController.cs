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
    public class EnrollmentController : BaseController, GenericRestController<EnrollmentDTO>
    {
        public EnrollmentController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SchoolID}/{StudentID}/{SectionID}")]
        public async Task<IActionResult> Delete(int SchoolID, int StudentID, int SectionID)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments.Where(x => x.SectionId == SectionID).Where(x => x.StudentId == StudentID).Where(x => x.SchoolId == SchoolID).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Enrollments.Remove(itm);
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

                var result = await _context.Enrollments.Select(sp => new EnrollmentDTO
                {
                     SectionId = sp.SectionId,
                      CreatedBy = sp.CreatedBy,
                       CreatedDate = sp.CreatedDate,
                        EnrollDate = sp.EnrollDate,
                         FinalGrade = sp.FinalGrade,
                          ModifiedBy = sp.ModifiedBy,
                           ModifiedDate = sp.ModifiedDate,
                            SchoolId = sp.SchoolId,
                             StudentId = sp.StudentId
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
        [Route("Get/{SchoolID}/{StudentID}/{SectionID}")]
        public async Task<IActionResult> Get(int SchoolID, int StudentID, int SectionID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                EnrollmentDTO? result = await _context
                    .Enrollments
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.StudentId == StudentID)
                     .Select(sp => new EnrollmentDTO
                     {
                        StudentId = sp.StudentId,
                         CreatedBy = sp.CreatedBy,
                          SectionId = sp.SectionId,
                           CreatedDate = sp.CreatedDate,
                            EnrollDate = sp.EnrollDate,
                             FinalGrade = sp.FinalGrade,
                              ModifiedBy = sp.ModifiedBy,
                               ModifiedDate = sp.ModifiedDate,
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
                                                EnrollmentDTO _EnrollmentDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments.Where(x => x.SectionId == _EnrollmentDTO.SectionId).Where(x => x.StudentId == _EnrollmentDTO.StudentId).Where(x => x.SchoolId == _EnrollmentDTO.SchoolId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Enrollment e = new Enrollment
                    {
                        SchoolId = _EnrollmentDTO.SchoolId,
                         StudentId = _EnrollmentDTO.StudentId,
                          SectionId = _EnrollmentDTO.SectionId,
                           CreatedDate = DateTime.Now,
                            CreatedBy = _EnrollmentDTO.CreatedBy,
                             ModifiedDate = DateTime.Now,
                              ModifiedBy = _EnrollmentDTO.ModifiedBy
                    };
                    _context.Enrollments.Add(e);
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
                                                EnrollmentDTO _EnrollmentDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments.Where(x => x.SectionId == _EnrollmentDTO.SectionId).Where(x => x.StudentId == _EnrollmentDTO.StudentId).Where(x => x.SchoolId == _EnrollmentDTO.SchoolId).FirstOrDefaultAsync();

                itm.SectionId = _EnrollmentDTO.SectionId;
                itm.StudentId = _EnrollmentDTO.StudentId;
                itm.SectionId = _EnrollmentDTO.SectionId;
                itm.ModifiedBy = _EnrollmentDTO.ModifiedBy;
                itm.ModifiedDate = DateTime.Now;

                _context.Enrollments.Update(itm);
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
