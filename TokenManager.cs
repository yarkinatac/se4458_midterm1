public static class TokenManager
{
    public static string? GetToken(HttpRequest? Request)
    {
        if (Request != null)
        {
            string? Token = Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(Token))
            {
                return Token.Replace("Bearer", "").Trim();
            }
        }

        return null;
    }
}
