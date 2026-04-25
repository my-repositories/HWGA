namespace HWGA.Theme01.CLR.EasyValueTypeVsReferenceType;

public class Program : BaseProgram
{
    protected override async Task Run(string[] args = null)
    {
        var personClass = new PersonClass() { Age = 20 };
        var personStruct = new PersonStruct() { Age = 20 };

        Console.WriteLine("Original:");
        Dump(personClass, personStruct);

        TryModifyPersonAge(personClass);
        TryModifyPersonAge(personStruct);
        Console.WriteLine("After TryModifyPersonAge:");
        Dump(personClass, personStruct);

        TryModifyPersonAgeByRef(ref personClass);
        TryModifyPersonAgeByRef(ref personStruct);
        Console.WriteLine("After TryModifyPersonAgeByRef:");
        Dump(personClass, personStruct);
    }

    public void TryModifyPersonAge(PersonClass personClass)
    {
        personClass.Age = 25;
    }

    public void TryModifyPersonAge(PersonStruct personStruct)
    {
        personStruct.Age = 25;
    }

    public void TryModifyPersonAgeByRef(ref PersonStruct personStruct)
    {
        personStruct.Age = 42;
    }

    public void TryModifyPersonAgeByRef(ref PersonClass personClass)
    {
        personClass.Age = 42;
    }

    private void Dump(PersonClass personClass, PersonStruct personStruct)
    {
        Console.WriteLine
        (
            $"personClass.Age = {personClass.Age} \t | \t "
            + $"personStruct.Age = {personStruct.Age}"
        );
        Console.WriteLine(new string('=', 42));
    }
}