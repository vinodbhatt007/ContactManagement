namespace ContactManagement.DataContract.DAL.Context.Tables
{
    class ContactDetailConfiguration: System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ContactDetail>
    {
        public ContactDetailConfiguration(): this("dbo")
        {

        }
        public ContactDetailConfiguration(string schemaName)
        {
            ToTable("ContactDetail", schemaName);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("Int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            Property(x => x.PhoneNumber).HasColumnName(@"PhoneNumber").HasColumnType("nvarchar").IsRequired().HasMaxLength(10);
            Property(x => x.Status).HasColumnName(@"Status").HasColumnType("bit").IsRequired();
        }
    }
}
