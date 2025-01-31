# Application Framework
*Updated: 02/08/2024*

When it comes to Windows desktop application development there are a range of options available. This page runs through some of them to aid in deciding which to develop this modding tool with.

First, some assumptions going into this project. Each of these provide an initial narrowing of frameworks that I'm considering:
1. **Windows only support:** Total Annihilation only supports Windows, and correspondingly other modding tools are Windows only. I'm anticipating little desire for Linux nor Mac support.
2. **Desktop application:** for simplicity of use and stability of such a complex program, desktop only is preferable.
3. **Written in C#**: I am personally most familier with Windows application development in C#, and most of the frameworks available support the language natively or with significant support.

Of all of the listed assumptions I expect **Windows only** to be the one most likely to be challenged. As such I will give a small measure of weighting to supporting other platforms in deciding which framework to use.

The chosen framework will need to meet the following requirements:
* **Longevity:** it must still be supported to ensure that the project will be run on future platforms.
* **Stability:** unstable modding tools are a pain in the arse to use, so the framework itself must be stable. (Which shouldn't be an issue for any of the ones I'm looking at.)
* **3D rendering support:** the goal for later in the project's roadmap is to support rendering of the 3DO models, with possible editing and creation further down the line.
* **Easy to share:** the framework should not be getting in the way of sharing the tool to others to use. Must be able to be packaged as an EXE, possibly even with an installation package (e.g. MSIX).

## WinForms
*Windows Forms; first-party*

Pros:
* Mature
* Very, very fast development
* Some personal experience
* Packages to EXE
* Updated to support .NET 8

Cons:
* Software rendered
* No native 3D rendering support
* No real separation of UI, logic and data
* Windows only

Having been out of the space for a few years I was surprised to see WinForms still around, but having looked into it I can see why. It facilitates rapid development, looks good enough, and is robust with no sandboxing. It has a target usecase in industrial spaces where an application needs to be put together quickly with little fuss.

However, there's little separation of the UI, logic, and data which will likely lead to difficulties with maintainence and expanding as the tool grows past a certain size. The lack of 3D rendering support requires bringing in something else (such as embedding WPF), which at that point it might be better to use another framework.

## WPF
*Windows Presentation Forms; first-party*

Pros:
* Mature
* Stable (used by Visual Studio at least until 2017)
* Lots of personal experience
* Favours splitting the UI, logic, and data
* Hardware rendered
* 3D rendering support
* Packages to EXE
* Supports .NET 8

Cons:
* Slower to get up and running
* No longer actively maintained outside of support for newer .NET versions
* Windows only

WPF appears to be still going strong. It's getting on now, but that simply means plenty of documentation, extentions, and robustness much like WinForms. It's support for splitting the UI, logic, and data is a big positive for a more complex application such as this project, as is its native 3D rendering. However, it's lack of full support is an issue for longevity.

## UWP
*Universal Windows Platform; first-party*

Pros:
* Mature-ish
* Uses XAML
* Uno platform can take UWP multiplatform
* Hardware rendered
* 3D rendering support

Cons:
* No personal experience
* Windows only
* Can only be shared through the Windows Store, or sideloaded
* No support for .NET 5+
* Unlike even older technologies it appears that [MS is actively migrating from UWP](https://developercommunity.visualstudio.com/t/Add-NET-678-support-to-UWP/1596483)

With Microsoft themselves dropping UWP it would be unwise to build the tool usng it as it fails the longevity need. The bolted on sideloading to get around the Windows Store requirement is a straight no to go alongside the longevity.

## WinUI
*Windows UI 3; first-party*

Pros:
* Uses XAML
* Uno platform can take WinUI multiplatform
* 3D rendering support
* Packages to EXE
* Supports .NET 8

Cons:
* Newer framework
* No personal experience
* Not quite feature parity with WPF/UWP
* Windows only

WinUI looks promising as a possible replacement to WPF, however it might still be early days for the framework as of writing. Between the lower levels of documentation, extensions, and not yet reaching feature parity with older frameworks it might be better to revisit in a year's time for another project. Though for cross-platform it does require another framework as a wrapper, which is more overhead.

## AvaloniaUI
*Third-party*

Pros:
* Mature
* Uses XAML
* 3D rendering support
* Actively maintained
* Multi-platform support
* Supports .NET 8

Cons:
* No personal experience
* Extra complexity for cross-platform
* Third party

While this is a third party framework, it takes close inspiration from WPF. Unlike WPF it is still actively maintained with newer feature being implemented, such as 3D rendering support. I may not have any personal experience with AvaloniaUI, given that it uses XAML and is very similar to WPF I doubt it will take long for me to find my feet with it.

## Decision

Chosen Framework: **WPF**, as it's the one I'm most familier with. A very close second is AvaloniaUI due to it being essentially a WPF 2, but the extra complexity for cross-platform when it's currently an unlikely need isn't worth it. However, picking WPF first and being firm with making use of a pattern like MVVM will keep the possibility of easily porting to AvaloniaUI in the future open.

WinForms, while providing fast development exchanges this for poorer maintainability for a larger project such as this. While at the same time restricting the possibility of opening up to cross-platform in the future if needed. Meanwhile it's too soon to be diving into WinUI. I came across a good descriptor that WinUI is good for applications that just need standard controls, but for any that need custom (as this one will with at least the 3D rendering) then WPF/AvaloniaUI is better.

And finally, UWP is clearly on the way out. Microsoft is hoping to replace it with some combination of WinUI and MAUI.