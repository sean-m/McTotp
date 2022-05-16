# Simple TOTP Generator
Written in .net. UI rendered with Avalonia. Cryptographic functions provided by the runtime.

## Features
- Cross platform (Windows first)
- [ ] Symmetric encryption for storing settings (rotate IV on save). Settings live in a file on disk to be backed up.
- [ ] Read QR code from screen.
- [ ] Show QR code for existing secrets.
- [ ] Hide codes after timeout with 4-20 digit pin (sha1 crypt)

### Windows only
- [ ] GPO for setting pin length, timeout.
- [ ] MSI package.
- [ ] Minimize/close to systray.