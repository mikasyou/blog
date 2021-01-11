export class RenderPageResult {
  result: boolean;
  renderPage: HTMLDivElement;
  fadePage: HTMLDivElement;

  constructor(result: boolean, renderpage: HTMLDivElement = null, fadepage: HTMLDivElement = null) {
    this.result = result;
    this.renderPage = this.renderPage;
    this.fadePage = this.fadePage;
  }
}


export class PageWrapper {

}


export class BlogComment {
  name: string;
  website: string;
  email: string;
  content: string;
  articleid: number;
}


export class BlogCommentReply {

  name: string;
  website: string;
  email: string;
  content: string;
  replyid: number;
  replyname: string;
  floorid: number;

}