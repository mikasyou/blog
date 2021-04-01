import { BlogComment, BlogCommentReply } from "./model"
import { setTitle } from "./router/router"

export const AppSettings = {
  UrlEncodeHeader: { "Content-Type": "application/x-www-form-urlencoded" },
  JsonHeader: { "Content-Type": "application/json" },
  ApiIndexPage: "/index",
  ApiArticlePage: "/article",
  ApiFriendPage: "/friends",
  ApiPostComment: "/api/postComment",
  ApiPostCommentReply: "/api/postComment/reply"
}

export async function HtmlResponse(resp: Response, errormsg: string = ""): Promise<string> {
  if (resp.ok) {
    let title = resp.headers.get("title")
    title && setTitle(decodeURI(title).replace(/\+/g, " "))
    let json = await resp.text()
    return json
  } else {
    return Promise.reject({ result: "0", message: "服务器异常", data: null })
  }
}
export async function JsonResponse(resp: Response, errormsg: string = ""): Promise<object> {
  if (resp.ok) {
    let title = resp.headers.get("title")
    title && setTitle(decodeURI(title).replace(/\+/g, " "))
    let json = await resp.json()
    return json
  } else {
    return Promise.reject({ result: "0", message: "服务器异常", data: null })
  }
}

export const pageService = {
  async getIndexPage(rowSize, pageIndex): Promise<string> {
    let resp = await fetch(AppSettings.ApiIndexPage, {
      method: "post",
      headers: AppSettings.UrlEncodeHeader
    })
    return HtmlResponse(resp)
  },

  async getArticlePage(id): Promise<string> {
    let resp = await fetch(AppSettings.ApiArticlePage + "/" + id, {
      method: "post",
      headers: AppSettings.UrlEncodeHeader
    })
    return HtmlResponse(resp)
  },

  async getFriendPage(): Promise<string> {
    let resp = await fetch(AppSettings.ApiFriendPage, {
      method: "post",
      headers: AppSettings.UrlEncodeHeader
    })
    return HtmlResponse(resp)
  }
}

export const apiService = {
  //
  async postComment(comment: BlogComment) {
    let resp = await fetch(AppSettings.ApiPostComment, {
      method: "post",
      headers: AppSettings.JsonHeader,
      body: JSON.stringify(comment)
    })

    return JsonResponse(resp)
  },
  async postCommentReply(comment: BlogCommentReply) {
    let resp = await fetch(AppSettings.ApiPostCommentReply, {
      method: "post",
      headers: AppSettings.JsonHeader,
      body: JSON.stringify(comment)
    })

    return JsonResponse(resp)
  }
}
