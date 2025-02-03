using SpanyolMVC.Models.Domain;

namespace SpanyolMVC.Repositories;

public interface IWordsRepository
{
    Task<IEnumerable<Words>> GetAllWordsAsync();
    
    Task<Words?> GetWordAsync(Guid id);
    
    Task<Words> AddWordAsync(Words word);
    
    Task<Words?> UpdateWordAsync(Words word);
    
    Task<Words?> DeleteWordAsync(Guid id);
    
    Task<List<Words>> GetRandomWordsAsync(int count);

    Task BulkInsertFromExcelAsync(string filePath);
}