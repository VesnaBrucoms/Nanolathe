# Current Situation

The current situation for Total Annihilation is that to make any mod requires keeping a reference handy, notepad, and a switching between several different utilities. While it's workable (the vast array, quality and sheer size of TA mods shows that it is), it isn't beginner friendly, nor friendly to those returning after a hiatus, and it's cumbersome. On top of that it's frustrating having to work out which utilities are still available, and even if they are whether they'll actually run on modern systems. Many of the still available guides were written a long time ago, thus much of what they reference and link to no longer exist. Making anything beyond a handful of new content (maps, units, etc) becomes somewhat overwhelming for those belonging to those groups. Myself included. Requiring a lot of effort to manage it all.

### Current utilities:

These are utilities created for Total Annihilation's formats. This is not an exhaustive list. They are almost entirely created by members of the community.
* hpiview/hpipacker/HPIEdit/etc - view/extract/pack HPI/CCX/UFO archives
* 3DO Builder - make model TA ready and textured
* Scriptor - write, compile, decompile unit scripts
* Annihilator - map editor
* Conflict Crusher - identifies (and fixes?) mod conflicts
* TA: Mutation - conflict resolver, mod profiles, and "mutations"
* GAF Builder/GAF Builder Pro - modify and create GAF files

These are tools created for other/general purposes.
* Wings3D - modelling
* Notepad/Notepad++/VSCode/etc - modify any text files (TDF, FBI, AI profiles, etc)
* Bryce/Terragen - produce better looking maps
* Photoshop/GIMP/etc - create 2D srites, animations, etc

### Examples/Use Cases

Depending on ones usecase the number of utilities required could range from manageable, to overwhealming.

For mapping:
* Notepad/Notepad++/VSCode/etc - edit OTA files, maybe also campaign TDF files, maybe AI profiles
* Annihilator - create the map itself
* (Optional) Bryce/Terragen - create new map styles to then import into Annihilator

Either two or three utilities to modify at minimum two files (OTA & TNT).

For unit creation:
* Notepad/Notepad++/VSCode/etc - edit/create FBIs, edit/create various TDFs (weapon, sounds, movement category, etc)
* Wings3D - modelling
* 3DO Builder - make model TA ready and textured
* Scriptor - write, compile, decompile
* GAF Builder/GAF Builder Pro - modify and create GAF files
* (Optionally) Photoshop/GIMP/etc - create 2D srites, animations, etc
* (Optionally) HPI tool - packing into UFO archive

So a brand new unit requires 4 utilities, across 5+ files (FBI, model, 3DO, BOS, COB, etc). All in various different directories that the game expects them to be in.

The main issue I have when modding TA is that it's a lot to keep track of. (Not to mention keeping on top of what utilities are best, etc.) The workflow isn't clean and simple. Not easy to compartmentalise.

## Comparison

Betheda's games have a core utility (CS, GECK, CK, etc) that handles most of the content creation. As well as a gathering point for the various workflows.

E.g. new sword: create model in Blender -> import into NifScope -> import into CS -> create plugin & play

Any changes you just go back to the CS.

## Proposal

A tool that is much like the CS. A central bit in the workflow where everything is brought together. It isn't a replacement for every utility as that would be a lot and possibly pushing the issue into the other extreme, but a lot of them to smooth out the flow.