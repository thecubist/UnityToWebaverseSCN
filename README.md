# UnityToWebaverseSCN
## Overview
Generates lines that can be pasted into a webaverse scn file for rapidly prototyping levels while providing a small number of features to change the overall format of the generated lines as well as a few features to aid in correctly mapping between unity positional measurements and webaverse positional measurements.

This is not designed to be a replacement for the webaverse level editors but rather a tool to help developers convert their unity scenes into webaverse scenes. Note that this package does not convert fbx files into webaverse compatible glb files. This project is solely for extracting unity positional data into a format that webaverse can understand. 

![image](https://user-images.githubusercontent.com/36249159/167119027-b18150ce-3bc5-44b6-a487-6b1a81c60b37.png)

## Current Features
- Export lines for multiple objects at once
- Set the dynamic flags for individual objects
- Add asset URL's for each object
- Export both condensed and indented scn lines
- Multiply position vectors for converting between Unity positional units and Webaverse positional units (Recomended value 100)

![image](https://user-images.githubusercontent.com/36249159/167113586-266f7749-932b-4727-b9ba-8a1e772501d9.png)

## How To Use
Step 1: Expand out the "Objects" list and click the '+' button

Step 2: Add a reference to a game object within your scene

Step 3: Add a link to your asset. If you are planning to run the scn locally you can put your glb within the root folder of webaverse and reference it using ..YourAssetName.glb

Step 4: Add the position multiplier to each object. This may require some trial and error as this may change depending on the scaling of your fbx assets

Step 5: Click the Generate scn Lines button

Step 6: Copy the output and paste it into an scn file (Note: Should be pasted into an existing scn file as the export does not contain all file flags)

Step 7: Load your scn in webaverse (If scene is white, check the commas in the scn file and ensure they are all added correctly) 

![image](https://user-images.githubusercontent.com/36249159/167169228-4c8e479f-add8-4bc3-b86e-a7a34f436f1f.png)



