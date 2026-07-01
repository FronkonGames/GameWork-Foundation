# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## 0.0.19 - 2026-07-01

### Added
- New pattern: Signal.
- Unit tests for all new features.

### Changed
- Updated to Unity 6000.3.6f1.

## 0.0.18 - 2026-06-19

### Added
- `Screenshooter` tool.
- Better documentation.
- New extensions.
- Weighted random, Gaussian sampling and enum random helpers in `Rand` and `EnumExtensions`.
- LINQ batch helpers: `Buffer`, `RollingWindow`, `TryPullFromDictionary`.
- Dictionary helpers: `SelectDictionary`, `Normalize`, `SumTogether`.
- RAII disposable helpers: `ActionDisposable`, `DestroyOnDispose`, `EnableThenDisable`, `DisposeTemporaryTexture`.
- `GizmoDraw`, helpers for `OnDrawGizmos`.
- Unit tests for the new extensions.

### Changed
- Updated to Unity 6000.0.76b1.
- Methods from Log class no longer show at Callstack.

## 0.0.17 - 2026-05-31

### Added
- Serializable DateTime, Time, KeyValuePair, and Dictionary for Unity serialization.
- New extensions.
- Unit tests for all new features.

### Changed
- Texture2D.Resize renamed to Texture2D.Resized to avoid conflict with Unity's deprecated method.

## 0.0.16 - 2026-05-29

### Added
- New patterns: Builder, Factory, Composite, Mediator, Memento, Chain of Responsibility, Object Pool pattern.
- Unit tests for all new features.

### Changed
- Migrated FindObjectOfType to FindFirstObjectByType/FindAnyObjectByType (Unity 6 deprecation).
- Updated README.

### Fixed
- Fixed tests for banker's rounding edge cases (Mathf.Round).
- Fixed test assertions for ColorExtensions (Opaque, FromHex with # prefix).
- Fixed test assertions for Vector extensions (ToString format, Remainder math).

## 0.0.15 - 2024-08-20

### Fixed
- Fixed assemblies.
- Fixed textarea styles in DevelopmentConsole.
- Fixed DebugDraw.
- Fixed HardwareMonitor.

## 0.0.14 - 2024-08-06

### Added
- State pattern.
- 4, 6, 10, 10, 20 and 100-sided dice added to Rand.
- Development console configuration parameters added to Settings.

### Changed
- SRP changed from Built-In to Universal RP (14.0.11).

### Fixed
- The development console scales correctly with the resolution.
- Tailwind.FromHTML() fixed.
- Small fixes.

## 0.0.13 - 2024-07-09

### Added
- Tailwind colors.
- Service Locator pattern.
- Check.IsAssignableFrom().
- Log.ExceptionArgument(), Log.ExceptionKeyNotFound().
- Utility to get the version and commit number from Git.
- Tag attribute.
- Prototype components: Swing.
- Added '[Prototype Asset Pack](https://assethunts.itch.io/prototype)' by [AssetHunts](https://assethunts.itch.io/).
- Added '[Mannequin Character Pack](https://assethunts.itch.io/mannequinfree)' by [AssetHunts](https://assethunts.itch.io/).

### Changed
- DebugDraw refactor.
- CachedMonoBehaviour renamed to CachedMonoBehaviour
- Runtime/Unity/Components moved to Runtime/Development/Prototype.
- Prototype components put inside the namespace 'FronkonGames.GameWork.Foundation.Prototype' to avoid conflicts with the class names.
- FPS counter changed by Hardware monitor.

### Fixed
- Log.Exception() does not throw exceptions.
- Small fixes.

## 0.0.12 - 2023-10-30

### Added
- Prototype components: third / first person cameras, material scrolls.

### Fixed
- Small fixes.

## 0.0.11 - 2023-10-28

### Added
- Command line parsing.
- Prototype components: collision / trigger tester, face to, follower, rotator, mover, FPS counter, free camera, screenshooter.

### Changed
- Updated to Unity 2022.3.

### Removed
- CalculateFPS class.

## 0.0.10 - 2023-05-08

### Added
- Custom Inspector.
- GUI.Scope disposable groups.
- sting.ToWords() extension.

### Fixed
- Small fixes.

## 0.0.9 - 2023-04-28

### Added
- Custom Inspector.
- Structures: Arraylist.
- IEnumable extensions.
- New Exceptions log.
- ReflectionExtensions.GetProperty(), GetField() by name.

### Fixed
- Small fixes.

## 0.0.8 - 2023-04-23

### Added
- Profiler memory stamps, sample and marker.
- PlayerPrefsUtils color.
- Quaternion extensions.
- string.ToVector4, string.ToQuaternion.

### Changed
- Settings refactor.
- MemoryBlock use Profiler.GetMonoHeapSize().
- string.ToColor parse comma separated color channels.

### Fixed
- Log.Info and Check message format.
- Small fixes.

## 0.0.7 - 2023-04-20

### Added
- Strategy pattern.
- Visitor pattern.

## 0.0.6 - 2023-04-15

### Added
- KeyCodeAttribute, with keystroke detection.
- Error message if any attribute is used with another type than expected.
- New color extensions: SetHue, SetSaturation, SetValue.

### Changed
- Most of the attributes limited to Property and Field.
- Private AttributesDemo variables.

### Fixed
- Color unit test.
- Transform.FlipPositive typo.

## 0.0.5 - 2023-03-19

### Fixed
- Reset() button SettingValue does not save the value.

## 0.0.4 - 2023-03-19

### Changed
- All the IHandleDraw programs throw exceptions in executable mode (should only be used in the Editor).

### Fixed
- Error in DestroyCommand.Execute() in executable versions.
- PlayerPrefsUtils.GetDateTime() parameter out implicit.

## 0.0.3 - 2023-03-19

### Added
- All configuration variables centralized in 'Edit > Preferences > Game:Work > Foundation'.
- New array extensions: Append, Contains, IndexOf, Remove, RemoveAt and Fill.
- Snap extension to Int and Float.
- Line numbers included in console messages.
- New attributes: Field, FieldGreat, FieldLess, FieldGreatEqual, FieldLessEqual, Slider, MinMaxSlider, MessageBox.

### Changed
- Best way to cache components in CachedMonoBehaviour.
- Simplified TimeBlock and MemoryBlock messages.
- Only the log messages are colored, not the file information, line number, etc.
- Added 'tooltip' field to LabelAttribute.

### Fixed
- Color.ToHex() starts with '#'.

## 0.0.2 - 2023-03-17

### Added
- PersistentMonoBehaviourSingleton, persistent singleton between scene changes.

### Fixed
- MonoBehaviourSingleton internal reference released when object destroyed [#1](https://github.com/FronkonGames/GameWork-Foundation/issues/1) (_Thanks to @TJHeuvel_).

## 0.0.1 - 2023-03-14

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

## 0.0.0 - 2022-09-06

_Initial release._