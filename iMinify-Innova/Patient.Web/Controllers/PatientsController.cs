using AutoMapper;

using Patients.Core.Services;

using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Patients.Core.Entities;
using Patients.Dto.Dtos;

namespace Patients.Web.Controllers
{
    [RoutePrefix("api/Patients")]
    public class PatientsController : ApiController
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public IHttpActionResult GetPatients()
        {

            try
            {
                List<Patient> patients =  _patientService.GetAll();

                List<PatientDto> patientsDtos = new List<PatientDto>();

                Mapper.Map(patients, patientsDtos);

                return Ok(patientsDtos);

            }
            catch (System.Exception ex)
            {

                throw;
            }
           
        }

        [Route("ById/{id:int}")]
        public async Task<IHttpActionResult> GetPatient(int id)
        {
            Patient patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            PatientDto patientDto = new PatientDto();

            Mapper.Map(patient, patientDto);

            return Ok(patientDto);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostPatient(PatientDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Patient patient = new Patient();
            
            Mapper.Map(patientDto, patient);
                       
            patient = await _patientService.AddAsync(patient);
            patientDto.Id = patient.Id;

            return CreatedAtRoute("ApiRoute", new { id = patientDto.Id }, patientDto);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _patientService.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return _patientService.GetByIdAsync(id) != null;
        }
    }
}