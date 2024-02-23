namespace P5_Express_Voitures_Identity.Models.Service
{

    public interface IPathService
    {
       string GetUploadsPath(string? filename = null, bool withWebRootPath = true);
    }

}
