using System.ComponentModel.DataAnnotations;

namespace ToyotaProject.Models.MyModels;

//Назва моделі
public class ToyotaModel
{
    [Key]
    public int Id { get; set; }
    public string? NameModel { get; set; }//Назва моделі
    public List<ComplectationModel>? ComplectationModels { get; set; } = new();
}