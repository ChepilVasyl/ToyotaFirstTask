using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ToyotaProject.Models.MyModels;
//Назва комплектації
public class ComplectationModel
{
    [Key]
    public int Id { get; set; }//Id
    public string? NameComplectation { get; set; }//Назва комплектації
    [ForeignKey("Model")]
    public int ModelId { get; set; }//Зовнішній ключ
    public ToyotaModel? Model { get; set; }//Навігаційна властивість
    public List<ComplectationColorModel> Colors { get; set; } = new();//Навігаційна властивість
}