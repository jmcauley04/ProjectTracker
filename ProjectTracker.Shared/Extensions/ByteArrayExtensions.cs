namespace ProjectTracker.Shared.Extensions;

public static class ByteArrayExtensions
{
    public static string ToJpgUrl(this byte[] bytes)
    {
        var imgString = Convert.ToBase64String(bytes);
        return $"data:image/jpeg;base64,{imgString}";
    }

    public static byte[] ToByteArray(this object image)
    {
        return Convert.FromBase64String(image.ToString());
    }
}
