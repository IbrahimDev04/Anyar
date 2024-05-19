namespace Anyar.Extensions
{
    public static class FileManager
    {
        public static bool IsValidType(this IFormFile formFile, string type)
            => formFile.ContentType.Contains(type);

        public static bool IsValidSize(this IFormFile formFile, int KByte)
            => formFile.Length <= KByte * 1024;

        public static async Task<string> FileManagedAsync(this IFormFile formFile, string path)
        {
            string ext = Path.GetExtension(formFile.FileName);
            string fileName = Path.GetRandomFileName();

            await using FileStream fileStream = new FileStream(Path.Combine(path, fileName + ext),FileMode.Create);
            await formFile.CopyToAsync(fileStream);
            
            return fileName + ext;
        }

        public static async Task Delete(this string FileName, string path)
        {
            File.Delete(Path.Combine(path, FileName));

        }

    }
}
