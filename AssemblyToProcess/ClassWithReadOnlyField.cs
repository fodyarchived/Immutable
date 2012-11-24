using System.Diagnostics;

[Immutable]
public class ClassWithReadOnlyField
{
	public readonly string Member = "InitialValue";


    public void Method()
    {
        Debug.WriteLine(Member);
    }
}