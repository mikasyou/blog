import "../sheets/friend.less";
import { pageService } from "../common/api";
import { RouterPage } from "../common/router/router";


export class FriendPage implements RouterPage {
  renderBody() {
    console.log("加载页面");

    return pageService.getFriendPage()
      .then(text => {
        return text;

      })
      .catch(json => {
        alert(json.message);
        return null;
      })
  }


  renderBodyAfter(): void {
  }


}