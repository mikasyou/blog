import "./modal-style.less";
export class MinimalModal {
  private static modalHeight = 60;
  private static wrapper: HTMLDivElement;
  private static getWrapper(): HTMLDivElement {
    if (MinimalModal.wrapper == null) {
      let modal = new MinimalModal();

      MinimalModal.wrapper = document.createElement("div");
      MinimalModal.wrapper.classList.add("minimal-modal-wrapper");
      MinimalModal.wrapper.id = "minimal-modal-wrapper";
      document.body.appendChild(MinimalModal.wrapper);
    }
    return MinimalModal.wrapper;
  }

  public static toast(message: string, time: number, classname: string = "info") {
    let count = MinimalModal.getWrapper().childElementCount;
    if (count == 1 && message != "操作过于频繁!") {
      MinimalModal.toast("操作过于频繁!", 2000, "warning");
      return;
    }
    if (count > 1) {
      return;
    }

    let item = <HTMLDivElement>document.createElement("div");
    item.classList.add("minimal-modal-item");
    item.style.top = (this.modalHeight * count) + "px";
    item.innerHTML = `<div class="minimal-modal ${classname}">${message}</div>`;
    MinimalModal.getWrapper().appendChild(item);
    item.clientWidth;
    let m = (<HTMLDivElement>item.firstChild);
    m.classList.add("show");
    setTimeout(() => {
      m.style.opacity = "0";
      setTimeout(() => { item.remove() }, 200);
    }, time);
  }
}


//import "./modal-style.less";
//export class MinimalModal {
//  private static modalHeight = 60;
//  private static wrapper: HTMLDivElement;
//  private static getWrapper(): HTMLDivElement {
//    if (MinimalModal.wrapper == null) {
//      let modal = new MinimalModal();

//      MinimalModal.wrapper = document.createElement("div");
//      MinimalModal.wrapper.classList.add("minimal-modal-wrapper");
//      MinimalModal.wrapper.id = "minimal-modal-wrapper";
//      document.body.appendChild(MinimalModal.wrapper);
//    }
//    return MinimalModal.wrapper;
//  }

//  public static toast(message: string, time: number, classname: string = "info") {
//    let count = MinimalModal.getWrapper().childElementCount;
//    if (count == 1 && message != "操作过于频繁!") {
//      MinimalModal.toast("操作过于频繁!", 2000, "warning");
//      return;
//    }
//    if (count > 1) {
//      return;
//    }

//    let item = <HTMLDivElement>document.createElement("div");
//    item.classList.add("minimal-modal-item");
//    item.style.top = (this.modalHeight * count) + "px";
//    item.innerHTML = `<div class="minimal-modal ${classname}">${message}</div>`;
//    MinimalModal.getWrapper().appendChild(item);
//    item.clientWidth;
//    let m = (<HTMLDivElement>item.firstChild);
//    m.classList.add("show");
//    setTimeout(() => {
//      m.style.opacity = "0";
//      setTimeout(() => { item.remove() }, 200);
//    }, time);


//  }
//}