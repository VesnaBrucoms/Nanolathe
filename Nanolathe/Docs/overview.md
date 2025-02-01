# Overview

Nanolathe is a modding tool that aims to make modding the video game Total Annihilation more streamlined.

These are the docs for the project.

* [Formats](/Formats/index.md)
* [Research](/Research/application%20tech.md)

Unlike what I've typically worked with, TA usually has many many different files storing different types of data atomically for different parts. Such as maps being comprised of OTA (settings), TNT (terrain & height), SCT (tiled terrain & height), and TDF (map elements) files.

For this full modding tool to be approachable and reletively easy to use for newcomers, I think the various different file formats need to be abstracted away. For example, creating a new unit should be: create new model, animate it, script it, input its settings, create unit picture. Then when saving the tool handles the creation of the various files in the background with the user none-the-wiser (unless they want to be).

## Roadmap

This is a big project that's going to take a long time to see to completion, as well as a lot of figuring out how to best abstract and present the information to the user. Even accounting for all of the exploratory work that I'm building off. A good rough guide for what the tool will be capable enough (for an initial full release) is Bethesda's Construction Kit. This allows for the creation of nearly anything in the game, and "brings together" the various other aspects of the assets in the form of linking game objects to models, icons, sounds, etc. Once this tool reaches even around the halfway mark reletive to the CS then it will likely be the most fully fledged TA modding tool out there.

So to see this through this is the roadmap:

1. Document file format info
2. Design game object models and first pass of workflows

From here it migt be best to proceed with the simpler formats and build from there. To begin with the TDF format looks to be ideal as it is used for a great variety of elements and doesn't vary in its basic structure. It's also a godd starting point for inital information that the tool will need (i.e. names, descriptions, etc).

3. Load the TDF file format, simply outputting the data
4. Input the loaded TDF data into the data models
5. Build UI to view the data
6. Add editing and saving

Next grouping of formats to tackle are the images. These are mostly reletively simple for images.

7. Load the GAF, TAF, BMP, PCX formats, simply outputting the data
8. Input the images into the data models
9. Update the UI to display the images
10. Update to allow image selection and saving of the selections (i.e. open file dialogue to select a different unitpic)

It's around this point that the tool enters into new territory for TA modding tools. Next, the linking to the remaining assets.

11. Update UI to display other assets linked to the objects
12. Update UI to allow selection and saving of models, sounds, palletes, scripts, etc

To really start rounding off unit creation and editing, models and scripts need to be fully loaded in.

13. Load BOS and 3DS formats, simply outputting the data
14. Input scripts and models into the object models
15. Update the UI to display scripts and models
16. Update to allow script editing, saving, and compiling

------

Create new unit -> create each file object (download, object, unit, etc)
    - each text file object :: gets and sets settings as their actual types, backed by settings file, tracks all possible settings and their types and default values, tracks all possible sections
        - create settings file :: stores settings as strings
            - create default sections
                - create variables
                - create subsections