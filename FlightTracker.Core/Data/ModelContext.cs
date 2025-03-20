using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlightTracker.Core.Data
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Airport> Airports { get; set; } = null!;
        public virtual DbSet<Bookflight> Bookflights { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Contactinfo> Contactinfos { get; set; } = null!;
        public virtual DbSet<Contactu> Contactus { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Home> Homes { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Testimonial> Testimonials { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userlogin> Userlogins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("User Id=C##TripIn1;Password=Test321;Data Source=localhost:1521/xe");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("C##TRIPIN1")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.HasKey(e => e.Aboutusid)
                    .HasName("SYS_C008834");

                entity.ToTable("ABOUTUS");

                entity.Property(e => e.Aboutusid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUTUSID");

                entity.Property(e => e.Aboutustext)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("ABOUTUSTEXT");

                entity.Property(e => e.Companyname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYNAME");

                entity.Property(e => e.Imagepath1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH1");

                entity.Property(e => e.Imagepath2)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH2");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("ADMIN");

                entity.Property(e => e.Adminid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ADMINID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.ToTable("AIRPORTS");

                entity.Property(e => e.Airportid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AIRPORTID");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Latitude)
                    .HasColumnType("NUMBER(10,8)")
                    .HasColumnName("LATITUDE");

                entity.Property(e => e.Longitude)
                    .HasColumnType("NUMBER(10,8)")
                    .HasColumnName("LONGITUDE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("0\n  ");
            });

            modelBuilder.Entity<Bookflight>(entity =>
            {
                entity.HasKey(e => e.Bookid)
                    .HasName("SYS_C008859");

                entity.ToTable("BOOKFLIGHT");

                entity.Property(e => e.Bookid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BOOKID");

                entity.Property(e => e.Allnumberofpassengers)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ALLNUMBEROFPASSENGERS");

                entity.Property(e => e.Flightid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FLIGHTID");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Bookflights)
                    .HasForeignKey(d => d.Flightid)
                    .HasConstraintName("FK_BOOKFLIGHT_FLIGHTS");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("COMPANY");

                entity.Property(e => e.Companyid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("COMPANYID");

                entity.Property(e => e.Companyname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<Contactinfo>(entity =>
            {
                entity.HasKey(e => e.Contactid)
                    .HasName("SYS_C008839");

                entity.ToTable("CONTACTINFO");

                entity.Property(e => e.Contactid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTACTID");

                entity.Property(e => e.Copyright)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("COPYRIGHT");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Facebooklink)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("FACEBOOKLINK");

                entity.Property(e => e.Instagramlink)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("INSTAGRAMLINK");

                entity.Property(e => e.Location)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Xlink)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("XLINK");
            });

            modelBuilder.Entity<Contactu>(entity =>
            {
                entity.HasKey(e => e.Contactusid)
                    .HasName("SYS_C008826");

                entity.ToTable("CONTACTUS");

                entity.Property(e => e.Contactusid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTACTUSID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Sentdate)
                    .HasColumnType("DATE")
                    .HasColumnName("SENTDATE");

                entity.Property(e => e.Subject)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Contactus)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_CONTACTUS_USERS");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasKey(e => e.Featuresid)
                    .HasName("SYS_C008877");

                entity.ToTable("FEATURES");

                entity.Property(e => e.Featuresid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FEATURESID");

                entity.Property(e => e.Featuresname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FEATURESNAME");

                entity.Property(e => e.Featurestext)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("FEATURESTEXT");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("FLIGHTS");

                entity.Property(e => e.Flightid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FLIGHTID");

                entity.Property(e => e.Arrivalairportid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ARRIVALAIRPORTID");

                entity.Property(e => e.Arrivaltime)
                    .HasPrecision(6)
                    .HasColumnName("ARRIVALTIME");

                entity.Property(e => e.Availableseats)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("AVAILABLESEATS")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.Companyid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("COMPANYID");

                entity.Property(e => e.Departureairportid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DEPARTUREAIRPORTID");

                entity.Property(e => e.Departuretime)
                    .HasPrecision(6)
                    .HasColumnName("DEPARTURETIME");

                entity.Property(e => e.Flightnumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FLIGHTNUMBER");

                entity.Property(e => e.Numberofpassengers)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("NUMBEROFPASSENGERS");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Status)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Arrivalairport)
                    .WithMany(p => p.FlightArrivalairports)
                    .HasForeignKey(d => d.Arrivalairportid)
                    .HasConstraintName("FK_FLIGHTS_ARRIVALAIRPORT");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.Companyid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COMPANYID");

                entity.HasOne(d => d.Departureairport)
                    .WithMany(p => p.FlightDepartureairports)
                    .HasForeignKey(d => d.Departureairportid)
                    .HasConstraintName("FK_FLIGHTS_DEPARTUREAIRPORT");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.ToTable("HOME");

                entity.Property(e => e.Homeid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HOMEID");

                entity.Property(e => e.Additionaltext)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ADDITIONALTEXT");

                entity.Property(e => e.Companyname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYNAME");

                entity.Property(e => e.Experiencetext)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("EXPERIENCETEXT");

                entity.Property(e => e.Footerparagraph)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("FOOTERPARAGRAPH");

                entity.Property(e => e.Imagepath1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH1");

                entity.Property(e => e.Imagepath2)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH2");

                entity.Property(e => e.Imagepath3)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH3");

                entity.Property(e => e.Paragraphbig)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("PARAGRAPHBIG");

                entity.Property(e => e.Paragraphsmall)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("PARAGRAPHSMALL");

                entity.Property(e => e.Traveltext)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TRAVELTEXT");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("INVOICE");

                entity.Property(e => e.Invoiceid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("INVOICEID");

                entity.Property(e => e.Discount)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("DISCOUNT");

                entity.Property(e => e.Filepath)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("FILEPATH");

                entity.Property(e => e.Flightid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FLIGHTID");

                entity.Property(e => e.Invoicedate)
                    .HasColumnType("DATE")
                    .HasColumnName("INVOICEDATE");

                entity.Property(e => e.Paymentid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PAYMENTID");

                entity.Property(e => e.Status)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Tax)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("TAX");

                entity.Property(e => e.Totalamount)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("TOTALAMOUNT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.Flightid)
                    .HasConstraintName("FK_INVOICE_FLIGHTID");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.Paymentid)
                    .HasConstraintName("FK_INVOICE_PAYMENTID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_INVOICE_USERID");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("PAYMENT");

                entity.Property(e => e.Paymentid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYMENTID");

                entity.Property(e => e.Amountpaid)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("AMOUNTPAID");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER(10,3)")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.Cardholdername)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CARDHOLDERNAME");

                entity.Property(e => e.Cardnumber)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Cvc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CVC");

                entity.Property(e => e.Expirydate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EXPIRYDATE");

                entity.Property(e => e.Flightid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FLIGHTID");

                entity.Property(e => e.Paymentdate)
                    .HasColumnType("DATE")
                    .HasColumnName("PAYMENTDATE");

                entity.Property(e => e.Paymentstatus)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PAYMENTSTATUS")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Flightid)
                    .HasConstraintName("FK_PAYMENT_FLIGHTID");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("RESERVATIONS");

                entity.Property(e => e.Reservationid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RESERVATIONID");

                entity.Property(e => e.Flightid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FLIGHTID");

                entity.Property(e => e.Reservationdate)
                    .HasColumnType("DATE")
                    .HasColumnName("RESERVATIONDATE");

                entity.Property(e => e.Totalamount)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("TOTALAMOUNT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Flightid)
                    .HasConstraintName("FK_RESERVATIONS_FLIGHTS");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_RESERVATIONS_USERS");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLES");

                entity.HasIndex(e => e.Rolename, "SYS_C008868")
                    .IsUnique();

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Testimonialid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTIMONIALID");

                entity.Property(e => e.Status)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Testimonialdate)
                    .HasColumnType("DATE")
                    .HasColumnName("TESTIMONIALDATE");

                entity.Property(e => e.Testimonialtext)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TESTIMONIALTEXT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_TESTIMONIAL_USERS");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Flightcount)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("FLIGHTCOUNT");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");
            });

            modelBuilder.Entity<Userlogin>(entity =>
            {
                entity.HasKey(e => e.Loginid)
                    .HasName("SYS_C008882");

                entity.ToTable("USERLOGINS");

                entity.Property(e => e.Loginid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGINID");

                entity.Property(e => e.Adminid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ADMINID");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Userlogins)
                    .HasForeignKey(d => d.Adminid)
                    .HasConstraintName("FK_USERLOGINS_ADMIN");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userlogins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERLOGINS_ROLES");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userlogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_USERLOGINS_USERS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
