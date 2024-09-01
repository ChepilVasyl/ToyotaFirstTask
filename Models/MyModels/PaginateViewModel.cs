namespace ToyotaProject.Models.MyModels;

public class PaginateViewModel
{
    public int PageSize { get; set; } = 2;
    public int PageNumber { get; set; } = 1;
    public int TotalPage { get; set; } = 1;
    public int TotalItems { get; set; } = 1;
    public string? SortColumn { get; set; } = "NameModel";
    public string? SortDirection { get; set; } = "asc";
}