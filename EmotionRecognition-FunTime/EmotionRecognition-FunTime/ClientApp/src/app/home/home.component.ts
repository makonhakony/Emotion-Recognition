import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
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
