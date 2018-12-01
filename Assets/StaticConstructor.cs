public static class StaticConstructor
{
    public static float foo;
    
    static StaticConstructor()// is called in thred, where static field is used
    {
        ThreadHelper.LogCurThread($"Static constructor is called");
    }
}