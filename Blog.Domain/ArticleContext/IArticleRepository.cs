using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.ArticleContext
{
    public interface IArticleRepository
    {
        IEnumerable<Article> ListArticle(int? limit = null, int? offset = null);
        Article              FindById(string articleId);
        void                 Save(Article article);
        ArticleComment       FindComment(string commandRootId);
    }
}