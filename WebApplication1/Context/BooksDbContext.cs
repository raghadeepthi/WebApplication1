using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Context
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
        {
       
        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                    new Author 
                    {
                        Id = Guid.Parse("937FC084-97A7-4F07-868E-125F8B460C8F"), 
                        FirstName = "firrstName1", 
                        LastName = "lastName1" 
                    }
                    //new Author 
                    //{ 
                    //    Id = Guid.Parse("B9D1967A-8A50-4451-A096-7B47347FF1E30"), 
                    //    FirstName = "firstName2", 
                    //    LastName = "lastName2" 
                    //},
                    //new Author 
                    //{ 
                    //    Id = Guid.Parse("C9D9B189-9812-4C18-93FC-D74E475CACE7"), 
                    //    FirstName = "firstName3", 
                    //    LastName = "lastName3" 
                    //}
                    
                    );
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    id = Guid.Parse("6ECAFF9F-B169-4832-B0F4-0293E8D289A1"),
                    Description = "FirstBookDescription",
                    Title = "FirstBook",
                    AuthorId = Guid.Parse("937FC084-97A7-4F07-868E-125F8B460C8F")
                }
                //new Book
                //{
                //    id = Guid.Parse("33CF978D-EE40-470A-8206-501C77F38D5A"),
                //    Description = "SecondBookDescription",
                //    Title = "SecondBook",
                //    AuthorId = Guid.Parse("B9D1967A-8A50-4451-A096-7B47347FF1E30")
                //},
                //new Book
                //{
                //    id = Guid.Parse("16DCD32D-F8A2-410E-A384-C20775611272"),
                //    Description = "ThirdBookDescription",
                //    Title = "ThirdBook",
                //    AuthorId = Guid.Parse("C9D9B189-9812-4C18-93FC-D74E475CACE7")
                //}
                );
            base.OnModelCreating(modelBuilder);

 
             
        }
    }

}
