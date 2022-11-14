﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Customs.WebAPI.Models.Employee
{
    public class BaseEmployeeVm
    {
        [DisplayName("Имя")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string LastName { get; set; }

        [DisplayName("Отчество")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string MiddleName { get; set; }

        [DisplayName("Номер удостоверения")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string IdNumber { get; set; }

        [DisplayName("Должность")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Role { get; set; }

        [DisplayName("Обслуживаемый склад")]
        [Required(ErrorMessage = "Обязательное поле")]
        public List<int> StorageId { get; set; }

        public List<SelectListItem> Storages { get; set; }
    }
}