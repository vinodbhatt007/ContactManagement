using ContactManagement.DataContract.DAL.Context.Tables;
using System.Data.Entity;

namespace ContactManagement.DataContract.DAL.Context
{
    class ContactManagementContext: DbContext
    {
        public ContactManagementContext()
        {
        }

        public ContactManagementContext(string connString): base(connString)
        {
            Database.SetInitializer<ContactManagementContext>(null);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public DbSet<ContactDetail> ContactDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ContactDetailConfiguration());
        }
    }
}
