namespace Handcom.Domain.DataAccess.Pagination.Base
{
    public class Page<T>
    {
        private const int GreaterThanOne = 1;
        private const int EqualToOne = 1;

        public List<T> Content { get; }
        public int TotalPages { get; }
        public int TotalElements { get; }
        public int Number { get; }
        public int Size { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }
        public bool IsFirst { get; }
        public bool IsLast { get; }

        public Page() =>
        Content = new List<T>();

        public Page(int total, List<T> content, Pageable pageable)
        {
            Number = pageable.Page;
            Size = pageable.Size;

            Content = content;

            TotalElements = total;

            TotalPages = (int)Math.Ceiling((double)TotalElements / pageable.Size);

            HasPrevious = Number > GreaterThanOne;
            HasNext = Number < TotalPages;

            IsFirst = Number == EqualToOne;
            IsLast = Number == TotalPages;
        }
    }
}
