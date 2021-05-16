export interface RouterPage {
  /**
   * 渲染页面，返回html字符串
   */
  renderBody(): Promise<string | null>
  /**
   * 渲染页面完成后执行
   */
  renderBodyAfter(): void
}

/**
 * 设计页面标题
 */
export function setTitle(title: string) {
  document.title = title
}

export class Router {
  private bodyElement: HTMLElement = document.getElementById("root") as HTMLDivElement
  private currentRouter: { path: string } = { path: window.location.pathname.toLowerCase() }
  /**
   * @param routers  配置路由数组正则
   * @param defaultPage 路由404时,默认路由
   */
  constructor(private routers: { path: RegExp; page: RouterPage }[], private defaultPage: RouterPage) {
    let router = this.routers.find(m => m.path.test(this.getCurrentBrowserPath()))
    if (router == null) {
      router = this.routers[0]
    }
    router.page.renderBodyAfter()
    document.body.addEventListener("click", e => {
      const link = e.composedPath().find(m => (m as HTMLElement).tagName === "ROUTER") as HTMLElement
      // 父级
      if (link != null) {
        console.log("router click")
        this.notify(link.dataset.href)
      }
    })

    // 监听浏览器前进后退事件
    window.onpopstate = (e: PopStateEvent) => {
      this.notify(this.getCurrentBrowserPath(), true)
    }
  }

  push(url: string, title: string = "") {
    window.history.pushState(null, title, url)
  }

  back() {
    window.history.back()
  }

  forward() {
    window.history.forward()
  }

  go(c: number) {
    window.history.go(c)
  }

  /**
   * 通知url变化
   * @param path 要跳转的页面url
   * @param history 是否为浏览历史记录 (用户通过浏览器前进后退跳转，而非超链接跳转)
   */
  notify(path: string | undefined, history: boolean = false) {
    if (path === null || path === undefined) {
      console.log("path is null")
      return
    }
    const title = document.title
    setTitle("正在努力加载... | Kaakira")
    // 当前已经是此页面
    if (path.toLowerCase() === this.currentRouter.path) {
      setTitle(title)
      console.log("already")
      return
    }
    console.log("load")
    const router = this.routers.find(m => m.path.test(path!))
    // 没有此路径页面
    if (!router) {
      console.log("not found")
      return
    }
    this.currentRouter.path = path
    if (!history) {
      this.push(path)
    }
    // 要隐藏的元素
    const fade = this.bodyElement.firstElementChild as HTMLDivElement
    this.hide(500, fade)
    // 要渲染的元素
    const render = document.createElement("div")
    render.classList.add("content-container")
    render.style.opacity = "0"
    // 要渲染的元素
    this.bodyElement.appendChild(render)
    router.page.renderBody().then(html => {
      render.innerHTML = html || ""
      router!.page.renderBodyAfter()
      this.show(500, render)
    })
  }

  private show(duration: number, render: HTMLDivElement) {
    const time = performance.now() + duration
    let frame: number
    const callback = () => {
      const t = duration - (time - performance.now())
      const i = this.easeInOut(t, 0, 1, duration)
      if (t >= duration) {
        render.style.opacity = "1"
        cancelAnimationFrame(frame)
        return
      }
      render.style.opacity = i.toString()
      frame = requestAnimationFrame(callback.bind(this))
    }
    callback()
  }
  private hide(duration: number, render: HTMLDivElement) {
    const time = performance.now() + duration
    let frame: number
    const callback = () => {
      const t = duration - (time - performance.now())
      const i = this.easeOut(t, 0, 1, duration)
      if (t >= duration) {
        render.remove()
        cancelAnimationFrame(frame)
        return
      }
      render.style.opacity = (1 - i).toString()

      frame = requestAnimationFrame(callback.bind(this))
    }
    callback()
  }

  private easeInOut(t: number, b: number, c: number, d: number): number {
    const seed = (t /= d / 2)
    if (seed < 1) {
      return (c / 2) * t * t * t + b
    }
    return (c / 2) * ((t -= 2) * t * t + 2) + b
  }
  private easeOut(t: number, b: number, c: number, d: number) {
    return -c * ((t = t / d - 1) * t * t * t - 1) + b
  }

  private getCurrentBrowserPath() {
    return window.location.pathname.toLowerCase()
  }
}
