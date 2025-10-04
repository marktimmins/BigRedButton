# This line loads the .NET assembly required to display Windows Forms elements like a message box.
Add-Type -AssemblyName System.Windows.Forms

# This command calls the 'Show' method from the MessageBox class to create a pop-up with your desired text.
[System.Windows.Forms.MessageBox]::Show("Hello World")