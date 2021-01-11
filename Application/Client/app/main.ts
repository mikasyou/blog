﻿import '../sheets/main.less';
import { Utility } from '../common/utils';
import { HomeIndexPageModel } from './index';
import { ArticlePage } from './article';
import { GlassEffectBoard } from '../common/GlassEffectBoard'
import { FriendPage } from './friend';
import { Router } from '../common/router/router';


(function () {
  // 初始化路由
  const router = new Router([
    { path: /\/$/, page: new HomeIndexPageModel() },
    { path: /\/index$/, page: new HomeIndexPageModel() },
    { path: /\/article\/.+?\/\d+?$/, page: new ArticlePage() },
    { path: /\/friends$/, page: new FriendPage() },
  ], new HomeIndexPageModel());

  // 竖屏时菜单连接点击用户体验优化
  let menuRouters = document.getElementById("menu-routers");
  menuRouters.addEventListener("click", (e) => {
    if (document.body.clientWidth <= 768) {
      let link = <HTMLElement>(e.composedPath().find(m => (<HTMLElement>m).tagName == "ROUTER"));
      if (link != null) {
        Utility.scrollTo(document.body.clientHeight);
      }
    }
  });

  // 设置背景板特效
  let glassBoard = new GlassEffectBoard({
    id: "animation-board",
    area: { col: 4, row: 10 }
  });
})();




