# eppz.Cloud

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