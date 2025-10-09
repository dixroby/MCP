namespace DynamicQueries.Entities.Dtos.Metadata
{
    public class OperatorSupportDto(IEnumerable<string> filters, 
        bool order = true, bool caseInsensitive = false)
    {
        public IEnumerable<string> Filters => filters;
        public bool Order => order;
        public bool CaseInsensitive => caseInsensitive;
    }
}
