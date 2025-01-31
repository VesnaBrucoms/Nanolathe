# .ota File Format

The OTA format is used to store map settings, difficulty settings, and campaign mission AI directives. An OTA file is always paired with a TNT file.

Contents:
* [Structure](#structure)
* [Variables](#variables)
* [References](#references)

## Structure

The bulk of OTA files are key/value pairs, deliminated by `=` and suffixed with `;`. Key/value pairs are usually on their own lines, but can share lines.

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

This structure is also used in [FBI](./FBI.md) (unit settings) and [TDF](./TDF.md) (general settings) files.

## Variables

aa

## References

* TA Design Guide, "OTA", (November 1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/otadesc.htm [Last accessed 08/08/2024]
* Unknown, "Initial Mission Commands", (1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/ta-ota-fmt.txt [Last accessed 08/08/2024]