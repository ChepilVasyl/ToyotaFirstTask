using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyotaProject.Models.MyModels;

//Зображення
public class ComplectationColorModel
{
    [Key]
    public int Id { get; set; }//Id
    public string? MainImageUrl { get; set; }//Url зображення
    public int ColorId { get; set; }//Зовнішній ключ
    [ForeignKey("ColorId")]
    public ColorModel? Color { get; set; }//Навігаційна властивість
    public int ComplectationId { get; set; }//Зовнішній ключ
    [ForeignKey("ComplectationId")]
    public ComplectationModel? Complectation { get; set; }//Навігаційна властивість
}