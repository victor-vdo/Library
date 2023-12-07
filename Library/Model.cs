namespace Library
{
    public abstract class Model
    {
        public Guid Id { get; set; }
        public Model()
        {
            Id = Guid.NewGuid();
        }
    }
}
