# Abstract

This is a movement controller intented for first person 3D grid/tile based dungeon crawlers like "Legend of Grimrock", "Eye of the beholder" and the likes.

Also it aims to be as minimalistic as possible while it comes with an simple level editor. There for it only implements "instant step" movement because that is basically one line of code per direction. 

Collision checks are done based on the underlying 2d tilemap and not using the buildin physisc/raycast system for the following reason:

While physics based controller are easy to set up and work fine in empty dungeons, it starts to become more challenging when you start to add moving entities (aka enemies) into the scene. You will end up placeing blocking colliders all over the place and while this will work it adds extra complexity as you need to manage those colliders as well. I am not even going into what you need to do if you want to place more then one entity onto a single tile. Its doable but it starts nice and easy but can become quiet challanging once you add realtime combat (its less of an issue with turnbased combat).

If you are looking for physics based controller with animated transistions and configurable animations curves you can checkout my "advanced" movement controller https://github.com/LutzGrosshennig/unity3d-advanced-grid-movement which was used in my Dungeon Crawler Game Jam 2023 entry. 

https://lutzgrosshennig.itch.io/the-shattered-sigil-of-harmony

![KgrO78](https://github.com/LutzGrosshennig/unity3d-minimal-tilebased-controller/assets/29707648/bd91f4f4-7ae4-4a3a-ad65-73c02dfc0732)


# The included level editor.
The level editor is quiet simple. Just add a checkmark for every tile that should be walkable. "Clear level" will destroy the instantiated level and "Generate level" will build one based on your layout and tile size setting. You may need to adjust the camera height and position as well as the far clip plane of the camera to match the tile size. Its a minimalistic approach after all.

Have fun,
Lutz
