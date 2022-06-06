import { Component, Inject } from '@angular/core';


@Component({
  selector: "app-root",
  templateUrl: "./fetch-data.component.html"
})
export class FetchDataComponent {
  title = "chat-float";
  response = "";
  message = "";
  config = {
    title: "Chat",
    subTitle: "EmotionRecognition"
  };
  setData() {
    this.response = this.message;
    this.message = "";
  }
  getMessage($event: any) {
    console.log($event);
  }
}


