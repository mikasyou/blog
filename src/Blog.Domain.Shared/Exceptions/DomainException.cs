using System;

namespace Blog.Domain.Shared.Exceptions {
    public class DomainException : Exception {
        private DomainException(string message) : base(message) {
        }


        public static DomainException Illogical(string remark) {
            return new DomainException(remark);
        }

        public static DomainException NotPersistent(string remark) {
            return new DomainException(remark);
        }
    }
}