using System.Collections.Generic;

namespace Common.Models {
    public class RegisterResult {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}