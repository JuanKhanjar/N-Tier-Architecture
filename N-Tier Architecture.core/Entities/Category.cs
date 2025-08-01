﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace N_Tier_Architecture.core.Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }=  Guid.NewGuid();

        [Required]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public required string CategoryName { get; set; }

        public string? CategoryDescription { get; set; }

        public string? CategoryImageUrl { get; set; } //if it is null then provide a default image

        public ICollection<Product>? Products { get; set; } = [];

        public CategoryProductsSummary? Summary { get; set; }

    }
}

/*
 
 modelBuilder.Entity<Category>()
    .HasIndex(c => c.CategoryName)
    .IsUnique(); // Ensure unique category names for integrity and faster lookups

 */