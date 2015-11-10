using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TimeLogging.Models;
using TimeLogging.DataAccess;
using System.Diagnostics;
using System.IO;

namespace TimeLogging.IntegrationTests
{
    [TestClass]
    public class TimeLogServiceTest
    {
        private TimeLogService tls;
        private static string dbSetupErrMsg;

        [ClassInitialize]
        public static void InitOnceOnly(TestContext context)
        {
            if (!InitDb())
            {
                Assert.Fail(dbSetupErrMsg);
            }
        }

        private static bool InitDb()
        {
            var publisher = new DbProjectPublisher();
            return publisher.InitDb(ref dbSetupErrMsg);
        }

        [TestInitialize]
        public void InitBeforeEachTest()
        {
            tls = new TimeLogService();
        }

        [TestMethod]
        public void GetEntriesByDate_FilterByDateSucceeds()
        {
            var dayInQuestion = new DateTime(2015, 11, 9);
            var results = tls.GetEntriesByDate(dayInQuestion);
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(
                (results[0].EndTime.Day == dayInQuestion.Day)
                && (results[0].EndTime.Year == dayInQuestion.Year)
                && (results[0].EndTime.Month == dayInQuestion.Month)
                );
        }
    }

    public class DbProjectPublisher
    {
        private string outputFilePath;

        public DbProjectPublisher()
        {
            outputFilePath = @"C:\doug\code\RESTfulExercises\TimeLogging\TimeLogging.Database\publish_output.txt";
            TruncateOutputFile();
        }

        private void TruncateOutputFile()
        {
            File.Delete(outputFilePath);
        }

        public bool InitDb(ref string dbSetupErrMsg)
        {
            string pathToSqlPackageExe = @"C:\Program Files (x86)\Microsoft SQL Server\110\DAC\bin\SqlPackage.exe";
            string args = @"/Action:Publish /SourceFile:C:\doug\code\RESTfulExercises\TimeLogging\TimeLogging.Database\bin\Debug\TimeLogging.Database.dacpac /Profile:C:\doug\code\RESTfulExercises\TimeLogging\TimeLogging.Database\TimeLogging.Database.Doug.publish.xml";

            var process = new Process();
            process.StartInfo.FileName = pathToSqlPackageExe;
            process.StartInfo.Arguments = args;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.OutputDataReceived += HandleRedirectedOutputEvents;

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            dbSetupErrMsg = string.Format("Look in: {0}. Exit code: {1}", outputFilePath, process.ExitCode);

            return process.ExitCode == 0;
        }

        private void HandleRedirectedOutputEvents(object sender, DataReceivedEventArgs e)
        {
            string line = e.Data;
            if (line != null)
            {
                using (var sw = File.AppendText(outputFilePath))
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
