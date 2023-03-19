# Changelog

## 1.0.5 - 2023-03-19

### Fixed

- Reset() button SettingValue does not save the value.

## 1.0.4 - 2023-03-19

### Changed

- All the IHandleDraw programs throw exceptions in executable mode (should only be used in the Editor).

### Fixed

- Error in DestroyCommand.Execute() in executable versions.
- PlayerPrefsUtils.GetDateTime() parameter out implicit.

## 1.0.3 - 2023-03-19

### Added

- All configuration variables centralized in 'Edit > Preferences > Game:Work > Foundation'.
- New array extensions: Append, Contains, IndexOf, Remove, RemoveAt and Fill.
- Snap extension to Int and Float.
- Line numbers included in console messages.
- New attributes: Field, FieldGreat, FieldLess, FieldGreatEqual, FieldLessEqual, Slider, MinMaxSlider, MessageBox.

### Changed

- Best way to cache components in BaseMonoBehaviour.
- Simplified TimeBlock and MemoryBlock messages.
- Only the log messages are colored, not the file information, line number, etc.
- Added 'tooltip' field to LabelAttribute.

### Fixed

- Color.ToHex() starts with '#'.

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