#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>

const char* ssid     = "Lan Solo";
const char* password = "Easy4You";

void sendData(char* data) {
  HTTPClient http;
  WiFiClientSecure client;
  client.setInsecure(); //used to 

  http.begin(client, "https://ptsv2.com/t/9rumn-1656244987/post");
  http.addHeader("Content-Type", "text/json");

  int httpCode = http.POST(data);
  Serial.print("Response code: ");
  Serial.println(httpCode);
  http.end();
}


void setup() {
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
}

void loop() 
{
    int rawLevel = analogRead(A0);

    // the 10kΩ/47kΩ voltage divider reduces the voltage, so the ADC Pin can handle it
    // According to Wolfram Alpha, this results in the following values:
    // 10kΩ/(47kΩ+10kΩ)*  5v = 0.8772v
    // 10kΩ/(47kΩ+10kΩ)*3.7v = 0.649v
    // 10kΩ/(47kΩ+10kΩ)*3.1v = 0.544
    // * i asumed 3.1v as minimum voltage => see LiPO discharge diagrams

    // convert battery level to percent
    int level = map(rawLevel, 526, 888, 0, 100);

    // i'd like to report back the real voltage, so apply some math to get it back
    // 1. convert the ADC level to a float
    // 2. divide by (R2[1] / R1 + R2)
    float realVoltage = (float)rawLevel / 1000 / (10000.f / (47000 + 10000));
    
    // build a nice string to send to influxdb or whatever you like
    char dataLine[64];
    // sprintf has no support for floats, but will be added later, so we need a String() for now
    sprintf(dataLine, "voltage percent=%d,adc=%d,real=%s,charging=%d\n",
        level, // cap level to 100%, just for graphing, i don't want to see your lab, when the battery actually gets to that level
        rawLevel,
        String(realVoltage, 3).c_str(),
        rawLevel > 800 ? 1 : 0 // USB is connected if the reading is ~870, as the voltage will be 5V, so we assume it's charging
    );

    sendData(dataLine);
    Serial.println(dataLine);

    delay(5000);

}