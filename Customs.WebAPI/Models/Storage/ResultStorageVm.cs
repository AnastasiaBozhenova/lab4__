using System.Collections.Generic;
using System.ComponentModel;

namespace Customs.WebAPI.Models.Storage
{
    public class ResultStorageVm
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Название склада")]
        public string Name { get; set; }

        [DisplayName("Товары на складе")]
        public List<string> Products { get; set; }
    }
}