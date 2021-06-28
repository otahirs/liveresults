using LiveResults.Model;
using System;
using System.Collections.Generic;
namespace LiveResults.Client
{
    public partial class FrmMonitor
    {
        IExternalSystemResultParser m_Parser;
        List<EmmaMysqlClient> m_Clients = new List<EmmaMysqlClient>();

        private int m_CompetitionID;

        public int CompetitionID
        {
            get { return m_CompetitionID; }
            set { m_CompetitionID = value; }
        }
	

        public void SetParser(IExternalSystemResultParser parser)
        {
            m_Parser = parser;
            m_Parser.OnLogMessage += OnLogMessage;
            m_Parser.OnResult += m_Parser_OnResult;
            m_Parser.OnRadioControl += (name, code, className, order) =>
            {
                foreach (EmmaMysqlClient client in m_Clients)
                {
                    client.SetRadioControl(className, code, name, order);
                }
            };
        }

        void m_Parser_OnResult(Result newResult)
        {
            foreach (EmmaMysqlClient client in m_Clients)
            {
                if (!client.IsRunnerAdded(newResult.ID))
                    client.AddRunner(new Runner(newResult.ID, newResult.RunnerName, newResult.RunnerClub, newResult.Class));
                else
                    client.UpdateRunnerInfo(newResult.ID, newResult.RunnerName, newResult.RunnerClub, newResult.Class, null);

                if (newResult.StartTime > 0)
                    client.SetRunnerStartTime(newResult.ID, newResult.StartTime);


                if (newResult.Time != -2)
                {
                    client.SetRunnerResult(newResult.ID, newResult.Time, newResult.Status);
                }

                if (newResult.SplitTimes != null)
                {
                    foreach (ResultStruct str in newResult.SplitTimes)
                    {
                        client.SetRunnerSplit(newResult.ID, str.ControlCode, str.Time);
                    }
                }
            }
        }

        public void Run()
        {
            EmmaMysqlClient.EmmaServer[] servers = EmmaMysqlClient.GetServersFromConfig();

            foreach (EmmaMysqlClient.EmmaServer srv in servers)
            {
                EmmaMysqlClient cli = new EmmaMysqlClient(srv.Host, 3309, srv.User, srv.Pw, srv.DB, m_CompetitionID);
                m_Clients.Add(cli);
                cli.OnLogMessage += OnLogMessage;
                cli.Start();
            }
            m_Parser.Start();
        }

        void OnLogMessage(string msg)
        {
            Console.WriteLine( DateTime.Now.ToString("hh:mm:ss") + " " + msg);
        }
    }
}