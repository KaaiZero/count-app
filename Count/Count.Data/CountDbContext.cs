using Count.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Count.Data
{
    public class CountDbContext:IdentityDbContext
    {
        public CountDbContext(DbContextOptions<CountDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<BmiUser> BmisUsers { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MealFood> MealFoods { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Post>()
                .HasMany(p => p.Files)
                .WithOne(p => p.Post)
                .HasForeignKey(p => p.PostId); 

            builder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);
            builder.Entity<User>()
                .HasMany(u => u.UserBmis)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);
            builder.Entity<User>()
                .HasMany(u => u.Days)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId);
            builder.Entity<Day>()
                .HasMany(d => d.Meals)
                .WithOne(m => m.Day)
                .HasForeignKey(m => m.DayId);


            //builder.Entity<MealFood>()
            //    .HasKey(mf => new { mf.MealId, mf.FoodId });
            //builder.Entity<MealFood>()
            //    .HasOne<Meal>(f => f.Meal)
            //    .WithMany(m => m.Foods)
            //    .HasForeignKey(f => f.MealId).OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<MealFood>()
            //    .HasOne<Food>(m => m.Food)
            //    .WithMany(f => f.Meals)
            //    .HasForeignKey(m => m.FoodId).OnDelete(DeleteBehavior.Restrict); 


            base.OnModelCreating(builder);
        }
    }
}
