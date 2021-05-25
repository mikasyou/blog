using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Events {
    public class ArticleAccessedEvent {
        public int ArticleId { get; }
        public string Ip { get; }

        public ArticleAccessedEvent(string ip, int articleId) {
            Ip = ip;
            ArticleId = articleId;
        }
    }
}