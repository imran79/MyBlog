using System;
using System.ComponentModel.DataAnnotations;
using Blog.Data.Entities;

namespace Blog.Api.Models.v1
{
    public class UpdatePostModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]      
        public string Title { get; set; }
        public string Content { get; set; }

        [Required]
        public Guid BlogId{get;set;}
    }
}