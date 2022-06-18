//import { Component } from '@angular/core';

//@Component({
//  selector: 'app-home',
//  templateUrl: './home.component.html',
//})
//export class HomeComponent {
//  title = "chat-float";
//  response = "";
//  message = "";
//  config = {
//    title: "Chat",
//    subTitle: "EmotionRecognition"
//  };
//  setData() {
//    this.response = this.message;
//    this.message = "";
//  }
//  getMessage($event: any) {
//    console.log($event);
//  }
//}


import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: "app-home",
  templateUrl: "./home.component.html"
})


export class HomeComponent {
  baseUrl = ""
  title = "chat-float";

  response = "";//AI's response
  //public resp: Text[] = [this.response];
  message = "";//Default response message
  input = ""; //User's input
  json = { "answer": "" };//json object to be sent
  userId = ""
  followedUpQuestion = ""


  config = {
    title: "Chat",
    subTitle: "My Friendly Bot. Tell me your story"
  };
  setData() {
    this.message = "";
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    var text = "blah blah";
    var strTest = "This is a `${text}`";
    console.log(strTest);
  }

  private extractAnalytics(result: any) : string{
    let analytics = result["questionAnalytics"]
    let text = analytics["sentiment"]
    if (analytics["location"]){
      text += "-" + "place"
    }

    if (analytics["name"]){
      text += "-" + "name"
    }

    if (analytics["time"]){
      text += "-" + "date"
    }

    return text
  }

  public getMessage($event: any) {
    this.input = $event;

    let formDataAnalyics = new FormData();
    formDataAnalyics.append("Text", this.input);
    if (this.userId){
      formDataAnalyics.append("QuestionId", this.followedUpQuestion);
    }
    if (this.followedUpQuestion){
      formDataAnalyics.append("UserId", this.userId);
    }


    this.http.post(this.baseUrl+ 'textanalysis/PostAnalytics', formDataAnalyics).subscribe((analyticsRes:any)=>{
      if (!analyticsRes["hasUser"]){
        this.userId = analyticsRes["userId"]
      }
      if (analyticsRes["isFollowing"]){
        this.followedUpQuestion = analyticsRes["id"]
      }
      let formDataQnA = new FormData();
      formDataQnA.append("question", this.input);
      formDataQnA.append("Text", this.extractAnalytics(analyticsRes));
      formDataQnA.append("Location", analyticsRes["questionAnalytics"]["location"] ?? null);
      formDataQnA.append("Name", analyticsRes["questionAnalytics"]["name"] ?? null);
      formDataQnA.append("Time", analyticsRes["questionAnalytics"]["time"] ?? null);


      this.http.post(this.baseUrl + 'Knowledgebase/MakeRequest', formDataQnA)
      .subscribe((result: any) => {
        console.log(result);
        this.response = result["text"];
      });
    })
  }
}
