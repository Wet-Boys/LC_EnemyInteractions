# Lmao

Enemies can now emote randomly, that's all.

Working on adding more functionality in the future, but a more general AI handler will be required


# Emote Devs

If any mod wants to add their own emotes to this, the current process is just

`EmoteOptions.onKillEmotes.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Default Dance", 30));`

or

`EmoteOptions.intermittentEmoteList.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Gangnam Style", 1));`

`onKillEmotes` will trigger when a player dies

`intermittentEmoteList` will be played on enemies every few minutes


`EnemyEmotes` have a string for the emote name and a float for the max time the emote will play (primarily used for looping emotes you only want to play for a few seconds)