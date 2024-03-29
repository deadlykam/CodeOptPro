<p align="center"><img src="https://imgur.com/vupUkm8.png"></p>

<p align="center"><a href="https://youtu.be/XJ1OeGddYXM" target="_blank"><img src="https://imgur.com/1ZV1Kks.png"></a></p>

# CodeOptPro

### Introduction
This is a simple Unity system that helps with performance.

## Table of Contents:
- [Prerequisites](#prerequisites)
- [Stable Build](#stable-build)
- [Installation](#installation)
- [Setup](#setup)
- [Features](#features)
  - [Advance Awake And Start Methods](#advance-awake-and-start-methods)
  - [Awake And Start Call Order](#awake-and-start-call-order)
  - [Performant Update](#performant-update)
    - [UpdateManagerLocal](#updatemanagerlocal)
    - [UpdateManagerGlobal](#updatemanagerglobal)
  - [Vector3 Performant Calculation](#vector3-performant-calculation)
  - [Performant Data Share/Use](#performant-data-shareuse)
    - [Actions](#1-actions)
    - [FixedVars](#2-fixedvars)
    - [Vars](#3-vars)
    - [Variable Creator](#variable-creator)
  - [TimerCountdown](#timercountdown)
  - [Bars](#bars)
  - [Searches](#searches)
    - [RayHit](#rayhit)
    - [RayHitSearch](#rayhitsearch)
  - [Pooling System](#pooling-system)
    - [Custom Pooling System](#custom-pooling-system)
  - [Folder Selection Palette](#folder-selection-palette)
- [Developer](#developer)
  - [CodeOptProSetupAuto](#codeoptprosetupauto)
  - [MonoAdvManager_Call](#monoadvmanager_call)
  - [MonoAdvManager](#monoadvmanager)
  - [UpdateManagerLocal](#updatemanagerlocal-1)
  - [UpdateManagerGlobal](#updatemanagerglobal-1)
  - [MonoAdv](#monoadv)
  - [MonoAdvUpdateLocal](#monoadvupdatelocal)
  - [MonoAdvUpdateGlobal](#monoadvupdateglobal)
- [Updates](#updates)
- [Versioning](#versioning)
- [Authors](#authors)
- [License](#license)

## Prerequisites
#### Unity Game Engine
Unity version **2021.3.25f1** and above should work. Some previous Unity versions should work as well but has not been tested. The main branch version is **2021.3.25f1**
***
## Stable Build
[Stable-v1.3.0](https://github.com/deadlykam/CodeOptPro/tree/Stable-v1.3.0) is the latest stable build of the project. The unitypackage for this project can also be found there. If development is going to be done on this project then it is adviced to branch off of any _Stable_ branches because they will **NOT** be changed or updated except for README.md. Any other branches are subjected to change including the main branch.
***
## Installation
1. First download the latest [CodeOptPro-v1.3.0.unitypackage](https://github.com/deadlykam/CodeOptPro/releases/tag/v1.3.0) from the latest Stable build.
2. Once download is completed open up the Unity project you want to use this project in.
3. Now go to Assets -> Import Package -> Custom Package.
4. Selet the CodeOptPro-v1.2.0 you just downloaded and open it.
5. Make sure everything is selected in the Import Unity Package otherwise there will be errors. Press the **Import** button to import the package.
6. Once import is done a new menu will popup called **KamranWali**.
7. This step is optional. To open the interface for CodeOptPro simply go to KamranWali -> CodeOptPro -> CodeOptPro.
***
## Setup
You must set the scene up before using the CodeOptPro. This MUST be done in every scene if this system is to be used. It is very easy to set up the scene for CodeOptPro. I made sure this process is also automated as well. Follow the steps below.
1. Open the CodeOptPro interface by going to KamranWali -> CodeOptPro -> CodeOptPro.
2. Once opened clicked the "SCENE SETUP" button. This will create a GameObject in the scene called _Managers_. By default two components will be added to called MonoAdvManager_Call and MonoAdvManager. That is it and the scene is ready for CodeOptPro.
***
## Features
#### Advance Awake And Start Methods:
The advance awake and start feature works like Unity's API Awake and Start method. The differences are that only one class, _MonoAdvManager_Call_, calls the Unity's API Awake and Start methods and that class calls the custom awake(AwakeAdv) and start(StartAdv) methods of other classes. This reduces the need to use Unity's API which in turn helps with performance. The custom awake and start methods are also called for the GameObjects that are inactive as well. Also as of this writing this feature will NOT work with instantiated objects but only with already loaded objects.

You can now have multiple _MonoAdvManager_Call_ components in one scene. I have also allowed grouping for this component as well. By default _DefaultManagerCallHelper_ group is used. You can use your own groups as well. It is simple to create a new group. Just right click any folder where you want to store the group then go to _Create -> CodeOptPro -> ScriptableObjects -> Managers -> MonoAdvManager_CallHelper_. Give the group any name you want. Then select the game object that contains the newly created _MonoAdvManager_Call_ component. In the _Helper_ field select the newly created group. Now select a game object that contains _MonoAdvManager_ component. In the _Manager_Caller_ field select the newly created group. That is it, you are done setting up a new _MonoAdvManager_Call_ component to work in the same scene. This is mainly useful when creating prefabs or updating them.

To use this feature simply import from _using KamranWali.CodeOptPro.Managers;_ and then extend from the class called _MonoAdv_. Afterwards just implement the imported methods which will be _AwakeAdv()_ and _StartAdv()_. Add your script to a GameObject. Now select your GameObject to open up the inspector for the script. Under _MonoAdv Global Properties_ we need to set the _Manager_ field. Click the field to open the selection window and for now select _DefaultManagerHelper_, I will later discuss in more details about _DefaultManagerHelper_. Now press the Play button and everything should be working. 

Once you exit the play mode you will notice that your scene has become dirty. This is done intentionally because just before entering the play mode CodeOptPro automatically finds all the objects needs to be referenced and stores them to the correct managers. So once this process is done only then will the play mode start. If you want to can enable auto save from the CodeOptPro interface from KamranWali -> CodeOptPro. Enabling this will save everything when entering play mode. So if you make any changes to the scene for testing those too will be saved. That is why I have it disabled by default so that the user understands what will happen if enabled.

#### Awake And Start Call Order:
Another powerful feature of CodeOptPro is that you can order which group's awake and start methods should be called first. By default every group belongs to _DefaultManagerHelper_. Adding a new group is easy. Just follow the steps below:
1. In the Project tab right any folder where you want to create a new group. Then go to Create -> CodeOptPro -> ScriptableObjects -> Managers -> MonoAdvManagerHelper. Give the group a name.
2. Select the _Managers_ GameObject in the scene. Then add a new component called _MonoAdvManager_. (You can add this component to any GameObject. It does NOT need to be the _Managers_ GameObject).
3. Now for the _Helper_ property under the _MonoAdvManager Global Properties_ select the newly created group.
4. Now open up the CodeOptPro interface, _KamranWali -> CodeOptPro_, if not opened already and press the _SETUP_ button.
5. Finally select the _Managers_ game object again. Now notice the *MonoAdvManager_Call* script. You will see under the _Managers_ property the newly added group is added to the bottom. So basically this means that the _DefaultManagerHelper_ will be first to be called in Awake() and Start() and then the newly added group. You can change the order by dragging the elements.

#### Performant Update:
The other good feature of CodeOptPro is that you can use custom update to update your scripts. This custom update allows you to share one Update() method with many scripts. This in turn saves lot of performance issues as calling Unity's Update() takes a hit on performance. There are two types of custom update class in CodeOptPro, _UpdateManagerLocal_ and _UpdateManagerGlobal_. The main logic between the two are same but the only difference is that the local one needs to be referenced in coupled way while the global one is referenced in a decoupled way. To use the custom update follow the steps below.

###### UpdateManagerLocal:
1. Create a new script and extend the class called _MonoAdvUpdateLocal_.
2. Import all the abstract methods. Now let me explain the importance of each methods below:
- a. _AwakeAdv()_: Custom awake method
- b. _StartAdv()_: Custom start method
- c. _UpdateObject()_: Custom update method. All the update logics must go here. Also if your logic requires calculation with Time.delta time then it is suggested that you calculate with _updateManager.GetTime()_ method. This method will return the correct delta time calculation for the custom update. If this is not used and Time.deltaTime is used instead then your logic will give weird results.
- d. _IsActive()_: This method is called by the update manager to check if the object is active or NOT. If object is active then the object's _UpdateObject()_ method will be called. If the object is deactive then the object's _UpdateObject()_ will NOT be called. You can set any conditions as to here to say what is considered active or inactive. For example you could use the object's gameobject to check if the object should be active or not by using _gameObject.activeSelf_. Remember if you do NOT use _gameObject.activeSelf_ to check if the object should be active or not then the _UpdateObject()_ will still be called when the game object is hidden.
- e. _SetActive(bool)_: This method sets the active state of the object. Use this method to activate or deactivate the object's _UpdateObject()_ method. For example if the _gameObject.activeSelf_ is being used in the method _IsActive()_ then in _SetActive(bool)_ just hide or show the game object by using the code _gameObject.SetActive(isActivate)_. Again you can use any logic here that decides how to activate the object for update.
3. Now once you are done implementing your new script then go back to the editor. Create a new GameObject or object for which your new script will be used for. Before adding your new script we first need to add the custom update manager. Click the _Add Component_ button and search and add the script called _UpdateManagerLocal_.
4. Now add your script. Once added drag and drop the _UpdateManagerLocal_ into the _updateManager_ field of your script which is under the _MonoAdvUpdateLocal Local Properties_.
5. Finally press the play button and your script should work especially the update logic inside _UpdateObject()_ method.

Now if you create more scripts that requires the use of update per frame then just drag and drop the _UpdateManagerLocal_ to the _updateManager_ field and they too will start to use update per frame and share the main Update() method from the _UpdateManagerLocal_. There is a field in _UpdateManagerLocal_ called _NumUpdate_. This value means how many objects should be updated per frame. For example if this value is set to 5 then 5 objects will be update in one frame cycle. If there are too many objects that needs to be updated then increasing this value should make the update process much better but that depends on your scripts and their logic.

###### UpdateManagerGlobal:
1. Create a new script and extend the class called _MonoAdvUpdateGlobal_.
2. Import all the abstract methods and implement them. For explanation see point 2 in UpdateManagerLocal. It is similar to that.
3. Create a new GameObjet or object for which your new script will be used for. Before adding your new script we first need to add the custom update manager. Click the _Add Component_ button and search and add the script called _UpdateManagerGlobal_. It is suggested to add the _UpdateManagerGlobal_ in a GameObject which is inside the _Managers_ game object so that it remains organized because a scriptable object is used to access this update manager in a decouple fashion.
4. Now we need to create the scriptable object for the _UpdateManagerGlobal_. Right click any folder where you want to store the helper scriptable object then go to Create -> CodeOptPro -> ScriptableObjects -> Managers -> UpdateManagerGlobalHelper. Then give it any name you want. After that set the newly created UpdateManagerGlobalHelper in the field called _Helper_ under the _UpdateManagerGlobal Global Properties_ inside the _UpdateManagerGlobal_.
5. Now add the newly created UpdateManagerGlobalHelper in your script in the field called _updateManager_ under _MonoAdvUpdateGlobal Global Properties_.
6. Finally press the play button and your script should work especially the update logic inside _UpdateObject()_ method.

_UpdateManagerGlobal_ has the _NumUpdate_ field as well and works similarly to _UpdateManagerLocal_ so for explanation on how the field works please check the notes there. The good thing about _UpdateManagerGlobal_ is that you only need to add the helper scriptable object for the update to happen. No need to search and drag and drop. Just simply add the scriptable object from the list and it is done. This saves time too.

#### Vector3 Performant Calculation:
I have also added performant Vector 3 calculations that will save some performance issue in the long run especially when it comes to Vector3 distance calculation. I will give just brief explanation of the methods
1. _Vec3.Distance(Vector3, Vector3)_ - This method calculates the distance between two Vector3s and the returned value is a squared value. This means that if you want to check if the distance of the two vector point is greater/less than 5 units then you must make the 5 squared which is simply 5x5 = 25. Meaning you are comparing against 25. This will save lot of performance issue later down the line when too many objects needs distance check.
2. _Vec3.Subtract(Vector3, Vector3)_ - This method subtracts two Vector3s without creating any garbage and returns a Vector3 value.
3. _Vec3.Add(Vector3, Vector3)_ - This method adds two Vector3s without creating any garbage and returns a Vector3 value.
4. _Vec3.Divide(Vector3, float)_ - This method multiplys a float value to the Vector3 value without creating any garbage and returns a Vector3 value.
5. _Vec3.Multiply(Vector3, float)_ - This method divides the Vector3 value with the float value without creating any garbage and returns a Vector3 value.

#### Performant Data Share/Use:
I have also added a feature that allows to share/use data in a performant way by using ScriptableObject. For now there are three categories of data share and each have their own different data types.
##### 1. Actions
In this category different type of action delegates are used and share. So if you want to share action delegates with no parameters or some basic property parameters then you can do so. Basically if you want to share a specific method type in a script with other scripts then you can use the Action Types. Below are all the types.
  - **Action** - This shares delegates with no parameters. In the _void Action.SetAction(System.Action)_ method only methods with no parameters can be set, example _void SomeMethod()_. Use _void Action.CallAction()_ method to invoke the shared delegate. To use Action simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.Action_.
  - **ActionBool** - This shares delegates with one bool parameter. In the _void ActionBool.SetAction(System.Action<bool>)_ method only methods with one bool parameter can be set, example _void SomeMethod(bool someBool)_. Use _void ActionBool.CallAction()_ method to invoke the shared delegate. To use ActionBool simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.ActionBool_.
  - **ActionDouble** - This shares delegates with one double parameter. In the _void ActionDouble.SetAction(System.Action<double>)_ method only methods with one double parameter can be set, example _void SomeMethod(double someDouble)_. Use _void ActionDouble.CallAction()_ method to invoke the shared delegate. To use ActionDouble simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.ActionDouble_.
  - **ActionFloat** - This shares delegates with one float parameter. In the _void ActionFloat.SetAction(System.Action<float>)_ method only methods with one float parameter can be set, example _void SomeMethod(float someFloat)_. Use _void ActionFloat.CallAction()_ method to invoke the shared delegate. To use ActionFloat simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.ActionFloat._
  - **ActionGameObject** - This shares delegates with one GameObject parameter. In the _void ActionGameObject.SetAction(System.Action<GameObject>)_ method only methods with one GameObject parameter can be set, example _void SomeMethod(GameObject someGameObject)_. Use _void ActionGameObject.CallAction()_ method to invoke the shared delegate. To use ActionGameObject simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.ActionGameObject_.
  - **ActionInt** - This shares delegates with one int parameter. In the _void ActionInt.SetAction(System.Action<int>)_ method only methods with one int paramater can be set, example _void SomeMethod(int someInt)_. Use _void ActionInt.CallAction()_ method to invoke the shared delegate. To use ActionInt simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.ActionInt_.
  - **ActionQuaternion** - This shares delegates with one quaternion parameter. In the _void ActionQuaternion.SetAction(System.Action<Quaternion>)_ method only methods with one quaternion parameter can be set, example _void SomeMethod(Quaternion someQuaternion)_. Use _void ActionQuaternion.CallAction()_ method to invoke the shared delegate. To use ActionQuaternion simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.ActionQuaternion_.
  - **ActionString** - This shares delegates with one string parameter. In the _void ActinoString.SetAction(System.Action<string>)_ method only methods with one string parameter can be set, example _void SomeMethod(string someString)_. Use _void ActionString.CallAction()_ method to invoke the shared delegate. To use ActionString simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.ActionString_.
  - **ActionTransform** - This shares delegates with one transform parameter. In the _void ActionTransform.SetAction(System.Action<Transform>)_ method only methods with one transform parameter can be set, example _void SomeMethod(Transform someTransform)_. Use _void ActionTransform.CallAction()_ method to invoke the shared delegate. To use ActionTransform simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.ActionTransform_.
  - **ActionVector2** - This shares delegates with one vector2 parameter. In the _void ActionVector2.SetAction(System.Action<Vector2>)_ method only methods with one vector2 parameter can be set, example _void SomeMethod(Vector2 someVec2)_. Use _void ActionVector2.CallAction()_ method to invoke the shared delegate. To use ActionVector2 simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.ActionVector2_.
  - **ActionVector3** - This shares delegates with one vector3 parameter. In the _void ActionVector3.SetAction(System.Action<Vector3>)_ method only methods with one vector3 parameter can be set, example _void SomeMethod(Vector3 someVec3)_. Use _void ActionVector3.CallAction()_ method to invoke the shared delegate. To use ActionVector3 simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Actions.ActionVector3_.
  - **BaseAction** - If you want to create your own shared _System.Action_ delegate type then extend from **BaseAction**. Just check out the above Action scripts to know how to code for it, it is very simple. The only difference would be the type and the name of the menu.

##### 2. FixedVars
In this category different type of data types are shared, example bool, float, int, string etc. You only need to create one fixed var and share it with multiple objects, example - If five objects needs an int value of 1 then create a fixed var of type int that has the value 1 and share that. In that way only one int value of 1 is created instead of five which saves some memory. Like the name suggests the values are fixed and can **NOT** be updated. Below are all the types.
  - **FixedBoolVar** - This FixedVar shares _bool_ data types. When creating the FixedBoolVar set the value either true or false by clicking the tick box. To get the value simply call the method _bool FixedBoolVar.GetValue()_. To use FixedBoolVar simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.FixedVars.FixedBoolVar_.
  - **FixedDoubleVar** - This FixedVar shares _double_ data types. When creating the FixedDoubleVar set the value to any double type value. To get the value simply call the method _double FixedDoubleVar.GetValue()_. To use FixedDoubleVar simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.FixedVars.FixedDoubleVar_.
  - **FixedFloatVar** - This FixedVar shares _float_ data types. When creating the FixedFloatVar set the value to any float type value. To get the value simply call the method _float FixedFloatVar.GetValue()_. To use FixedFloatVar simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.FixedVars.FixedFloatVar_.
  - **FixedIntVar** - This FixedVar shares _int_ data types. When creating the FixedIntVar set the value to any int type value. To get the value simply call the method _int FixedIntVar.GetValue()_. To use FixedIntVar simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.FixedVars.FixedIntVar_.
  - **FixedLayerMaskVar** - This FixedVar shares _LayerMask_ data types. When creating the FixedLayerMaskVar select any layer mask values. To get the value simply call the method _LayerMask FixedLayerMask.GetValue()_. To use FixedLayerMaskVar simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.FixedVar.FixedLayerMaskVar_.
  - **FixedStringVar** - This FixedVar shares _string_ data types. When creating the FixedStringVar set the value to any string type value. To get the value simply call the method _string FixedStringVar.GetValue()_. To use FixedStringVar simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.FixedVars.FixedStringVar_.
  - **FixedVector2Var** - This FixedVar shares _Vector2_ data types. When creating the FixedVector2Var set the value to any Vector2 type value. To get the value simply call the method _Vector2 FixedVector2Var.GetValue()_. To use FixedVector2Var simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.FixedVars.FixedVector2Var_.
  - **FixedVector3Var** - This FixedVar shares _Vector3_ data types. When creating the FixedVector3Var set the value to any Vector3 type value. To get the value simply call the method _Vector3 FixedVector3Var.GetValue()_. To use FixedVector3Var simply import by calling _using KamranWali.CodeOptPro.ScriptableObjects.FixedVars.FixedVector3Var_.
  - **BaseFixedVar** - If you want to create your own FixedVar type then you must extend from **BaseFixedVar**. Just check out the above FixedVar scripts to know how to code for it, it is very simple. The only difference would be the type and the name of the menu.

##### 3. Vars
Just like FixedVars this category shares different type of data types as well, example bool, float, int, string etc. The only difference is that you can **NOT** set any values here like FixedVars and the values may change. Vars basically shares values that are constantly changing. For example - You have 5 objects that wants to know the player's position. Then just create a Vector3Var and make the player script constantly update the newly created Vector3Var. Then add the newly created Vector3Var to the other 5 objects. Now all of those 5 objects have access to the player's position without the need of player script reference. Below are all the types.
  - **BoolVar** - This Var shares _bool_ data types. To set the value simply call _void BoolVar.SetValue(bool value)_. To get the value just call _bool BoolVar.GetValue()_. To use BoolVar just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.BoolVar_.
  - **CameraVar** - This Var shares _Camera_ data types. To set the value simply call _void CameraVar.SetValue(Camera value)_. To get the value just call _Camera CameraVar.GetValue()_. To use CameraVar just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.CameraVar_.
  - **DoubleVar** - This Var shares _double_ data types. To set the value simply call _void DoubleVar.SetValue(double value)_. To get the value just call _double DoubleVar.GetValue()_. To use DoubleVar just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.DoubleVar_.
  - **FloatVar** - This Var shares _float_ data types. To set the value simply call _void FloatVar.SetValue(float value)_. To get the value just call _float FloatVar.GetValue()_. To use FloatVar just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.FloatVar_.
  - **GameObjectVar** - This Var shares _GameObject_ data types. To set the value simply call _void GameObjectVar.SetValue(GameObject value)_. To get the value just call _GameObject GameObjectVar.GetValue()_. To use GameObjectVar just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.GameObjectVar_.
  - **IntVar** - This Var shares _int_ data types. To set the value simply call _void IntVar.SetValue(int value)_. To get the value just call _int IntVar.GetValue()_. To use IntVar just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.IntVar_.
  - **QuaternionVar** - This Var shares _Quaternion_ data types. To set the value simply call _void QuaternionVar.SetValue(Quaternion value)_. To get the value just call _Quaternion QuaternionVar.GetValue()_. To use QuaternionVar just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.QuaternionVar_.
  - **StringVar** - This Var shares _string_ data types. To set the value simply call _void StringVar.SetValue(string value)_. To get the value just call _string StringVar.GetValue()_. To use StringVar just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.StringVar_.
  - **TransformVar** - This Var shares _Transform_ data types. To set the value simply call _void TransformVar.SetValue(Transform value)_. To get the value just call _Transform TransformVar.GetValue()_. To use TransformVar just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.TransformVar_.
  - **Vector2Var** - This Var shares _Vector2_ data types. To set the value simply call _void Vector2Var.SetValue(Vector2 value)_. To get the value just call _Vector2 Vector2Var.GetValue()_. To use Vector2Var just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.Vector2Var_.
  - **Vector3Var** - This Var shares _Vector3_ data types. To set the value simply call _void Vector3Var.SetValue(Vector3 value)_. To get the value just call _Vector3 Vector3Var.GetValue()_. To use Vector3Var just import by calling _using KamranWali.CodeOptPro.ScriptableObjects.Vars.Vector3Var_.
  - **BaseVar** - If you want to create your own Var type then you must extend from **BaseVar**. Just check out the above Var scripts to know how to code for it, it is very simple. The only difference would be the type and the name of the menu.

#### Variable Creator
There are 2 ways to create a variable from any category. Below are the two ways.
1. The first way is to right click any folder where you want to create a variable and then go to _CodeOptPro -> ScriptableObjects -> Actions_ or _CodeOptPro -> ScriptableObjects -> FixedVars_ or _CodeOptPro -> ScriptableObjects -> Vars_. This will then create a variable and you can name it anything you want and if it is a type where you can give values then you can give it a value as well.
2. The second way is use the _Variable Creator_ window to create any variable. You can open the _Variable Creator_ window by going to the menu _KamranWali -> CodeOptPro -> Variable Creator_. I would recommend using the _Variable Creator_ as it is faster to create a variable and you can select which location every specific variables should be created in. See the image below how the _Variable Creator_ looks like and I will explain the highlighted parts.

| ![Variable-Creator1.png](https://imgur.com/I7c9IZl.png) | 
|:--:| 
| *Variable Creator* |

  - **a.** _Name_ - This where you give the name of the variable you want to create.
  - **b.** _Update Path_ - If you want to update the path where the new variable will be created then right click the folder where the variable should be created and selected _Copy Path_. Paste the copied path in the path field, _f_. Finally press the _Update Path_ button and the path will be updated. This will only update the path for 1 variable type, in this case _Action_ type. This way the _Variable Creator_ will allow you to have different paths for different variable types. The default path is _Assets/KamranWali/CodeOptPro/SO_Data_.
  - **c.** _Category_ - This is where you get to select from which category the variable will be created. For now there are 3 categories which are _Actions_, _FixedVariables_ and _Variables_.
  - **d.** _Variable Type_ - This is where you get to select which type of variable to create. Each category have different type of variable types. The image shows _Actions_ but this label changes when selecting a different category.
  - **e.** _Log_ - Here the logs for the _Variable Creator_ will be shown.
  - **f.** _Path_ - This is the path or folder location where the new variable will be created. You can updated this path as well. Follow the instructions in _b._ to see how to update path.
  - **g.** _Create Variable_ - This button will create the new variable type. Remember to give a name to the variable otherwise this button will **NOT** be visible. Also the name of the button _Create Variable_ will change with the variable type selected so that you will know what type you are creating.

#### TimerCountdown:
Added a timer interface that handles everything timer related blue print. Used it to create countdown timer called [TimerCountdown](https://github.com/deadlykam/CodeOptPro/blob/7799362173cf57412db51a058df00c29ec26d2a9/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Timers/TimerCountdown.cs). The script does as the name suggests which is it count downs to 0. This timer also calculates the normal value for the count down which may help later to sync up some other logic or features of yours.

To use _TimerCountdown_ you must first call the method _void StartSetup(float)_ in the Start or StartAdv methods. The float value it takes in is in seconds which means 1f = 1s. Then in the Update method you must call _void UpdateTimer(float)_ method for the count down to happen. The float value here determines how fast the count down will happen so 1 is normal speed, higher than 1 means countdown will be faster and less than 1 means countdown will happen slower. _void UpdateTimer(float)_ also has Time.deltaTime being calculated so no need to calculate that. I have commented all the methods for the timer. If you want to know what each method does then please read all the comments for the method in [ITimer](https://github.com/deadlykam/CodeOptPro/blob/7799362173cf57412db51a058df00c29ec26d2a9/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Timers/ITimer.cs).

#### Bars:
Added a bar interface that handles everything bar related blue print called [IBar](https://github.com/deadlykam/CodeOptPro/blob/b6e1013d4797a11a78c36cff2b880ddb4001c2cb/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Bars/IBar.cs). Used it to create an abstract class called [BaseBar](https://github.com/deadlykam/CodeOptPro/blob/b6e1013d4797a11a78c36cff2b880ddb4001c2cb/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Bars/BaseBar.cs) that handles most common logics between bars. Then used that to create [NormalBar](https://github.com/deadlykam/CodeOptPro/blob/b6e1013d4797a11a78c36cff2b880ddb4001c2cb/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Bars/NormalBar.cs) that works like any common bars.

To use _NormalBar_ you must first call the _void StartSetup(int)_ in the Start or StartAdv methods. The int value it takes is the max value for the bar. The value of the bar will be from 0(included) to max value(included). I have commented about the methods in the script so please read the comments in [IBar](https://github.com/deadlykam/CodeOptPro/blob/b6e1013d4797a11a78c36cff2b880ddb4001c2cb/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Bars/IBar.cs) to get an idea. I recommend using _NormalBar_ as health bars for npcs and players. I will give brief explanations of the methods below for the _NormalBar_
1. _void StartSetup(int)_ - This method sets up the bar at the start of the game. This method MUST be called in the start method otherwise there will be error. Later if you want to change the max value for the bar then you can call this method again.
2. _float GetNormalValue()_ - This method returns the normal value of the bar which is basically _curValue/maxValue_.
3. _void AddValue(int)_ - This will add to the bar's current value. The current value will not exceed the maximum value and will stop at the maximum value.
4. _int GetCurrentValue()_ - This method returns the current bar value.
5. _void SetCurrent(int)_ This method sets the current value. If the value given is higher than the maximum value then the maximum value will be set so current value will never exceed maximum value.
6. _int GetMaxValue_ - This method returns the maximum threshold value of the bar.
7. _bool IsDepleted()_ - This method checks if the bar has been depleted that is if the current value has become 0. 0 = true, 0 != false.
8. _void Restore()_ - This method restores the bar to the maximum value. That is making current value equal to maximum value.
9. _void RemoveValue(int)_ - This method removes an amount from the current value. The lowest current value will go is 0 and will not go below it.

#### Searches:
For now I have added a performant way to use the ray cast to hit objects and to get the farthest and closest objects. No need to keep repeating same code over and over again like a cycle. Below are the scripts that does that.
##### [RayHit](https://github.com/deadlykam/CodeOptPro/blob/f781787e8fc82608b5bb7adebc485c5c38264785/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Searches/RayHit.cs):
This script uses a ray to find all the objects that that ray has hit. It uses Unity's _Physics.RaycastNonAlloc(...)_ method to find objects that have hit the ray. Using this method is very performant friendly. I have commented in the scripts in what each constructor and method does but below I will explain it briefly.
- **RayHit(int hitSize, float rayRange, LayerMask hitMask)** - This is the constructor that MUST be called to initialize the RayHit object. The _hitSize_ parameter will give a limited number of objects to be hit by the ray and then stored. The _rayRange_ parameter defines the range of the ray or how far the ray can go. The _hitMask_ parameter tells the ray which layer the ray can hit. _Example: RayHit rayHit = new RayHit(5, 100f, someLayerMask);_
- **void SetRay(Ray ray)** - This method will replace the current ray with the given ray. So if there are other rays that you want to use then you can just replace the ray here. _Example: rayHit.SetRay(someRay);_
- **void SetRay(Vector3 origin, Vector3 dir)** - This method will set the current ray with the given parameters. The _origin_ parameter is the point from where the ray will be generated. The _dir_ parameter is the direction the ray will go towards. _Example: rayHit.SetRay(Vector3.zer0, Vector3.forward);_
- **void CalculateHits** - This method calculates and stores the number of objects the ray has hit. To get the number of objects hit call the method _int GetNumHit()_ after. _Example: rayHit.CalculateHits(); int numHit = rayHit.GetNumHit();_
- **bool IsHit()** - This method checks if at least 1 object has been hit. If there is a hit then it will return true. If there are NO hits then it will return false.
- **int GetNumHit()** - This method gets the number of objects that has been hit. For this method to work _void CalculateHits()_ must be called first. _Example: rayHit.CalculateHits(); int numHit = rayHit.GetNumHit();_

##### [RayHitSearch](https://github.com/deadlykam/CodeOptPro/blob/f781787e8fc82608b5bb7adebc485c5c38264785/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Searches/RayHitSearch.cs):
This script finds the closest or farthest hit object from the ray. This script also extends from _RayHit_ script so it contains all the methods and features from there as well. Again I have commented in the script to help with understanding it so check the script out and will explain the script briefly here as well.
- **RaycastHit GetClosestHit(Vector3 origin)** - This method finds the closest hit object. The hit objects are found when the ray hits the objects. The parameter _origin_ is the point from where the distance check is done and from where the closest hit object is found.
```
Example:
RayHitSearch rayHitSearch = new RayHitSearch(5, 100f, someLayerMask);
rayHitSearch.SetRay(Vector3.zero, Vector3.forward);
rayHitSearch.CalculateHits();
RaycastHit hitObject = rayHitSearch.GetClosestHit(Vector3.zero);
```
- **RaycastHit GetFarthestHit(Vector3 origin)** - This method finds the farthest hit object. The hit objects are found when the ray hits the objects. The parameter _origin_ is the point from where the distance check is done and from where the farthest hit object is found.
```
Example:
RayHitSearch rayHitSearch = new RayHitSearch(5, 100f, someLayerMask);
rayHitSearch.SetRay(Vector3.zero, Vector3.forward);
rayHitSearch.CalculateHits();
RaycastHit hitObject = rayHitSearch.GetFarthestHit(Vector3.zero);
```

#### Pooling System
Added the pooling system feature. This tool allows to pool any objects or components. By default I have added pooling system for GameObjects and Transforms called _PoolGameObjectGlobal_, _PoolGameObjectLocal_, _PoolTransformGlobal_ and _PoolTransformLocal_. You can also create your own pooling system by extending the scripts _BasePoolGlobal_ or _BasePoolLocal_. Let me explain how the pooling system works with the already added default pool objects and how you can apply that to your own custom pooling objects or components.

For example let's use the _PoolGameObjectGlobal_. Create a GameObject in the scene called _ExamplePool_. Now add 10 more GameObjects inside _ExamplePool_. Now we need to create the _RequestGameObject_ scriptable object. To do this simply right click anywhere in the project and go to _Create -> CodeOptPro -> ScriptableObjects -> RequestObjects_ and then select _RequestGameObject_. Name it _RequestSomeObjects_. Now add the _PoolGameObjectGlobal_ component to the _ExamplePool_ GameObject. Now we need to fill up the properties of the component. For the _Manager_ and _Update Manager_ fields give whatever _MonoAdvManagerHelper_ and _UpdateManagerGlobalHelper_ you are using. Then for the _AddRequest_ field give the _RequestSomeObjects_, for the _SizeRequest_ give _FixedIntVar_ value of 5 and for _EnableAtStart_ give the _FixedBoolVar_ value of true. I will explain what each of these field does later down below.

Now lets create a new script called _RequestExample_ and write the code like below.
```
using KamranWali.CodeOptPro.Pools;
using KamranWali.CodeOptPro.ScriptableObjects.RequestObjects;
using UnityEngine;

public class RequestExample : MonoBehaviour, IRequestObject<GameObject>
{
    [SerializeField] private RequestGameObject _requestObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _requestObject.CallAction(this);
    }

    public void ReceivePoolObject(GameObject poolObject)
    {
        Debug.Log($"Requested GameObject: {poolObject.name}");
    }
}
```
To recieve pool objects you must extend the generic interface script called IRequesetObject<T> and because we are working with GameObjects the generic type must be GameObject as well. The method _void ReceivePoolObject(GameObject)_ is the interface method that will receive the pool object. Here we will simply print the name of the game object received. To request for a pool object must call the _requestObject which in this case will take _IRequestObject<GameObject>_ which the script is a child of.

Now go back to the editor. Create another GameObject anywhere in the scene and call it _Receiver_. Add the script _RequestGameObject_ to it. For the _Request Object_ field give the _RequestSomeObjects_ scriptable object. Finally hit play and press the _Space Bar_. You will notice that each time you press the _Space Bar_ a new GameObject is received and the name for it is shown. After hiting the _Space Bar_ more than 10 times you will see the objects received begins to cycle. That is it, this is how you use the pooling system.

Alright so let me explain the last 3 fields of the pool system.
- **Add Request:** This is the delegate scriptable object that will request the pooling system for an object.
- **Size Request:** This is the size of the request array for the pooling system. This means that the pooling system can only handle _n_ amount of requests at a time. If the total requests received at one time is greater than _n_ then some of the requests will be lost. So make sure you give enough request size so that no requests are missed.
- **Enable At Start:** This flag decides if to allow the pool system to work when the game starts. If you make it _false_ then you can activate the pooling system yourself by sending true to the method _SetActive(bool)_.

##### Custom Pooling System:
To create a custom pooling system is simple. There are two tasks you need to do for the custom pooling system to work. The first task is to extend either _BasePoolGlobal<T>_ or _BasePoolLocal<T>_. Set the generic type to any object or component you wish, Example: To a script called _SomeObject_

Example:
```
public class PoolSomeObjectGlobal : BasePoolGlobal<SomeObject>
{
}
```

The seacond task is to create a _BaseRequest<T>_ scriptable object that will be used for requesting pool objects. The generic type here MUST be same as the one in pooling system, Example: To the script called _SomeObject_

Example:
```
[CreateAssetMenu(fileName = "RequestSomeObject",
                 menuName = "CodeOptPro/ScriptableObjects/RequestObjects/" +
                            "RequestSomeObject",
                 order = 1)]
public class RequestSomeObject : BaseRequest<SomeObject> { }
```

That is it. You are now done in creating the custom pooling system. All you have to do now is create the scriptable object and apply that to both the pooling system and the one that will receive the objects.

#### Folder Selection Palette
| ![Variable-Creator1.png](https://imgur.com/0yWbgxP.png) | 
|:--:| 
| *Fig: Folder Selection Palette* |

This feature allows the user to store the path locations of any folder and then by clicking the button will select that folder in the Project view. No need to look around for the important folders anymore. To open _Folder Selection Palette_ window click the menu _KamranWali_ then _CodeOptPro_ and finally _Folder Selection Palette_. Let me explain what each field does.
- **a.** _Name_ - This is where you give the name for the folder selection. It can be any name but keeping it same as the folder name will help you understand which folder it is.
- **b.** _Path_ - This is where you give the path of the folder you want to select. It is very easy to get the path just open up the folder in the _Project View_ window then right click an empty space in the folder and select _Copy Path_. Once the path has been copied then just paste it in this field.
- **c.** _ADD FOLDER_ - Once you have given the name and the path this button will be enabled. Clicking this button will add a folder selection button below and the name will be same as the name given while adding. The image _Fig: Added Selection Button_ below shows how it looks like when a selection button is added.
- **d.** _Mode Type_ - The mode type allows the user to either select the folder locations or edit the folder locations buttons. There are two modes. The first mode is called _Select Mode_. This mode allows the user to select the folder selection buttons. The second mode is called _Edit Mode_. This is the mode where the user can edit the selections buttons. See the image _Fig: Edit Mode_ to see how the edit mode looks like. Here you can move around the buttons and/or delete them. Make sure after making all the changes you click the _SAVE_ button to save all the changes.

| ![Variable-Creator1.png](https://imgur.com/9TmMdUQ.png) | 
|:--:| 
| *Fig: Added Selection Button* |

| ![Variable-Creator1.png](https://imgur.com/Ys2hRyT.png) | 
|:--:| 
| *Fig: Edit Mode* |
***
## Developer
I tried to keep the development process for the developers as simple as possible. So if you want to modify CodeOptPro then I will try my best to explain how to.
#### [CodeOptProSetupAuto](https://github.com/deadlykam/CodeOptPro/blob/1bcb72a7dc3534c3a427f3d1bb14a781cefbc62f/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Editor/CodeOptProSetupAuto.cs):
This is the main class that handles all the automations for the CodeOptPro system. The _Setup()_ method is where it happens. If create a new script that extends from classes _MonoAdv_, _MonoAdvUpdateLocal_, _MonoAdvUpdateGlobal_, _UpdateManagerLocal_ or _UpdateManagerGlobal_ then you do not need make any changes in this method. BUT if you create a new script that does not extend any of the mentioned classes then you have to apply the logics necesarry for automation and setting up for use. The best way to achieve this is to follow the steps for the field _ums_Local_, _ums_Global_ or *_objects*.

#### [MonoAdvManager_Call](https://github.com/deadlykam/CodeOptPro/blob/1bcb72a7dc3534c3a427f3d1bb14a781cefbc62f/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Managers/MonoAdvManager_Call.cs):
This is the class that calls the custom awake and start methods for _MonoAdvManager_. This is also the only class that uses Unity's _Awake()_ and _Start()_ methods. That is why there should be only one of these classes throughout the entire scene. The _Awake()_ method calls the _PreAwakeAdv()_ and _AwakeAdv()_. The main purpose of _PreAwakeAdv()_ is to allow the _MonoAdvManager_ to setup up the helper reference which can then later be used by every script that has that reference. The _Start()_ method calls the _StartAdv()_ method of the manager.

The methods inside the region called _Editor Script_ are NOT recommended to be called during runtime. This is only needed for _CodeOptProSetupAuto_ during automation setup.

#### [MonoAdvManager](https://github.com/deadlykam/CodeOptPro/blob/1bcb72a7dc3534c3a427f3d1bb14a781cefbc62f/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Managers/MonoAdvManager.cs):
This is the class that calls all the custom awake and start methods for every objects that is stored inside it. The methods _AwakeAdv()_ and _StartAdv()_ loops through all the objects and calls each one of their custom awake and start methods. It is recommended NOT to call the editor scripts under the region called _Editor Methods_ during runtime. Those methods are only used for _CodeOptProSetupAuto_.

#### [UpdateManagerLocal 1](https://github.com/deadlykam/CodeOptPro/blob/1bcb72a7dc3534c3a427f3d1bb14a781cefbc62f/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Managers/UpdateManagerLocal.cs):
This is the class responsible for calling the custom update method in _MonoAdvUpdateLocal_ objects. This class shares the _Update()_ methods with the objects stored inside it. The field *_timeDelta* is very important in this class. This field must be calculated with _Time.deltaTime_ to get the actual time for accurate calculation. The method _GetTime()_ already calculates the actual time. Also in the _AwakeAdv()_ you can see how *_timeDelta* is calculated.

The method _UpdateObject()_ is where objects' custom update are called. Here you can see the objects _bool IsActive()_ method is called to see if the object is active and ONLY then their _UpdateObject()_ method is called.

It is recommended NOT to call the editor scripts under the region called _Editor Methods_ during runtime. Those methods are only used for _CodeOptProSetupAuto_.

#### [UpdateManagerGlobal 1](https://github.com/deadlykam/CodeOptPro/blob/1bcb72a7dc3534c3a427f3d1bb14a781cefbc62f/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Managers/UpdateManagerGlobal.cs):
This class is same as _UpdateManagerLocal_. Please see the description in _[UpdateManagerLocal](#updatemanagerlocal)_ for how _UpdateManagerGlobal_ works. The only difference is that _UpdateManagerGlobal_ uses a scriptable object called _UpdateManagerGlobalHelper_ to communicate between different scripts. This allows the scripts to be decoupled where as for _[UpdateManagerLocal](#updatemanagerlocal)_ the scripts become coupled.

#### [MonoAdv](https://github.com/deadlykam/CodeOptPro/blob/1bcb72a7dc3534c3a427f3d1bb14a781cefbc62f/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Managers/MonoAdv.cs):
This is the class the allows custom awake and start to be used by its children. If you look at the code you will see there are 2 methods inside the region called _Editor Scripts_. The methods are _Init()_ and _bool HasManager()_. Let me explain the methods below:
- _Init()_ - This method initializes the MonoAdv object and is ONLY called from _CodeOptProSetupAuto_. In this case it is adding itself to the helper manager. So if you require and form of setup before entering the play mode and need automation this is the method to override and implement.
- _bool HasManager()_ - This method just checks if all the managers has been setup. If you require certain logic to be true then this is the method to implement. If this method returns false then _CodeOptProSetupAuto_ will stop the automation process and give a warning that something went wrong and needs fixing. This is basically to help the user know what went wrong.
It is recommended NOT to call these methods during run time and ONLY to call them if you understand what they do.

#### [MonoAdvUpdateLocal](https://github.com/deadlykam/CodeOptPro/blob/8524978f4a7d67e2cd096397d45a329dbdc87076/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Managers/MonoAdvUpdateLocal.cs):
This is the class that allows custom update to be used by its children. It also extends _MonoAdv_ so all the children will also get the custom awake and start methods. These class 3 main methods and 2 editor methods. I will explain them briefly as follow:
- _Init()_ - This method initializes the _MonoAdvUpdateLocal_ and is ONLY caled from _CodeOptProSetupAuto_. In this case it is adding itself to the _MonoAdvManager_ and _UpdateManagerLocal_.
- _bool HasManager()_ - This method checks if all the managers has been setup. In this case the _MonoAdvManager_ and the _UpdateManagerLocal_. This method is only called from _CodeOptProSetupAuto_.
- _UpdateObject()_ - This is an abstract method that needs to implemented by the children classes. This is the custom update method. So any upate logic should be done here.
- _SetActive(bool)_ - This method is an abstract method that needs to be implemented by the children classes. In this method you should decide how the object should be active/deactive for custom update.
- _bool IsActive()_ - This method is an abstract method that needs to be implemented by the children classes. This method checks if the object is active/deactive for custom update. When the method returns true then the object's _UpdateObject()_ will be called by the update manager but when the method returns false then the object's _UpdateObject()_ will NOT be called.

When extending this class you must refer a _UpdateManagerLocal_ in the _updateManager_ field in the inspector. Otherwise it won't work and the CodeOptPro system will throw an error warning while setup.

#### [MonoAdvUpdateGlobal](https://github.com/deadlykam/CodeOptPro/blob/8524978f4a7d67e2cd096397d45a329dbdc87076/CodeOptPro/Assets/KamranWali/CodeOptPro/Scripts/Managers/UpdateManagerGlobal.cs):
This is same as _MonoAdvUpdateLocal_. See the details there to understand. The only difference is that _UpdateManagerGlobalHelper_ needs to be refered in the _updateManager_ field in the inspector.
***
## Updates
Here I will share all the updates done to the newer versions. Below are the updates.
1. Added _IUpdate_ interface. This is needed so that other interfaces can extend from this interface and get the methods. Later may also simplify search for MonoAdv objects by using _IUpdate_. This may allow other custom classes to be searched as well.
2. Added pooling system.
3. Added _IUpdateManager_ interface. Now UpdateManagerLocal, UpdateManagerGlobal and UpdateManagerGlobalHelper are child of _IUpdateManager_. This helps with generic scripts with constraints.
4. Added a new feature called _Folder Selection Palette_. This feature allows the user to store any folder locations and then later can go to those folders instantly, in the _Project View_, by clicking a button. Check out more details about _Folder Selection Palette_ [here](#folder-selection-palette).
***
## Versioning
The project uses [Semantic Versioning](https://semver.org/). Available versions can be seen in [tags on this repository](https://github.com/deadlykam/CodeOptPro/tags).
***
## Authors
- Syed Shaiyan Kamran Waliullah 
  - [Kamran Wali Github](https://github.com/deadlykam)
  - [Kamran Wali Twitter](https://twitter.com/KamranWaliDev)
  - [Kamran Wali Youtube](https://www.youtube.com/channel/UCkm-BgvswLViigPWrMo8pjg)
  - [Kamran Wali Website](https://deadlykam.github.io/)
***
## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details.
***
