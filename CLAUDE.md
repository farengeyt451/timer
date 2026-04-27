# Tik Project — Session Context

## What This Is

A .NET 10 console countdown utility named `tik`. No external dependencies.

## CLI Design

```
tik -t 5 -u minutes
tik -t 30 -u s
tik -t 1 -u hour
```

Units accepted: `s`, `sec`, `seconds`, `m`, `min`, `minutes`, `h`, `hr`, `hours` (case-insensitive).

## Current Status

- [x] Step 1: Requirements gathered
- [x] Step 2: Delegation agreement (see docs/delegation.md)
- [x] Step 3: Architecture and planning (see docs/)
- [x] Step 4: Implementation complete
- [x] Step 5: CI/CD — GitHub Actions release workflow for linux-x64

## Key Decisions

- Binary name: `tik` (renamed from `timer` to avoid PATH conflicts)
- Progress bar display: `[████░░░░░░] HH:MM:SS remaining`
- Finish: ASCII spinner (~2 sec) + success message
- Colors via `Console.ForegroundColor` only (no ANSI escape codes — cross-platform safe)
- In-place rendering via `Console.SetCursorPosition` (no scroll jitter)
- Code split: `src/ArgParser.cs`, `src/TimeConverter.cs`, `src/TimerEngine.cs`, `src/ConsoleRenderer.cs`
- Namespace: `Tik`

## Docs

- `docs/architecture.md` — full component breakdown and file structure
- `docs/todo.md` — 6-phase implementation checklist
- `docs/delegation.md` — agreed roles and working rules

## Working Agreement

- Human: direction, review, visual acceptance, final approval
- AI: all implementation, iteration on feedback
- AI does not proceed without explicit Human command
