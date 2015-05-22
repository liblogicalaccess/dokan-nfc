# DokanNFC
RFID / NFC File System Driver for Windows.

## Installation
### Dependencies
 - Microsoft .NET Framework 4.5
 - [LibLogicalAccess 1.77.1](http://artifacts.islog-services.eu/repository/rfid-releases/eu/islog/lib/readers/liblogicalaccess-exe/1.77.1/liblogicalaccess-exe-1.77.1.zip) (RFID Middleware)
 - [Dokan 0.7.2](https://github.com/dokan-dev/dokany/releases/download/0.7.2/DokanInstall_0.7.2.exe) (User Mode File System Driver)
 - A compatible RFID reader, see [compatible device list](http://liblogicalaccess.islog.com/wiki/doku.php/hardware-list#readers).
 
### General
 - Download and install all prerequisites
 - Download and install the latest release from https://github.com/islog/dokan-nfc/releases/latest
 - Restart your computer or restart *DokanNFCSvc* Windows Service
 - A new drive should appear, you can use it browse and edit your RFID / NFC chip with regular files
 
## License
GPL, see **LICENSE.GPL.txt** file.