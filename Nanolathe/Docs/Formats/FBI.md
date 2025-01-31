# .fbi File Format

The FBI format is used to store a unit's settings, in particular this is where the various other files used in unit creation are essentially linked together.

Contents:
* [Structure](#structure)
* [Variables](#variables)
* [References](#references)

## Structure

The bulk of FBI files are key/value pairs, deliminated by `=` and suffixed with `;`. Key/value pairs are usually on their own lines, but can share lines.

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

This structure is also used in [OTA](./OTA.md) (map settings) and [TDF](./TDF.md) (general settings) files.

## Variables

The following table describes all possible variables in an FBI file, the value type expected, and whether they are mandatory (TA Design Guide, 1998).

All variables come under the `[UNITINFO]` section.

| Name | Description | Type | Required? |
| ---- | ----------- | ---- | --------- |
| Acceleration | How fast the unit accelerates | float | Yes |
| ActivateWhenBuilt | Is this unit's default state ACTIVATED | bool | No |
| ai_limit | How many of the units is the AI allowed to build | string | No |
| ai_weight | How much the AI favours building this unit | string | No |
| altfromsealevel | For flying units: how high up they fly over sea? | int | No |
| amphibious | Can this unit go under water and on land | bool | No |
| antiweapons | Does this unit shoot at weapons fired by other units | bool | No |
| attackrunlength | For flying units: distance they must travel before they can fire (drop bombs, etc) | int | No |
| BMcode | Unknown. All units are 1, except for construction plants which are 0 | int | Yes |
| BadTargetCategory | Category of units that this unit will find difficulty shooting at | string | Yes |
| BankScale | For flying units: the amount this unit banks when turning | int | No |
| BrakeRate | How fast the unit deccelerates | int | Yes |
| BuildAngle | Unknown. Possibly how much to vary this unit's orientation when building. Like how solar panels are at different angles t one another | int | No |
| BuildCostEnergy | How much energy does this unit cost to build | int | Yes |
| BuildCostMetal | How much metal does this unit cost to build | int | Yes |
| BuildTime | How long it takes to build this unit. 10,000 is the average | int | Yes |
| Builddistance | How far away can this unit build | int | No |
| Builder | Can this unit build other units | bool | Yes |
| canattack | Can this unit attack other units | bool | Yes |
| CanCapture | Can this unit capture other units | bool | No |
| CanDgun | Can this unit fire a D-Gun like weapon | bool | No |
| Canfly | Can this unit fly | bool | No |
| canguard | Can this unit be given guard orders | bool | Yes |
| canhover | Can this unit hover | bool | No |
| canload | Can this unit carry other units | bool | No |
| canmove | Can this unit move | bool | Yes |
| canpatrol | Can this unit be given patrol orders | bool | Yes |
| CanReclamate | Can this unit reclaim objects | bool | No |
| canstop | Can this unit be stopped | bool | Yes |
| cantbetransported | Is this a unit that can't be transported | bool | Yes |
| Category | A list of categories that this unit belongs to. This categories are used to help inform behaviour, such as in AI profiles. | list | Yes |
| CloakCost | How much energy this unit uses while cloaked | int | No |
| Commander | Is this unit a commander | bool | No |
| Copyright | Copyright of this unit. Must have the value `Copyright 1997 Humongous Entertainment. All rights reserved.` | string | Yes |
| Corpse | Name of the feature this unit turns into when it dies | string | Yes |
| cruisealt | For flying units: altitude at which this unit flies | int | No |
| DamageModifier | Rate at which this unit heals itself | float | No |
| DefaultMissionType | Default orders for this unit in missions | string | Yes |
| Description | Description of this unit that's displayed when hovered over | string | Yes |
| Designation | Not important, can have any value | string | Yes |
| digger | Does this unit have parts that dig under the ground | bool | No |
| Downloadable | Indicates that this is a downloaded unit, but isn't required | bool | Yes |
| EnergyMake | The energy this unit produces when active | int | Yes |
| EnergyStorage | Amount added to the maximum amount of energy that can be stored | int | Yes |
| EnergyUse | Amount of energy this unit uses when active | int | Yes |
| ExplodeAs | ID of the explosion if this unit explodes | string | Yes |
| ExtractsMetal | Rate at which this unit extracts metal | float | No |
| firestandorders | Can this unit give these orders | bool | Yes |
| Floater | Does this unit float on water | bool | No |
| FootprintX | Size of this unit's footprint (space it takes up) on the X-axis | int | Yes |
| FootprintZ | Size of this unit's footprint (space it takes up) on the Y-axis | int | Yes |
| FrenchDescription | | string | Yes |
| FrenchName | | string | Yes |
| GermanDescription | | string | Yes |
| GermanName | | string | Yes |
| HealTime | Time taken for this unit to heal itself | int | No |
| HideDamage | Whether to hide the damage done to enemy units (i.e. kill count) | bool | No |
| HoverAttack | For flying units: will this unit continue attacking from the same spot | bool | No |
| ImmuneToParalyzer | Is this unit immune to paralyzing weaons | bool | No |
| init_cloaked | Does this unit start cloaking when it's built | bool | No |
| IsAirBase | Does this unit provide landing for air units | bool | No |
| IsFeature | Is this unit a map feature, like dragons teeth | bool | No |
| istargetingupgrade | Does this unit improve the targeting of units around it so that they can shoot at enemy units on the radar as though they are in the line of sight | bool | No |
| ItalianDescription | | string | Yes |
| ItalianName | | string | Yes |
| JapaneseDescription | | string | No |
| JapaneseName | | string | No |
| kamikaze | Does this unit kill itself as part of its attack | bool | No |
| kamikazedistance | How far from the target must this unit be to kamikaze | int | No |
| MakesMetal | Does this unit make metal | bool | No |
| maneuverleashlength | How far from this unit's order will it vary from if it's distracted | int | Yes |
| MaxDamage | This unit's hit points | int | Yes |
| MaxSlope | What is the steepest slope this unit can move on | int | Yes |
| MaxVelocity | What is this unit's top speed | int | Yes |
| MaxWaterDepth | How deep can this unit go | int | Yes |
| MetalMake | Amount of metal this unit makes | int | No |
| MetalStorage | Amount added to the maximum amount of energy that can be stored | int | Yes |
| mincloakdistance | Area around this unit that must be clear of other units to remain cloaked | int | No |
| MinWaterDepth | Minimum amount of water this unit can be in | int | No |
| MobileStandOrders | Unknown. This is 1 unless the unit is a transport | bool | Yes |
| MoveRate1 | For flying units: Unknown. | int | No |
| MovementClass | Name of the class of movement that describes how this unit moves | string | Yes |
| Name | This unit's name | string | Yes |
| NoAutoFire | Will this unit not automatically fire | bool | Yes |
| NoChaseCategory | Category of units that this unit will not case after | string | No |
| norestrict | Does this unit appear in the multiplayer restriction menu | bool | No |
| NoShadow | Does this unit not have a shadow | bool | No |
| Objectname | Name of the 3DO file to use for this unit | string | Yes |
| onoffable | Can this unit be toggled between active and inactive states | bool | No |
| Ovradjust | Unknown | bool | No |
| PigLatinDescription | | string | No |
| PigLatinName | | string | No |
| PitchScale | Unknown | bool | No |
| RadarDistance | How far does this unit's radar extend out to | int | Yes |
| RadarDistanceJam | How far does this unit's radar jamming extend out to | int | No |
| Scale | Unknown | int | No |
| SelfDestructAs | Explosion that happens when the unit self destructs | string | Yes |
| selfdestructcountdown | In seconds how long the self destruct countdown lasts | int | No |
| ShootMe | Does this unit broadcast itself as a target | bool | Yes |
| ShowPlayerName | Should the player's name be shown as the description | bool | No |
| Side | Which faction does this unit belong to | string | Yes |
| SightDistance | Distance of this unit's line of sight (max 400) | int | Yes |
| SonarDistance | How far does this unit's sonar extend out to | int | No |
| SonarDistanceJam | How far does this unit's sonar jamming extend out to | int | No |
| sortbias | Unknown | int | No |
| SoundCategory | Name of the collection of sounds to use for this unit | string | Yes |
| SpanishDescription | | string | Yes |
| SpanishName | | string | Yes |
| StandingFireOrder | Initial fire order this unit is built with | enum | Yes |
| StandingMoveOrder | Initial move order this unit is built with | enum | Yes |
| Stealth | Is this unit invisible on radar and sonar | bool | No |
| SteeringMode | Way in which this unit turns | int | Yes |
| TEDClass | What type of unit this is. Possibly not important | string | Yes |
| teleporter | Is this unit a teleporter | bool | No |
| ThreeD | Is this unit 3D | bool | Yes |
| TidalGenerator | Does this unit generate energy from the tides | bool | No |
| TransMaxUnits | Maximum number of units this unit can carry | int | No |
| transportcapacity | Same as TransMaxUnits? | int | No |
| transportsize | Same again? | int | No |
| TurnRate | How quickly this unit turns | int | Yes |
| UnitName | The internal name for this unit that is used in other files. Not displayed to the player | string | Yes |
| UnitNumber | Unique ID number for this unit | int | Yes |
| Upright | Is this unit upright | bool | Yes |
| Version | Version of *Total Annihilation* this unit will work with | int | Yes |
| WaterLine | How high on the model to apply the water line | int | No |
| Weapon1 | Name of this unit's primary weapon | string | Yes |
| Weapon2 | Name of this unit's secondary weapon | string | Yes |
| Weapon3 | Name of this unit's tertiary weapon | string | No |
| WindGenertor | Amount of energy generated by this unit from the wind | int | No |
| WorkerTime | How quickly this unit nanolathes | int | Yes |
| wpri_badTargetCategory | Category of unit(s) that this unit's primary weapon will find difficult shooting at | string | Yes |
| wsec_badTargetCategory | Category of unit(s) that this unit' secondary weapon will find difficult shooting at | string | No |
| YardMap | Defines the construction space of a construction place TODO | string? | No |
| ZBuffer | Does this unit have a z buffer | bool | Yes |

## References

* Marti D, "ta-fbi-fmt" (1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/ta-fbi-fmt.txt [Last accessed 08/08/2024]
* TA Design Guide, "FBI", (December 1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/fbidesc.htm [Last accessed 08/08/2024]