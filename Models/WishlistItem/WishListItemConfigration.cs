using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class WishListItemConfigration:IEntityTypeConfiguration<WishListItem>
    {
        public void Configure(EntityTypeBuilder<WishListItem> builder)
        {
            builder.ToTable("WishListItem");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();

            builder
               .HasOne(e => e.Product)
               .WithMany(e => e.WishLists)
               .HasForeignKey(e => e.ProductID)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
            builder
               .HasOne(e => e.User)
               .WithMany(e => e.WishList)
               .HasForeignKey(e => e.UserID)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        }
    }
}
