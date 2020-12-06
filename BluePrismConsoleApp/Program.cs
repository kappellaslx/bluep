using Autofac;
using System;
using System.Collections.Generic;
using System.IO;

namespace BluePrismConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckArguments(args);

            var container = BuildContainer();

            var mainService = container.Resolve<IMainService>();

            // start the main service
            mainService.StartService(args[0], args[1], args[2], args[3]);
        }

        /// <summary>
        /// Check console application arguments
        /// </summary>
        /// <param name="args"></param>
        private static void CheckArguments(string[] args)
        {
            // check if all required arguments were passed
            if (args.Length != 4)
            {
                throw new ArgumentException("The number of arguments passed must be 4");
            }

            // check if the dictionary file exists
            if (!File.Exists(args[0]))
            {
                throw new ArgumentException("Dictionary file not found");
            }

            // check if start and end words have 4 charaters length
            if (args[1].Length != 4 || args[2].Length != 4)
            {
                throw new ArgumentException("Start word and end word must be 4 charaters length");
            }

            // check if the start word is smaller than the end word alphabetically ordered
            if (String.Compare(args[1], args[2]) >= 0)
            {
                throw new ArgumentException("Start word must be smaller than end word alphabetically ordered");
            }

            // check if the result file name is valid
            if (args[3].IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                throw new ArgumentException("Result file name is not valid");
            }
        }

        /// <summary>
        /// Build autofac container
        /// </summary>
        /// <returns>container</returns>
        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainService>()
                .As<IMainService>()
                .InstancePerDependency();

            builder.RegisterType<WordsProcessorService>()
                .As<IWordsProcessorService>()
                .InstancePerDependency();

            builder.RegisterType<EnglishDictionary>()
                .As<IEnglishDictionary>()
                .SingleInstance();

            builder.RegisterType<FileManager>()
                .As<IFileManager>()
                .InstancePerDependency();

            return builder.Build();
        }
    }
}
