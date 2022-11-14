using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Customs.WebAPI.Models.Product
{
    public class ResultProductVm : BaseProductVm
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Название склада")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string StorageName { get; set; }

        [DisplayName("Пошлина за единицу измерения товара")]
        [Required(ErrorMessage = "Обязательное поле")]
        public double Duty { get; set; }
    }
}