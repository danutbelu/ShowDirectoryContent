using System.Text;

namespace ShowDirectoryContent
{
   public class DirectoryLogger
   {
      private readonly string _sourcePath;
      private readonly string _destinationFile;

      private static readonly string[] ExcludedDirectories =
      {
            "bin",
            "obj",
            ".git",
            ".vs"
        };

      public DirectoryLogger(string sourcePath)
      {
         _sourcePath = sourcePath;

         var fileName = $"dir_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
         _destinationFile = Path.Combine(Path.GetDirectoryName(sourcePath)!, fileName);
      }

      public void ExtractContent()
      {
         using var writer = new StreamWriter(_destinationFile, false, Encoding.UTF8);
         var rootDir = new DirectoryInfo(_sourcePath);

         WriteDirectory(rootDir, writer, 0);
      }

      private void WriteDirectory(DirectoryInfo dir, StreamWriter writer, int level)
      {
         if (IsHidden(dir) || IsExcludedDirectory(dir.Name))
         {
            return;
         }

         string indent = new string(' ', level * 2);
         writer.WriteLine($"{indent}[{dir.Name}]");

         // Files
         foreach (var file in dir.GetFiles())
         {
            if (IsHidden(file))
               continue;

            writer.WriteLine($"{indent}  - {file.Name}");
         }

         // Subdirectories
         foreach (var subDir in dir.GetDirectories())
         {
            if (IsHidden(subDir) || IsExcludedDirectory(subDir.Name))
               continue;

            WriteDirectory(subDir, writer, level + 1);
         }
      }

      private static bool IsHidden(FileSystemInfo info)
      {
         return (info.Attributes & FileAttributes.Hidden) != 0;
      }

      private static bool IsExcludedDirectory(string name)
      {
         return Array.Exists(ExcludedDirectories, d =>
             d.Equals(name, StringComparison.OrdinalIgnoreCase));
      }
   }
}