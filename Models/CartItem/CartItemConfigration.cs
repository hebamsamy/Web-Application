using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class CartItemConfigration:IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItem");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(b => b.Quantity).HasDefaultValue(1);
            builder.Property(b => b.SupPrice).IsRequired();
            builder
               .HasOne(e => e.Product)
               .WithMany(e => e.CartLists)
               .HasForeignKey(e => e.ProductID)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
            builder
               .HasOne(e => e.User)
               .WithMany(e => e.CartList)
               .HasForeignKey(e => e.UserID)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        }
    }
}
