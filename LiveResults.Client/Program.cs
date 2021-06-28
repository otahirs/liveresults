using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using LiveResults.Client.Parsers;


namespace LiveResults.Client
{

    class Program
    {
        public static int Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                new Option<int>("--compId", description: "Competition id from liveresultat.orienteering.se")
                {
                    IsRequired = true,
                    ArgumentHelpName = "id"
                },
                new Option<DateTime>("--zeroTime", description: "Start 0 time")
                {
                    IsRequired = true,
                    ArgumentHelpName = "hh:mm"
                },
                new Option<string>("--startFile", description: "path to startlist file")
                {
                    IsRequired = true,
                    ArgumentHelpName = "path"
                },
                new Option<string>("--finishFile", description: "path to finish/race file")
                {
                    IsRequired = true,
                    ArgumentHelpName = "path"
                },
                new Option<string>(
                    "--rawSplitsFile",
                    description: "path to raw splits file"
                ),
                new Option<string>(
                    "--dsqFile",
                    description: "path to disqualified file"
                ),
                new Option<string>(
                    "--radioFile",
                    description: "path to radio controls file"
                ),
                new Option<bool>(
                    "--isRelay",
                    description: "set if race is relay"
                )
            };
            rootCommand.Name = "EmmaClient";
            rootCommand.Description = "A linux fork of EmmaClient for uploading live results to liveresultat.orienteering.se";
            rootCommand.Handler = CommandHandler.Create<int, DateTime, string, string, string, string, string, bool>((compId, zeroTime, startFile, finishFile, rawSplitsFile, dsqFile, radioFile, isRelay) =>
            {
                FrmMonitor monForm = new FrmMonitor();
                monForm.SetParser(new RacomFileSetParser(startFile, rawSplitsFile, finishFile, dsqFile, radioFile, zeroTime, isRelay));
                monForm.CompetitionID = compId;
                monForm.Run();
            });

            return rootCommand.Invoke(args);
        }
    }
}