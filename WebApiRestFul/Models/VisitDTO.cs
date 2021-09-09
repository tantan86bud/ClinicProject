using System.Runtime.InteropServices;

// В проектах SDK, таких как этот, некоторые атрибуты сборки, которые ранее определялись
// в этом файле, теперь автоматически добавляются во время сборки и заполняются значениями,
// заданными в свойствах проекта. Подробные сведения о том, какие атрибуты включены
// и как настроить этот процесс, см. на странице: https://aka.ms/assembly-info-properties.


// При установке значения false для параметра ComVisible типы в этой сборке становятся
// невидимыми для компонентов COM. Если вам необходимо получить доступ к типу в этой
// сборке из модели COM, установите значение true для атрибута ComVisible этого типа.

//[assembly: ComVisible(false)]

// Следующий GUID служит для идентификации библиотеки типов typelib, если этот проект
// будет видимым для COM.

//[assembly: Guid("8029638c-9f52-4c68-b84b-3b18e07a5d30")]
namespace WebApiRestFul
{
    public class VisitDTO : Visit
    {
        public string NamePatient { get; set; }
        public int IdPatient { get; set; }
        public  VisitDTO (Visit visit)
        {
            this.Id = visit.Id;
            this.DateVisit = visit.DateVisit;
            this.TypeVisit = visit.TypeVisit;
            this.Diagnosis= visit.Diagnosis;
            this.IdPatient = visit.Patient.Id;
            this.NamePatient = visit.Patient.Name;
        }

    }
}