#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>

const char* ssid     = "Lan Solo";
const char* password = "Easy4You";
const char* publishAdress = "http://192.168.1.10/iot/create";
const char* deviceId = "Viir";
const char* roomName = "Roof";

const int wakeupIntervalUS = 1 * 60 * 1000 * 1000;

//other names
//Siurin
//Rahu
//Axan

void prepareData(String* jsonString) {
  StaticJsonDocument<200> doc;

  doc["RoomName"]     = roomName;
  doc["Battery"]      = 0.1f;
  doc["Temperature"]  = 10.3f;
  doc["Humidity"]     = 0.1f;
  doc["Co2"]          = 1000;

  serializeJson(doc, *jsonString);
}

void sendData() {
  HTTPClient http;
  WiFiClientSecure client;
  client.setInsecure(); //used to 

  http.begin(client, publishAdress);
  http.addHeader("Content-Type", "text/json");

  String data;
  prepareData(&data);
  int httpCode = http.POST(data);
  Serial.print("Response code: ");
  Serial.println(httpCode);
  http.end();
}

void setup()
{
  Serial.begin(115200);
  delay(1000);
  Serial.println();

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
  Serial.println("Still alive :)");

  delay(5000);
}


