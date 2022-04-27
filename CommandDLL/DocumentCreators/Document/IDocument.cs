using System.Threading.Tasks;

namespace CommandDLL
{
    public interface IDocument
    {
        string Name { get; }
        byte[] GetDocumntContent();
        Task SaveAsync(string path);
    }

}

