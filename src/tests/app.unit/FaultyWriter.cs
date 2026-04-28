using System.Text;

namespace HWGA.Tests;

public class FaultyWriter : TextWriter
{
    public override Encoding Encoding => Encoding.UTF8;
    public override void Write(string? value) => throw new IOException("Disk full");
    public override void WriteLine(string? value) => throw new IOException("Disk full");
}
