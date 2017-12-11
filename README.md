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

## License

> Licensed under the [MIT license](http://en.wikipedia.org/wiki/MIT_License).
