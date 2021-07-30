# NoDetailsForClienters

A mod to spoof values such as FPS and Ping.

Because some VRC mods show those values for other people.
Even though they're normally meant to be only visible to yourself in the game's UI.

## Usage

To use: Install [MelonLoader](https://melonwiki.xyz), and drag the dll file into the Mods folder under your VRChat installation directory.

Then you can edit the values in the MelonPreferences or with UIX.

Note that if some mod (such as PlayerList) uses a different method such as deltaTime to get your own FPS, it'll display the real FPS.
As far as I'm aware, though, remote clients can't see your deltaTime.

## TODO

* Possibly spoof platform?
* IP address -> Should use VPN.
* HWID? Knah has made a mod for it.

## Building

Open in Visual Studio, copy the few Melonloader DLLs into the Libs folder and press build.

## Development

Ping me in the VRC modding discord server if you want to contribute or have issues, or just do that here on Github. Please do note though that this mod is enough for my needs as is, so for feature requests it'd be good if you could submit a PR instead of just asking for it.
