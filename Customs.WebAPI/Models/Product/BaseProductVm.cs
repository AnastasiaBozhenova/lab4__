using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Customs.WebAPI.Models.Product
{
    public class BaseProductVm
    {
        [DisplayName("Наименование")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }

        [DisplayName("Единица измерения")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, int.MaxValue)]
        public int UnitMeasurement { get; set; }
    }
}