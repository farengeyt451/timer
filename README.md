# tik

A self-contained .NET 10 console countdown utility. No external dependencies.

## Usage

```
tik -t <value> -u <unit>
```

| Flag | Description |
|------|-------------|
| `-t` | Time value (positive integer) |
| `-u` | Time unit (see accepted values below) |

**Accepted units**

| Unit | Accepted values |
|------|----------------|
| Seconds | `s`, `sec`, `seconds` |
| Minutes | `m`, `min`, `minutes` |
| Hours | `h`, `hr`, `hours` |

Units are case-insensitive.

**Examples**

```
tik -t 30 -u s
tik -t 5 -u minutes
tik -t 1 -u hour
```

## Display

While tik is running, the console shows a live progress bar and remaining time updated every second:

```
[████████░░░░░░░░░░░░] 00:03:42 remaining
```

When the countdown reaches zero, a short ASCII spinner animation plays followed by a success message.

## Installation

Download the latest binary for your platform from the [Releases](https://github.com/farengeyt451/tik/releases) page.

**Linux**

```bash
curl -L https://github.com/farengeyt451/tik/releases/latest/download/tik-linux-x64 -o tik
chmod +x tik
sudo mv tik /usr/local/bin/
```

**Windows (PowerShell)**

```powershell
Invoke-WebRequest -Uri https://github.com/farengeyt451/tik/releases/latest/download/tik-win-x64.exe -OutFile tik.exe
Move-Item tik.exe "$env:USERPROFILE\AppData\Local\Microsoft\WindowsApps\"
```

After installation, run it directly:

```
tik -t 5 -u minutes
```

## Requirements

- .NET 10 SDK (only needed if building from source)

## Build and Run

```
dotnet build
dotnet run -- -t 5 -u minutes
```

Or publish a self-contained binary:

```
dotnet publish -c Release -r linux-x64 --self-contained
```

Replace `linux-x64` with your target runtime identifier (`win-x64`, `osx-arm64`, etc.).

## Error Handling

The application validates all input and exits with code `1` on failure, printing a descriptive message and usage hint. Checked conditions include:

- Missing `-t` or `-u` flags
- Non-numeric or non-positive value for `-t`
- Unrecognized time unit
