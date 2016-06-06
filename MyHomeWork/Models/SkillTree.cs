namespace MyHomeWork.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SkillTree : DbContext
    {
        public SkillTree() 
            : base("name=SkillTree")
        {
        }

        public virtual DbSet<AccountBook> AccountBooks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<MyHomeWork.Models.AccountBookViewModel> AccountBookViewModels { get; set; }
    }
}
