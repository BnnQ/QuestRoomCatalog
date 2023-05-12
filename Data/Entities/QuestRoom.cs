using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Homework.Data.Entities
{
    public class QuestRoom
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Название")]
        [MaxLength(128, ErrorMessage = "Длина названия не должна превышать 128 символов.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Жанр")]
        [MaxLength(128, ErrorMessage = "Длина жанра не должна превышать 128 символов.")]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        [MaxLength(3092, ErrorMessage = "Длина описания не должна превышать 3092 символов.")]
        public string Description { get; set; } = null!;
        
        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Время прохождения в минутах")]
        [Range(10, 3600, ErrorMessage = "Время прохождения должно быть в диапазоне между 10 и 3600.")]
        public int MinutesDuration { get; set; }
        
        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Минимальное количество игроков")]
        [Range(1, 50, ErrorMessage = "Минимальное количество игроков должно быть в диапазоне между 1 и 50.")]
        public int MinimumNumberOfPlayers { get; set; }
        
        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Максимальное количество игроков")]
        [Range(2, 100, ErrorMessage = "Максимальное количество игроков должно быть в диапазоне между 2 и 100.")]
        public int MaximumNumberOfPlayers { get; set; }
        
        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Минимальный возраст игроков")]
        [Range(4, 21, ErrorMessage = "Минимальный возраст игроков быть в диапазоне между 4 и 21.")]
        public int MinimumAge { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Адрес")]
        [MaxLength(256, ErrorMessage = "Длина адреса не должна превышать 256 символов.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Номер телефона")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Это поле должно соответствовать формату номера мобильного телефона.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("E-mail адрес")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Это поле должно соответствовать формату email-адреса.")]
        [MaxLength(256, ErrorMessage = "Длина e-mail адреса не должна превышать 256 символов.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Название компании")]
        [MaxLength(128, ErrorMessage = "Длина названия компании не должна превышать 128 символов.")]
        public string CompanyName { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Рейтинг")]
        [Range(0, 5, ErrorMessage = "Рейтинг должен быть в диапазоне между 0 и 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Уровень страха")]
        [Range(0, 5, ErrorMessage = "Уровень страха быть в диапазоне между 0 и 5.")]
        public int FearLevel { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Сложность")]
        [Range(0, 5, ErrorMessage = "Сложность должна быть в диапазоне между 0 и 5.")]
        public int DifficultyLevel { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        [DisplayName("Цена (грн.)")]
        [DataType(DataType.Currency, ErrorMessage = "Значение цены должно быть денежного формата.")]
        [Range(50, 5000000, ErrorMessage = "Цена должна быть в диапазоне между 50 и 5000000 грн.")]
        public double Price { get; set; }
        
        [DisplayName("Логотип")]
        public string? PathToLogo { get; set; }
    }
}