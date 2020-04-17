# Apollo_DOTS - Dissertation

This project is a prototype copy of "Apollo" using Unity DOTS instead.

# Details

Main goal is to convert GameObject into Entities in runtime trhough code
This way, I demonstrate a second version of the prototype with huge difference in performace which
is measured in FPS.

# Dissertation Ambition

My dissertation research area is focused on data-oriented design, where I am trying to build a tiny game for smartphones running on Android OS in two versions build with Unity engine, one in object-oriented programming and one in data-oriented design using Unity Data-Oriented Technology Stack (DOTS); Using DOTS we can take full advantage of today's multi-core processors with Unity's new high performance, multi-threaded systems. As a developer designing a system with DOD is very different compare to OOP. For instance, DOD has to do with the ideal data, where OOP has to do with objects. Although, DOTS provides programmers with a convenient sandbox to write safe multi-threaded code for massive performance gains, while also optimizing thermal control and battery life on players mobile devices. DOD will also help me to write robust code reusable and easy for others to understand.

DOTS features that I am going to use to build DOD version are Entity Component System (ECS), C# Job System and Burst Compiler. With ECS I can write high-performance C# code that focuses on the actual problems, such as the data and behavior that make up the game itself. The principles of ECS are three as the acronyms. For example, entities are one of the three elements and they are responsible to represent individual "things" instead of objects we have entities in the game or application. An entity has neither behavior nor data, but instead it identifies which pieces of data belong together. Components which is the second element store the data of our entity (e.g fields , variables). In addition, components are the data associated with entities, but organizd by the data itself rather than by entity(this difference in organization is one of the key differences between an object-oriented and a data-oriented design). Systems which is the third principle element provides the behavior (the logic that transforms the component data from its current state to its next state for example, a system might update the positions of all moving entities by their velocity times the time interval since the previous frame)

The goal of the study is to measure the performance in frames (FPS) of both design paradigms and achieve a high optimized system. In addition, I challenged my self to prove battery consumption in contrast of both OOP and DOD. At the moment I haven't finished the system yet and I am currently working on it, however, the literature review and some other documentations have been already completed. To be honest, I am not sure how these Unity packages function behind the system, but I have done some simulations and I have seen huge differences in performance. To give you an example ( a simulation written with OOP spawns 800 2D sprites with each object its own data running at 30 fps on PC, and a simulation written with DOD spawns 7.000-10.000 entities 2D sprites with each entity its own data running at 60 fps stable). Despite that, these packages that I am using are all preview packages an are not official shipped yet. I have a lot of problems with versions and unknown errors because of that, but I am sure in the further future these systems are going to make Unity more valuable for the industries.

To be honest and give you a clear conclusion about my studies, I am struggling to figure out how to write code using these systems because there is no official documentation out there, only some online essays (talks) where they show examples of code. This topic is focused on games rather than applications, because games are greedy in terms of ram and CPU usage. This doesn't mean that OOP is dead but with DOD you can build for example real time strategy games where you will have hundreds of objects doing something at the same frame and there is coming DOD which will make the gameplay much more smooth with high optimizations into the system and targeting more platforms out there where they might have poor hardware.

# Unity Entity Component System reading
https://docs.unity3d.com/Packages/com.unity.entities@0.1/manual/ecs_core.html

