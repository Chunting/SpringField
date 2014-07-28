using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ResumeCollector.Lib
{
    public class TaskReport
    {
        private StringBuilder report;

        public TaskReport(string folderName)
        {
            InitReport(folderName);
        }

        public void InitReport(string folderName)
        {
            report = new StringBuilder();
            RenderReportHead(folderName);
        }

        private void RenderReportHead(string folderName)
        {
            report.AppendLine("<html>");
            report.Append("<head><title>");
            report.Append("Report for folder [");
            report.Append(folderName);
            report.Append("]</title>");
            report.AppendLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' /></head>");
            report.AppendLine("<body><table border='1'>");
            report.Append("<tr><td colspan='4'>");
            report.Append("Report of process the folder [");
            report.Append(folderName);
            report.Append("] at [");
            report.Append(DateTime.Now.ToLongDateString());
            report.Append("&nbsp;&nbsp");
            report.Append(DateTime.Now.ToLongTimeString());
            report.AppendLine("]</td></tr>");
            RenderEntry("Folder", "Email", "Name", "Reason");
        }

        private void RenderReportFoot()
        {
            report.AppendLine("</table></body>");
            report.Append("</html>");
        }

        public void RenderEntry(string folder, string email, string name, string reason)
        {
            if (string.IsNullOrEmpty(folder))
            {
                folder = "#";
            }
            if (string.IsNullOrEmpty(email))
            {
                email = "N/A";
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "N/A";
            }
            if (string.IsNullOrEmpty(reason))
            {
                reason = "N/A";
            }

            report.AppendLine("<tr>");
            RenderHyperlink(folder);
            RenderCell(email);
            RenderCell(name);
            RenderCell(reason);
            report.AppendLine("</td>");
        }

        private void RenderCell(string content)
        {
            report.Append("<td>");
            report.Append(content);
            report.AppendLine("</td>");
        }

        private void RenderHyperlink(string content)
        {
            report.Append("<td><a target='_blank' href='");
            report.Append(content);
            report.Append("'>");
            report.Append(content);
            report.AppendLine("</a></td>");
        }

        public void End()
        {
            RenderReportFoot();
        }

        public void SaveAs(string filename)
        {
            //RenderReportFoot();
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }
            using (StreamWriter fileWriter = new StreamWriter(filename))
            {
                fileWriter.Write(report.ToString());
                fileWriter.Flush();
                fileWriter.Close();
            }
        }

        public override string ToString()
        {
           return report.ToString();
        }
    }
}
