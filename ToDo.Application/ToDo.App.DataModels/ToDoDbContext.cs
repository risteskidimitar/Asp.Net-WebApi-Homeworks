using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ToDo.App.DataModels
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Tasks { get; set; }
        public DbSet<SubTask> Subtasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder
                .Entity<User>()
                .ToTable(nameof(User))
                .HasKey(p => p.Id);

            modelBuilder
                .Entity<User>()
                .Property(p => p.Username)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder
                .Entity<User>()
                .Property(p => p.Password)
                .HasMaxLength(50)
                .IsRequired();

            #endregion

            #region Task
            modelBuilder
                .Entity<Todo>()
                .ToTable("Tasks")
                .HasKey(p => p.Id);

            modelBuilder
                .Entity<Todo>()
                .Property(p => p.Title)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder
                .Entity<Todo>()
                .Property(p => p.DateCreated)
                .HasDefaultValue();

            modelBuilder
                .Entity<Todo>()
                .HasOne(p => p.User)
                .WithMany(p => p.Tasks)
                .HasForeignKey(p => p.UserId);


            #endregion

            #region SubTask

            modelBuilder
                .Entity<SubTask>()
                .ToTable(nameof(SubTask))
                .HasKey(p => p.Id);

            modelBuilder
                .Entity<SubTask>()
                .Property(p => p.Title)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder
                .Entity<SubTask>()
                .Property(p => p.TaskDescription)
                .HasMaxLength(250)
                .IsRequired();

            modelBuilder
                .Entity<SubTask>()
                .HasOne(p => p.Todo)
                .WithMany(p => p.Subtasks)
                .HasForeignKey(p => p.TodoId);

            #endregion

            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }
        public void Seed(ModelBuilder modelBuilder)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("123Go"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            modelBuilder
                .Entity<User>()
                .HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Dimitar" ,
                    LastName = "Risteski",
                    Username = "dimris",
                    Password = hashedPassword

                }
                );

            modelBuilder.Entity<Todo>()
                .HasData(
                new Todo
                {
                    Id = 1,
                    Title = "Calisthenics Workout",
                    DateCreated = new DateTime(2020, 10, 10),
                    IsCompleted = true,
                    UserId = 1

                },
                new Todo
                {
                     Id = 2,
                     Title = "Working",
                     DateCreated = new DateTime (2020, 10, 13),
                     IsCompleted = false,
                     UserId = 1                   
                }

                );

            modelBuilder.Entity<SubTask>()
                .HasData(
                new SubTask
                {
                    Id = 1,
                    Title = "pullups",
                    IsCompleted = true,
                    TaskDescription = "5x5 pull-aps",
                    TodoId = 1                      
                },
                new SubTask
                {
                    Id = 2,
                    Title = "dips",
                    IsCompleted = true,
                    TaskDescription = "5x10 dips",
                    TodoId = 1
                },
                new SubTask
                {
                    Id = 3,
                    Title = "Quarterly Reports",
                    IsCompleted = false,
                    TaskDescription = "Making portfolio analysis",
                    TodoId = 2                     
                },
                new SubTask
                {
                    Id = 4,
                    Title = "Web Api",
                    IsCompleted = false,
                    TaskDescription = "Studing WebApi at SEDC",
                    TodoId = 2
                }
                );

        }
    }
}
