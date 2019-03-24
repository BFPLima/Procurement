using Procurement.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Procurement.Model.Entities;

namespace Procurement.Repository.EF
{
    public class ProcurementContext : DataContext // DbContext
    {

        public ProcurementContext() : base("name=ProcurementConnection")//  base("ProcurementConnection")
        {


        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AdminInfo> AdminInfos { get; set; }
        public DbSet<CustumerInfo> CustumerInfos { get; set; }
        public DbSet<SupplierInfo> SupplierInfos { get; set; }

        //Enttities
        public DbSet<ItemType> ItensType { get; set; }
        public DbSet<TemplateItem> ItensModel { get; set; }
        public DbSet<TemplateAttribute> ModelsAttributes { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<SupplierOffer> SupplierOffers { get; set; }

        public DbSet<CustumerOrder> CustumerOrders { get; set; }
        public DbSet<CustumerOrderItem> CustumerOrderItems { get; set; }

        



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            //      modelBuilder.Entity<RoleUser>()
            //.HasKey(c => new { c.RoleID, c.UserID });

            //      modelBuilder.Entity<Role>()
            //          .HasMany(c => c.Users)
            //          .WithRequired()
            //          .HasForeignKey(c => c.UserID);

            //      modelBuilder.Entity<User>()
            //          .HasMany(c => c.Roles)
            //          .WithRequired()
            //          .HasForeignKey(c => c.);

            modelBuilder.Entity<Role>()
               .HasMany<User>(r => r.Users)
               .WithMany(u => u.Roles)
               .Map(ru =>
               {
                   ru.MapLeftKey("RoleID");
                   ru.MapRightKey("UserID");
                   ru.ToTable("RoleUsers");

               });

            //modelBuilder.Entity<User>()
            //   .HasMany(u => u.Roles)
            //   .WithMany(r => r.Users)
            //   .Map(ru =>
            //   {
            //       ru.MapLeftKey("UserID");
            //       ru.MapRightKey("RoleID");
            //       ru.ToTable("RoleUsers");

            //   });






            base.OnModelCreating(modelBuilder);
        }
    }
}
