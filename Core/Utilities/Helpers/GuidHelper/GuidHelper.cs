namespace Core.Utilities.Helpers.GuidHelper
{
    public static class GuidHelper
    {
        public static string CreateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
