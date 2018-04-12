using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataKioskStacks.DataContract;
using DataKioskStacks.DataContract.Admin;

namespace DataKioskStacks.DataManager
{
    public partial class DataKioskEntities : DbContext
    {

        public DataKioskEntities() :
            base("name=DataKioskEntities")
        {
            Configuration.ProxyCreationEnabled = false;
            Database.CommandTimeout = 2000;
        }


        #region User Manager
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<UserLoginActivity> UserLoginActivities { get; set; }
        public DbSet<DeviceAccessAuthorization> DeviceAccessAuthorizations { get; set; }
        #endregion

        #region Kiosk

        //public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        public DbSet<ClientStation> ClientStations { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<LocalArea> LocalAreas { get; set; }
        public virtual DbSet<SerialNumberKeeper> SerialNumberKeepers { get; set; }

        #endregion



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DataKioskEntities>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("EnrollKiosk");

            #region Portal Admins

            modelBuilder.Entity<Role>()
                .HasMany(x => x.UserRoles)
                .WithRequired(x => x.Role)
                .WillCascadeOnDelete(true);

            //modelBuilder.Entity<User>()
            //    .HasMany(x => x.UserRoles)
            //    .WithRequired(x => x.User)
            //    .WillCascadeOnDelete(true);


            //modelBuilder.Entity<Student>()
            //    .HasOptional(s => s.Address) // Mark Address property optional in Student entity
            //    .WithRequired(ad => ad.Student); // mark Student property as required in StudentAddress entity. Cannot save StudentAddress without Student


            //modelBuilder.Entity<User>()
            //    .HasOptional(x => x.UserRole)
            //    .WithRequired(x => x.User);

            //modelBuilder.Entity<UserRole>()
            //   .HasOptional(x => x.User)
            //   .WithRequired(x => x.UserRole);



            modelBuilder.Entity<User>()
                .HasMany(x => x.UserDevices)
                .WithRequired(x => x.User)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasMany(x => x.UserLoginActivities)
                .WithRequired(x => x.User)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<UserDevice>()
                .HasMany(x => x.DeviceAccessAuthorizations)
                .WithRequired(x => x.UserDevice)
                .WillCascadeOnDelete(true);

            #endregion

            #region Relationship - Fluent API

            modelBuilder.Entity<Organization>()
                .HasMany(x => x.Users)
                .WithRequired(x => x.Organization)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Organization>()
                .HasMany(x => x.ClientStations)
                .WithRequired(x => x.Organization)
                .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Enroller>()
            //    .HasMany(x => x.Enrollers)
            //    .WithRequired(x => x.ClientStation)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<ClientStation>()
            //    .HasMany(x => x.Enrollers)
            //    .WithRequired(x => x.ClientStation)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Enroller>()
            //    .HasRequired(x => x.ClientStation)
            //    .WithRequiredPrincipal()
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Enroller>()
                .HasMany(x => x.Beneficiaries)
                .WithRequired(x => x.Enroller)
                .WillCascadeOnDelete(true);


            //modelBuilder.Entity<Beneficiary>()
            //    .Property(x => x._Biometric)
            //    .HasColumnName("BeneficiaryBiometricDetail")
            //    .HasColumnType("text");

            #endregion

        }
    }
}
