using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Customs.WebAPI.Models.Duties
{
    public class ResultDutyVm
    {
        public int Id { get; set; }

        [DisplayName("Номер скалада")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string StorageName { get; set; }

        [DisplayName("Наименование товара")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string ProductName { get; set; }

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
        public string FullEmployeeName { get; set; }

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