<p align="center"><img src="Media/banner.png"/></p>

<p align="center"><b>Generic Code And Tools To Build The Basis Of A Framework To Develop Unity Based Games</b></p>

<br>
<p align="center">
  <a style="text-decoration:none">
    <img src="https://img.shields.io/github/package-json/v/FronkonGames/GameWork-Foundation?style=flat-square" alt="version" />
  </a>  
  <a style="text-decoration:none">
    <img src="https://img.shields.io/github/license/FronkonGames/GameWork-Foundation?style=flat-square" alt="license" />
  </a>
  <a style="text-decoration:none">
    <img src="https://img.shields.io/github/languages/top/FronkonGames/GameWork-Foundation?style=flat-square" alt="top language" />
  </a>
  <a style="text-decoration:none">
    <img src="https://img.shields.io/codacy/grade/5ee510ac2f9d411583a0eb248744d75f?style=flat-square" alt="code quality" />
  </a>
</p>

## Requisites 🔧

- Unity 2021.2 or higher.
- Test Framework 1.1.31 or higher.

## Installation ⚙️

### Editing your 'manifest.json'

- Open the manifest.json file of your Unity project.
- In the section "dependencies" add:

```c#
{
  ...
  "dependencies":
  {
    ...
    "FronkonGames.GameWork.Foundation": "git+https://github.com/FronkonGames/GameWork-Foundation.git"
  }
  ...
}
```

### Git

Just clone the repository into your Assets folder:

```c#
git clone https://github.com/FronkonGames/GameWork-Foundation.git 
```

## Use 🚀

The functionality is divided into folders, this is its structure:

```
|
|\_Runtime......................... Utilities for the game.
|   |\_Attributes.................. Attributes for fields and class properties.
|   |\_Data........................ Useful data structures.
|   |\_Development................. Developer utilities.
|   |   |\_Check................... Assert extension.
|   |   |\_Draw.................... Utilities for drawing gameplay information.
|   |    \_Profiling............... To find bottlenecks.
|   |\_Extensions.................. Utility extensions.
|   |   |\_System.................. C# extensions.
|   |    \_Unity................... Unity extensions.
|   |\_Math........................ Mathematical utilities.
|   |\_Patterns.................... Design patterns.
|   |   |\_Behavioral.............. Behavioural patterns.
|   |   |\_Creational.............. Creation patterns.
|   |    \_Structural.............. Structure patterns.
|    \_Unity....................... Utilities for Unity.
|       |\_MonoBehaviours.......... MonoBehaviours utilities.
|        \_Utils................... Misc.
|
 \_Editor.......................... Editor utilities.
    |\_Drawers..................... Custom attribute viewers.
     \_Inspector................... Editor appearance utilities.
```

Check the comments for each file for more information.

### Attributes

<table>
<tr><th>

```c#
[Title("Attributes Demo")]
```
</th><th><img src="Media/attributes.title.png"/></th></tr>
</table>


## License 📜

Code released under [MIT License](https://github.com/FronkonGames/GameWork-Foundation/blob/main/LICENSE.md).