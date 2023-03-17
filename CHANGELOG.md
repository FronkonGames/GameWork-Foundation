# Changelog

## 1.0.2 - 2023-03-17

### Added

- PersistentMonoBehaviourSingleton, persistent singleton between scene changes.

### Fixed

- MonoBehaviourSingleton internal reference released when object destroyed [#1](https://github.com/FronkonGames/GameWork-Foundation/issues/1) (_Thanks to @TJHeuvel_).

## 1.0.1 - 2023-03-14

### Added

- HasKey, DeleteKey and DeleteAll in PlayerPrefsUtils.
- String, Quaternions support in PlayerPrefsUtils.
- More Unit tests.
- Changelog ;)

### Fixed

- Publisher.Notify() is now protected.
- Int.BytesToHumanReadable() returns the string converted to InvariantCulture.
- Rand.Range(int, int) returns int in an inclusive range.
- MathConstants.GoldenRatio syntax error.
- Missing comments.

## 1.0.0 - 2022-09-06

_Initial release._