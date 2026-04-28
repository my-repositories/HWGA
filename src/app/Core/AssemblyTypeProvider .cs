namespace HWGA.Core;

public class AssemblyTypeProvider : ITypeProvider
{
    public IList<Type> GetProgramTypes()
    {
        var interfaceType = typeof(IProgram);
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => interfaceType.IsAssignableFrom(p) && p is { IsClass: true, IsAbstract: false })
            .OrderBy(x => x.FullName)
            .ToList();
    }
}