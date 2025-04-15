using SpanyolMVC.Models.ViewModels;

namespace SpanyolMVC.Repositories;

public interface IEvaluationRepository
{
    Task<EvaluationViewModel> GetEvaluationAsync(Guid userId);
}