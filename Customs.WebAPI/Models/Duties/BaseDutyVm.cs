using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Customs.WebAPI.Models.Duties
{
    public class BaseDutyVm
    {
        [DisplayName("Номер скалада")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int StorageId { get; set; }

        public List<SelectListItem> Storages { get; set; }

        [DisplayName("Наименование товара")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int ProductId { get; set; }

        public List<SelectListItem> Products { get; set; }

        [DisplayName("Дата поступления товара")]
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime DateOfReceipt { get; set; }

        [DisplayName("Количество поступления товара")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, int.MaxValue)]
        public int AmountOfReceipt { get; set; }

        [DisplayName("Номер таможненного документа")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, int.MaxValue)]
        public int CustomsDocumentNumber { get; set; }

        [DisplayName("Таможненный агент")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int EmployeeId { get; set; }

        public List<SelectListItem> Employees { get; set; }

        [DisplayName("Размер пошлины за весь товар")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, int.MaxValue)]
        public double DutyForAllProducts { get; set; }

        [DisplayName("Дата уплаты пошлины")]
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime DutyPaymentDate { get; set; }

        [DisplayName("Дата вывоза товара")]
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime ExportProductsDate { get; set; }
    }
}