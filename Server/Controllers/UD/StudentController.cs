﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Shared;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using AutoMapper;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : BaseController, GenericRestController<StudentDTO>
    {

        public StudentController(OCTOBEROracleContext context,
    IHttpContextAccessor httpContextAccessor,
    IMemoryCache memoryCache)
: base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{StudentID}")]
        public async Task<IActionResult> Delete(int StudentID)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students.Where(x => x.StudentId == StudentID).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Students.Remove(itm);
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

                var result = await _context.Students.Select(sp => new StudentDTO
                {
                    StudentId = sp.StudentId,
                     CreatedBy = sp.CreatedBy,
                      CreatedDate = sp.CreatedDate,
                       Employer = sp.Employer,
                        FirstName = sp.FirstName,
                         LastName = sp.LastName,
                          ModifiedBy = sp.ModifiedBy,
                           ModifiedDate = sp.ModifiedDate,
                            Phone = sp.Phone,
                             RegistrationDate = sp.RegistrationDate,
                              Salutation = sp.Salutation,
                               StreetAddress = sp.StreetAddress,
                                Zip = sp.Zip
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
        [Route("Get/{SchoolID}/{StudentID}")]
        public async Task<IActionResult> Get(int SchoolID, int StudentID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                StudentDTO? result = await _context
                    .Students
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.StudentId == StudentID)
                     .Select(sp => new StudentDTO
                     {
                         StudentId = sp.StudentId,
                          Zip = sp.Zip,
                           CreatedBy = sp.CreatedBy,
                            ModifiedDate = sp.ModifiedDate,
                             CreatedDate = sp.CreatedDate,
                              Employer = sp.Employer,
                               FirstName = sp.FirstName,
                                LastName = sp.LastName,
                                 ModifiedBy = sp.ModifiedBy,
                                  Phone = sp.Phone,
                                   RegistrationDate = sp.RegistrationDate,
                                    Salutation = sp.Salutation,
                                     StreetAddress = sp.StreetAddress
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
                                                StudentDTO _StudentDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students.Where(x => x.StudentId == _StudentDTO.StudentId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Student s = new Student
                    {
                         StudentId = _StudentDTO.StudentId,
                          FirstName = _StudentDTO.FirstName,
                           LastName = _StudentDTO.LastName,
                            Phone = _StudentDTO.Phone,
                             Salutation = _StudentDTO.Salutation
                    };
                    _context.Students.Add(s);
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
                                                Student _StudentDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students.Where(x => x.StudentId == _StudentDTO.StudentId).FirstOrDefaultAsync();

                itm.FirstName = _StudentDTO.FirstName;
                itm.LastName = _StudentDTO.LastName;

                _context.Students.Update(itm);
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

        public Task<IActionResult> Put([FromBody] StudentDTO _T)
        {
            throw new NotImplementedException();
        }
    }
}
