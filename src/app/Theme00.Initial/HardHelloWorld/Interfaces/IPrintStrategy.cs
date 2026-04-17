namespace HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

public interface IPrintStrategy 
{ 
    IStatusCode SetupPrinting(); 
    IStatusCode Print(IHelloWorldString str); 
}