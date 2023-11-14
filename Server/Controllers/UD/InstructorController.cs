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
    public class InstructorController : BaseController, GenericRestController<CourseDTO>
    {
        public InstructorController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{InstructorID}")]
        public async Task<IActionResult> Delete(int InstructorID)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors.Where(x => x.InstructorId == InstructorID).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Instructors.Remove(itm);
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

                var result = await _context.Instructors.Select(sp => new InstructorDTO
                {
                    InstructorId = sp.InstructorId,
                     SchoolId = sp.SchoolId,
                      CreatedBy = sp.CreatedBy,
                       CreatedDate = sp.CreatedDate,
                        FirstName = sp.FirstName,
                         LastName = sp.LastName,
                          ModifiedBy = sp.ModifiedBy,
                           ModifiedDate = sp.ModifiedDate,
                            Phone = sp.Phone,
                             Salutation = sp.Salutation,
                              StreetAddress = sp.StreetAddress,
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
        [Route("Get/{SchoolID}/{InstructorID}")]
        public async Task<IActionResult> Get(int SchoolID, int InstructorID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                InstructorDTO? result = await _context
                    .Instructors
                    .Where(x => x.InstructorId == InstructorID)
                    .Where(x => x.SchoolId == SchoolID)
                     .Select(sp => new InstructorDTO
                     {
                         InstructorId = sp.InstructorId,
                          StreetAddress = sp.StreetAddress,
                           Salutation = sp.Salutation,
                            Phone = sp.Phone,
                             ModifiedDate = sp.ModifiedDate,
                              ModifiedBy = sp.ModifiedBy,
                               LastName = sp.LastName,
                                CreatedBy = sp.CreatedBy,
                                 CreatedDate = sp.CreatedDate,
                                  FirstName = sp.FirstName,
                                   SchoolId = SchoolID,
                                    Zip = sp.Zip
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
                                                InstructorDTO _InstructorDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors.Where(x => x.InstructorId == _InstructorDTO.InstructorId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Instructor i = new Instructor
                    {
                        FirstName = _InstructorDTO.FirstName,
                        LastName = _InstructorDTO.LastName,
                        Salutation = _InstructorDTO.Salutation
                    };
                    _context.Instructors.Add(i);
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
                                                InstructorDTO _InstructorDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors.Where(x => x.InstructorId == _InstructorDTO.InstructorId).FirstOrDefaultAsync();

                itm.Phone = _InstructorDTO.Phone;
                itm.Zip = _InstructorDTO.Zip;
                itm.Salutation = _InstructorDTO.Salutation;
                itm.StreetAddress = _InstructorDTO.StreetAddress;

                _context.Instructors.Update(itm);
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
