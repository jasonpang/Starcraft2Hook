Starcraft 2 Hook
==============

Description
------------

Hooks Starcraft 2's DirectX APIs to draw overlays and read information. Inspired by the similar [Stanford project](http://graphics.stanford.edu/~mdfisher/GameAIs.html).

The [Stanford project](http://graphics.stanford.edu/~mdfisher/GameAIs.html), written in C++, hooks into the game's DirectX drawing to read text and model data so that it can play the game. My program, written in C#, is able to hook into the game and display an FPS counter and colorize textures. The transition from managed to unmanaged code (C# is managed, DirectX is unmanaged) slows down the frame rate to around 2 FPS even on a fast quad-core computer.

Tech Stack
----------

* C#
  * [EasyHook](http://easyhook.codeplex.com/) for DirectX API interception
  * [SharpDX](http://sharpdx.org/) for a managed implementation of DirectX
  * NLog for logging
  
Interesting tidbits
-------------------
* Of the 118 DirectX API functions, almost all are hooked
* Extensive program architecture, multiple library projects with an Injector to inject the DLL into the target process
