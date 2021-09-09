using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// В проектах SDK, таких как этот, некоторые атрибуты сборки, которые ранее определялись
// в этом файле, теперь автоматически добавляются во время сборки и заполняются значениями,
// заданными в свойствах проекта. Подробные сведения о том, какие атрибуты включены
// и как настроить этот процесс, см. на странице: https://aka.ms/assembly-info-properties.


// При установке значения false для параметра ComVisible типы в этой сборке становятся
// невидимыми для компонентов COM. Если вам необходимо получить доступ к типу в этой
// сборке из модели COM, установите значение true для атрибута ComVisible этого типа.
namespace WebApiRestFul.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    public class PatientsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PatientsController(ApplicationContext context)
        {
            _context = context;

        }
        [HttpGet]
        public ActionResult<IQueryable<Patient>> Get()
        {
            var result = _context.Patients as IQueryable<Patient>;

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> Post(Patient patient)
        {
             _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return patient;
        }
        [HttpPut]
        public async Task<ActionResult<Patient>> Put(Patient patient)
        {
            if (_context.Patients.Any(o => o.Id == patient.Id))
            {
                         

            try
                {

                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();  
                }
                
            }
            return Ok(patient);    
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var visits = _context.Visits.Where(v => v.Patient.Id == id);
            foreach (var v in visits)
            {
                _context.Remove(v);
            }
            _context.SaveChanges();
            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(patient);
                _context.SaveChanges();
            }

            return Ok();
        }

    }
}