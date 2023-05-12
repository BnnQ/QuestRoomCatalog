﻿// <auto-generated />
using Homework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Homework.Migrations
{
    [DbContext(typeof(QuestRoomContext))]
    partial class QuestRoomContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Homework.Data.Entities.QuestRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(3092)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(3092)");

                    b.Property<int>("DifficultyLevel")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("FearLevel")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("MaximumNumberOfPlayers")
                        .HasColumnType("int");

                    b.Property<int>("MinimumAge")
                        .HasColumnType("int");

                    b.Property<int>("MinimumNumberOfPlayers")
                        .HasColumnType("int");

                    b.Property<int>("MinutesDuration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PathToLogo")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)")
                        .HasDefaultValue("media/QuestRoom/Logos/stub.jpg");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("money");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("QuestRooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "пер. Шевченко 4 (Майдан), район Шевченковский, станция метро Площадь Независимости",
                            CompanyName = "QuestTronix",
                            Description = "Спустя тысячелетие от основания школы магии случилось непоправимое, портал перехода на платформе 9 3/4 стал работать в одностороннем порядке и отправляет всех проходивших через него в Неверлэнд.\r\nВсе кто покидал волшебный мир уже не могли вернуться обратно, а волшебные палочки чародеев исчезали при переходе. Так постепенно, все живые обитатели покинули волшебный мир, и он стал совсем другим, опустел, потускнел и ему грозит разрушение.\r\nКак бы волшебники не пытались пронести палочки, никому это не удавалось, и только обычный человек способен пронести волшебную палочку при переходе.\r\nУ вас есть возможность получить звание Волшебника и вернуть Хогвартсу былые времена. Для этого необходимо найти и доставить волшебную палочку Альбусу Дамблдору, который так же находится в Неверлэнде.\r\nТеперь судьба всего волшебного мира только в ваших руках. Успеете ли вы пройти до конца чтобы спаси волшебный мир. Поспеши, ведь времени осталось не так много.",
                            DifficultyLevel = 4,
                            Email = "questtronix@gmail.com",
                            FearLevel = 1,
                            Genre = "Квесты приключение / Квесты по мотивам фильмов",
                            MaximumNumberOfPlayers = 5,
                            MinimumAge = 14,
                            MinimumNumberOfPlayers = 2,
                            MinutesDuration = 75,
                            Name = "Гарри Поттер: Путешествие в Неверленд",
                            PathToLogo = "media/QuestRoom/Logos/harryPotter.jpg",
                            PhoneNumber = "+380673348752",
                            Price = 1000m,
                            Rating = 5
                        },
                        new
                        {
                            Id = 2,
                            Address = "Anabioz Quest, пер. Шевченко 4 (Майдан), район Шевченковский, станция метро Площадь Независимости",
                            CompanyName = "AdventureMakers",
                            Description = "Вы команда федеральных маршалов, оказавшиеся на так называемом \"Острове проклятых\".\r\nЗдесь расположена психиатрическая лечебница для душевнобольных преступников.\r\nВсе бы ничего, но на острове уже который день идет дождь и нарушено энергоснабжение.\r\nДо того как резервное питание иссякнет осталось не более 75 минут, а психов в камерах сдерживает только электричество.\r\nВам необходимо восстановить электроснабжение маяка и выбраться, но единственный выход с острова через причал.\r\nУдачи!",
                            DifficultyLevel = 4,
                            Email = "adventureMakers@gmail.com",
                            FearLevel = 3,
                            Genre = "Детективные квесты / Квесты по мотивам фильмов",
                            MaximumNumberOfPlayers = 6,
                            MinimumAge = 11,
                            MinimumNumberOfPlayers = 1,
                            MinutesDuration = 75,
                            Name = "Остров проклятых",
                            PathToLogo = "media/QuestRoom/Logos/shutterIsland.webp",
                            PhoneNumber = "+380988371520",
                            Price = 1000m,
                            Rating = 5
                        },
                        new
                        {
                            Id = 3,
                            Address = "ул. Александра Довженко 3, район Соломенский, станция метро Шулявская",
                            CompanyName = "EscapeQuest",
                            Description = "«Побег из Шоушенка» - это запоминающийся квест, участие в котором захватывает дух. Это приключение по сюжету хита мирового кинематографа (по высоким оценкам IMDB и Кинопоиску) проводится в реальном времени.\r\nОбязательно прими участие в квесте, если:\r\nжаждешь испытать разнообразные яркие эмоции, вступить в непростую схватку со злом и ощутить сладкий вкус победы над ним;\r\nхочешь узнать, чего на самом деле стоит свобода, и понять цену каждого прожитого мгновения;\r\nобожаешь выбираться из непростых ловушек и чувствовать себя самым сильным и смелым.\r\n«Побег из Шоушенка» - квест-комната, которая открывает тебе доступ ко всем этим возможностям. Оказавшись в темнице, подобно настоящему заключенному, ты столкнешься с интересными испытаниями. Эти минуты ты наверняка будешь вспоминать всю жизнь.\r\nУдастся ли тебе вырваться на волю, как это сделал герой кинохита Энди Дюфрейн? Ведь время так скоротечно. Достаточно ли ты смел, чтобы продолжить борьбу, даже если все складывается не в твою пользу?\r\nУ тебя есть единственный способ это узнать – приходи и сыграй с нами.\r\nКвест комната «Побег из Шоушенка»",
                            DifficultyLevel = 4,
                            Email = "escapeQuest@gmail.com",
                            FearLevel = 2,
                            Genre = "Антуражные квесты / Квесты с актерами",
                            MaximumNumberOfPlayers = 6,
                            MinimumAge = 15,
                            MinimumNumberOfPlayers = 2,
                            MinutesDuration = 75,
                            Name = "Побег из Шоушенка",
                            PathToLogo = "media/QuestRoom/Logos/shawshankRedemption.webp",
                            PhoneNumber = "+380637001890",
                            Price = 1100m,
                            Rating = 5
                        },
                        new
                        {
                            Id = 4,
                            Address = "ул. Шота Руставели 3, район Печерский, станция метро Дворец спорта",
                            CompanyName = "QuestMania",
                            Description = "Команда грабителей, которые тщательно продумывают каждое свое дело и просчитывают всё до мелочей, получает заказ от человека, у которого есть компромат на них.\r\nОграбление века, взамен на свободу. Ведь нужно украсть, не просто деньги. Для этого дела они разрабатывают сложную и многоходовую комбинацию идеального ограбления.",
                            DifficultyLevel = 5,
                            Email = "questmania@gmail.com",
                            FearLevel = 1,
                            Genre = "Квесты с ограблением / Квесты следствие и расследование",
                            MaximumNumberOfPlayers = 7,
                            MinimumAge = 14,
                            MinimumNumberOfPlayers = 2,
                            MinutesDuration = 80,
                            Name = "Не пойман - не вор",
                            PathToLogo = "media/QuestRoom/Logos/notCaughtNotThief.webp",
                            PhoneNumber = "+380964306062",
                            Price = 1000m,
                            Rating = 5
                        },
                        new
                        {
                            Id = 5,
                            Address = "ул. Саксаганского 63/28 , район Соломенский , станция метро Университет",
                            CompanyName = "QuestRush",
                            Description = "Квест «Экзамен» создан по одноименному культовому фильму 2009 года, персонажи которого проходят необычный кастинг в компанию. Отбор на должность ведется в жестких условиях: по замыслу начальника, это верный способ выявить лучшего сотрудника.\r\nКвест глубоко погружает в атмосферу фильма благодаря взятым из него репликам героев, музыке, а также декорациям, выполненным в стиле кинокартины. Создатели игры предложили самостоятельный, остросюжетный сценарий и соединили его в упоительный коктейль с атмосферой саспенса, которая не покидает таинственную комнату на протяжении всего квеста. Авторские находки не дадут игрокам предугадать финал – в середине действие повернется неожиданным образом.\r\nВсе эти достоинства делают технотриллер “Экзамен” действительно незабываемым развлечением.",
                            DifficultyLevel = 5,
                            Email = "questrush@gmail.com",
                            FearLevel = 4,
                            Genre = "Квесты в стиле триллер / Технологичные квесты",
                            MaximumNumberOfPlayers = 6,
                            MinimumAge = 16,
                            MinimumNumberOfPlayers = 2,
                            MinutesDuration = 80,
                            Name = "Экзамен",
                            PathToLogo = "media/QuestRoom/Logos/exam.webp",
                            PhoneNumber = "+380674427888",
                            Price = 900m,
                            Rating = 4
                        },
                        new
                        {
                            Id = 6,
                            Address = "Anabioz Quest, пер. Шевченко 4 (Майдан), район Шевченковский, станция метро Площадь Независимости",
                            CompanyName = "QuestFinder",
                            Description = "Накануне! - Совершено несколько жестоких убийств. - Вы в числе подозреваемых! - По результатам допроса, который начнется через час, дело будет закрыто! - Если ты не хочешь стать жертвой продажного правосудия, Торопись, - у тебя всего один час на побег!",
                            DifficultyLevel = 2,
                            Email = "questfinder@gmail.com",
                            FearLevel = 0,
                            Genre = "Квесты следствие и расследование / Детективные квесты",
                            MaximumNumberOfPlayers = 5,
                            MinimumAge = 12,
                            MinimumNumberOfPlayers = 1,
                            MinutesDuration = 60,
                            Name = "Допросная 24",
                            PathToLogo = "media/QuestRoom/Logos/interrogation24.webp",
                            PhoneNumber = "+380937140820",
                            Price = 500m,
                            Rating = 3
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
