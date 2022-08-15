using System;

namespace Blog.Data.Entities {
    public interface IEntity {
        public Guid Id { get; set; }

        public Boolean IsArchived { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}