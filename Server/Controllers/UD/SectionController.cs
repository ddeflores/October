﻿using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Models;
using OCTOBER.Shared;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
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
    public class SectionController : BaseController, GenericRestController<SectionDTO>
    {
        public SectionController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }
        [HttpDelete]
        [Route("Delete/{SectionID}")]
        public async Task<IActionResult> Delete(int SectionID)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionId == SectionID).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Sections.Remove(itm);
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

                var result = await _context.Sections.Select(sp => new SectionDTO
                {
                     Capacity = sp.Capacity,
                      CourseNo = sp.CourseNo,
                       CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                         InstructorId = sp.InstructorId,
                          Location = sp.Location,
                           ModifiedBy = sp.ModifiedBy,
                            ModifiedDate = sp.ModifiedDate,
                             SchoolId = sp.SchoolId,
                              SectionId = sp.SectionId,
                               SectionNo = sp.SectionNo,
                                StartDateTime = sp.StartDateTime
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
        [Route("Get/{SchoolID}/{SectionID}")]
        public async Task<IActionResult> Get(int SchoolID, int SectionID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                SectionDTO? result = await _context
                    .Sections
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.SchoolId == SchoolID)
                     .Select(sp => new SectionDTO
                     {
                         Capacity = sp.Capacity,
                          SectionId = sp.SectionId,
                           StartDateTime = sp.StartDateTime,
                            InstructorId = sp.InstructorId,
                             CourseNo = sp.CourseNo,
                              Location = sp.Location

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
                                                SectionDTO _SectionDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionId== _SectionDTO.SectionId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Section s = new Section
                    {
                        Location = _SectionDTO.Location,
                         SectionId= _SectionDTO.SectionId,
                          CourseNo= _SectionDTO.CourseNo,
                           InstructorId= _SectionDTO.InstructorId,
                            StartDateTime= _SectionDTO.StartDateTime,
                             Capacity= _SectionDTO.Capacity,
                    };
                    _context.Sections.Add(s);
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
                                                SectionDTO _SectionDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionId == _SectionDTO.SectionId).FirstOrDefaultAsync();

                itm.Location = _SectionDTO.Location;
                itm.SectionId = _SectionDTO.SectionId;
                itm.CourseNo = _SectionDTO.CourseNo;
                itm.InstructorId = _SectionDTO.InstructorId;
                itm.Capacity = _SectionDTO.Capacity;
                itm.StartDateTime = _SectionDTO.StartDateTime;


                _context.Sections.Update(itm);
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
