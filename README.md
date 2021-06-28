## Linux CLI version of EmmaClient (Racom files only)

EmmaClient stripped of everything but Racom files upload, bundled with simple CLI interface.  
Go to release page for a net5 single-file self-contained binary.  
Made to work with https://github.com/Quick-Event/quickbox/ EmmaClient export service.  

based on https://github.com/petlof/liveresults

---

### usage
```
EmmaClient
  A linux fork of EmmaClient for uploading live results to liveresultat.orienteering.se

Usage:
  EmmaClient [options]

Options:
  --compId <id> (REQUIRED)         Competition id from liveresultat.orienteering.se
  --zeroTime <hh:mm> (REQUIRED)    Start 0 time
  --startFile <path> (REQUIRED)    path to startlist file
  --finishFile <path> (REQUIRED)   path to finish/race file
  --rawSplitsFile <rawSplitsFile>  path to raw splits file
  --dsqFile <dsqFile>              path to disqualified file
  --radioFile <radioFile>          path to radio controls file
  --isRelay                        set if race is relay
  --version                        Show version information
  -?, -h, --help                   Show help and usage information
```
example
```
EmmaClient --compId 19571 --zeroTime 12:00 --startFile /home/otahirs/MyRaceEmmaExport/test.start.txt --finishFile /home/otahirs/MyRaceEmmaExport/test.finish.txt 
```
