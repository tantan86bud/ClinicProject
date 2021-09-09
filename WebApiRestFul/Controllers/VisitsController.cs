using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// В проектах SDK, таких как этот, некоторые атрибуты сборки, которые ранее определялись
// в этом файле, теперь автоматически добавляются во время сборки и заполняются значениями,
// заданными в свойствах проекта. Подробные сведения о том, какие атрибуты включены
// и как настроить этот процесс, см. на странице: https://aka.ms/assembly-info-properties.


// При установке значения false для параметра ComVisible типы в этой сборке становятся
// невидимыми для компонентов COM. Если вам необходимо получить доступ к типу в этой
// сборке из модели COM, установите значение true для атрибута ComVisible этого типа.
namespace WebApiRestFul
{
    [ApiController]

    [Route("api/[controller]")]
   
    public class VisitsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public VisitsController(ApplicationContext context)
        {
            _context = context;

        }
        [HttpGet]
        public ActionResult<IQueryable<VisitDTO>> Get()
        {
            var result = _context.Visits
                  .Include(u => u.Patient) 
                  as IQueryable<Visit>;
            List<VisitDTO> VisitsDto =new List<VisitDTO>();
            foreach (Visit r in result)
            {
                VisitDTO visitDto = new VisitDTO(r);
                
                visitDto.Patient = null;
                VisitsDto.Add(visitDto);
            }
           

            return Ok(VisitsDto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Visit visit)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == visit.Patient.Id);
            //Patient p = new Patient();
            if (patient != null)
            {
                Visit newvisit = new Visit();
                newvisit = visit;
                newvisit.Patient = patient;

                _context.Visits.Add(newvisit);
                await _context.SaveChangesAsync();
                return newvisit.Id;
            }
            else
            {
                return NotFound();
            }


        }
        [HttpPut]
        public async Task<ActionResult<int>> Put(Visit visit)
        {
            if (_context.Visits.Any(o => o.Id == visit.Id))
            {
                var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == visit.Patient.Id);
                if (patient != null)
                {
                    visit.Patient = patient;
                    visit.PatientId = patient.Id;
                    try
                    {

                        _context.Update(visit);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            return visit.Id;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {


            var visit = await _context.Visits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(visit);
                _context.SaveChanges();
            }

            return Ok();
        }

    }
}