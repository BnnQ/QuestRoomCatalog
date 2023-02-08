using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Homework.Data.Entities
{
    public class QuestRoom
    {
        public int Id { get; set; }
        
        [Required]
        [DisplayName("Название")]
        [MaxLength(128)]
        public string Name { get; set; } = null!;

        [Required]
        [DisplayName("Жанр")]
        [MaxLength(128)]
        public string Genre { get; set; } = null!;

        [Required]
        [DisplayName("Описание")]
        [MaxLength(3092)]
        public string Description { get; set; } = null!;
        
        [Required]
        [DisplayName("Время прохождения в минутах")]
        [Range(10, 3600)]
        public int MinutesDuration { get; set; }
        
        [Required]
        [DisplayName("Минимальное количество игроков")]
        [Range(1, 50)]
        public int MinimumNumberOfPlayers { get; set; }
        
        [Required]
        [DisplayName("Максимальное количество игроков")]
        [Range(2, 100)]
        public int MaximumNumberOfPlayers { get; set; }
        
        [Required]
        [DisplayName("Минимальный возраст игроков")]
        [Range(4, 21)]
        public int MinimumAge { get; set; }

        [Required]
        [DisplayName("Адрес")]
        [MaxLength(256)]
        public string Address { get; set; } = null!;

        [Required]
        [DisplayName("Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [DisplayName("E-mail адрес")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(256)]
        public string Email { get; set; } = null!;

        [Required]
        [DisplayName("Название компании")]
        [MaxLength(128)]
        public string CompanyName { get; set; } = null!;

        [Required]
        [DisplayName("Рейтинг")]
        [Range(0, 5)]
        public int Rating { get; set; }

        [Required]
        [DisplayName("Уровень страха")]
        [Range(0, 5)]
        public int FearLevel { get; set; }

        [Required]
        [DisplayName("Сложность")]
        [Range(0, 5)]
        public int DifficultyLevel { get; set; }

        [Required]
        [DisplayName("Цена (грн.)")]
        [Range(50, 5000000)]
        public double Price { get; set; }
        
        [Required]
        [DisplayName("Логотип")]
        [MaxLength(256)]
        public string PathToLogo { get; set; } = null!;

        public QuestRoom()
        {
            //empty
        }
        public QuestRoom(string name, string genre, string description, int minutesDuration, int minimumNumberOfPlayers, int maximumNumberOfPlayers, int minimumAge, string address, string phoneNumber, string email, string companyName, int rating, int fearLevel, int difficultyLevel, double price, string pathToLogo)
        {
            Name = name;
            Genre = genre;
            Description = description;
            MinutesDuration = minutesDuration;
            MinimumNumberOfPlayers = minimumNumberOfPlayers;
            MaximumNumberOfPlayers = maximumNumberOfPlayers;
            MinimumAge = minimumAge;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            CompanyName = companyName;
            Rating = rating;
            FearLevel = fearLevel;
            DifficultyLevel = difficultyLevel;
            Price = price;
            PathToLogo = pathToLogo;
        }
        public QuestRoom(int id, string name, string genre, string description, int minutesDuration, int minimumNumberOfPlayers, int maximumNumberOfPlayers, int minimumAge, string address, string phoneNumber, string email, string companyName, int rating, int fearLevel, int difficultyLevel, double price, string pathToLogo) 
            : this(name, genre, description, minutesDuration, minimumNumberOfPlayers, maximumNumberOfPlayers, minimumAge, address, phoneNumber, email, companyName, rating, fearLevel, difficultyLevel, price, pathToLogo)
        {
            Id = id;
        }
    }
}