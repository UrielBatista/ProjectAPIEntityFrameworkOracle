namespace Pro.Search.PersonDomains.PersonEngine.Commons
{
    public class PageAndFilteredRequestParams
    {
        private const int _maxPageSize = 4;
        private const int _minPageNumber = 1;

        private int pageSize = 4;
        private int pageNumber;

        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = value <= 0 ? _minPageNumber : value;
        }

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value <= 0 ? _maxPageSize : value; 
        }

        public bool flagsValue { get; set; }
    }
}
