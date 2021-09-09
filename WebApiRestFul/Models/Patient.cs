using System;
using System.Collections.Generic;
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

//[assembly: Guid("fdc3523b-f543-4805-809e-21b66f85b44e")]
namespace WebApiRestFul
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime DateBirth { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }
        public List<Visit> Visits { get; set; }
        public Patient()
        {
            Visits = new List<Visit>();


        }
    }
}