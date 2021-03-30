import { ArticlePage } from "./app/article"
import { FriendPage } from "./app/friend"
import { HomeIndexPageModel } from "./app/index"
import { GlassEffectBoard } from "./common/GlassEffectBoard"
import { Router } from "./common/router/router"
import { Utility } from "./common/utils"
import "./sheets/main.less"
;(function () {
  // 初始化路由
  const router = new Router(
    [
      { path: /\/$/, page: new HomeIndexPageModel() },
      { path: /\/index$/, page: new HomeIndexPageModel() },
      { path: /\/article\/.+?\/\d+?$/, page: new ArticlePage() },
      { path: /\/friends$/, page: new FriendPage() }
    ],
    new HomeIndexPageModel()
  )

  // 竖屏时菜单连接点击用户体验优化
  let menuRouters = document.getElementById("menu-routers")!
  menuRouters.addEventListener("click", e => {
    if (document.body.clientWidth <= 768) {
      let link = <HTMLElement>e.composedPath().find(m => (<HTMLElement>m).tagName == "ROUTER")
      if (link != null) {
        Utility.scrollTo(document.body.clientHeight)
      }
    }
  })

  // 设置背景板特效
  let glassBoard = new GlassEffectBoard({
    id: "animation-board",
    area: { col: 4, row: 10 }
  })
})()
