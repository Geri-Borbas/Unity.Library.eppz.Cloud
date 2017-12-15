# eppz.Cloud [![Build Status](https://travis-ci.org/eppz/Unity.Test.eppz.png?branch=master)](https://travis-ci.org/eppz/Unity.Test.eppz)
> part of [**Unity.Library.eppz**](https://github.com/eppz/Unity.Library.eppz)


 iCloud Key-value store native iOS plugin for Unity. With callbacks on changes per value.


## Simple usage

You can use it just like `PlayerPrefs`, if you don't need more control.

```csharp
// Set values.
Cloud.SetStringForKey("eppz!", "name");
Cloud.SetFloatForKey(0.5f, "volume");
Cloud.SetIntForKey(4, "level");
Cloud.SetBoolForKey(true, "sound");

// Get values.
nameLabel.text = cloud.StringForKey("name");
volumeSlider.value = cloud.FloatForKey("volume");
game.level = cloud.IntForKey("level");
sound.volume = cloud.BoolForKey("sound");
```

As iCloud Key-Value store uses a local container on the device to manage data, iCloud Key-value store is effectively always available.

> See corresponding official **iCloud Design Guide** at [Choose the Proper iCloud Storage API](https://developer.apple.com/library/content/documentation/General/Conceptual/iCloudDesignGuide/Chapters/iCloudFundametals.html#//apple_ref/doc/uid/TP40012094-CH6-SW28) for details.


##  iCloud Key-Value store limitations

It is probably best suitable for stuff like preferences / game progress. Files, and binary data is likely to grow over the size limitations below pretty soon.

+ Storage is 1 MB per user
+ The maximum number of keys is 1024
+ Size limit for each value is 1 MB
+ The maximum length for a key string is 64 bytes (does not count against your 1 MB total quota)

> See corresponding official **iCloud Design Guide** at [Data Size Limits for Key-Value Storage](https://developer.apple.com/library/content/documentation/General/Conceptual/iCloudDesignGuide/Chapters/DesigningForKey-ValueDataIniCloud.html#//apple_ref/doc/uid/TP40012094-CH7-SW8) for details.


## Advanced usage

If you want to have a finer grained control over the details of synchronizing (key changes, conflict resolution, user changes), you'll probably like features below.

```csharp
// Keep UI in sync with cloud content.
Cloud.OnKeyChange("gold", (int value) =>
{ goldLabel.text = value; });
```

```csharp
// Simple resolution if local and remote values differ.
Cloud.OnKeyChange("gold", (int value) =>
{
	bool isConflict = (user.golds != value);
    if (isConflict)
    {
    	// Resolve strategy.
    	user.golds = Math.Max(user.golds, value);
    	// Sync (if any) new value.
        Cloud.SetIntForKey(user.golds, "gold");
        // Show (if any) new value.
    	goldLabel.text = user.golds;
    }
});
```

You can implement you own conflict resolution strategy at each value change callbacks. Also by these live callbacks you have the opportunity to update the game state / UI state on the fly.

On each callback, you can poll a paramerer `Cloud.latestChangeReason` that holds the reason for the given cloud update. Please note that when a cloud update occurs due to a user change (use signed out from iCloud, then signed in with a different user), you'll probably have to overwrite the game states without orchestrating any conflict resolution.


## Best practice for resolving conflicts

> See corresponding official **iCloud Design Guide** at [Resolving Key-Value Conflicts](https://developer.apple.com/library/content/documentation/General/Conceptual/iCloudDesignGuide/Chapters/DesigningForKey-ValueDataIniCloud.html#//apple_ref/doc/uid/TP40012094-CH7-SW6).

## License

> Licensed under the [MIT license](http://en.wikipedia.org/wiki/MIT_License).
