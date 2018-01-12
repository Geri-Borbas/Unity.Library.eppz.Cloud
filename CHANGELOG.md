# eppz.Cloud

* 1.0.9

	+ iOS
		+ Removed log messages

* 1.0.8

	+ Unsubscrbe delegates / actions at `Cloud.OnDestroy()`
	+ Forced to add bitcode (`-fembed-bitcode` compiler flag)

* 1.0.7

	+ `Simulation.KeyValueStore`
		+ Added warning if non-existent keys queried

* 1.0.6

	+ `Cloud.plugin` gets instantiated lazy
	+ Added `Cloud.Initialize()` for external initialization hook
	+ Simulation classes can be (somewhat) subclassed
		+ Suitable to implement JSON based simulation in a product

* 1.0.5

	+ Fixes
		+ Populate plugin instance even if no mobile platform is selected (yet)
		+ `Editor` class namespaced

* 1.0.4

	+ On device account change test (app only suspended)
	+ Null check for `UnityString.m`
	+ iOS sandbox app
		+ UI populating upon start (like Unity sandbox app)
		+ Conflict resolving turned off by default

* 1.0.3

	+ Fixed sandbox project

* 1.0.1 - 1.0.2

	+ Initial load (`PopulateElementsFromCloud()`)
	+ Project settings
	+ Editor `AccountChange` test
		+ Added one more cloud simulation data
		+ Conflict resolution gets override

* 1.0.0

	+ Fixed Unity messaging from plugin
	+ Added Unity build post processing (for iCloud setup)

* 0.9.9

	+ Arbitrary `OnCloudChange` callback
		+ Can prevent `Cloud` from updating keys upon given circumstances
			+ Should turn off conflict reolution here at least
	+ Populate `latestChangeReason`
		+ `ChangeReason` enum is shared across `EPPZ.Cloud` namespace
	+ Logging can be switched from `Model.Settings`

* 0.9.85

	+ Hierarchy issue
		+ Conflict resolving actions should be invoked before UI update
			+ Added `priority` parameter to `OnKeyChange` (optional)
			+ `KeyValuePair` sorts actions by `priority` on the fly

* 0.9.81



	+ Added slight visual clue when a key changed (`Blink` animation)
	+ Fixes some inspector issues

* 0.9.8

	+ Testing / Model
		+ Key-value definitions is now in asset `eppz!/Cloud/Settings`
			+ Holds array of `KeyValuePair` definitions
		+ Editor cloud simulation in asset `eppz!/Cloud/Key-value store simulation`
			+ `Model.Simulation.KeyValueStore`
				+ Holds simulated key-value pairs
				+ Can invoke a simulated `CloudDidChange` event from inspector
					+ It results in the very same (!) method call that native plugins call from their respective codebase
				+ Custom inspectors / property drawers to somewhat declutter simulation experience
			+ Plugin class `Cloud_Editor` uses it to source accessors
		+ Change reason in callbacks

* 0.9.1

	+ `Scenes`
		+ Added UI helpers to plugin namespace

* 0.9.0

	+ `Scenes`
		+ Sandbox scene `Controller` implemented
		+ UI events hooked up
		+ `Canvas` and `Cloud` serialized to prefabs as well
	+ `Cloud`
		+ Made public features static
		+ Hooked up event registering
	+ `Model.KeyValuePair`
		+ Model holds the actions registered for on change events
	+ `Plugin.Cloud_Editor`
		+ Some logging when values set

* 0.7.0

	+ Built Unity sandbox scene (`Cloud.unity`) test UI

* 0.6.3

	+ Modeled iOS notification payload
		+ Now can be deserialized using `JsonUtility`
	+ Hooked up value type accessors

* 0.6.2

	+ Plain C plugin interface (with `NSString` parameters) for sandbox app usage
	+ Removed string conversions from sandbox app `ViewController`

* 0.6.0

	+ Icons
	+ Conflict resolving can be switched from UI
		+ Some refactor on action grouping
	+ Initialization happens upon `viewDidAppear`

* 0.5.0

	+ Added value types
		+ `EPPZ_Cloud_StringForKey` / `EPPZ_Cloud_SetStringForKey`
		+ `EPPZ_Cloud_FloatForKey` / `EPPZ_Cloud_SetFloatForKey`
		+ `EPPZ_Cloud_IntForKey` / `EPPZ_Cloud_SetIntForKey`
		+ `EPPZ_Cloud_BoolForKey` / `EPPZ_Cloud_SetBoolForKey`
	+ iOS Sandbox app implemented
		+ Hooked up UI outlets / actions
		+ Added keys with corresponding actions (blocks)
		+ Implemented per value conflict resolution strategies

* 0.4.2

	+ Built iOS Sandbox app test UI
		+ Explored auto layout constraints / stack views

* 0.4.0

	+ Implemented Unity plugin architecture
		+ `Plugin.Cloud` common plugin base class
		+ Individual platform implementations
			+ `Plugin.Cloud_iOS`
			+ `Plugin.Cloud_Android`
			+ `Plugin.Cloud_Editor`
		+ Key-value update models (key-value pair model hooks)
			+ Events can be invoked on remote updates
		+ `Cloud : MonoBehaviour` class for scene use
			+ Native plugins sends message(s) to this `GameObject`
			+ Key-value update models can be hooked up here
			+ Encapsulates various platform implementations
		+ `Plugin.Cloud_iOS` DLL imports implemented / hooked up
			+ Copy Files Phase for `eppz! Cloud.a` have setup

* 0.0.2

	+ Added `iOS~` native plugin project 
		+ Uses Key-Value store
		+ Can be tested directly in Xcode sandbox iOS project (actually on device)
		+ `NSNotification.userInfo` sent directly to Unity (via JSON)

* 0.0.1

	+ Initial commit