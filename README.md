# tik

A self-contained .NET 10 console countdown utility. No external dependencies.

## Usage

```bash
tik -t <value> -u <unit> [-c <color>]
tik --time <value> --unit <unit> [--color <color>]
tik -v | --version
```

| Flag | Description |
|------|-------------|
| `-t, --time` | Time value (positive integer) |
| `-u, --unit` | Time unit (see accepted values below) |
| `-c, --color` | Progress bar color (optional, default: `cyan`) |
| `-v, --version` | Display version information |

**Accepted units**

| Unit | Accepted values |
|------|----------------|
| Seconds | `s`, `sec`, `seconds` |
| Minutes | `m`, `min`, `minutes` |
| Hours | `h`, `hr`, `hours` |

**Accepted colors**

`red`, `green`, `yellow`, `blue`, `cyan`, `magenta`, `white`

All flags are case-insensitive.

**Examples**

```bash
tik -t 30 -u s
tik --time 5 --unit minutes --color green
tik -t 1 -u hour -c magenta
```

## Display

While tik is running, the console shows a live progress bar, remaining time, and an animated spinner updated every second:

```
[████████░░░░░░░░░░░░] 00:03:42 remaining [/]
```

When the countdown reaches zero, a success message appears.

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
