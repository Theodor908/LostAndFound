using System;
using LostAndFound.Helpers;

namespace LostAndFound.Models;

public class CategoryListDTO
{
    public string SearchTerm { get; set; } = string.Empty;
    public PagedList<CategoryDTO> CategoryList { get; set; } = new PagedList<CategoryDTO>();
}
