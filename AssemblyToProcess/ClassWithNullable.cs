using System.Diagnostics;

[Immutable]
public class ClassWithNullable
{
    public int? Member;

    public ClassWithNullable()
    {
        Member = 3;
        Debug.WriteLine(Member);
    }
}