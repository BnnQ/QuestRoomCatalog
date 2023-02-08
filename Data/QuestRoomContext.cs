using Homework.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework.Data
{
    public class QuestRoomContext : DbContext
    {
        public DbSet<QuestRoom> QuestRooms { get; set; } = default!;

        public QuestRoomContext(DbContextOptions options) : base(options)
        {
            //empty
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestRoom>(questRoomBuilder =>
            {
                questRoomBuilder.HasKey(questRoom => questRoom.Id);

                questRoomBuilder.Property<int>(nameof(QuestRoom.Id))
                                .IsRequired();

                questRoomBuilder.Property<string>(nameof(QuestRoom.Name))
                       .IsRequired()
                       .HasColumnType("nvarchar(128)")
                       .HasMaxLength(128);

                questRoomBuilder.Property<string>(nameof(QuestRoom.Genre))
                       .IsRequired()
                       .HasColumnType("nvarchar(128)")
                       .HasMaxLength(128);

                questRoomBuilder.Property<string>(nameof(QuestRoom.Description))
                       .IsRequired()
                       .HasColumnType("nvarchar(3092)")
                       .HasMaxLength(3092);

                questRoomBuilder.Property<int>(nameof(QuestRoom.MinutesDuration))
                       .IsRequired()
                       .HasConversion(minutesDuration => minutesDuration, minutesDuration => Math.Max(10, Math.Min(3600, minutesDuration)));

                questRoomBuilder.Property<int>(nameof(QuestRoom.MinimumNumberOfPlayers))
                       .IsRequired()
                       .HasConversion(minimumNumberOfPlayers => minimumNumberOfPlayers, minimumNumberOfPlayers => Math.Max(1, Math.Min(50, minimumNumberOfPlayers)));

                questRoomBuilder.Property<int>(nameof(QuestRoom.MaximumNumberOfPlayers))
                       .IsRequired()
                       .HasConversion(maximumNumberOfPlayers => maximumNumberOfPlayers, maximumNumberOfPlayers => Math.Max(2, Math.Min(100, maximumNumberOfPlayers)));

                questRoomBuilder.Property<int>(nameof(QuestRoom.MinimumAge))
                       .IsRequired()
                       .HasConversion(minimumAge => minimumAge, minimumAge => Math.Max(4, Math.Min(21, minimumAge)));

                questRoomBuilder.Property<string>(nameof(QuestRoom.Address))
                                .IsRequired()
                                .HasColumnType("nvarchar(256)")
                                .HasMaxLength(256);

                questRoomBuilder.Property<string>(nameof(QuestRoom.Email))
                                .IsRequired()
                                .HasColumnType("nvarchar(256)")
                                .HasMaxLength(256);

                questRoomBuilder.Property<string>(nameof(QuestRoom.CompanyName))
                                .IsRequired()
                                .HasColumnType("nvarchar(128)")
                                .HasMaxLength(128);

                questRoomBuilder.Property<int>(nameof(QuestRoom.Rating))
                       .IsRequired()
                       .HasConversion(rating => rating, rating => Math.Max(0, Math.Min(5, rating)));

                questRoomBuilder.Property<int>(nameof(QuestRoom.FearLevel))
                       .IsRequired()
                       .HasConversion(fearLevel => fearLevel, fearLevel => Math.Max(0, Math.Min(5, fearLevel)));

                questRoomBuilder.Property<int>(nameof(QuestRoom.DifficultyLevel))
                       .IsRequired()
                       .HasConversion(difficultyLevel => difficultyLevel, difficultyLevel => Math.Max(0, Math.Min(5, difficultyLevel)));

                questRoomBuilder.Property<string>(nameof(QuestRoom.PathToLogo))
                                .HasDefaultValue("/media/QuestRoom/Logos/stub.jpg")
                                .HasColumnType("nvarchar(256)")
                                .HasMaxLength(256);

                questRoomBuilder.Property<double>(nameof(QuestRoom.Price))
                       .IsRequired()
                       .HasColumnType("money")
                       .HasConversion(price => price, price => Math.Max(50, Math.Min(5000000, price)));
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}