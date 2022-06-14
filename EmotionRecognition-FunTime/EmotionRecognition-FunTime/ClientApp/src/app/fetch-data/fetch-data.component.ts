import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: "app-root",
  templateUrl: "./fetch-data.component.html",
  styleUrls: ["./fetch-data.css"]
})
export class FetchDataComponent {
  baseUrl = ""
  title = "chat-float";
  
  response = "";//AI's response
  //public resp: Text[] = [this.response];
  message = "";//Default response message
  input = ""; //User's input
  json = { "answer": ""};//json object to be sent

  config = {
    title: "Chat",
    subTitle: "EmotionRecognition"
  };
  setData() {
    //this.response = this.message;
    this.message = "";
  }
  
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


  getMessage($event: any) {
    this.input = $event;
    //console.log(this.input);
    //this.json.question = this.input;


    const xhttp = new XMLHttpRequest();
    var jsonData = JSON.stringify(this.json);
    

    let formData = new FormData();
    formData.append("question", this.input);


    this.http.post(this.baseUrl + 'Knowledgebase/MakeRequest', formData)
      .subscribe((result: any) => {
        //let res = JSON.stringify(result);
        console.log(result);
        console.log(result.answers[0].answer);
        
        this.response = JSON.stringify(result.answers[0].answer);
        
        
        //alert('successful');
      });


  }


}

interface Text {
  text: string;
}
