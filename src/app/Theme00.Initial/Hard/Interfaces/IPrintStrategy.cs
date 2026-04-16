namespace HWGA.Interfaces;

public interface IPrintStrategy 
{ 
    IStatusCode SetupPrinting(); 
    IStatusCode Print(IHelloWorldString str); 
}