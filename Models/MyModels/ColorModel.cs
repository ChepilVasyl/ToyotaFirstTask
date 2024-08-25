using System.ComponentModel.DataAnnotations;

namespace ToyotaProject.Models.MyModels;
//Це для ярлика: колір, код кольру, зображення
public class ColorModel
{
    [Key] public int Id { get; set; }//Id
    public string? NameColor { get; set; }//Ім'я кольору
    public string? CodeColor { get; set; }//Код кольору
    public string? UrlColor { get; set; }//Url зображення
    public List<ComplectationColorModel>? ComplectationModels { get; set; } = new();//Навігаційна властивість
}