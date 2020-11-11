# ASIM-VR

## About

Made with Unity 2019.4.9f1 using URP.    
This project uses the following external assets and packages:  
- [Embedded Browser](https://assetstore.unity.com/packages/tools/gui/embedded-browser-55459) (Not Included)    
- [XR-Interaction toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@0.9/manual/index.html)  
- [OpenVR xr plugin](https://github.com/ValveSoftware/unity-xr-plugin)  
- [Mock HMD](https://docs.unity3d.com/Packages/com.unity.xr.mock-hmd@1.1/manual/index.html)  

## How to use
- Clone repository: `git clone https://github.com/ASIM-VR/ASIM-VR.git`
- Open with Unity 2019.4.9f1
- Select target device from `File/Project Settings/XR Plug-in Management`

## Project Structure
Project related files should be under `Assets/ASIM-VR` folder.  
Project related examples should be under `Assets/ASIM-VR/Examples` folder. It should be possible to exluce examples from the project.  
External asset should be in the `Assets/` folder.  
`Assets/Local` can be used to store local files which are automatically ignored by the .gitignore. However, .gitkeep inside the folder is used to include the folder in version control.

## Coding Conventions
This project shall follow the [C# coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions) by Microsoft.

#### Naming Conventions  
**Camel Case** for function parameters, local variables, private and protected fields.  
**Pascal Case** for public fields and for all struct, class, function and property names.

## New Features
New features should be implemented in separate branches. New branch should be created to develop a new feature. Once a the new feature is finished, it should be reviewed by atleast one person that has not worked on the feature branch. The reviewee should look for mistakes and errors found related to the feature developed in the branch. These might include: lack of documentation, typos, logic errors and spaghetti code. Once the new feature is finished, reviewed and accepted it can be merged in to the master branch.