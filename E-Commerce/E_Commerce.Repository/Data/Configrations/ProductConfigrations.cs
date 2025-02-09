﻿using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.Configrations
{
    internal class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(product => product.ProductBrand).WithMany()
                .HasForeignKey(product => product.BrandId);

            builder.HasOne(product => product.ProductType).WithMany()
                .HasForeignKey(product => product.TypeId);
            builder.Property(p => p.Price).HasColumnType("decimal(18;5)");
        }
    }
}
