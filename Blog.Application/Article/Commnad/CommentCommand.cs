using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Article.Commnad
{
    public record CommentCommand(string Name, string Content,string Email, int? CommentReply);
}
