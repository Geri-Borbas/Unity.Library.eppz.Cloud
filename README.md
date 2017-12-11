# eppz.Cloud [![Build Status](https://travis-ci.org/eppz/Unity.Test.eppz.png?branch=master)](https://travis-ci.org/eppz/Unity.Test.eppz)
> part of [**Unity.Library.eppz**](https://github.com/eppz/Unity.Library.eppz)

 iCloud Key-Value store native iOS plugin for Unity.

```csharp
cloud.OnKeyChanged("gold", (int value) =>
{
    if (value > user.golds)
    {
        user.golds = value;
        UI.UpdateGolds();
    }
    else
    {
        cloud.SetIntForKey(user.golds, "gold");
    }
});
```

##  iCloud Key-Value store limitations

+ Storage is 1 MB per user
+ The maximum number of keys is 1024
+ Size limit for each value is 1 MB
+ The maximum length for a key string is 64 bytes (does not count against your 1 MB total quota)

> See corresponding official **iCloud Design Guide** at [Data Size Limits for Key-Value Storage](https://developer.apple.com/library/content/documentation/General/Conceptual/iCloudDesignGuide/Chapters/DesigningForKey-ValueDataIniCloud.html#//apple_ref/doc/uid/TP40012094-CH7-SW8) for details.

## Best practice for resolving conflicts

> See corresponding official **iCloud Design Guide** at [Resolving Key-Value Conflicts](https://developer.apple.com/library/content/documentation/General/Conceptual/iCloudDesignGuide/Chapters/DesigningForKey-ValueDataIniCloud.html#//apple_ref/doc/uid/TP40012094-CH7-SW6).

## License

> Licensed under the [MIT license](http://en.wikipedia.org/wiki/MIT_License).
