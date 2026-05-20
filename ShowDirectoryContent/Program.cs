namespace ShowDirectoryContent
{
   internal class Program
   {
      static void Main(string[] args)
      {
         if (args.Length == 0)
         {
            Console.WriteLine("No input directory provided");
            Console.WriteLine($"Usage: ShowDirectoryContent <directory_path>");
            
            return;
         }

         DirectoryLogger directoryLogger = new DirectoryLogger(args[0]);
         directoryLogger.ExtractContent();
      }
   }
}