# eppz.Cloud [![Build Status](https://travis-ci.org/eppz/Unity.Test.eppz.png?branch=master)](https://travis-ci.org/eppz/Unity.Test.eppz)
> part of [**Unity.Library.eppz**](https://github.com/eppz/Unity.Library.eppz)

📦 Unity native iOS plugin for iCloud Key-Value store.

```csharp
cloud.SetIntForKey(true, "gold");
cloud.OnKeyChanged("gold", (int value) =>
{
    if (value > user.golds)
    {
        user.golds = value;
        UI.UpdateGolds();
    }
});
cloud.onUserChanged(() => { alert("User changed, restarting game."); } );
```

## License

> Licensed under the [MIT license](http://en.wikipedia.org/wiki/MIT_License).
