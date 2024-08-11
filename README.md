![GitHub tag (with filter)](https://img.shields.io/github/v/tag/fidarit/cs2-Vampirism?style=for-the-badge&label=Version)
![GitHub Repo stars](https://img.shields.io/github/stars/fidarit/cs2-Vampirism?style=for-the-badge)
![GitHub all releases](https://img.shields.io/github/downloads/fidarit/cs2-Vampirism/total?style=for-the-badge)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/fidarit/cs2-Vampirism/master?style=for-the-badge)

# VampirismCS2
CS2 implementation of vampirism plugin written in C# for CounterStrikeSharp.

## Config
The configuration file can be found at the following path: `addons\counterstrikesharp\configs\plugins\VampirismCS2\VampirismCS2.json`

```json
{
  "Permissions": {
    "*": {
      "Enabled": true,
      "OnHeadShotOnly": true,
      "OnKillOnly": true,
      "Multiplier": 0.5,

      "GG_IgnoreLeader": true
    },
    "@css/vip": {
      "Enabled": true,
      "OnHeadShotOnly": true,
      "OnKillOnly": true,
      "Multiplier": 0.75,

      "GG_IgnoreLeader": false
    }
  },
  "ConfigVersion": 1
}
```

Integration with [GunGame](https://github.com/ssypchenko/cs2-gungame):

- `GG_IgnoreLeader` parameter allow you to enable ignoring of GunGame leader


## Requirements
- [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master)
- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp)

## Installation
1. Download the zip file from the [latest release](../../releases), and extract the contents into your `counterstrikesharp/plugins` directory.

## Acknowledgments
If you appreciate the project then please take the time to star the repository 🙏

![Star us](https://github.com/b3none/gdprconsent/raw/development/.github/README_ASSETS/star_us.png)
