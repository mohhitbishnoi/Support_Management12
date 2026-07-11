using DocumentFormat.OpenXml.ExtendedProperties;
using Domain.Entitis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Persistence.DataContexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet <User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Otp> Otps { get; set; }
        public DbSet<Department> Departments { get; set; }
        public  DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<SlASetting> SlASettings { get; set; }
        public DbSet<Notification> notifications { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<TicketAttechment> ticketsAttechments { get;set; }
        public DbSet<TicketHistroy> ticketsHistroys { get;set; }
        public DbSet<TicketReply> ticketsReplys { get; set; }
        public DbSet<Company> Companys { get; set; }
        
        
    }
}

