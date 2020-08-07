namespace Chess.Core.Domain
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 40;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize 
        { 
            get{ return _pageSize;} 
            set {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            } 
        }
    }
}