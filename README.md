# NoDetailsForClienters

A mod to spoof values such as FPS and Ping.

Because some VRC mods show those values for other people.
Even though they're regularly only visible to yourself in standard game UIs'.

This mod's intent is to give users more options for privacy, since not everyone wants others to know how good or bad your PC & internet is.

## Warning

Modding is of course against VRC TOS. And of course this mod is provided without any warranty.

As an additional warning, the reason this mod is not verified in the modding group is as follows:

> Ping/FPS spoofing is known to break IK interpolation, is easily detectable by VRC (if they wanted to), and those numbers are available in vanilla client via debug overlays. Lots of downsides for vanishingly little benefit

The mentioned concerns are of course valid, but I personally believe that it should be up to each individual to determine if the benefits outweigh the downsides.

## Usage

To use: Install [MelonLoader](https://melonwiki.xyz), and drag the dll file into the Mods folder under your VRChat installation directory.

Then you can edit the values in the MelonPreferences or with UIX.

Note that if some mod (such as PlayerList) uses a different method such as deltaTime to get your own FPS, it'll display the real FPS.
As far as I'm aware, though, remote clients can't see your deltaTime.

## Attribution

null mentioned the values to spoof in the [VRC modding discord](https://discord.gg/7EQCmgrUnH).
Which made implementing this mod rather quick & easy, so huge props to them.

## Spoofing other values

* Platform: possible feature I guess? PR's welcome
* IP address: use a VPN.
* HWID: use [Knah's HWID patch](https://github.com/knah/ML-UniversalMods#hwidpatch)
* SteamID: use [Knah's NoSteamAtAll](https://github.com/knah/ML-UniversalMods#nosteamatall) to disable having a SteamID

## Building

Open in Visual Studio, copy the few Melonloader DLLs into the Libs folder and press build.

## Development

Ping me in the VRC modding discord server if you want to contribute or have issues, or just do that here on Github. Please do note though that this mod is enough for my needs as is, so for feature requests it'd be good if you could submit a PR instead of just asking for it.
