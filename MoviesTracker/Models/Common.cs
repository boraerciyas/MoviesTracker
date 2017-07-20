namespace MoviesTracker.Models
{
    public static class Common
    {

    }

    public static class DatabaseColumn
    {
        public const string _ID = "ID";
        public const string Title = "Title";
        public const string ReleaseTime = "ReleaseTime";
        public const string Rate = "Rate";
        public const string Genre = "Genre";
        public const string Status = "Status";
    }

    public static class FilmsControllerMethods
    {
        public const string Index = "Index";
        public const string Details = "Details";
        public const string Create = "Create";
        public const string Edit = "Edit";
        public const string Delete = "Delete";
    }

    public static class OrderDirection
    {
        public const string Ascending = "ASC";
        public const string Descending = "DESC";
    }

}