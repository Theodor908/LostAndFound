using AutoMapper;

namespace LostAndFound.Helpers;

public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
    where TSource : class
    where TDestination : class
{
    public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
    {
        var items = context.Mapper.Map<List<TDestination>>(source.Items);

        return new PagedList<TDestination>
        {
            Items = items,
            CurrentPage = source.CurrentPage,
            TotalPages = source.TotalPages,
            PageSize = source.PageSize,
            TotalCount = source.TotalCount
        };
    }
}
