namespace Catalog.API.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg):base("Product not found!")
        {
           
        }
    }
}
