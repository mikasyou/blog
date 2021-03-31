﻿export class RenderPageResult {
  result: boolean
  renderPage: HTMLDivElement | undefined
  fadePage: HTMLDivElement | undefined

  constructor(result: boolean, renderpage?: HTMLDivElement, fadepage?: HTMLDivElement) {
    this.result = result
    this.renderPage = renderpage
    this.fadePage = fadepage
  }
}

export class PageWrapper {}

export class BlogComment {
  name: string
  website: string
  email: string
  content: string
  articleid: number
}

export class BlogCommentReply {
  name: string
  website: string
  email: string
  content: string
  replyid: number
  replyname: string
  floorid: number
}
