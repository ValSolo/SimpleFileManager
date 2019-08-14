namespace FileManagerSolution
{
    public interface IFileManager
    {
        void WriteFile(string fileName, byte[] data);
        bool ReadFile(string fileName, ref byte[] data);
        void DeleteFile(string fileName);
        void DeleteUnusedFiles();
    }
}
