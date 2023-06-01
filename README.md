<p align="center"><img src="https://imgur.com/vupUkm8.png"></p>

<p align="center"><a href="https://youtu.be/TiChA9fTR-4" target="_blank"><img src="https://imgur.com/1ZV1Kks.png"></a></p>

# CodeOptPro

### Introduction
This is a simple Unity system that helps with performance.

## Table of Contents:

## Prerequisites
#### Unity Game Engine
Unity version **2021.3.25f1** and above should work. Some previous Unity versions should work as well but has not been tested. The main branch version is **2021.3.25f1**
***
## Stable Build
[Stable-v1.0.0]() is the latest stable build of the project. The unitypackage for this project can also be found there. If development is going to be done on this project then it is adviced to branch off of any _Stable_ branches because they will NOT be changed or updated except for README.md. Any other branches are subjected to change including the main branch.
***
## Installation
1. First download the latest [CodeOptPro-vx.x.x.unitypackage]() from the latest Stable build.
2. Once download is completed open up the Unity project you want to use this project in.
3. Now go to Assets -> Import Package -> Custom Package.
4. Selet the CodeOptPro-vx.x.x you just downloaded and open it.
5. Make sure everything is selected in the Import Unity Package otherwise there will be errors. Press the **Import** button to import the package.
6. Once import is done a new menu will popup called **KamranWali**.
7. This step is optional. To open the interface for CodeOptPro simply go to KamranWali -> CodeOptPro.
***
## Setup
You must set the scene up before using the CodeOptPro. This MUST be done in every scene if this system is to be used. It is very easy to set up the scene for CodeOptPro. I made sure this process is also automated as well. Follow the steps below.
1. Open the CodeOptPro interface by going to KamranWali -> CodeOptPro.
2. Once opened clicked the "SCENE SETUP" button. This will create a GameObject in the scene called _Managers_. By default two components will be added to called MonoAdvManager_Call and MonoAdvManager. That is it and the scene is ready for CodeOptPro.
***
## Features
#### Advance Awake & Start Methods:
The advance awake and start feature works like Unity's API Awake and Start method. The differences are that only one class, _MonoAdvManager_Call_, calls the Unity's API Awake and Start methods and that class calls the custom awake(AwakeAdv) and start(StartAdv) methods of other classes. This reduces the need to use Unity's API which in turn helps with performance. The custom awake and start methods are also called for the GameObjects that are inactive as well. Also as of this writing this feature will NOT work with instantiated objects but only with already loaded objects.

To use this feature simply import from _using KamranWali.CodeOptPro.Managers;_ and then extend from the class called _MonoAdv_. Afterwards just implement the imported methods which will be _AwakeAdv()_ and _StartAdv()_. Add your script to a GameObject. Now select your GameObject to open up the inspector for the script. Under _MonoAdv Global Properties_ we need to set the _Manager_ field. Click the field to open the selection window and for now select _DefaultManagerHelper_, I will later discuss in more details about _DefaultManagerHelper_. Now press the Play button and everything should be working. 

Once you exit the play mode you will notice that your scene has become dirty. This is done intentionally because just before entering the play mode CodeOptPro automatically finds all the objects needs to be referenced and stores them to the correct managers. So once this process is done only then will the play mode start. If you want to can enable auto save from the CodeOptPro interface from KamranWali -> CodeOptPro. Enabling this will save everything when entering play mode. So if you make any changes to the scene for testing those too will be saved. That is why I have it disabled by default so that the user understands what will happen if enabled.

#### Awake & Start Call Order:
Another powerful feature of CodeOptPro is that you can order which group's awake and start methods should be called first. By default every group belongs to _DefaultManagerHelper_. Adding a new group is easy. Follow the steps below:
1. In the Project tab right any folder where you want to create a new group. Then go to Create -> CodeOptPro -> ScriptableObjects -> Managers -> MonoAdvManagerHelper. Give the group a name.
2. Select the _Managers_ GameObject in the scene. Then add a new component called _MonoAdvManager_.
3. Now for the _Helper_ property under the _MonoAdvManager Global Properties_ select the newly created group.
4. Finally in MonoAdvManager_Call
***
