using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gouda.Persistence.EntityConfigurations.Implementation
{
    using Gouda.Domain;
    using ValueConverters.Implementation;

    public class SatelliteConfiguration : IEntityTypeConfiguration<Satellite>
    {
        public void Configure(EntityTypeBuilder<Satellite> builder)
        {
            builder.Property(nameof(Satellite.IPAddress))
                .HasConversion(IPAddressValueConverter.Instance)
                .HasMaxLength(4);
        }
    }
}
