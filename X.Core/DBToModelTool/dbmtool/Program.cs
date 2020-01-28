using CommandLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace dbmtool
{
    class Options
    {
        [Option('r', "read", Required = true, HelpText = "Input files or directories to be copied")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('d', "destination",
         Default = "",
         Required = true,
         HelpText = "Destination")]
        public string Destination { get; set; }


        // Omitting long name, defaults to name of property, ie "--verbose"
        [Option(
          Default = false,
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option('o',"override",
          Default = false,
          HelpText = "Override files/directory at destination")]
        public bool Override { get; set; }

        [Option('g', "guessnamespace",
          Default = true,
          HelpText = "Guess namespace at destination based on folder name")]
        public bool GuessNamespace { get; set; }

        [Option('n', "namespace",
         Default = "",
         Required = false,
         HelpText = "Namespace at destination")]
        public string Namespace { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
                .WithNotParsed<Options>((errs) => HandleParseError(errs));
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            errs.Output();
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            var envPath = Environment.CurrentDirectory;
            var destination = Path.Join(envPath, opts.Destination);

            Console.WriteLine($"Destination: {destination}");
            foreach(var item in opts.InputFiles)
            {
                var path = Path.Join(envPath, item);
                var isDirectory = File.GetAttributes(item).HasFlag(FileAttributes.Directory);
                if (isDirectory)
                {
                    var files = Directory.GetFiles(path);
                    foreach(var file in files)
                    {
                        CopyAndReplace(file, destination, opts);   
                    }
                }
                else
                {
                    CopyAndReplace(path, destination, opts);
                }
                
            }
        }

        private static void CopyAndReplace(string file, string destination, Options opts)
        {
            var filename = System.IO.Path.GetFileName(file);
            if(filename.EndsWith("Context.cs"))
            {
                Console.WriteLine($"Skipped: {file} because it's DB context");
                return;
            }
            var destFile = Path.Join(destination, filename);
            if(File.Exists(destFile))
            {
                if (opts.Override)
                {
                    System.IO.File.Copy(file, destFile, opts.Override);
                    namespaceChanger(destFile, opts);
                    Console.WriteLine($"Copied and replaced: {file} to {destFile}");
                }
            }
            else
            {
                System.IO.File.Copy(file, destFile, false);
                namespaceChanger(destFile, opts);
                Console.WriteLine($"Copied: {file} to {destFile}");
            }
        }

        static void namespaceChanger(string fileName, Options opts)
        {
            string[] arrLine = File.ReadAllLines(fileName);

            for (int i = 0; i < arrLine.Length; i++)
            {
                string line = arrLine[i];
                if (line.TrimStart().StartsWith("namespace"))
                {
                    if (opts.GuessNamespace && string.IsNullOrWhiteSpace(opts.Namespace))
                    {
                       string dir = new DirectoryInfo(fileName).Parent.Name;
                       arrLine[i] = $"namespace {dir}";
                    }
                    else
                    {
                        arrLine[i] = $"namespace {opts.Namespace}";
                    }
                }
            }
            
            File.WriteAllLines(fileName, arrLine);
        }
    }
}
