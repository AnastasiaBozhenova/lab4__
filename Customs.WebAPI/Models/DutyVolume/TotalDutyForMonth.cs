using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Customs.WebAPI.Models.DutyVolume
{
    public class TotalDutyForMonth
    {
        [DisplayName("Наименование скалада")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int StorageId { get; set; }

        [DisplayName("Таможненный агент")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int EmployeeId { get; set; }

        [DisplayName("Сумма пошлин за текущий месяц")]
        [Required(ErrorMessage = "Обязательное поле")]
        public double SumOfDutyForMonth { get; set; }

        [DisplayName("Сумма всех пошлин за текущий год")]
        [Required(ErrorMessage = "Обязательное поле")]
        public double TotalDuty { get; set; }

        [DisplayName("Наименование товара")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int ProductId { get; set; }

        public List<SelectListItem> Products { get; set; }

        [DisplayName("Начальная дата")]
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime StartDate { get; set; }

        [DisplayName("Конечная дата")]
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime EndDate { get; set; }
    }
}