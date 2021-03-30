import '../sheets/misty-light-windows.less';
import "../sheets/article.less";
import { pageService } from "../common/api";
import { RouterPage } from "../common/router/router";
import { CommentComponent } from "../common/comment/comment";
import { MinimalModal } from "../common/modal/modal";


export class ArticlePage implements RouterPage {
  anchorIndex = 0;
  anchorList: NodeListOf<HTMLAnchorElement>;
  renderBody() {
    this.anchorIndex = 0;
    return pageService.getArticlePage(location.pathname.split("/")[3])
      .then(text => {
        return text;

      })
      .catch(json => {
        alert(json.message);
        return null;
      })
  }


  renderBodyAfter(): void {
    let comment = new CommentComponent();

    // 初始化目录跟随（锚点）
    this.anchorList = document.querySelectorAll(".aticle-catalog-ul li a") as NodeListOf<HTMLAnchorElement>;
    if (this.anchorList && this.anchorList.length > 0) {
      this.anchorList[0].classList.add("active");
      let anchorHeader: HTMLElement[] = [];
      this.anchorList.forEach(value => {
        anchorHeader.push(document.querySelector("a[name=" + value.hash.replace("#", "") + "]").parentElement);
      })

      window.onscroll = (e) => {
        if (scrollY <= anchorHeader[0].offsetTop) {
          this.anchorList[this.anchorIndex].classList.remove("active");
          this.anchorList[0].classList.add("active");
          this.anchorIndex = 0;
          return;
        }
        for (let i = 0; i < this.anchorList.length; i++) {
          if (i == this.anchorList.length - 1 || (scrollY >= anchorHeader[i].offsetTop && scrollY <= anchorHeader[i + 1].offsetTop)) {
            if (this.anchorIndex == i) {
              return;
            }
            this.anchorList[this.anchorIndex].classList.remove("active");
            this.anchorList[i].classList.add("active");
            this.anchorIndex = i;
            return;
          }
        }
      };
    }
  }

}