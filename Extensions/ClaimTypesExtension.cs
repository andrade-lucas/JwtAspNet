using System.Security.Claims;

namespace JwtAspNet.Extensions;

public static class ClaimTypesExtension
{
    public static int Id(this ClaimsPrincipal user)
    {
		try
		{
            var id = user.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "0";
			return int.Parse(id);
        }
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			return 0;
		}
    }

    public static string Name(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return "";
        }
    }

    public static string Email(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return "";
        }
    }

    public static string GivenName(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? "";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return "";
        }
    }

    public static string Image(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(x => x.Type == "image")?.Value ?? "";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return "";
        }
    }
}
