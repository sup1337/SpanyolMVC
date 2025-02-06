using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SpanyolMVC.Data;
using SpanyolMVC.Models.Domain;

namespace SpanyolMVC.Repositories;

public class WordsRepository:IWordsRepository
{
  private readonly SpanishDbContext _spanishDbContext;

    public WordsRepository(SpanishDbContext spanishDbContext)
    {
        _spanishDbContext = spanishDbContext;
    }

    public async Task<IEnumerable<Words>> GetAllWordsAsync(string? searchQuery = null, int pageNumber = 1, int pageSize = 100)
    {
        var query = _spanishDbContext.Words.AsQueryable();
        if (string.IsNullOrWhiteSpace(searchQuery) == false)
        {
            query = query.Where(x => x.Infinitive.Contains(searchQuery) ||
                                     x.English.Contains(searchQuery) ||
                                     x.Hungarian.Contains(searchQuery) ||
                                     x.Italian.Contains(searchQuery) ||
                                     x.French.Contains(searchQuery) ||
                                     x.German.Contains(searchQuery));
        }
        // Pagination
        // Skip 0 Take 5 -> Page 1 of 5 results
        // Skip 5 Take next 5 -> Page 2 of 5 results
        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
    }

    public Task<Words?> GetWordAsync(Guid id)
    {
        return _spanishDbContext.Words.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Words> AddWordAsync(Words word)
    {
        await _spanishDbContext.Words.AddAsync(word);
        await _spanishDbContext.SaveChangesAsync();
        return word;
    }

    public async Task<Words?> UpdateWordAsync(Words word)
    {
       var existingWord = await _spanishDbContext.Words.FirstOrDefaultAsync(x => x.Id == word.Id);
       
       if(existingWord != null)
       {
           existingWord.Infinitive = word.Infinitive;
           existingWord.English = word.English;
           existingWord.Hungarian = word.Hungarian;
           existingWord.Italian = word.Italian;
           existingWord.French = word.French;
           existingWord.German = word.German;
           existingWord.Gerund = word.Gerund;
           existingWord.Participle = word.Participle;
           existingWord.Present1 = word.Present1;
           existingWord.Present2 = word.Present2;
           existingWord.Present3 = word.Present3;
           existingWord.Present4 = word.Present4;
           existingWord.Present5 = word.Present5;
           existingWord.Present6 = word.Present6;
           existingWord.Imperfect1 = word.Imperfect1;
           existingWord.Imperfect2 = word.Imperfect2;
           existingWord.Imperfect3 = word.Imperfect3;
           existingWord.Imperfect4 = word.Imperfect4;
           existingWord.Imperfect5 = word.Imperfect5;
           existingWord.Imperfect6 = word.Imperfect6;
           existingWord.Indefinite1 = word.Indefinite1;
           existingWord.Indefinite2 = word.Indefinite2;
           existingWord.Indefinite3 = word.Indefinite3;
           existingWord.Indefinite4 = word.Indefinite4;
           existingWord.Indefinite5 = word.Indefinite5;
           existingWord.Indefinite6 = word.Indefinite6;
           existingWord.Future1 = word.Future1;
           existingWord.Future2 = word.Future2;
           existingWord.Future3 = word.Future3;
           existingWord.Future4 = word.Future4;
           existingWord.Future5 = word.Future5;
           existingWord.Future6 = word.Future6;
           existingWord.Conditional1 = word.Conditional1;
           existingWord.Conditional2 = word.Conditional2;
           existingWord.Conditional3 = word.Conditional3;
           existingWord.Conditional4 = word.Conditional4;
           existingWord.Conditional5 = word.Conditional5;
           existingWord.Conditional6 = word.Conditional6;
           existingWord.SubjunctivePresent1 = word.SubjunctivePresent1;
           existingWord.SubjunctivePresent2 = word.SubjunctivePresent2;
           existingWord.SubjunctivePresent3 = word.SubjunctivePresent3;
           existingWord.SubjunctivePresent4 = word.SubjunctivePresent4;
           existingWord.SubjunctivePresent5 = word.SubjunctivePresent5;
           existingWord.SubjunctivePresent6 = word.SubjunctivePresent6;
           existingWord.SubjunctiveImperfect1 = word.SubjunctiveImperfect1;
           existingWord.SubjunctiveImperfect2 = word.SubjunctiveImperfect2;
           existingWord.SubjunctiveImperfect3 = word.SubjunctiveImperfect3;
           existingWord.SubjunctiveImperfect4 = word.SubjunctiveImperfect4;
           existingWord.SubjunctiveImperfect5 = word.SubjunctiveImperfect5;
           existingWord.SubjunctiveImperfect6 = word.SubjunctiveImperfect6;
           existingWord.ImperativePositive2 = word.ImperativePositive2;
           existingWord.ImperativePositive3 = word.ImperativePositive3;
           existingWord.ImperativePositive4 = word.ImperativePositive4;
           existingWord.ImperativePositive5 = word.ImperativePositive5;
           existingWord.ImperativePositive6 = word.ImperativePositive6;
           existingWord.ImperativeNegative2 = word.ImperativeNegative2;
           existingWord.ImperativeNegative3 = word.ImperativeNegative3;
           existingWord.ImperativeNegative4 = word.ImperativeNegative4;
           existingWord.ImperativeNegative5 = word.ImperativeNegative5;
           existingWord.ImperativeNegative6 = word.ImperativeNegative6;
           await _spanishDbContext.SaveChangesAsync();
           return existingWord;
       }

       return null;
    }

    public async Task<Words?> DeleteWordAsync(Guid id)
    {
        var existingWord = await _spanishDbContext.Words.FindAsync(id);
        
        if(existingWord != null)
        {
            _spanishDbContext.Words.Remove(existingWord);
            await _spanishDbContext.SaveChangesAsync();
            return existingWord;
        }

        return null;
    }
    public async Task<List<Words>> GetRandomWordsAsync(int count)
    {
        var totalWords = await _spanishDbContext.Words.CountAsync();
        if (count > totalWords)
        {
            count = totalWords;
        }
        return await _spanishDbContext.Words.OrderBy(words => Guid.NewGuid() ).Take(count).ToListAsync();
    }

    public async Task BulkInsertFromExcelAsync(string filePath)
    {
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets.FirstOrDefault();

        if (worksheet == null)
            throw new Exception("The Excel file is empty or invalid.");

        var rowCount = worksheet.Dimension.Rows;

        var wordsList = new List<Words>();

        for (int row = 2; row <= rowCount; row++) // Feltételezzük, hogy az első sor a fejléc
        {
            var word = new Words
            {
                Hungarian = worksheet.Cells[row, 1].Text,
                English = worksheet.Cells[row, 2].Text,
                Italian = worksheet.Cells[row, 3].Text,
                French = worksheet.Cells[row, 4].Text,
                German = worksheet.Cells[row, 5].Text,
                Infinitive = worksheet.Cells[row, 6].Text,
                Gerund = worksheet.Cells[row, 7].Text,
                Participle = worksheet.Cells[row, 8].Text,
                Present1 = worksheet.Cells[row, 9].Text,
                Present2 = worksheet.Cells[row, 10].Text,
                Present3 = worksheet.Cells[row, 11].Text,
                Present4 = worksheet.Cells[row, 12].Text,
                Present5 = worksheet.Cells[row, 13].Text,
                Present6 = worksheet.Cells[row, 14].Text,
                Imperfect1 = worksheet.Cells[row, 15].Text,
                Imperfect2 = worksheet.Cells[row, 16].Text,
                Imperfect3 = worksheet.Cells[row, 17].Text,
                Imperfect4 = worksheet.Cells[row, 18].Text,
                Imperfect5 = worksheet.Cells[row, 19].Text,
                Imperfect6 = worksheet.Cells[row, 20].Text,
                Indefinite1 = worksheet.Cells[row, 21].Text,
                Indefinite2 = worksheet.Cells[row, 22].Text,
                Indefinite3 = worksheet.Cells[row, 23].Text,
                Indefinite4 = worksheet.Cells[row, 24].Text,
                Indefinite5 = worksheet.Cells[row, 25].Text,
                Indefinite6 = worksheet.Cells[row, 26].Text,
                Future1 = worksheet.Cells[row, 27].Text,
                Future2 = worksheet.Cells[row, 28].Text,
                Future3 = worksheet.Cells[row, 29].Text,
                Future4 = worksheet.Cells[row, 30].Text,
                Future5 = worksheet.Cells[row, 31].Text,
                Future6 = worksheet.Cells[row, 32].Text,
                Conditional1 = worksheet.Cells[row, 33].Text,
                Conditional2 = worksheet.Cells[row, 34].Text,
                Conditional3 = worksheet.Cells[row, 35].Text,
                Conditional4 = worksheet.Cells[row, 36].Text,
                Conditional5 = worksheet.Cells[row, 37].Text,
                Conditional6 = worksheet.Cells[row, 38].Text,
                SubjunctivePresent1 = worksheet.Cells[row, 39].Text,
                SubjunctivePresent2 = worksheet.Cells[row, 40].Text,
                SubjunctivePresent3 = worksheet.Cells[row, 41].Text,
                SubjunctivePresent4 = worksheet.Cells[row, 42].Text,
                SubjunctivePresent5 = worksheet.Cells[row, 43].Text,
                SubjunctivePresent6 = worksheet.Cells[row, 44].Text,
                SubjunctiveImperfect1 = worksheet.Cells[row, 45].Text,
                SubjunctiveImperfect2 = worksheet.Cells[row, 46].Text,
                SubjunctiveImperfect3 = worksheet.Cells[row, 47].Text,  
                SubjunctiveImperfect4 = worksheet.Cells[row, 48].Text,
                SubjunctiveImperfect5 = worksheet.Cells[row, 49].Text,
                SubjunctiveImperfect6 = worksheet.Cells[row, 50].Text,
                ImperativePositive2 = worksheet.Cells[row, 51].Text,
                ImperativePositive3 = worksheet.Cells[row, 52].Text,
                ImperativePositive4 = worksheet.Cells[row, 53].Text,
                ImperativePositive5 = worksheet.Cells[row, 54].Text,
                ImperativePositive6 = worksheet.Cells[row, 55].Text,
                ImperativeNegative2 = worksheet.Cells[row, 56].Text,
                ImperativeNegative3 = worksheet.Cells[row, 57].Text,
                ImperativeNegative4 = worksheet.Cells[row, 58].Text,
                ImperativeNegative5 = worksheet.Cells[row, 59].Text,
                ImperativeNegative6 = worksheet.Cells[row, 60].Text
                
            };

            wordsList.Add(word);
        }

        // Adatok mentése adatbázisba
        foreach (var word in wordsList)
        {
            await AddWordAsync(word);
        }
    }

    public async Task<int> CountAsync(string searchQuery = null)
    {
        var query = _spanishDbContext.Words.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(x => x.Infinitive.Contains(searchQuery) ||
                                     x.English.Contains(searchQuery) ||
                                     x.Hungarian.Contains(searchQuery) ||
                                     x.Italian.Contains(searchQuery) ||
                                     x.French.Contains(searchQuery) ||
                                     x.German.Contains(searchQuery));
        }

        return await query.CountAsync();
    }
}