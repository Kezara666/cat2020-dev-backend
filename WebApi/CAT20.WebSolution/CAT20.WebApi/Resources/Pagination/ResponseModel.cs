namespace CAT20.WebApi.Resources.Pagination;

public class ResponseModel<T> where T : class
{
    public int totalResult { get; set; }
    public IEnumerable<T> list { get; set; }
}
