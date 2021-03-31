import { pageService } from "../common/api"
import { RouterPage } from "../common/router/router"
import "../sheets/index.less"
export class HomeIndexPageModel implements RouterPage {
  renderBodyAfter(): void {}

  renderBody() {
    return pageService
      .getIndexPage(10, 1)
      .then(text => {
        return text
      })
      .catch(json => {
        alert(json.message)
        return null
      })
  }
}
