# Introduction
The framework implements the Coordinator pattern in C# for Unity, using UniTask for asynchronous transitions between coordinators and Zenject for dependency injection. The framework also includes a Coordinators browser, built using UI elements, which allows the user to view all active coordinators and their nested children.

## Getting Started
1. To begin, ensure that you have the 2022.2.f1 version of Unity installed on your machine.
2. Download the github project and open it with unity.
3. To use the framework, you will need to create a new script that inherits from the AbstractCoordinator class. This script will handle the logic for a specific section of your application.
4. You can then transition to your coordinator using AsyncRouter, which is included in the framework package.
## Using the Coordinators Browser
![image](https://user-images.githubusercontent.com/36601610/212541651-66fca449-c6e3-4073-b2fa-9bd34ea0f2a7.png)

1. To access the Coordinators browser, open Tools -> Coordinator Browser.
2. The browser will display a list of all active coordinators, as well as their nested children.
3. You can use the browser to easily navigate between coordinators and view their current state.
4. You modify browser using UI builder for your specific needs
## Dependency Injection with Zenject
1. The framework work examples uses Zenject for dependency injection. It's not neccaserary but this allows you to easily manage dependencies between classes in your application.
2. To use Zenject, you will first need to add the Zenject package to your Unity project - https://github.com/modesttree/Zenject
## Asynchronous Transitions with UniTask
1. The framework uses UniTask for asynchronous transitions between coordinators, UniTask is a good choice for working with Unity as it's designed to work seamlessly with the Unity's main thread which allows us to work with monobehaviour object in unity scene in async funtctions.
2. To use UniTask, you will first need to add the UniTask package to your Unity project - https://github.com/Cysharp/UniTask
3. You can then use the ASyncRouter.TransitionAsync() or ASyncRouter.TransitionModallyAsync() methods to transition to a new coordinator asynchronously.
## Examples
In the package you can find AdditionalExtra folder that has some work examples :

- Game loading
- Confirmation popup
- Scene transitioning

These examples serve as a starting point for understanding how to use the framework in your own projects. They can be used as-is, or modified to fit the specific needs of your application. Keep in mind that these examples are provided for educational purposes only, and should be thoroughly tested before being used in a production environment.

## Conclusion
This framework provides an easy to use implementation of the Coordinator pattern in C# for Unity, allowing you to easily manage the navigation and flow of your application. The Coordinators browser, allows you to easily view all active coordinators and their nested children. The framework also uses UniTask for asynchronous transitions and Zenject for dependency injection, making it easy to manage dependencies between classes.

## Coordinator pattern
The Coordinator pattern is a design pattern used to manage the navigation and flow of an application. It is often used in iOS development but can also be applied to other platforms such as Unity.

The main idea behind the Coordinator pattern is to separate the navigation and flow logic of an application from the view controllers. This allows for a cleaner separation of concerns and makes it easier to manage the complexity of an application.

### Pros
- Improved separation of concerns
- Increased testability
- Improved code reusability
### Cons
- Increased complexity
- Additional boilerplate code
- Additional learning curve
