using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Articles {
    public class ArticleAccessLog {
        public int Id { get; }

        public DateTime CreateData { get; }

        public string Ip { get; }

        // for ef core
        protected ArticleAccessLog() {
            Ip = default!;
            CreateData = default!;
            Id = default!;
        }

        public ArticleAccessLog(string ip) {
            Ip = ip;
            CreateData = DateTime.Now;
        }
    }
}