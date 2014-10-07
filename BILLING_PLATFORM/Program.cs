using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;
using System.ComponentModel;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;
using System.Net;



namespace BILLING_PLATFORM
{
    class Program
    {
        static void Main(string[] args)
        {
            int tim = 1;
            while (tim != 0)
            {
                Console.WriteLine("Mediation starting ...");
                string txtCSVFolderPath = ConfigurationManager.AppSettings["path"];
                string[] txtCSVFilePath = Directory.GetFiles(@"" + txtCSVFolderPath + "", "*.csv");
                System.Data.DataTable csvData;
                foreach (string d in txtCSVFilePath)
                {
                    List<string> phoneNumbers = new List<string>();
                    List<int> cptApl = new List<int>();
                    Console.WriteLine("Connection to:" + d);
                    csvData = GetDataTabletFromCSVFile(d);
                    Console.WriteLine("File retrieved");
                    Console.WriteLine("Rows count:" + csvData.Rows.Count);
                    Console.ReadLine();
                    Console.WriteLine("Data Retrieved !\n");
                    char[] tmp = { '.', 'c', 's', 'v' };
                    string t1;
                    string to;
                    t1 = d.Remove(0, txtCSVFolderPath.Length + 1);
                    to = t1.TrimEnd(tmp);
                    Console.WriteLine("Data processing ....\n");
                    Console.ReadKey();
                    double total = 0;
                    int cptNotValidCall = 0;
                    int cptValidCall = 0;
                    Console.WriteLine("Searching ...\n");
                    Document pdf = new Document(iTextSharp.text.PageSize.A4, 5, 5, 10, 10);
                    PdfWriter wri = PdfWriter.GetInstance(pdf, new FileStream("INVOICE_" + to.ToUpper() + ".pdf", FileMode.Create));
                    pdf.Open();
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("e-billing-logo.jpg");
                    logo.ScaleToFit(300, 75f);
                    PdfPTable header = new PdfPTable(2);
                    header.AddCell(logo);
                    header.AddCell("WEB BILLING PLATFORM!");
                    header.DefaultCell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                    pdf.Add(header);
                    Paragraph p = new Paragraph("    ");
                    pdf.Add(p);
                    pdf.Add(p);
                    PdfPTable Somme = new PdfPTable(1);
                    PdfPTable CDR = new PdfPTable(6);
                    CDR.AddCell("Date et Heure");
                    CDR.AddCell("N° Appelant");
                    CDR.AddCell("Type Appel");
                    CDR.AddCell("N° Appelé");
                    CDR.AddCell("Durée");
                    CDR.AddCell("Prix appel");
                    for (int i = 0; i < csvData.Rows.Count - 1; i++)
                    {
                        double prixAppel = 0;
                        if (csvData.Rows[i][10].ToString() != "EV_OFFHOOK_CONNECTED") { cptNotValidCall = cptNotValidCall + 1; }
                        else
                        {
                            cptValidCall = cptValidCall + 1;
                            if (csvData.Rows[i][5].ToString() == "NATIONAL")
                            {
                                total = total + 0;
                                CDR.AddCell(csvData.Rows[i][0].ToString());
                                CDR.AddCell(csvData.Rows[i][4].ToString());
                                CDR.AddCell(csvData.Rows[i][5].ToString());
                                CDR.AddCell(csvData.Rows[i][6].ToString());
                                CDR.AddCell(csvData.Rows[i][9].ToString());
                                CDR.AddCell(Math.Round(prixAppel, 2).ToString());
                            }
                            else if (csvData.Rows[i][5].ToString() == "EUROPEAN")
                            {
                                if (!phoneNumbers.Contains(csvData.Rows[i][4].ToString()))
                                {
                                    phoneNumbers.Add(csvData.Rows[i][4].ToString());
                                    cptApl.Add(1);
                                }
                                int t = phoneNumbers.IndexOf(csvData.Rows[i][4].ToString());
                                cptApl[t] = cptApl[t] + 1;
                                if (cptApl[t] <= 10)
                                {
                                    prixAppel = ((0.270 / 60) * Convert.ToInt16(csvData.Rows[i][9].ToString()));
                                    total = total + prixAppel;
                                }
                                else
                                {
                                    prixAppel = (((0.270 / 60) - ((0.270 / 60) * 0.1)) * Convert.ToInt16(csvData.Rows[i][9].ToString()));
                                    total = total + prixAppel;
                                }
                                CDR.AddCell(csvData.Rows[i][0].ToString());
                                CDR.AddCell(csvData.Rows[i][4].ToString());
                                CDR.AddCell(csvData.Rows[i][5].ToString());
                                CDR.AddCell(csvData.Rows[i][6].ToString());
                                CDR.AddCell(csvData.Rows[i][9].ToString());
                                CDR.AddCell(Math.Round(prixAppel, 2).ToString());
                            }
                            else if (csvData.Rows[i][5].ToString() == "INTERNATIONAL")
                            {
                                total = total + 0.1;
                                prixAppel = 0.1;
                                if (Convert.ToInt16(csvData.Rows[i][9].ToString()) > 600)
                                {
                                    prixAppel = ((0.085 / 60) * 600);
                                    total = total + prixAppel;
                                    int duration = Convert.ToInt16(csvData.Rows[i][9].ToString()) - 600;
                                    prixAppel = prixAppel + ((0.05 / 60) * duration);
                                    total = total + ((0.05 / 60) * duration);
                                }
                                else if (Convert.ToInt16(csvData.Rows[i][9].ToString()) < 600)
                                {
                                    prixAppel = ((0.085 / 60) * Convert.ToInt16(csvData.Rows[i][9].ToString()));
                                    total = total + prixAppel;
                                }
                                prixAppel = prixAppel + 0.1;
                                CDR.AddCell(csvData.Rows[i][0].ToString());
                                CDR.AddCell(csvData.Rows[i][4].ToString());
                                CDR.AddCell(csvData.Rows[i][5].ToString());
                                CDR.AddCell(csvData.Rows[i][6].ToString());
                                CDR.AddCell(csvData.Rows[i][9].ToString());
                                CDR.AddCell(Math.Round(prixAppel, 2).ToString());
                            }
                        }
                    }
                    Console.WriteLine(Math.Round(total, 2));
                    Console.ReadKey();
                    Console.WriteLine("\nWriting Log File ...");
                    PdfPCell summ = new PdfPCell(new Phrase("Total :" + Math.Round(total, 2).ToString()));
                    summ.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    summ.Border = 0;
                    summ.HorizontalAlignment = 1;
                    Somme.AddCell(summ);
                    pdf.Add(p);
                    pdf.Add(Somme);
                    pdf.Add(p);
                    pdf.Add(CDR);
                    pdf.Close();
                    Console.WriteLine("#============================================================================#");
                    Console.Write("\r\nFichier  : ");
                    Console.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    Console.WriteLine("  ___________________________\n");
                    Console.WriteLine("  {0}", "   Prix TOTAL:   " + Math.Round(total, 2) + "   Total EDRs in file:    " + csvData.Rows.Count + "    EDRs Traite    " + cptValidCall + "    EDRs Non traite:    " + cptNotValidCall + "   Nom du fichier:   " + d + "  ");
                    Console.WriteLine("#============================================================================#");
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("your@email.com");
                    mail.To.Add(to);
                    mail.Subject = "Invoice eBilling!";
                    mail.Body = "Hi Dear,\n\nHere is your invoice the total of the uploaded CDR is: " + (Math.Round(total, 2)) + "€ \n\nSincerly eBilling!";
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment("INVOICE_" + to.ToUpper() + ".pdf");
                    mail.Attachments.Add(attachment);
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("your@email.com", "password");
                    SmtpServer.EnableSsl = true;
                    try
                    {
                        SmtpServer.Send(mail);
                        Console.WriteLine("\nMail have been sent");
                    }
                    catch
                    {
                        Console.WriteLine("\nEror Mail was not sent");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        Log("   Prix TOTAL:   " + Math.Round(total, 2) + "   Total EDRs in file:    " + csvData.Rows.Count + "    EDRs Traite    " + cptValidCall + "    EDRs Non traite:    " + cptNotValidCall + "   Nom du fichier:   " + d + "  ", w);
                    }
                    Console.WriteLine("\nLog File Updated !");
                    File.Delete(d);
                    //File.Delete("INVOICE_" + to.ToUpper() + ".pdf");
                    Console.ReadKey();
                }
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.WriteLine("#============================================================================#");
            w.Write("\r\nFichier  : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine("  ___________________________\n");
            w.WriteLine("  {0}", logMessage);
            w.WriteLine("#============================================================================#");
        }

        private static System.Data.DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            System.Data.DataTable csvData = new System.Data.DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { ";" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return csvData;
        }

    }
}
