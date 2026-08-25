using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetworkFVV.Models;

namespace SocialNetworkFVV.Data.Configuration
{
    public class MessageConfuiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Mesages").HasKey(p => p.MessageId);
            builder.Property(x => x.MessageId).UseIdentityColumn();
        }
    }
}
