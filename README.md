# UnitySandbox
Tested in Unity 2019.1.9f1

Testing unity execution order and different possibilities for runing your code, such as:
- Plain C# Timer
- RuntimeInitializeOnLoadMethod
- Static constructor
- Check for AppDomain

Keep in mind, that unity API is not thread safe!!!
Multithreading:
- Check TransformAccess - incredible performance tool
- Check AsyncAwait

- Check for great project consistency tool by DarrenTsung with a little customization - https://github.com/DarrenTsung/DTValidator
UnitTests for scene/assets consistency are great tools to find and resolve nasty issues with Unity projects.
- Check for compile time tracker by DarrenTsung https://github.com/DarrenTsung/DTCompileTimeTracker

And some more useful scripts
