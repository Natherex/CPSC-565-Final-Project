# CPSC-565-Final-Project

<h3 align="center">CPSC 565 Emergent Behaviour: Antibacteria Resistance Simulation</h3>

<!-- ABOUT THE PROJECT -->
## About The Project

This project simulates the behaviour of bacteria in a petri dish. The bacteria have the ability to quorum sense and adapt to their environment through evolution. 
Quorum sensing is the ability for bacteria to detect the population of neighbouring bacteria. The bacteria achieve this by releasing autoinducers and also being able to sense the concentration of autoinducers in their environment. The higher the concentration the more bacteria are present.

Our simulation also allows the user to place antibiotics in the petri dish and also remove said antibiotics. The simulation can simulate two different types of antibiotics. The first is Ciprofloxacin which targets bacterias when they divide, in our simulation this antibiotic is 100% effecive if present during division of a bacteria. The second type of bacteria is tetracycline which targets the bacteria directly but is less effective. The effectiveness of tetracycline can be modified in the simulation.

To add Ciprofloxacin: press 1

To add tetracycline: press 2

In our simulation bacteria use quorum sensing to determine when they should replicate and when they should not. When a cell has decided it no longer wants to replicate it is visually shown by having the cell turn red.The cell starts off white and will change back to white when it decides it wants to start replicating again.

![sim3](https://user-images.githubusercontent.com/25419186/115100899-30593800-9efd-11eb-9d7b-954d5d855cbb.gif)


<!-- GETTING STARTED -->
## Getting Started

Our program is run in unity using unity 2019.4.19f1. The aspect ratio that is used while running the simulation is also critical for ideal UI and views. We recommend an aspect ratio of 16:10 when in game mode.

Simply pressing play in unity starts the simulation with the number of bacteria selected in settings. The default is 5. These initial bacteria each have a set of random attributes.

<!-- Features -->
## Features

The first feature is to add anti bacteria disks. These are added by moving your mouse to the desired location and pressing the following:

	-To add Ciprofloxacin: press 1
	
	-To add tetracycline: press 2
	
	-To remove a disk: press 9 while hovering over a disk 

The second feature present is to see the attributes of an individual bacteria to do this simply click on a bacteria.

Our third feature allows the user to start a new simulation based on a single bacteria. When a bacteria is pressed an option pops up to "Streak a new plate" This option starts a new simulation with all the starting bacteria having the same settings as the bacteria that was streaked(pressed on).

<!-- Settings -->
## Inner Workings

- The simulation has a grid composed of 100 boxes. Each box starts with an equal amount of the total agar. The bacteria can then eat from a box if it still has agar(food) in it.
- To Limit the size of colonies a stress variable was added. Over time this stress variable decreases and when it reaches 0 the cell dies. This was done to kill off old bacteria so we would have the computer resources avaiable to simulate the newly replicated bacteria.
- Cell spawn locations are random at the beggining of the simulation.
- If the simulation is turned off and turned back on the total level of agar is carried over from the previous simulation.
- If a cell is streaked the starting agar level for the new simulation is set to a default value of 1,000. 
- The agar level can be changed dynamically during the simulation if desired. Ie if the bacteria run out of food you can add aditional food from the right UI without having to stop the simulation.

<!-- Settings -->
## Settings

Our simulation can be customize in 2 ways. First settings can be modified by going into the scriptableObjects folder and clicking on the UISettings. The settings can then be modified in the inspector. The properties that can be found here with a brief description of their functionality is as follows:

1. Reproduction Limit: Controls how many times each individual bacteria is permitted to replicate.
2. Agar Level: Controls how much food is present in the simulation environment.
3. AB Radius: Controls the effective radius of anticiotics
4. Split Threshold: Cost of cells splitting
5. Tet Resistance : how tolerant the bacteria are to tetracycline
6. Energy: The starting energy of cells
7. Number of Cells: how many cells are present at the start of the simulation
8. LAI_1 Mutation Radius : The rate at which the LAI_1 production mutates. LAI are the autoinducer being used which let bacteria determine how many neighbouring bacteria there are
9. Reproduction Mutation Radius : The rate at which the time it takes cells to divide mutates
10. QS Threshold mutation Radius : The rate at which the QS threshold mutates. The QS threshold is how many LAI_1's a bacteria has to sense before it goes it stops reproducing

These setting can also be modified in the UI on the right hand side during the simulation. The submit button must be pressed for the effects of the changes to take place. Certain settings will not take place however until the simulation is reset; namely the number of cells present at the start of the simulation.

<!-- CONTACT -->
## Contacts

Sammy Elrafih  - sammy.elrafih1@ucalgary.ca,
Isha Afzaal - isha.afzaal1@ucalgary.ca,
Ainslie Velthoen 
