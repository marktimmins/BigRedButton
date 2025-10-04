// File for the ESP32 using the Arduino framework.
// This sketch reads a button press and sends a message over USB serial.

const int buttonPin = 23;  // Use GPIO23 for the button.
                           // You can change this to any available GPIO pin.

// The state of the button, used to detect a press and release.
int buttonState = 0;
int lastButtonState = 0;

void setup() {
  // Initialize the serial communication at a baud rate of 9600.
  // This is a slow but reliable speed.
  Serial.begin(9600);
  
  // Set the button pin as an input.
  // We use INPUT_PULLUP to use the internal pull-up resistor,
  // which simplifies the wiring. Connect the button between the pin and GND.
  // Alternatively, use INPUT and a physical pull-down resistor
  // to connect the button between the pin and 3.3V.
  pinMode(buttonPin, INPUT_PULLUP);
}

void loop() {
  // Read the current state of the button.
  // With INPUT_PULLUP, a HIGH value means the button is not pressed.
  // A LOW value means the button is pressed.
  buttonState = digitalRead(buttonPin);

  // Check if the button state has changed from HIGH to LOW (a press event).
  if (buttonState != lastButtonState && buttonState == LOW) {
    // A button press was detected.
    Serial.println("BUTTON_PRESS"); // Send a simple message to the computer.
    // The Python script will be looking for this exact string.
  }
  
  // Wait a short while to debounce the button.
  delay(50);
  
  // Save the current state as the last state for the next loop.
  lastButtonState = buttonState;
}
