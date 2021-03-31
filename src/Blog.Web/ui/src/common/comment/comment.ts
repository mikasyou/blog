import { apiService } from "../api"
import { MinimalModal } from "../modal/modal"
import "./comment-style.less"

export class CommentComponent {
  commentMainForm: HTMLDivElement
  commentFormWrapper: HTMLDivElement
  inputTexts: NodeListOf<HTMLInputElement>
  commentElement: HTMLDivElement
  btnReply: HTMLButtonElement

  reply: {
    replyid: number
    replyname: string
    floorid: number
  }

  // 0 :发表评论,1:回复评论
  replyType: number = 0

  contentText: HTMLTextAreaElement
  nameText: HTMLInputElement
  emailText: HTMLInputElement
  websiteText: HTMLInputElement

  constructor() {
    this.commentFormWrapper = document.getElementById("minimal-comment") as HTMLDivElement
    this.commentMainForm = document.getElementById("comment-main-form") as HTMLDivElement
    this.commentElement = <HTMLDivElement>document.getElementById("comment")
    this.commentElement.addEventListener("click", this.onReplyClick.bind(this))

    this.nameText = <HTMLInputElement>document.getElementById("txt-name")
    this.emailText = <HTMLInputElement>document.getElementById("txt-email")
    this.websiteText = <HTMLInputElement>document.getElementById("txt-website")
    this.contentText = <HTMLTextAreaElement>document.getElementById("txt-content")

    // 输入框小特效
    this.inputTexts = document.querySelectorAll(".minimal-group input")
    this.inputTexts.forEach((value: HTMLInputElement, index) => {
      if (value.value != "") {
        value.classList.add("hasvalue")
      } else {
        value.classList.remove("hasvalue")
      }
      value.addEventListener("change", function (e) {
        let text = <HTMLInputElement>e.target
        if (text.value != "") {
          text.classList.add("hasvalue")
        } else {
          text.classList.remove("hasvalue")
        }
      })
    })

    // 回复按钮事件
    // document.querySelectorAll(".btn-reply")

    // 发表评论按钮
    this.btnReply = <HTMLButtonElement>document.getElementById("post-comment")
    this.btnReply.addEventListener("click", e => {
      this.postComment(e.target as HTMLButtonElement)
    })
  }

  // 点击回复按钮
  onReplyClick(e: MouseEvent) {
    console.log("点击回复")
    let replyBtn = e.composedPath().find((m: HTMLElement) => m.classList && m.classList.contains("btn-reply")) as HTMLLinkElement
    if (replyBtn == null) {
      return
    }
    let activeReplyBtn = document.querySelector(".btn-reply.active") as HTMLElement
    // 回复楼层
    if (!replyBtn.classList.contains("active")) {
      if (activeReplyBtn != null) {
        activeReplyBtn.classList.remove("active")
        activeReplyBtn.innerText = "回复"
      }

      replyBtn.classList.add("active")
      this.commentMainForm.classList.add("active")
      replyBtn.innerText = "取消回复"
      this.replyType = 1
      let comment = e
        .composedPath()
        .find(
          (m: HTMLElement) => m.classList && (m.classList.contains("comment-post") || m.classList.contains("comment-reply"))
        ) as HTMLDivElement
      comment.after(this.commentMainForm)

      this.reply = {
        floorid: parseInt(replyBtn.dataset.floorid!, 10),
        replyid: parseInt(replyBtn.dataset.replyid!, 10),
        replyname: replyBtn.dataset.replyname!
      }
    } else {
      activeReplyBtn.classList.remove("active")
      activeReplyBtn.innerText = "回复"
      this.replyType = 0
      replyBtn.classList.remove("active")
      replyBtn.innerText = "回复"
      this.commentMainForm.classList.remove("active")
      this.commentFormWrapper.insertBefore(this.commentMainForm, this.commentFormWrapper.firstElementChild)
    }
  }
  pattern = {
    email: /^([A-Za-z0-9_\-\.\u4e00-\u9fa5])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,8})$/
  }
  // 发表评论
  postComment(btn: HTMLButtonElement) {
    // 过滤
    if (!this.nameText.value) {
      MinimalModal.toast("写个昵称吧", 2000)
      return
    }
    if (!this.emailText.value) {
      MinimalModal.toast("邮箱不能为空", 2000)
      return
    }
    if (!this.pattern.email.test(this.emailText.value)) {
      MinimalModal.toast("邮箱格式不正确", 2000)
      return
    }
    if (!this.contentText.value) {
      MinimalModal.toast("不会是忘记写评论了吧", 2000)
      return
    }
    if (this.replyType === 0) {
      apiService
        .postComment({
          content: this.contentText.value,
          email: this.emailText.value,
          name: this.nameText.value,
          website: this.websiteText.value,
          articleid: parseInt(btn.dataset.article!, 10)
        })
        .then(json => {
          MinimalModal.toast("评论成功，后台审核中", 2000, "success")
        })
        .catch(json => {
          MinimalModal.toast("牙白...出现bug,评论失败", 2000, "error")
        })
    } else {
      apiService
        .postCommentReply({
          content: this.contentText.value,
          email: this.emailText.value,
          name: this.nameText.value,
          website: this.websiteText.value,
          floorid: this.reply.floorid,
          replyid: this.reply.replyid,
          replyname: this.reply.replyname
        })
        .then(json => {
          MinimalModal.toast("评论成功，后台审核中", 2000, "success")
        })
        .catch(json => {
          MinimalModal.toast("牙白...出现bug,评论失败", 2000, "error")
        })
    }
  }
}
