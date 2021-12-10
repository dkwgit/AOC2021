# Advent of Code 2021

## Requirements

.NET 6.0

## How to run for all days

Easiest way to run is to cd to the project directory and run via dotnet

```PowerShell
cd AOC2021
dotnet run
```

## How to run for specific days

Passing `--days x [y] [z]` on the command line will run specific days(s). [y] and [z] show that, optionally, more than one day can be passed. Passed in days can be zero padded or not.

Example of passing days through to the program via `dotnet run` command, run from directory where csproj is located:

```PowerShell
dotnet run -- --days 05 6 12
```

## Getting command line help/usage message

```PowerShell
dotnet run -- --help
```

Or send it a bad command line

```PowerShell
dotnet run -- --askxqzu
```
