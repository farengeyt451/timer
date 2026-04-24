# TODO — Timer Implementation

## Phase 1: Project Setup
- [x] Create `src/` folder structure
- [x] Update `timer.csproj` if needed (nullable, implicit usings already enabled)

## Phase 2: Core Logic
- [ ] `ArgParser.cs` — parse and validate `-t` / `-u` args, print usage on error
- [ ] `TimeConverter.cs` — convert value + unit to total seconds, validate > 0

## Phase 3: Timer Engine
- [ ] `TimerEngine.cs` — countdown loop with 1-second tick, call renderer on each tick

## Phase 4: Console Renderer
- [ ] `ConsoleRenderer.cs` — in-place progress bar with `Console.SetCursorPosition`
- [ ] Color support via `Console.ForegroundColor` (no escape codes)
- [ ] `HH:MM:SS` remaining display
- [ ] Finish animation: ASCII spinner (~2 sec) + success message

## Phase 5: Entry Point
- [ ] `Program.cs` — wire `ArgParser` → `TimeConverter` → `TimerEngine`
- [ ] Clean exit codes

## Phase 6: Verification
- [ ] Manual test: seconds, minutes, hours units and shorthands
- [ ] Manual test: invalid inputs (bad unit, negative value, missing args)
- [ ] Visual acceptance: progress bar renders cleanly, no scroll jitter
- [ ] Visual acceptance: finish animation looks good
