using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Customs.WebAPI.Models.Product
{
    public class CreateProductVm : BaseProductVm
    {
        [DisplayName("Наименование склада")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int StorageId { get; set; }


        public List<SelectListItem> Storages { get; set; }
    }
}