# CardboardVoiceCommands

Available voice commands sorted by scene and script:

---1_Positioning---
Tell sphere to go to blue or red Cube.
MoveObjectWithVoice.cs:
"right",
"left"	

Move the player to blue or red Cube. Look at one of the cubes and say:
"blue",
"red",
"stop",
"fast",
"slow"
(WalkWithVoice.cs)

---2_ObjectIdentification---

Make the sphere go to blue or red cube. Look at one of the cubes and say:
"go"
(GoThere.cs)

WalkWithVoice.cs:
"blue",
"red",
"stop",
"fast",
"slow"


---3_InformationMapping---

Label the paintings. Look at the sign under the painting and say:
"That is ..."
(LabelMe.cs)

---4_Disambiguation---

Make a specific statue disappear. Look at group of statues and say: 

"far" (removes the statue farthest away),
"near",
"right",
"left",
"delete" (deletes every Statue)
(DistiguishByVoice.cs)

Tell a specific sphere how to behave. Look at one of the spheres and say:
"down",
"big",
"small"
(VoiceAndGazeControl.cs)
