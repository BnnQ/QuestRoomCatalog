using Homework.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework.Data
{
    public class QuestRoomContext : DbContext
    {
           public DbSet<QuestRoom>? QuestRooms { get; set; }

        public QuestRoomContext(DbContextOptions options) : base(options)
        {
            //empty
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestRoom>(questRoomBuilder =>
            {
                questRoomBuilder.HasKey(questRoom => questRoom.Id);

                questRoomBuilder.Property(questRoom => questRoom.Id)
                                .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.Name)
                       .HasColumnType("nvarchar(128)")
                       .IsUnicode()
                       .HasMaxLength(128)
                       .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.Genre)
                       .HasColumnType("nvarchar(128)")
                       .IsUnicode()
                       .HasMaxLength(128)
                       .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.Description)
                       .HasColumnType("nvarchar(3092)")
                       .IsUnicode()
                       .HasMaxLength(3092)
                       .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.MinutesDuration)
                       .HasConversion(minutesDuration => minutesDuration, minutesDuration => Math.Max(10, Math.Min(3600, minutesDuration)))
                       .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.MinimumNumberOfPlayers)
                       .HasConversion(minimumNumberOfPlayers => minimumNumberOfPlayers, minimumNumberOfPlayers => Math.Max(1, Math.Min(50, minimumNumberOfPlayers)))
                       .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.MaximumNumberOfPlayers)
                       .HasConversion(maximumNumberOfPlayers => maximumNumberOfPlayers,
                              maximumNumberOfPlayers => Math.Max(2, Math.Min(100, maximumNumberOfPlayers)))
                       .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.MinimumAge)
                       .HasConversion(minimumAge => minimumAge, minimumAge => Math.Max(4, Math.Min(21, minimumAge)))
                       .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.Address)
                                .HasColumnType("nvarchar(256)")
                                .IsUnicode()
                                .HasMaxLength(256)
                                .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.Email)
                       .HasColumnType("nvarchar(256)")
                       .IsUnicode()
                                .HasMaxLength(256)
                                .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.CompanyName)
                                .HasColumnType("nvarchar(128)")
                                .IsUnicode()
                                .HasMaxLength(128)
                                .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.Rating)
                       .HasConversion(rating => rating, rating => Math.Max(0, Math.Min(5, rating)))
                       .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.FearLevel)
                       .HasConversion(fearLevel => fearLevel, fearLevel => Math.Max(0, Math.Min(5, fearLevel)))
                       .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.DifficultyLevel)
                       .HasConversion(difficultyLevel => difficultyLevel, difficultyLevel => Math.Max(0, Math.Min(5, difficultyLevel)))
                       .IsRequired();

                questRoomBuilder.Property(questRoom => questRoom.PathToLogo)
                                .HasDefaultValue("media/QuestRoom/Logos/stub.jpg")
                                .HasColumnType("nvarchar(256)")
                                .IsUnicode()
                                .HasMaxLength(256);

                questRoomBuilder.Property(questRoom => questRoom.Price)
                       .HasColumnType("money")
                       .HasConversion(price => price, price => Math.Max(50, Math.Min(5000000, price)))
                       .IsRequired();
            });

            var questRooms = new QuestRoom[]
            {
                new("Гарри Поттер: Путешествие в Неверленд", "Квесты приключение / Квесты по мотивам фильмов", "Спустя тысячелетие от основания школы магии случилось непоправимое, портал перехода на платформе 9 3/4 стал работать в одностороннем порядке и отправляет всех проходивших через него в Неверлэнд.\r\nВсе кто покидал волшебный мир уже не могли вернуться обратно, а волшебные палочки чародеев исчезали при переходе. Так постепенно, все живые обитатели покинули волшебный мир, и он стал совсем другим, опустел, потускнел и ему грозит разрушение.\r\nКак бы волшебники не пытались пронести палочки, никому это не удавалось, и только обычный человек способен пронести волшебную палочку при переходе.\r\nУ вас есть возможность получить звание Волшебника и вернуть Хогвартсу былые времена. Для этого необходимо найти и доставить волшебную палочку Альбусу Дамблдору, который так же находится в Неверлэнде.\r\nТеперь судьба всего волшебного мира только в ваших руках. Успеете ли вы пройти до конца чтобы спаси волшебный мир. Поспеши, ведь времени осталось не так много.", 75, 2, 5, 14, "пер. Шевченко 4 (Майдан), район Шевченковский, станция метро Площадь Независимости", "+380673348752", "questtronix@gmail.com", "QuestTronix", 5, 1, 4, 1000, "media/QuestRoom/Logos/harryPotter.jpg"),
                new("Остров проклятых", "Детективные квесты / Квесты по мотивам фильмов", "Вы команда федеральных маршалов, оказавшиеся на так званом \"Острове проклятых\".\r\nЗдесь расположена психиатрическая лечебница для душевнобольных преступников.\r\nВсе бы ничего, но на острове уже который день идет дождь и нарушено энергоснабжение.\r\nДо того как резервное питание иссякнет осталось не более 75 минут, а психов в камерах сдерживает только электричество.\r\nВам необходимо восстановить электроснабжение маяка и выбраться, но единственный выход с острова через причал.\r\nУдачи!", 75, 1, 6, 11, "Anabioz Quest, пер. Шевченко 4 (Майдан), район Шевченковский, станция метро Площадь Независимости", "+380988371520", "adventureMakers@gmail.com", "AdventureMakers", 5, 3, 4, 1000, "media/QuestRoom/Logos/shutterIsland.webp"),
                new("Побег из Шоушенка", "Антуражные квесты / Квесты с актерами", "«Побег из Шоушенка» - это запоминающийся квест, участие в котором захватывает дух. Это приключение по сюжету хита мирового кинематографа (по высоким оценкам IMDB и Кинопоиску) проводится в реальном времени.\r\nОбязательно прими участие в квесте, если:\r\nжаждешь испытать разнообразные яркие эмоции, вступить в непростую схватку со злом и ощутить сладкий вкус победы над ним;\r\nхочешь узнать, чего на самом деле стоит свобода, и понять цену каждого прожитого мгновения;\r\nобожаешь выбираться из непростых ловушек и чувствовать себя самым сильным и смелым.\r\n«Побег из Шоушенка» - квест-комната, которая открывает тебе доступ ко всем этим возможностям. Оказавшись в темнице, подобно настоящему заключенному, ты столкнешься с интересными испытаниями. Эти минуты ты наверняка будешь вспоминать всю жизнь.\r\nУдастся ли тебе вырваться на волю, как это сделал герой кинохита Энди Дюфрейн? Ведь время так скоротечно. Достаточно ли ты смел, чтобы продолжить борьбу, даже если все складывается не в твою пользу?\r\nУ тебя есть единственный способ это узнать – приходи и сыграй с нами.\r\nКвест комната «Побег из Шоушенка»", 75, 2, 6, 15, "ул. Александра Довженко 3, район Соломенский, станция метро Шулявская", "+380637001890", "escapeQuest@gmail.com", "EscapeQuest", 5, 2, 4, 1100, "media/QuestRoom/Logos/shawshankRedemption.webp"),
                new("Не пойман - не вор", "Квесты с ограблением / Квесты следствие и расследование", "Команда грабителей, которые тщательно продумывают каждое свое дело и просчитывают всё до мелочей, получает заказ от человека, у которого есть компромат на них.\r\nОграбление века, взамен на свободу. Ведь нужно украсть, не просто деньги. Для этого дела они разрабатывают сложную и многоходовую комбинацию идеального ограбления.", 80, 2, 7, 14, "ул. Шота Руставели 3, район Печерский, станция метро Дворец спорта", "+380964306062", "questmania@gmail.com", "QuestMania", 5, 1, 5, 1000, "media/QuestRoom/Logos/notCaughtNotThief.webp"),
                new("Экзамен", "Квесты в стиле триллер / Технологичные квесты", "Квест «Экзамен» создан по одноименному культовому фильму 2009 года, персонажи которого проходят необычный кастинг в компанию. Отбор на должность ведется в жестких условиях: по замыслу начальника, это верный способ выявить лучшего сотрудника.\r\nКвест глубоко погружает в атмосферу фильма благодаря взятым из него репликам героев, музыке, а также декорациям, выполненным в стиле кинокартины. Создатели игры предложили самостоятельный, остросюжетный сценарий и соединили его в упоительный коктейль с атмосферой саспенса, которая не покидает таинственную комнату на протяжении всего квеста. Авторские находки не дадут игрокам предугадать финал – в середине действие повернется неожиданным образом.\r\nВсе эти достоинства делают технотриллер “Экзамен” действительно незабываемым развлечением.", 80, 2, 6, 16, "ул. Саксаганского 63/28 , район Соломенский , станция метро Университет", "+380674427888", "questrush@gmail.com", "QuestRush", 4, 4, 5, 900, "media/QuestRoom/Logos/exam.webp"),
                new("Допросная 24", "Квесты следствие и расследование / Детективные квесты", "Накануне! - Совершено несколько жестоких убийств. - Вы в числе подозреваемых! - По результатам допроса, который начнется через час, дело будет закрыто! - Если ты не хочешь стать жертвой продажного правосудия, Торопись, - у тебя всего один час на побег!", 60, 1, 5, 12, "Anabioz Quest, пер. Шевченко 4 (Майдан), район Шевченковский, станция метро Площадь Независимости", "+380937140820", "questfinder@gmail.com", "QuestFinder", 3, 0, 2, 500, "media/QuestRoom/Logos/interrogation24.webp")
                    
            };

            modelBuilder.Entity<QuestRoom>()
                   .HasData(questRooms);

            base.OnModelCreating(modelBuilder);
        }
    }
}