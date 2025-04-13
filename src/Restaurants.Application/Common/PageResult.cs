namespace Restaurants.Application.Common;

public class PageResult<T>
{
    public PageResult (IEnumerable<T> items, int totalItems, int pageSize, int pageNumber){
        Items = items;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
    }

    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }

}
