import "../sheets/index.less";
import { pageService } from '../common/api';
import { RouterPage } from "../common/router/router";
export class HomeIndexPageModel implements RouterPage {
  renderBodyAfter(): void {
  }

  renderBody() {
    return pageService.getIndexPage(10, 1)
      .then(text => {
        return text;
      })
      .catch(json => {
        alert(json.message);
        return null;
      })
  }
}