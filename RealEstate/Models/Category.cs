﻿using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? FullImageUrl { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
