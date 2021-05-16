import { pageService } from "../common/api"
import { CommentComponent } from "../common/comment/comment"
import { RouterPage } from "../common/router/router"
import "../sheets/article.less"
import "../sheets/misty-light-windows.less"

export class ArticlePage implements RouterPage {
  anchorIndex = 0
  anchorList!: NodeListOf<HTMLAnchorElement>
  renderBody() {
    this.anchorIndex = 0
    const articleId = parseInt(location.pathname.split("/")[3], 10)
    return pageService
      .getArticlePage(articleId)
      .then(text => {
        return text
      })
      .catch(json => {
        alert(json.message)
        return null
      })
  }

  renderBodyAfter(): void {
    const comment = new CommentComponent()

    // 初始化目录跟随（锚点）
    this.anchorList = document.querySelectorAll(".aticle-catalog-ul li a") as NodeListOf<HTMLAnchorElement>
    if (this.anchorList && this.anchorList.length > 0) {
      this.anchorList[0].classList.add("active")
      const anchorHeader: HTMLElement[] = []
      this.anchorList.forEach(value => {
        anchorHeader.push(document.querySelector("a[name=" + value.hash.replace("#", "") + "]")!.parentElement!)
      })

      window.onscroll = (e: Event) => {
        if (scrollY <= anchorHeader[0].offsetTop) {
          this.anchorList[this.anchorIndex].classList.remove("active")
          this.anchorList[0].classList.add("active")
          this.anchorIndex = 0
          return
        }
        for (let i = 0; i < this.anchorList.length; i++) {
          if (i === this.anchorList.length - 1 || (scrollY >= anchorHeader[i].offsetTop && scrollY <= anchorHeader[i + 1].offsetTop)) {
            if (this.anchorIndex === i) {
              return
            }
            this.anchorList[this.anchorIndex].classList.remove("active")
            this.anchorList[i].classList.add("active")
            this.anchorIndex = i
            return
          }
        }
      }
    }
  }
}
