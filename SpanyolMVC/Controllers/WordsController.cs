using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpanyolMVC.Models.Domain;
using SpanyolMVC.Models.ViewModels;
using SpanyolMVC.Repositories;

namespace SpanyolMVC.Controllers;

[Authorize(Roles = "Manager")]
public class WordsController : Controller
{
    private readonly IWordsRepository _wordsRepository;

    public WordsController(IWordsRepository wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Add")]
    public async Task<IActionResult> Add(AddWordRequest addWordRequest)
    {
        var word = new Words
        {
            Difficulty = addWordRequest.Difficulty,
            Hungarian = addWordRequest.Hungarian,
            English = addWordRequest.English,
            Italian = addWordRequest.Italian,
            French = addWordRequest.French,
            German = addWordRequest.German,
            Infinitive = addWordRequest.Infinitive,
            Gerund = addWordRequest.Gerund,
            Participle = addWordRequest.Participle,
            Present1 = addWordRequest.Present1,
            Present2 = addWordRequest.Present2,
            Present3 = addWordRequest.Present3,
            Present4 = addWordRequest.Present4,
            Present5 = addWordRequest.Present5,
            Present6 = addWordRequest.Present6,
            Imperfect1 = addWordRequest.Imperfect1,
            Imperfect2 = addWordRequest.Imperfect2,
            Imperfect3 = addWordRequest.Imperfect3,
            Imperfect4 = addWordRequest.Imperfect4,
            Imperfect5 = addWordRequest.Imperfect5,
            Imperfect6 = addWordRequest.Imperfect6,
            Indefinite1 = addWordRequest.Indefinite1,
            Indefinite2 = addWordRequest.Indefinite2,
            Indefinite3 = addWordRequest.Indefinite3,
            Indefinite4 = addWordRequest.Indefinite4,
            Indefinite5 = addWordRequest.Indefinite5,
            Indefinite6 = addWordRequest.Indefinite6,
            Future1 = addWordRequest.Future1,
            Future2 = addWordRequest.Future2,
            Future3 = addWordRequest.Future3,
            Future4 = addWordRequest.Future4,
            Future5 = addWordRequest.Future5,
            Future6 = addWordRequest.Future6,
            Conditional1 = addWordRequest.Conditional1,
            Conditional2 = addWordRequest.Conditional2,
            Conditional3 = addWordRequest.Conditional3,
            Conditional4 = addWordRequest.Conditional4,
            Conditional5 = addWordRequest.Conditional5,
            Conditional6 = addWordRequest.Conditional6,
            SubjunctivePresent1 = addWordRequest.SubjunctivePresent1,
            SubjunctivePresent2 = addWordRequest.SubjunctivePresent2,
            SubjunctivePresent3 = addWordRequest.SubjunctivePresent3,
            SubjunctivePresent4 = addWordRequest.SubjunctivePresent4,
            SubjunctivePresent5 = addWordRequest.SubjunctivePresent5,
            SubjunctivePresent6 = addWordRequest.SubjunctivePresent6,
            SubjunctiveImperfect1 = addWordRequest.SubjunctiveImperfect1,
            SubjunctiveImperfect2 = addWordRequest.SubjunctiveImperfect2,
            SubjunctiveImperfect3 = addWordRequest.SubjunctiveImperfect3,
            SubjunctiveImperfect4 = addWordRequest.SubjunctiveImperfect4,
            SubjunctiveImperfect5 = addWordRequest.SubjunctiveImperfect5,
            SubjunctiveImperfect6 = addWordRequest.SubjunctiveImperfect6,
            ImperativePositive2 = addWordRequest.ImperativePositive2,
            ImperativePositive3 = addWordRequest.ImperativePositive3,
            ImperativePositive4 = addWordRequest.ImperativePositive4,
            ImperativePositive5 = addWordRequest.ImperativePositive5,
            ImperativePositive6 = addWordRequest.ImperativePositive6,
            ImperativeNegative2 = addWordRequest.ImperativeNegative2,
            ImperativeNegative3 = addWordRequest.ImperativeNegative3,
            ImperativeNegative4 = addWordRequest.ImperativeNegative4,
            ImperativeNegative5 = addWordRequest.ImperativeNegative5,
            ImperativeNegative6 = addWordRequest.ImperativeNegative6,
        };
        await _wordsRepository.AddWordAsync(word);
        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> List(string? searchQuery,
        int pageSize = 10,
        int pageNumber = 1)
    {
        // Összes szó száma a keresési feltétel alapján
        var totalWords = await _wordsRepository.CountAsync(searchQuery);

        // Oldalak számának kiszámítása
        var totalPages = (int)Math.Ceiling(totalWords / (double)pageSize);

        // Oldalszám korrekciója
        if (pageNumber > totalPages)
        {
            pageNumber--;
        }

        if (pageNumber < 1)
        {
            pageNumber++;
        }

        // Adatok átadása a nézetnek
        ViewBag.TotalPages = totalPages;
        ViewBag.SearchQuery = searchQuery;
        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        // Szavak lekérése a keresési és lapozási feltételek alapján
        var words = await _wordsRepository.GetAllWordsAsync(searchQuery, pageNumber, pageSize);
        return View(words);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var words = await _wordsRepository.GetWordAsync(id);

        if (words != null)
        {
            var editWordsRequest = new EditWordsRequest
            {
                Id = words.Id,
                Difficulty = words.Difficulty,
                Hungarian = words.Hungarian,
                English = words.English,
                Italian = words.Italian,
                French = words.French,
                German = words.German,
                Infinitive = words.Infinitive,
                Gerund = words.Gerund,
                Participle = words.Participle,
                Present1 = words.Present1,
                Present2 = words.Present2,
                Present3 = words.Present3,
                Present4 = words.Present4,
                Present5 = words.Present5,
                Present6 = words.Present6,
                Imperfect1 = words.Imperfect1,
                Imperfect2 = words.Imperfect2,
                Imperfect3 = words.Imperfect3,
                Imperfect4 = words.Imperfect4,
                Imperfect5 = words.Imperfect5,
                Imperfect6 = words.Imperfect6,
                Indefinite1 = words.Indefinite1,
                Indefinite2 = words.Indefinite2,
                Indefinite3 = words.Indefinite3,
                Indefinite4 = words.Indefinite4,
                Indefinite5 = words.Indefinite5,
                Indefinite6 = words.Indefinite6,
                Future1 = words.Future1,
                Future2 = words.Future2,
                Future3 = words.Future3,
                Future4 = words.Future4,
                Future5 = words.Future5,
                Future6 = words.Future6,
                Conditional1 = words.Conditional1,
                Conditional2 = words.Conditional2,
                Conditional3 = words.Conditional3,
                Conditional4 = words.Conditional4,
                Conditional5 = words.Conditional5,
                Conditional6 = words.Conditional6,
                SubjunctivePresent1 = words.SubjunctivePresent1,
                SubjunctivePresent2 = words.SubjunctivePresent2,
                SubjunctivePresent3 = words.SubjunctivePresent3,
                SubjunctivePresent4 = words.SubjunctivePresent4,
                SubjunctivePresent5 = words.SubjunctivePresent5,
                SubjunctivePresent6 = words.SubjunctivePresent6,
                SubjunctiveImperfect1 = words.SubjunctiveImperfect1,
                SubjunctiveImperfect2 = words.SubjunctiveImperfect2,
                SubjunctiveImperfect3 = words.SubjunctiveImperfect3,
                SubjunctiveImperfect4 = words.SubjunctiveImperfect4,
                SubjunctiveImperfect5 = words.SubjunctiveImperfect5,
                SubjunctiveImperfect6 = words.SubjunctiveImperfect6,
                ImperativePositive2 = words.ImperativePositive2,
                ImperativePositive3 = words.ImperativePositive3,
                ImperativePositive4 = words.ImperativePositive4,
                ImperativePositive5 = words.ImperativePositive5,
                ImperativePositive6 = words.ImperativePositive6,
                ImperativeNegative2 = words.ImperativeNegative2,
                ImperativeNegative3 = words.ImperativeNegative3,
                ImperativeNegative4 = words.ImperativeNegative4,
                ImperativeNegative5 = words.ImperativeNegative5,
                ImperativeNegative6 = words.ImperativeNegative6
            };

            return View(editWordsRequest);
        }

        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditWordsRequest editWordsRequest)
    {
        var word = new Words
        {
            Id = editWordsRequest.Id,
            Difficulty = editWordsRequest.Difficulty,
            Hungarian = editWordsRequest.Hungarian,
            English = editWordsRequest.English,
            Italian = editWordsRequest.Italian,
            French = editWordsRequest.French,
            German = editWordsRequest.German,
            Infinitive = editWordsRequest.Infinitive,
            Gerund = editWordsRequest.Gerund,
            Participle = editWordsRequest.Participle,
            Present1 = editWordsRequest.Present1,
            Present2 = editWordsRequest.Present2,
            Present3 = editWordsRequest.Present3,
            Present4 = editWordsRequest.Present4,
            Present5 = editWordsRequest.Present5,
            Present6 = editWordsRequest.Present6,
            Imperfect1 = editWordsRequest.Imperfect1,
            Imperfect2 = editWordsRequest.Imperfect2,
            Imperfect3 = editWordsRequest.Imperfect3,
            Imperfect4 = editWordsRequest.Imperfect4,
            Imperfect5 = editWordsRequest.Imperfect5,
            Imperfect6 = editWordsRequest.Imperfect6,
            Indefinite1 = editWordsRequest.Indefinite1,
            Indefinite2 = editWordsRequest.Indefinite2,
            Indefinite3 = editWordsRequest.Indefinite3,
            Indefinite4 = editWordsRequest.Indefinite4,
            Indefinite5 = editWordsRequest.Indefinite5,
            Indefinite6 = editWordsRequest.Indefinite6,
            Future1 = editWordsRequest.Future1,
            Future2 = editWordsRequest.Future2,
            Future3 = editWordsRequest.Future3,
            Future4 = editWordsRequest.Future4,
            Future5 = editWordsRequest.Future5,
            Future6 = editWordsRequest.Future6,
            Conditional1 = editWordsRequest.Conditional1,
            Conditional2 = editWordsRequest.Conditional2,
            Conditional3 = editWordsRequest.Conditional3,
            Conditional4 = editWordsRequest.Conditional4,
            Conditional5 = editWordsRequest.Conditional5,
            Conditional6 = editWordsRequest.Conditional6,
            SubjunctivePresent1 = editWordsRequest.SubjunctivePresent1,
            SubjunctivePresent2 = editWordsRequest.SubjunctivePresent2,
            SubjunctivePresent3 = editWordsRequest.SubjunctivePresent3,
            SubjunctivePresent4 = editWordsRequest.SubjunctivePresent4,
            SubjunctivePresent5 = editWordsRequest.SubjunctivePresent5,
            SubjunctivePresent6 = editWordsRequest.SubjunctivePresent6,
            SubjunctiveImperfect1 = editWordsRequest.SubjunctiveImperfect1,
            SubjunctiveImperfect2 = editWordsRequest.SubjunctiveImperfect2,
            SubjunctiveImperfect3 = editWordsRequest.SubjunctiveImperfect3,
            SubjunctiveImperfect4 = editWordsRequest.SubjunctiveImperfect4,
            SubjunctiveImperfect5 = editWordsRequest.SubjunctiveImperfect5,
            SubjunctiveImperfect6 = editWordsRequest.SubjunctiveImperfect6,
            ImperativePositive2 = editWordsRequest.ImperativePositive2,
            ImperativePositive3 = editWordsRequest.ImperativePositive3,
            ImperativePositive4 = editWordsRequest.ImperativePositive4,
            ImperativePositive5 = editWordsRequest.ImperativePositive5,
            ImperativePositive6 = editWordsRequest.ImperativePositive6,
            ImperativeNegative2 = editWordsRequest.ImperativeNegative2,
            ImperativeNegative3 = editWordsRequest.ImperativeNegative3,
            ImperativeNegative4 = editWordsRequest.ImperativeNegative4,
            ImperativeNegative5 = editWordsRequest.ImperativeNegative5,
            ImperativeNegative6 = editWordsRequest.ImperativeNegative6
        };

        var updatedWord = await _wordsRepository.UpdateWordAsync(word);

        if (updatedWord != null)
        {
            // Show success notification
            ViewBag.SuccessMessage = "Word updated successfully";
        }
        else
        {
            // Show error notification
            ViewBag.ErrorMessage = "Failed to update word";
        }

        return RedirectToAction("List", new { id = editWordsRequest.Id });
    }


    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedWord = await _wordsRepository.DeleteWordAsync(id);

        if (deletedWord != null)
        {
            // Show success notification
            return RedirectToAction("List");
        }

        // Show an error notification
        return RedirectToAction("Edit", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteFromList(Guid id)
    {
        var deletedWord = await _wordsRepository.DeleteWordAsync(id);

        if (deletedWord != null)
        {
            // Show success notification
            TempData["SuccessMessage"] = "Words deleted successfully.";
            return RedirectToAction("List");
        }

        // Show an error notification
        TempData["ErrorMessage"] = "Failed to delete Words";
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult UploadFile()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }


            await _wordsRepository.BulkInsertFromExcelAsync(filePath);


            System.IO.File.Delete(filePath);


            return RedirectToAction("List");
        }


        ViewBag.ErrorMessage = "No file uploaded";
        return View();
    }
}