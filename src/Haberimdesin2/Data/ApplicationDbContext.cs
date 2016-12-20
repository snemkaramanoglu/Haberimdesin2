using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Haberimdesin2.Models;

namespace Haberimdesin2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HaberEntity> Haber { get; set; }
        public DbSet<CommentEntity> Comment { get; set; }
        public DbSet<ImageEntity> Image { get; set; }
        public DbSet<CategoryEntity> Category { get; set; }
        public DbSet<LikeCommentEntity> LikeComment { get; set; }
        public DbSet<DislikeCommentEntity> DislikeComment { get; set; }
        public DbSet<LikeHaberEntity> LikeHaber { get; set; }
        public DbSet<DislikeHaberEntity> DislikeHaber { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        internal void saveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
