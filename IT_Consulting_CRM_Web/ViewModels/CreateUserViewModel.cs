﻿using System.ComponentModel.DataAnnotations;

namespace IT_Consulting_CRM_Web.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Пароль не верный")]
        public string ConfirmPassword { get; set; }
    }
}
