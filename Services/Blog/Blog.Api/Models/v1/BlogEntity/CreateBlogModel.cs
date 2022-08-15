using System;
using System.ComponentModel.DataAnnotations;
using Blog.Data.Entities;

namespace Blog.Api.Models.v1 {
    public class CreateBlogModel {
        [Required]
        public string Name { get; set; }

        [Required]
        public BlogType BlogType { get; set; }

        [Required]

        public Guid AuthorId{get;set;}

    }
}