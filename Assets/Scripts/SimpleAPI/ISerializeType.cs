public interface ISerializeType
{
    public T Deserialize<T>(string text);
    public string ContentType { get; }
}
