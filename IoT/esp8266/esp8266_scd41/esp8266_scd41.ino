#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>
#include <Wire.h>
#include "SparkFun_SCD4x_Arduino_Library.h"

const char* ssid     = "Lan Solo";
const char* password = "Easy4You";
const char* publishAdress = "http://192.168.1.10/api/iot";
const char* deviceId = "Viir";
const char* roomName = "Roof";

const int wakeupIntervalUS = 1 * 60 * 1000 * 1000;

//other names
//Siurin
//Rahu
//Axan

SCD4x scd;

//returns between 0 and 100 
int readBatteryLevel() {
  const int MIN_M_VOLTAGE = 526;
  const int MAX_M_VOLTAGE = 888;

  int rawLevel = analogRead(A0);
  return constrain(map(rawLevel, MIN_M_VOLTAGE, MAX_M_VOLTAGE, 0, 100), 0, 100);
}

void prepareData(String* jsonString) {
  StaticJsonDocument<200> doc;

  //while (!scd.dataAvailable()) {}

  doc["RoomName"]     = roomName;
  doc["Battery"]      = readBatteryLevel();
  doc["Temperature"]  = scd.getTemperature();
  doc["Humidity"]     = scd.getHumidity();
  doc["Co2"]          = (float) scd.getCO2();

  serializeJson(doc, *jsonString);
}

void sendData() {
  WiFiClient client;
  //WiFiClientSecure client;
  {
    HTTPClient http;
    //client.setInsecure(); //used to supress certificate checking

    http.begin(client, publishAdress); //"https://ptsv2.com/t/7je9b-1658175607/post");
    http.addHeader("Content-Type", "text/json");

    String data;
    prepareData(&data);
    int httpCode = http.POST(data);
    Serial.println(data);
    Serial.print("Response code: ");
    Serial.println(httpCode);
    http.end();
  }
}

void setup() 
{ 
  Wire.begin();

  Serial.begin(115200);
  delay(1000);
  Serial.println();

  scd.begin();

  WiFi.begin(ssid, password);
  Serial.print("Connecting");
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(200);
    Serial.print(".");
  }
  Serial.println();
  Serial.print("Connected, IP address: ");
  Serial.println(WiFi.localIP());

  sendData();
  Serial.println("Sleeping");
  ESP.deepSleep(wakeupIntervalUS);  
  //RST, GPIO 16 must be connected for the ESP8266 to wake up!!
}

void loop() {
  sendData();
  Serial.println("Alive");
  delay(1 * 60 * 1000);

  // if (scd.dataAvailable()) {
  //   Serial.print("Temp: ");
  //   Serial.print(scd.getTemperature());

  //   Serial.print("Humidity: ");
  //   Serial.print(scd.getHumidity());

  //   Serial.print("Co2: ");
  //   Serial.print(scd.getCO2());
  // }
  // else {
  //   Serial.println("no data availble");
  // }
}


