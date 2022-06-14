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
    //$.ajax({
    //  method: "POST",
    //  url: "/api/Knowledgebase",
    //  data: jsonData,
    //  contentType: "application/json",
    //  dataType: "json",
    //  beforeSend: function (xhttp: any) {
    //    console.log("ready");
    //  },
    //  success: function () {

    //    alert("Success!");

    //  },
    //  error: function () {
    //    alert("Failed. Status: " + xhttp.status + ".");

    //  }

    //});


    //  $http.post('api/WarehousePlan/AllowWarehousePlan', $scope.modelVM).then(
    //    function (successResponse) {
    //      if (successResponse.status == '200') {
    //        alert("Data Submit Successfully..");
    //      }
    //    },
    //    function (errorResponse) {
    //      alert("Not Successful");
    //    });
    //}
  }
  //constructor(  , @Inject('BASE_URL') baseUrl: string) {
  //  Http.post(baseUrl + 'Knowledgebase', Option: { params: { data: jsonData } }
  //  ).subscribe((result: any) => {
  //    this.response = result;
  //  }, (error: any) => console.error(error));
  //}

  //constructor(private http: HttpClient) { }

  //async ngOnInit() {
  //  await this.loadData();
  //}
  //async loadData() {

  //}


}

interface Text {
  text: string;
}
