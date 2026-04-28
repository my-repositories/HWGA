using HWGA.Core;

namespace HWGA.Theme01.CLR.EasyValueTypeVsReferenceType;

public class Program(TextWriter output) : BaseProgram(output)
{
    protected override async Task Run(string[]? args = null)
    {
        var personClass = new PersonClass() { Age = 20 };
        var personStruct = new PersonStruct() { Age = 20 };

        await Output.WriteLineAsync("Original:");
        await Dump(personClass, personStruct);

        TryModifyPersonAge(personClass);
        TryModifyPersonAge(personStruct);
        await Output.WriteLineAsync("After TryModifyPersonAge:");
        await Dump(personClass, personStruct);

        TryModifyPersonAgeByRef(ref personClass);
        TryModifyPersonAgeByRef(ref personStruct);
        await Output.WriteLineAsync("After TryModifyPersonAgeByRef:");
        await Dump(personClass, personStruct);
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

    private async Task Dump(PersonClass personClass, PersonStruct personStruct)
    {
        await Output.WriteLineAsync
        (
            $"personClass.Age = {personClass.Age} \t | \t "
            + $"personStruct.Age = {personStruct.Age}"
        );
        await Output.WriteLineAsync(new string('=', 42));
    }
}