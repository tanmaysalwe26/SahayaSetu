using Microsoft.EntityFrameworkCore;
using SahayaSetu.Models;

namespace SahayaSetu.Data
{
    public class MyAppDbContext: DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<NGO> NGOs { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<ResourceRequest> ResourceRequests { get; set; }
        public DbSet<VolunteerRequest> VolunteerRequests { get; set; }
        public DbSet<FundraiserRequest> FundraiserRequests { get; set; }
        public DbSet<VolunteerParticipation> VolunteerParticipations { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<ResourceContribution> ResourceContributions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Donor)
                .WithOne(d => d.User)
                .HasForeignKey<Donor>(d => d.UserId);

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasOne(r => r.NGO)
                    .WithMany(n => n.Requests)
                    .HasForeignKey(r => r.NgoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.RequestType)
                    .HasConversion<string>();

                entity.Property(e => e.Status)
                    .HasConversion<string>();
            });

            modelBuilder.Entity<ResourceRequest>()
                .HasOne(rr => rr.Request)
                .WithOne(r => r.ResourceRequest)
                .HasForeignKey<ResourceRequest>(rr => rr.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VolunteerRequest>()
                .HasOne(vr => vr.Request)
                .WithOne(r => r.VolunteerRequest)
                .HasForeignKey<VolunteerRequest>(vr => vr.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FundraiserRequest>()
                .HasOne(fr => fr.Request)
                .WithOne(r => r.FundraiserRequest)
                .HasForeignKey<FundraiserRequest>(fr => fr.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VolunteerParticipation>()
                .HasOne(vp => vp.VolunteerRequest)
                .WithMany(vr => vr.VolunteerParticipations)
                .HasForeignKey(vp => vp.VolunteerRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VolunteerParticipation>()
                .HasOne(vp => vp.Donor)
                .WithMany(d => d.VolunteerParticipations)
                .HasForeignKey(vp => vp.DonorId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Donor)
                .WithMany()
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.FundraiserRequest)
                .WithMany()
                .HasForeignKey(d => d.FundraiserRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ResourceContribution>()
                .HasOne(rc => rc.Donor)
                .WithMany()
                .HasForeignKey(rc => rc.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ResourceContribution>()
                .HasOne(rc => rc.ResourceRequest)
                .WithMany()
                .HasForeignKey(rc => rc.ResourceRequestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
