![ASIM-VR_banner_image](./Documents/img/banner.jpg)

# ASIM-VR

## About

Made with [Unity 2019.4.9f1](https://unity3d.com/unity/whats-new/2019.4.9) using [URP](https://unity.com/srp/universal-render-pipeline), [XR-Interaction toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@0.9/manual/index.html) and [Embedded Browser](https://assetstore.unity.com/packages/tools/gui/embedded-browser-55459) (Embedded Browser is not Included).  

[Demo Video](https://youtu.be/ttccAH9lsLM)  
[Poster](./Documents/ASIM-VR_Poster.pdf)


## How to use
- Clone repository: `git clone https://github.com/ASIM-VR/ASIM-VR.git`
- Open with Unity 2019.4.9f1
- Import Embedded Browser
- Extract Embedded Browsers `DemoBrowserAssets.zip` to the root directory of the project
- Import `Assets/ASIM-VR/Prefabs/Browser/EmbeddedBrowser_URP.unitypackage`
- Drag the target device to the top of the list in `File/Project Settings/Player` under the `XR Settings` section
- Depending on the active device you may need to modify the components in XR Rig

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