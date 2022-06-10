import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: "app-root",
  templateUrl: "./fetch-data.component.html"
})
export class FetchDataComponent {
  
  title = "chat-float";
  
  response = "";//AI's response
  //public resp: Text[] = [this.response];
  message = "";//Default response message
  input = ""; //User's input
  json = { question: "" };//json object to be sent

  config = {
    title: "Chat",
    subTitle: "EmotionRecognition"
  };
  setData() {
    //this.response = this.message;
    this.message = "";
  }

  constructor(private http: HttpClient) { }


  getMessage($event: any) {
    this.input = $event;
    console.log(this.input);
    this.json.question = this.input;


    const xhttp = new XMLHttpRequest();
    var jsonData = JSON.stringify(this.json);

    this.http.post('/Knowledgebase/MakeRequest', jsonData)
      .subscribe((result:any) => {
        this.response = result;
        alert('successful');
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
