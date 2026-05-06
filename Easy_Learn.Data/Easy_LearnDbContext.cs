using Easy_Learn.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Easy_Learn.Data
{
    public class Easy_LearnDbContext : IdentityDbContext<UserEntity>
    {
        public Easy_LearnDbContext(DbContextOptions<Easy_LearnDbContext> options) : base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<VideoEntity> Videos { get; set; }
        public DbSet<PrerequisiteEntity> Prerequisites { get; set; }
        public DbSet<RequestForTeacherEntity> RequestForTeachers { get; set; }
        public DbSet<CourseEntityUserEntity> CourseEntityUserEntities { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetailEntity> OrderDetails { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<QuestionCourseEntity> QuestionsCourse { get; set; }
        public DbSet<AnswerQuestionEntity> AnswerQuestions { get; set; }
        public DbSet<FavoriteEntity> Favorites { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<UserEntity>();

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            builder.Entity<CourseEntityUserEntity>()
                .HasKey(c => new
                {
                    c.CourseId,
                    c.UserId
                });

            builder.Entity<FavoriteEntity>().HasKey(f => new
            {
                f.UserId,
                f.CourseId
            });
            builder.Entity<TeacherEntity>().HasData(new TeacherEntity() { UserId = "18149c73-0fa1-4a70-bafe-cacc563aa858" ,Id = 1
            });
            builder.Entity<UserEntity>()
                .HasData( new UserEntity()
                {
                    Id = "18149c73-0fa1-4a70-bafe-cacc563aa858",
                    Age = 18,
                    UserName = "@AbolfazlAdmin",
                    Full_Name = "Abolfazl mohamadi",
                    NormalizedUserName = "@ABOLFAZLADMIN",
                    Email = "abolfazlmohamadi690@gmail.com",
                    NormalizedEmail = "ABOLFAZLMOHAMADI690@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abolfazl@123")
                });

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "a9bd493c-5cf6-4001-bc7a-26344444ba4d",
                    Name = "User",
                    NormalizedName = "USER"
                }, new IdentityRole()
                {
                    Id = "f9a5f5ac-92a0-41bb-8344-01f12b7f4626",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole()
                {
                    Id = "fe85e993-821b-47c7-807f-1ff608fe3c9b",
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "f9a5f5ac-92a0-41bb-8344-01f12b7f4626",
                    UserId = "18149c73-0fa1-4a70-bafe-cacc563aa858"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "a9bd493c-5cf6-4001-bc7a-26344444ba4d",
                    UserId = "18149c73-0fa1-4a70-bafe-cacc563aa858"
                },
                new IdentityUserRole<string>()
                {
                    UserId = "18149c73-0fa1-4a70-bafe-cacc563aa858",
                    RoleId = "fe85e993-821b-47c7-807f-1ff608fe3c9b"
                });

            base.OnModelCreating(builder);
        }

    }
}
