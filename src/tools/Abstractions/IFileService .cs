namespace HWGA.ReadmeUpdater.Abstractions;

public interface IFileService 
{
    void UpdateReadmeTable(string filePath, string newContent);
}