# .tdf File Format

The TDF format is used to store a variety of settings, stats and other miscellanious game data needed by *Total Annihilation*. They are text files that are structured in a way that's not too dissimilar to INI files.

Contents:
* [Structure](#structure)
* [Variables](#variables)
  - [ALLSOUND](#allsound)
  - [BUILDINFO](#buildinfo)
  - [Campaign](#campaign)
  - [CATEGORY](#category)
  - [Download](#download)
  - [Features](#features)
  - [HELP](#help)
  - [LoS](#los)
  - [METEOR](#meteor)
  - [MOVEINFO](#moveinfo)
  - [SIDEDATA](#sidedata)
* [References](#references)

## Structure

The bulk of all TDF files are key/value pairs, deliminated by `=` and suffixed with `;`. Key/value pairs are usually on their own lines, but can share lines.

```
name=High Energy Laser;
rendertype=0;
AnotherKey=Value; onSameLine=val;
```

Keys are always strings, while values can be strings, integers, floats, booleans (stored as `0` and `1`), string lists, or enums.

The key/value pairs are organised under headings formatted as `[HEADER]` (sometimes `[header]`), and surrounded by `{ }`. A section can include another subsection. For example:

```
[HEADER]
    {
    key1=value;
    key2=another;
    [NESTED]
        {
        key1=more;
        }
    }
```

In-line comments are surrounded by a `/*` and `*/`.

```
name=High Energy Laser;
rendertype=0; /*This is a comment*/
```

While every use of the TDF format features different pairs of data, they all share this same basic structure.

This structure is also used in [FBI](./FBI.md) (unit settings) and [OTA](./OTA.md) (map settings) files.

## Variables

What follows is a description of all of the various uses of the TDF format and the values defined.

### ALLSOUND
*gamedata/*

Describes mappings of fixed game events (e.g. button clicked) to sound file.

| Name | Description | Type | Section | Required? |
| ---- | ----------- | ---- | ------- | --------- |
| sound | Name of the sound file to play | string | Any valid event name | Yes |

```
[ActivateAllStatBars]
    {
    sound=explode.wav;
    }
[ENDGAMESTATBAR]
    {
    sound=beep6;
    }
```

### BUILDINFO
*gamedata/*

Describes development build info. Only used by Cavedog.

| Name | Description | Type | Section | Required? |
| ---- | ----------- | ---- | ------- | --------- |
| UserName | Possibly the name of the user who initiated this build | string | BuiltFor | Yes |
| Company | Full name of the developer | string | BuiltFor | Yes |
| Date | Date of the build | string | BuiltFor | Yes |

```
[BuiltFor]
    {
    UserName=ex;
    Company=Cavedog Entertainment;
    Date= July 14, 1997;
    }
```

### CATEGORY
*gamedata/*

Describes unit categories, but does not appear to be used.

| Name | Description | Type | Section | Required? |
| ---- | ----------- | ---- | ------- | --------- |
| description | Description of this unit category | string | Any unit category name | Yes |

```
[Generic Unit]
    {
    description = Just Plain 'ol boring units;
    }
[Plant]
    {
    description = Unit Creation Plant;
    }
[Vtol]
    {
    description = Some type of units;
    }
```

### Campaign

*camps/*

Describes the missions in the campaign. Each mission section header must be appended with an integer to indicate its position in the campaign. They are 0 indexed.

| Name | Description | Type | Section | Required? |
| ---- | ----------- | ---- | ------- | --------- |
| campaignside | Side the campaign is played through as | string | HEADER | Yes |
| missionfile | File name of the mission map's OTA file | string | MISSIONX | Yes |
| missionname | Display name of the mission | string | MISSIONX | Yes |
| Germanmissionname | German display name of the mission | string | MISSIONX | Yes |
| Frenchmissionname | French display name of the mission | string | MISSIONX | Yes |
| Italianmissionname | Italian display name of the mission (added by *Core Contingency*) | string | MISSIONX | Yes |

```
[HEADER]
    {
    campaignside=ARM;
    }

[MISSION0]
    {
    missionfile=Krogoth Encounter.ota;
    missionname=1: Krogoth Encounter!;
    Germanmissionname=...
    }
[MISSION1]
    {
    ...
    }
...
```

### Download

*download/*

Describes which build menus of which contruction units can build what units. Originally this use was applied by Cavedog to append the free downloadable units to existing construction units. Each entry must be unique for every unit. So if two construction units are meant to build the same unit, then two entries must be written.

| Name | Description | Type | Section | Required? |
| ---- | ----------- | ---- | ------- | --------- |
| UNITMENU | Short name of the unit that build this unit | string | MENUENTRY | Yes |
| MENU | Index of the menu to put the button. The "page" on which to put the button. NOTE: index starts at 2 | int | MENUENTRY | Yes |
| BUTTON | Position on the menu to put the button. Valid value range 0-5 | int | MENUENTRY | Yes |
| UNITNAME | Short name of the unit to build | string | MENUENTRY | Yes |

```
[MENUENTRY]
    {
    UNITMENU=CORAVP;     // Who builds it?
    MENU=4;              // Which menu?
    BUTTON=2;            // Which button in the menu?
    UNITNAME=Hellfire;   // Unit to be built
    }
```

### Features

*features/x*

Describes various map elements like trees, rocks, corpses, etc. Each section's name should be unique, as it describes a specific feature.

| Name | Description | Type | Required? |
| ---- | ----------- | ---- | --------- |
| animating | Is the GAF file an animation | bool | No |
| animtrans | Possibly marks if the animation is transparent | bool | No |
| autoreclaimable | Will a construction unit reclaim this when patrolling | bool | No |
| burnmax | Possibly specifies maximum duration this feature should burn for | int | No |
| burnmin | Possibly specifies minimum duration this feature should burn for | int | No |
| burnweapon | The "weapon" that this does damage with to nearby units if it's burning | string | No |
| category | The category this feature belongs to | string | Yes |
| description | Text that appears at the bottom of the screen when the mouse hovers over this feature | string | Yes |
| blocking | Can a unit walk over this feature | bool | Yes |
| damage | This feature's hitpoints. When it reaches 0 it either disappears or changes to another feature indicated by `featuredead` | int | Yes |
| energy | Amount of energy gained when reclaimed | int | Yes |
| featuredead | Short name of the feature this one changes to when `damage` reaches 0 | string | Yes |
| featurereclamate | Short name of the feature this one changes to when reclaimed | string | Yes |
| filename | GAF filename of this feature's art. Omit *.gaf* | string | No |
| flamable | Can this feature catch fire | bool | No |
| footprintx | Feature's width | int | Yes |
| footprintz | Feature's height | int | Yes |
| geothermal | Can geothermal plants be built on top of this feature | bool | No |
| height | How tall is this feature. Used for determining whether a shot can pass over it | int | Yes |
| hitdensity | Unknown | int | Yes |
| indestructible | Is this impossible to destroy | bool | No |
| metal | Amount of metal gained when reclaimed, or extracted by extractors. If this feature is to be able to be extracted, then the max is 255 | int | Yes |
| nodisplayinfo | Do not display name | bool | No |
| object | 3DO filename of this feature's art | string | No |
| permanent | Is it permanent to the map? | bool | No |
| reclaimable | Can this feature be reclaimed | bool | Yes |
| reproduce | Does nothing | bool | No |
| reproducearea | Does nothing | int | No |
| seqname | Name of the specific GAF animtation sequence in the GAF file specified by `filename` | string | No |
| seqnameburn | Name of the animation sequence to use for burning | string | No |
| seqnamedie | Name of the animation sequence to use when this feature "dies" | string | No |
| seqnamereclamate | Name of the animation sequence to use when this feature is reclaimed | string | Yes |
| seqnameshad | Name of the animation sequence to use as the shadow | string | No |
| shadtrans | Is the shadow transparent | bool | No |
| sparktime | Duration to wait before this feature catches fire if another nearby is burning | int | No |
| spreadchance | Percentage chance this or another feature spreads the burning state? | int | No |
| world | Which world (map types) does this feature belong to. Used by map editors | string | Yes

```
[ARMALPHA_DEAD]
    {
    world=all;
    description=Wreckage;
    category=arm_corpses;
    object=armalpha_dead;
    featuredead=armalpha_heap;
    footprintx=5;
    footprintz=5;
    height=12;
    blocking=1;
    hitdensity=23;
    metal=344;
    damage=4545;
    reclaimable=1;
    featurereclamate=smudge01;
    seqnamereclamate=tree1reclamate;
    }

[ARMALPHA_HEAP]
    {
    world=all;
    description=Metal Shards;
    category=heaps;
    object=4x4a;
    footprintx=2;
    footprintz=2;
    blocking=0;
    hitdensity=4;
    metal=344;
    damage=34;
    reclaimable=1;
    featurereclamate=smudge01;
    seqnamereclamate=tree1reclamate;
    }
```

### HELP

*gamedata/*

Describes the info displayed when help is requested.

| Name | Description | Type | Required? |
| ---- | ----------- | ---- | --------- |
| LineX | Text to display | string | Yes |

```
[Help]
    {
    Line0 = CTRL+A|Select all units;
    ...
    }
```

### LOS

*gamedata/*

Describes the line of sight around units. Set in various tables, though th source used isn't 100% sure what they do.

| Name | Description | Type | Section | Required? |
| ---- | ----------- | ---- | ------- | --------- |
| numtables | Number of tables defined in this file | int | TABLEINFO | Yes? |
| numlines | Number of line variables that follow this | int | TABLEX | Yes |
| linex | Comma separated list of numbers | string? | TABLEX | Yes |

```
// Line of sight tables
[TABLEINFO]
    {
    numtables=9;
    }

...

[TABLE2]
    {
    numlines=4; // Radius of 2
    line1= 2, 0, 1, 0, 2;
    line2= 2, 0, 1, 1, 2;
    line3= 1, 1, 1;
    line4= 2, 1, 0, 2, 1;
    }
```

### METEOR

*gamedata/*

Describes default settings for the meteor storm that happens on some maps.

| Name | Description | Type | Required? |
| ---- | ----------- | ---- | --------- |
| MeteorWeapon | Short name of the "weapon" to use | string | Yes |
| MeteorRadius | Size of the area to affect | int | Yes |
| MeteorDensity | Number of strikes per second-ish | int | Yes |
| MeteorDuration | Duration to wait between strikes? | int | Yes |
| MeteorInterval | Duration to wait between strikes? | int | Yes |

```
[Default]
    {
    MeteorWeapon = Meteor;
    MeteorRadius = 300; 	// A bit under a screen in size
    MeteorDensity = 2; 	// About two strikes per second
    MeteorDuration = 5; 	// Five seconds per strike
    MeteorInterval = 60; 	// One minute between strikes
    }
```

### MOVEINFO

*gamedata/*

Describes the classes of movement types that units use. The name of one class is used in the unit FBI `MovementClass` key like `MovementClass=TANKBH3`.

| Name | Description | Type | Section | Required? |
| ---- | ----------- | ---- | ------- | --------- |
| Name | Short name that other files can refer to this class with | string | CLASSX | Yes |
| FootprintX | Width | int | CLASSX | Yes |
| FootprintZ | Height | int | CLASSX | Yes |
| MaxWaterDepth | How deep can this type of unit move into | int | CLASSX | Yes? |
| MaxSlope | Steepest slope this type of unit can traverse | int | CLASSX | Yes? |

```
[CLASS0]
    {
    Name=KBOTSS2;
    FootprintX=2;
    FootprintZ=2;
    MaxWaterDepth=12;
    MaxSlope=32;
    }
[CLASS1]
    {
    Name=KBOTSF2;
    FootprintX=2;
    FootprintZ=2;
    MaxWaterDepth=12;
    MaxSlope=11;
    }
```

### SIDEDATA

*gamedata/*

Describes what the screen looks like for each side, and what units they can build at the start of the game.

**Side information**

Describes the logos, layout, and colours of the various UI elements.

Example (from https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfgdata.htm)

```
[SIDE0]
    {
    name=ARM;
    nameprefix=ARM;
    commander=ARMCOM;
    intgaf=ARMINT;
    font=console;
    fontgui=armbutt;
    energycolor=208;
    metalcolor=224;

    [LOGO]
        { x1=132; y1=5; x2=152; y2=25; }

    [ENERGYBAR]
        { x1=471; y1=11; x2=592; y2=13; }
    .....
    }
```

**Canbuild information**

Describes what each side's units can build. This is the method used by Cavedog during the game's development.

Example (from https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfgdata.htm)

```
[CANBUILD]
    {
    [ARMCOM]
        {
        canbuild1=ARMSOLAR;
        canbuild2=ARMWIN;
        canbuild3=ARMESTOR;
        canbuild4=ARMMSTOR;
        canbuild5=ARMMEX;
        canbuild6=ARMMAKR;
        canbuild7=ARMLAB;
        .....
        }
    [ARMLAB]
        {
        canbuild1=ARMCK;
        canbuild2=ARMPW;
        canbuild3=ARMROCK;
        canbuild4=ARMHAM;
        canbuild5=ARMJETH;
        }
    .....
    }
```

New units do not need to be added to the section. The author speculates that this was either used for another tool, or was simply the old system.

### SOUND

*gamedata/*

Describes groupings of sounds for units. Referenced in FBI files under `SoundCategory`.

Example (from https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfgdata.htm)

```
[CORE_TANK]
    {
    select1=tcorsel;
    ok1=tcormove;
    arrived1=tcorst0p;
    cant1=cantdo4;
    underattack=warning1;
    count5=count1;
    count4=count2;
    count3=count3;
    count2=count4;
    count1=count5;
    count0=count6;
    canceldestruct=cancel2;
    }
```

Each value is the relevant sound file's name, minus *.wav*.

### TRANSLATE

*gamedata/*

Describes mappings for other languages.

Example (from https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfgdata.htm)

```
[3d]
    {
    piglatin=d3ay;
    German = 3D;
    French = 3-D;
    }
[Sound Effects Volume]
    {
    //looks like they automated pig-latin translations... poorly
    piglatin=ound Effects VolumeSay;
    German = Lautst�rke der Soundeffekte;
    French = Volume effets sonores;
    }
```

### UNITVIEW

*gamedata/*

Appears to describe settings for a possible development tool.

### Useonly

*camps/useonly/*

Describes the units that are allowed by the player in a campaign mission.

Example (from https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfuonly.htm)

The filename must match the corresponding OTA file for the mission map, and that OTA file must refer to this TDF file under `useonlyunits`.

```
[ARMACSUB]
    {
    }
[ARMASON]
    {
    }
[ARMASY]
    {
    }
[ARMATL]
    {
    }
[ARMBATS]
    {
    }
```

Each header must be the ID for a unit that is intended to be allowed to be used by the player.

### Weapon

*weapons/*

Describes the stats and settings of a particular weapon.

Example (from https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfweapon.htm)

```
[Hellfire_LASER]
    {
    ID=243;
    name=High Energy Laser;
    rendertype=0;
    lineofsight=1;
    turret=1;
    range=460;
    reloadtime=.865;
    weaponvelocity=400;
    areaofeffect=15;
    duration=.2;
    soundstart=ion;
    soundhit=Laser2;
    firestarter=100;
    beamweapon=1;
    color=165; /*Pink & Dark Pink*/
    color2=225;
    tolerance=8000;
    explosiongaf=hellfirelaser;
    explosionart=pinkboom1;
    waterexplosiongaf=fx;
    waterexplosionart=h2oboom1;
    lavaexplosiongaf=fx;
    lavaexplosionart=lavasplashsm;
    startsmoke=1;
    [DAMAGE]
        {
        default=130;
        }
    }
```

`[]` - String - header is the internal name of the weapon.

`[DAMAGE]` - String - this section defines the default damage of the weapon, as well as any specific damages to specific units.

`accuracy` - int - lower is more accurate. Possible range is 0 to 32768.

`aimrate` - int - how fast it aims. Only used in unit viewer and only set for artillery. 

`areaofeffect` - int - size of the area to deal damage.

`ballistic` - bool - does it fire like artillery shells?

`beamweapon` - bool - like a laser?

`burnblow` - bool - does it detonate when it reaches the end of its range?

`burst` - int - number of shots fired at a time.

`burstrate` - float - delay in seconds between each shot in a burst.

`color` - int - Colour of the beam weapon. Must be a value from the corresponding pallete file.

`color2` - int - colour used to blend with the first?

`commandfire` - bool - must be expressly ordered by the player to be fired, like the D-gun.

`cruise` - bool - unknown

`dropped` - bool - is it dropped like a bomb?

`duration` - float - how long a beam is. Higher value results in a longer beam.

`edgeeffectiveness` - float - Percentage of the damage to deal to units at the edge of the blast area.

`endsmoke` - bool - draw smoke when weapon stops firing.

`energy` - unknown - does not appear to do anything.

`energypershot` - int - energy consumed per shot.

`explosionart` - string - name of the animation sequence in the GAF.

`explosiongaf` - string - name of the GAF file containing the `explosionart`. Omit *.gaf* extension.

`firestarter` - int - Percentage chance weapon causes a fire.

`flighttime` - int - Time the unt will fly for when it enters second stage?

`groundbounce` - bool - Weapon will bounce off the ground first before detonating.

`guidance` - bool - weapon is guided and uses `turnrate` to track.

`ID` - int - unique ID of the weapon. TA has a hardcoded limit of 255.

`lavaexplosionart`

`lavaexplosiongaf`

`lineofsight` - bool - Does it fire in a straight line.

`metal` - unknown - not used.

`metalpershot` - int - how much metal is consumed per shot

`meteor` - bool - Is it a meteor weapon?

`minbarrelangle` - int - minimum angle in degrees the barrel can point.

`model` - string - model to display the weapon. This is the projectile, not the mount. Omit *.3do*

`name` - string - Display name

`noautorange` - bool - does not detonate when it reches maximum range, and instead keeps going

`noexplode` - bool - like a "don't stop" when it hits a target, like the D-gun

`noradar` - bool - is it hidden from the radar?

`paralyzer` - bool - does it paralyze? Length of time defined in damage field

`pitchtolerance` - int - up and down of aiming?

`propeller` - bool - does weapon have a propeller to spin?

`randomdecay` - float - varys length of fire time

`range` - int - range in pixels

`reloadtime` - float - seconds between shots, or bursts if it's a burst

`rendertype` - int, flag - which rendering system to use: 0=laser, 1=modelled, 2=not rendered, 3=dgun, 4=plasma shell, 5=flame, 6=bomb, 7=lightning

`selfprop` - bool - self-propelled?

`shakeduration` - int - how long does screen shake last

`shakemagnitude` - int - how vigorous to shake

`smokedelay` - float - time between puffs of smoke. Only used if `smoketrail=1`

`smoketrail` - bool - does it emit a smoke trail as it moves?

`soundhit` - string - sound when it hits

`soundstart` - string - sound when fired

`soundtrigger` - bool - for bursts. 1 = `soundstart` played every shot, 0 = `soundstart` played every burst.

`soundwater` - string

`sprayangle` - int - deviation from the straight line of the shot

`startsmoke` - bool - draw smoke when fired?

`startvelocity` - int - start at this velocity instead of 0

`stockpile` - bool - can they be built and stored?

`targetable` - bool - can this be shot down?

`tolerance` - int - used to add soft limits to the accuracy. How far away from pointing at the target to still be able to fire? 0 = dead ahead

`tracks` - bool - tracks the target

`turnrate` int - how quickly guided weapons track targets

`turret` - bool - is weapon fired from a turret?

`twophase` - bool - does weapon have two distinct phases?

`unitsonly` - bool - does weapon only effect enemy units?

`vlaunch` - bool - launched vertically?

`waterexplosionart` - string

`waterexplosiongaf` - string

`waterweapon` - bool - weapon travels through water?

`weaponacceleration` - int - (pixels/second)/second, how quickly does it reach `weaponvelocity`

`weapontimer` - float - how long in seconds is weapon active?

`weapontype2` - does not appear to be used

`weaponvelocity` - int - maximum valocity in pixels/second

## References

* TA Design Guide, "Campaign TDF Information", (September 1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfcamp.htm [Last accessed 08/08/2024]
* TA Design Guide, "Download TDF Information", (December 1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfdown.htm [Last accessed 08/08/2024]
* TA Design Guide, "Features TDF Information", (December 1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdffeat.htm [Last accessed 08/08/2024]
* TA Design Guide, "Gamedata TDF Information", (September 1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfgdata.htm [Last accessed 08/08/2024]
* TA Design Guide, "Useonly TDF Information", (September 1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfuonly.htm [Last accessed 08/08/2024]
* TA Design Guide, "Weapon TDF Information", (September 1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/tdfweapon.htm [Last accessed 08/08/2024]