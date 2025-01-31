# Units

In TA units (includes buildings) are one of the more complex aspects of the game as they comprise the greatest number of file files and type of data.

To get one new unique unit with a new weapon into the game, you need to:

1. Create a new model (.3do)
2. Apply the textures (texture .gaf)
3. Script the animaions (.bos compiled into .cob)
4. Create a unitpic for the UI (.pcx)
5. Create the weapon (weapon .tdf)
6. Finally, bring it together with the general stats (.fbi)

That doesn't even include adding new sounds (.wav and then listed into a sound category .tdf), nor enabling the AI to use it (.txt). This needs to be abstracted to simplify the creaion process, to make it easier and smoother to get new units into the game.

## Model

```
name: string
description: string
model: Model
script: Script
unitpic: Pcx
weapons: Array[Weapon]
...(other variables for the various stats and other data)
```