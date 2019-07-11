
Sample Project for SimCapiUnity3D
--------


## Requirements ##

Unity

SimCapiUnity3D

## Setting up the project ##

1. Download the SimCapiUnity3D asset package and import it into the project.

https://github.com/SmartSparrow/simcapi-unity3d/releases


## Building to WebGL ##

1. Switch to the WebGL Platform in the Unity Build settings.

2. In the WebGL Player Settings under `Settings for WebGL` select the `SimCapi` WebGL Template.

This template adds the required line of code:

```window.gameInstance = gameInstance;```

under the standard line of code that loads the unity player:

```var gameInstance = UnityLoader.instantiate("gameContainer", "%UNITY_WEBGL_BUILD_URL%");```

So the final html file must contain at least these 2 lines of code

```
var gameInstance = UnityLoader.instantiate("gameContainer", "%UNITY_WEBGL_BUILD_URL%");
window.gameInstance = gameInstance;
```


## link.xml ##

The SimCapiUnity3D package includes the file called `link.xml`.

This is to stop unity from stripping code that is used by `Json.Net.Unity3D`.

If this file is not present when you build the WebGL project, you will get a `MissingMethodException` when trying to run the application in the browser.

More info at the bottom of this page:
https://github.com/SaladLab/Json.Net.Unity3D#unity-compatibility


## Running in Smart Sparrow ##

You can either upload the files to a server that has a valid SSL certificate.

OR

Use NODEJS to host a local HTTPS: Server