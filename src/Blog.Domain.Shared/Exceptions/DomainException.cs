using System;

namespace Blog.Domain.Shared.Exceptions {
    public class DomainException : Exception {
        private DomainException(string message) : base(message) {

        }


        public static DomainException Illogic(string remark) {
            return new DomainException(remark);
        }
    }


}
