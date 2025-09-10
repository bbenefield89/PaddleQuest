# Paddle Quest – Smoke Test Checklist

A **smoke test** ensures that after refactoring or moving files around, the core game loop and menus still work without deep testing.

---

## ✅ Title Screen

* [ ] Game launches without errors.
* [ ] Title screen displays correctly with background and starfield.
* [ ] Buttons visible: **New Game**, **Options** (disabled), **Quit**.
* [ ] Clicking **New Game** transitions to Match Setup.
* [ ] Clicking **Quit** closes the game.

---

## ✅ Match Setup Screen

* [ ] Screen loads with starfield background.
* [ ] Match type (e.g., **ScoreLimit**) dropdown available.
* [ ] Options for Score Limit and Time Limit are adjustable.
* [ ] **Main Menu** button returns to Title Screen.
* [ ] **Start Match** button transitions into gameplay.

---

## ✅ Gameplay Scene

* [ ] Both paddles spawn (Player 1 on left in neon green, Player 2 on right in magenta).
* [ ] Ball spawns in center and launches correctly.
* [ ] Scoreboard visible at top (P1 score green, P2 score magenta).
* [ ] Scoring works (ball crossing boundary increments correct player score).
* [ ] Background starfield animates correctly.

---

## ✅ Pause Menu

* [ ] Pressing **Pause** (ESC or assigned key) opens menu overlay.
* [ ] Options visible: **Resume Game**, **Options** (disabled), **Main Menu**, **Quit**.
* [ ] **Resume Game** resumes play without issues.
* [ ] **Main Menu** returns safely to Title Screen.
* [ ] **Quit** closes the game.

---

## ✅ Victory Screen

* [ ] Trigger win condition (e.g., P1 reaches score limit).
* [ ] Victory screen displays "Player X has won!".
* [ ] Buttons visible: **Play Again**, **Main Menu**, **Quit**.
* [ ] **Play Again** restarts match setup.
* [ ] **Main Menu** returns to Title Screen.
* [ ] **Quit** closes the game.

---

## ✅ Audio

* [ ] Background music plays on Title Screen and Gameplay.
* [ ] SFX play when ball collides with paddles/walls.
* [ ] Victory sound triggers on win.

---

## ✅ Stability

* [ ] No missing resources (.tres, fonts, audio) after moving files.
* [ ] No crashes when navigating between all menus and gameplay.
* [ ] Escaping back to Main Menu multiple times does not break references.

---

### Notes

This is a **lightweight functional pass** — just making sure nothing critical broke after reorganizing files. Full regression tests (physics, scoring edge cases, AI behavior, etc.) come later.
