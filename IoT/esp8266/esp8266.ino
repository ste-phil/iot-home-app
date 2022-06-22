#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>

const char* ssid     = "Lan Solo";
const char* password = "Easy4You";
const char* publishAdress = "http://192.168.1.10/iot/data";
const char* deviceId = "Viir";
const char* roomName = "Roof";

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

void setup()
{
  Serial.begin(115200);
  Serial.println();

  WiFi.begin(ssid, password);

  Serial.print("Connecting");
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }
  Serial.println();

  HTTPClient http;
  WiFiClient client;
  http.begin(client, publishAdress);
  http.addHeader("Content-Type", "text/plain");

  String data;
  prepareData(&data);
  int httpCode = http.POST(data);
  http.end();
  
  Serial.print("Connected, IP address: ");
  Serial.println(WiFi.localIP());
}

void loop() {}


