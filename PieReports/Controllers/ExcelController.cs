using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PieReports.Encryption_Decryption;
using PieReports.Models;
using ViewModel.Login;
using ViewModel.Reports;
using Wangkanai.Detection.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Office.Core;

namespace PieReports.Controllers
{
    public class ExcelController : Controller
    {
        private readonly ILogger<ExcelController> _logger;

        private readonly IDetectionService _detectionService;

        private IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public const string SessionKeyAge = "_Age";
        public ExcelController(ILogger<ExcelController> logger,
            IDetectionService detectionService,
             IConfiguration config)
        {
            _logger = logger;
            _detectionService = detectionService;
            _config = config;
        }
      public string FromBase64String(string inputdata)
        {
            byte[] decodedBytes = Convert.FromBase64String(inputdata);
            return Encoding.UTF8.GetString(decodedBytes);
             
        }
        public IActionResult DownloadPerformanceExcel(string FINYR, string Family)
        {
           
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingreportDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_report rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_report>(dtoq);
            rd.FINYR = FromBase64String(FINYR);
            rd.Family = FromBase64String(Family);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_performance_holding_report> psprptperformanceholding = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_report>>(data1);

            psp_dsp_report_page_setup header= pspdspreportpagesetupDetails();


            try
            {
                using (var workbook = new XLWorkbook())
                {
                    
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Performance Holding");

                     worksheet.AddPicture(_config.GetValue<string>("ImagePath"));                    

                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " +header.hdr_address ;
                    worksheet.Cell(2, 3).Value =   header.hdr_contact ;
                    worksheet.Cell(3, 3).Value =  header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Family Name"; 
                    worksheet.Cell(4, 2).Value = "Client Name";
                    worksheet.Cell(4, 3).Value = "Sub category";
                    worksheet.Cell(4, 4).Value = "contribution";
                    worksheet.Cell(4, 5).Value = "Holding cost ";
                    worksheet.Cell(4, 6).Value = "Current Market Value";
                    worksheet.Cell(4, 7).Value = "% Holding";
                    worksheet.Cell(4, 8).Value = "Unrealised Profit(Long Term)";
                    worksheet.Cell(4, 9).Value = "Realised Profit(Short Term)";
                    for (int index = 1; index <= psprptperformanceholding.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        psprptperformanceholding[index - 1].family_name;

                        worksheet.Cell(index + 4, 2).Value =
                        psprptperformanceholding[index - 1].main_client_name;

                        worksheet.Cell(index + 4, 3).Value =
                        psprptperformanceholding[index - 1].sub_category;

                        worksheet.Cell(index + 4, 4).Value =
                       psprptperformanceholding[index - 1].contribution;

                        worksheet.Cell(index + 4, 5).Value =
                        psprptperformanceholding[index -1].hld_cost;

                        worksheet.Cell(index + 4, 6).Value =
                        psprptperformanceholding[index - 1].Market_Value;

                        worksheet.Cell(index + 4, 7).Value =
                       psprptperformanceholding[index - 1].holding_per;

                        worksheet.Cell(index + 4, 8).Value =
                        psprptperformanceholding[index - 1].long_unreal_profit;

                        worksheet.Cell(index + 4, 9).Value =
                        psprptperformanceholding[index - 1].srt_ttl_gain;
                    }
                    int hr = psprptperformanceholding.Count;
                    hr += 5;

                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim() ;
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);
                   

                    worksheet.Cell(hr + 2, 1).Value =  header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);

                    
                    worksheet.Cell(hr + 3, 1).Value =  header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);


                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "" + rd.Family + "_PerformanceReport_"+ DateTime.Now.ToString("dd-MMM-yyy") +".xlsx"            
          ); 
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadIncomeDividendopupExcel(string IDfinyear, string IDfamilyid, string client_id)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            
            rd.finyr = Convert.ToInt32(FromBase64String(IDfinyear));
            rd.family_id = Convert.ToInt32(FromBase64String(IDfamilyid));
            rd.main_client_id = FromBase64String(client_id);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);
            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("DividendDetails");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(5, 1).Value = "Client";
                    worksheet.Cell(5, 2).Value = "Date";
                    worksheet.Cell(5, 3).Value = "ISIN";
                    worksheet.Cell(5, 4).Value = "Scrip";
                    worksheet.Cell(5, 5).Value = "Amount";
                    for (int index = 1; index <= dividend.Count; index++)
                    {
                        worksheet.Cell(index + 5, 1).Value =
                        dividend[index - 1].client_name;

                        worksheet.Cell(index + 5, 2).Value =
                      dividend[index - 1].dividend_date;


                        worksheet.Cell(index + 5, 3).Value =
                      dividend[index - 1].ISIN;

                        worksheet.Cell(index + 5, 4).Value =
                        dividend[index - 1].scrip_name;

                        worksheet.Cell(index + 5, 5).Value =
                        dividend[index - 1].value;
                    }

                    int hr = dividend.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);


                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "" + rd.main_client_id + "_IncomeReport_"+ DateTime.Now.ToString("dd-MMM-yyy") +".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadIncomeDividendopupPdf(string IDfinyear, string IDfamilyid, string client_id,string FinyearPdf, string FamilyNamePdf)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            rd.finyr = Convert.ToInt32(FromBase64String(IDfinyear));
            rd.family_id = Convert.ToInt32(FromBase64String(IDfamilyid));
            rd.main_client_id = FromBase64String(client_id);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();
                    // string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearPdf + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);


                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyNamePdf + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table1 = new PdfPTable(5);
                    Table1.WidthPercentage = 100;

                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(headers.Hdr_name + headers.hdr_address + "\n" + headers.hdr_contact + "\n" + headers.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Client", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table1.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table1.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("ISIN", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table1.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Scrip", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table1.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table1.AddCell(cell_4);
                    document.Add(Table1);

                    PdfPTable Table2 = new PdfPTable(5);
                    Table2.WidthPercentage = 100;

                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));

                    foreach (var screen in dividend)
                    {
                        Phrase phrase = new Phrase(new Phrase(screen.client_name, font));
                        PdfPCell cell00 = new PdfPCell(phrase);
                        cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table2.AddCell(cell00);
                        Phrase phrase1 = new Phrase(new Phrase(Convert.ToString(screen.dividend_date), font));
                        PdfPCell cell01 = new PdfPCell(phrase1);
                        cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table2.AddCell(cell01);
                        Phrase phrase2 = new Phrase(new Phrase(screen.ISIN, font));
                        PdfPCell cell02 = new PdfPCell(phrase2);
                        cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table2.AddCell(cell02);
                        Phrase phrase3 = new Phrase(new Phrase(screen.ISIN, font));
                        PdfPCell cell03 = new PdfPCell(phrase3);
                        cell03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table2.AddCell(cell03);
                        Phrase phrase4 = new Phrase(new Phrase(screen.ISIN, font));
                        PdfPCell cell04 = new PdfPCell(phrase4);
                        cell04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table2.AddCell(cell04);
                        //Table2.AddCell(getCell(Convert.ToString(screen.client_name), PdfPCell.ALIGN_CENTER));
                        //Table2.AddCell(getCell(Convert.ToString(screen.dividend_date), PdfPCell.ALIGN_CENTER));
                        //Table2.AddCell(getCell(Convert.ToString(screen.ISIN), PdfPCell.ALIGN_CENTER));
                        //Table2.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                        //Table2.AddCell(getCell(Convert.ToString(screen.value), PdfPCell.ALIGN_CENTER));

                        //Table2.AddCell(Convert.ToString(screen.client_name));
                        //Table2.AddCell(Convert.ToString(screen.dividend_date));
                        //Table2.AddCell(Convert.ToString(screen.ISIN));
                        //Table2.AddCell(Convert.ToString(screen.scrip_name));
                        //Table2.AddCell(Convert.ToString(screen.value));
                    }
                    document.Add(Table2);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(headers.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();
                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "" + rd.main_client_id + "_Income_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadPerformancePopupExcel(string client_id, string sub_category,string familyid,string Finy)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingdirectequityreportDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_direct_equity_report rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_direct_equity_report>(dtoq);
            rd.FamilyID = FromBase64String(familyid);
            rd.FINYRData = FromBase64String(Finy);
            rd.Subcategory = FromBase64String(sub_category);
            rd.ClientID = FromBase64String(client_id); 
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);

           string subcategory = FromBase64String(sub_category);
            if (subcategory == "Direct Equity")
            {
                List<psp_rpt_performance_holding_direct_equity_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_direct_equity_report>>(data1);

                try
                {
                    psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.Add("Performance Holding");
                        worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                        worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                        worksheet.Cell(2, 3).Value = header.hdr_contact;
                        worksheet.Cell(3, 3).Value = header.hdr_email;
                        worksheet.Cell(4, 1).Value = "Client";
                        worksheet.Cell(4, 2).Value = "Scrip";
                        worksheet.Cell(4, 3).Value = "ISIN";
                        worksheet.Cell(4, 4).Value = "Quantity";
                        worksheet.Cell(4, 5).Value = "Holding cost";
                        worksheet.Cell(4, 6).Value = "Current Market Value";
                        for (int index = 1; index <= psprptperformanceholdingdirectequity.Count; index++)
                        {
                            worksheet.Cell(index + 5, 1).Value =
                            psprptperformanceholdingdirectequity[index - 1].main_client_name;

                            worksheet.Cell(index + 5, 2).Value =
                          psprptperformanceholdingdirectequity[index - 1].scrip_name;


                            worksheet.Cell(index + 5, 3).Value =
                          psprptperformanceholdingdirectequity[index - 1].isin;
                            worksheet.Cell(index + 5, 4).Value =
                        psprptperformanceholdingdirectequity[index - 1].Quantity;
                            worksheet.Cell(index + 5, 5).Value =
                       psprptperformanceholdingdirectequity[index - 1].holding_per;
                            worksheet.Cell(index + 5, 6).Value =
                    psprptperformanceholdingdirectequity[index - 1].Market_Value;
                        }

                        int hr = psprptperformanceholdingdirectequity.Count;
                        hr += 7;
                        worksheet.Cell(hr, 1).Value = header.ftr1;
                        worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                        worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                        worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                        worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                        worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                        worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                        worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                        using var stream = new MemoryStream();
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        // return File(content, contentType, fileName);


                        return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "" + rd.ClientID + "_PerformanceReport_DirectEquity_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
              );
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (subcategory == "Debt MF")
            {
                List<psp_rpt_performance_holding_debt_mf_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_debt_mf_report>>(data1);

                try
                {
                    psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.Add("Performance Holding");
                        worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                        worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                        worksheet.Cell(2, 3).Value = header.hdr_contact;
                        worksheet.Cell(3, 3).Value = header.hdr_email;
                        worksheet.Cell(4, 1).Value = "Client";
                        worksheet.Cell(4, 2).Value = "Scrip";
                        worksheet.Cell(4, 3).Value = "Date_of_purchase";
                        worksheet.Cell(4, 4).Value = "Quantity";
                        worksheet.Cell(4, 5).Value = "Holding cost";
                        worksheet.Cell(4, 6).Value = "Current Market Value";
                        for (int index = 1; index <= psprptperformanceholdingdirectequity.Count; index++)
                        {
                            worksheet.Cell(index + 5, 1).Value =
                            psprptperformanceholdingdirectequity[index - 1].main_client_name;

                            worksheet.Cell(index + 5, 2).Value =
                          psprptperformanceholdingdirectequity[index - 1].scrip_name;


                            worksheet.Cell(index + 5, 3).Value =
                          psprptperformanceholdingdirectequity[index - 1].date_of_purchase;
                            worksheet.Cell(index + 5, 4).Value =
                        psprptperformanceholdingdirectequity[index - 1].Quantity;
                            worksheet.Cell(index + 5, 5).Value =
                       psprptperformanceholdingdirectequity[index - 1].hld_cost;
                            worksheet.Cell(index + 5, 6).Value =
                    psprptperformanceholdingdirectequity[index - 1].Market_Value;
                        }

                        int hr = psprptperformanceholdingdirectequity.Count;
                        hr += 7;
                        worksheet.Cell(hr, 1).Value = header.ftr1;
                        worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                        worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                        worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                        worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                        worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                        worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                        worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                        using var stream = new MemoryStream();
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        // return File(content, contentType, fileName);


                        return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "" + rd.ClientID + "_PerformanceReport_DebtMF_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
              );
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (subcategory == "Bonds")
            {
                List<psp_rpt_performance_holding_bonds_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_bonds_report>>(data1);

                try
                {
                    psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.Add("Performance Holding");
                        worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                        worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                        worksheet.Cell(2, 3).Value = header.hdr_contact;
                        worksheet.Cell(3, 3).Value = header.hdr_email;
                        worksheet.Cell(4, 1).Value = "Client";
                        worksheet.Cell(4, 2).Value = "Scrip";
                        worksheet.Cell(4, 3).Value = "Category";
                        worksheet.Cell(4, 4).Value = "Sub_category";
                        worksheet.Cell(4, 5).Value = "Date_of_purchase";
                        worksheet.Cell(4, 6).Value = "Quantity";
                        for (int index = 1; index <= psprptperformanceholdingdirectequity.Count; index++)
                        {
                            worksheet.Cell(index + 5, 1).Value =
                            psprptperformanceholdingdirectequity[index - 1].main_client_name;

                            worksheet.Cell(index + 5, 2).Value =
                          psprptperformanceholdingdirectequity[index - 1].scrip_name;


                            worksheet.Cell(index + 5, 3).Value =
                          psprptperformanceholdingdirectequity[index - 1].category;
                            worksheet.Cell(index + 5, 4).Value =
                        psprptperformanceholdingdirectequity[index - 1].sub_category;
                            worksheet.Cell(index + 5, 5).Value =
                       psprptperformanceholdingdirectequity[index - 1].date_of_purchase;
                            worksheet.Cell(index + 5, 6).Value =
                    psprptperformanceholdingdirectequity[index - 1].quantity;
                        }

                        int hr = psprptperformanceholdingdirectequity.Count;
                        hr += 7;
                        worksheet.Cell(hr, 1).Value = header.ftr1;
                        worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                        worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                        worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                        worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                        worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                        worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                        worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                        using var stream = new MemoryStream();
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        // return File(content, contentType, fileName);


                        return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "" + rd.ClientID + "_PerformanceReport_Bonds_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
              );
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (subcategory == "Equity PMS")
            {
                List<psp_rpt_performance_holding_equity_pms_report_new_format> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_equity_pms_report_new_format>>(data1);

                try
                {
                    psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.Add("Performance Holding");
                        worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                        worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                        worksheet.Cell(2, 3).Value = header.hdr_contact;
                        worksheet.Cell(3, 3).Value = header.hdr_email;
                        worksheet.Cell(4, 1).Value = "Client";
                        worksheet.Cell(4, 2).Value = "Scrip";
                        worksheet.Cell(4, 3).Value = "ISIN";
                        worksheet.Cell(4, 4).Value = "Quantity";
                        worksheet.Cell(4, 5).Value = "Holding cost";
                        worksheet.Cell(4, 6).Value = "Current Market Value";
                        for (int index = 1; index <= psprptperformanceholdingdirectequity.Count; index++)
                        {
                            worksheet.Cell(index + 5, 1).Value =
                            psprptperformanceholdingdirectequity[index - 1].main_client_name;

                            worksheet.Cell(index + 5, 2).Value =
                          psprptperformanceholdingdirectequity[index - 1].scrip_name;


                            worksheet.Cell(index + 5, 3).Value =
                          psprptperformanceholdingdirectequity[index - 1].isin;
                            worksheet.Cell(index + 5, 4).Value =
                        psprptperformanceholdingdirectequity[index - 1].Quantity;
                            worksheet.Cell(index + 5, 5).Value =
                       psprptperformanceholdingdirectequity[index - 1].holding_per;
                            worksheet.Cell(index + 5, 6).Value =
                    psprptperformanceholdingdirectequity[index - 1].Market_Value;
                        }

                        int hr = psprptperformanceholdingdirectequity.Count;
                        hr += 7;
                        worksheet.Cell(hr, 1).Value = header.ftr1;
                        worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                        worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                        worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                        worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                        worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                        worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                        worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                        using var stream = new MemoryStream();
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        // return File(content, contentType, fileName);


                        return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "" + rd.ClientID + "_PerformanceReport_EquityPMS_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
              );
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (subcategory == "Equity MF")
            {
                List<psp_rpt_performance_holding_equity_mf_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_equity_mf_report>>(data1);

                try
                {
                    psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.Add("Performance Holding");
                        worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                        worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                        worksheet.Cell(2, 3).Value = header.hdr_contact;
                        worksheet.Cell(3, 3).Value = header.hdr_email;
                        worksheet.Cell(4, 1).Value = "Client";
                        worksheet.Cell(4, 2).Value = "Scrip";
                        worksheet.Cell(4, 3).Value = "Fund Style";
                        worksheet.Cell(4, 4).Value = "Quantity";
                        worksheet.Cell(4, 5).Value = "Holding cost";
                        worksheet.Cell(4, 6).Value = "Current Market Value";
                        for (int index = 1; index <= psprptperformanceholdingdirectequity.Count; index++)
                        {
                            worksheet.Cell(index + 5, 1).Value =
                            psprptperformanceholdingdirectequity[index - 1].main_client_name;

                            worksheet.Cell(index + 5, 2).Value =
                          psprptperformanceholdingdirectequity[index - 1].scrip_name;


                            worksheet.Cell(index + 5, 3).Value =
                          psprptperformanceholdingdirectequity[index - 1].fund_style;
                            worksheet.Cell(index + 5, 4).Value =
                        psprptperformanceholdingdirectequity[index - 1].Quantity;
                            worksheet.Cell(index + 5, 5).Value =
                       psprptperformanceholdingdirectequity[index - 1].holding_per;
                            worksheet.Cell(index + 5, 6).Value =
                    psprptperformanceholdingdirectequity[index - 1].Market_Value;
                        }

                        int hr = psprptperformanceholdingdirectequity.Count;
                        hr += 7;
                        worksheet.Cell(hr, 1).Value = header.ftr1;
                        worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                        worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                        worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                        worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                        worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                        worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                        worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                        using var stream = new MemoryStream();
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        // return File(content, contentType, fileName);


                        return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "" + rd.ClientID + "_PerformanceReport_EquityMF_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
              );
                    }
                }
                catch (Exception ex)
                {

                }
            }


            return null;
        }

        public IActionResult DownloadPerformancePopupPdf(string client_id, string sub_category, string familyid, string Finy,string FinyearValue,string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingdirectequityreportDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_direct_equity_report rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_direct_equity_report>(dtoq);
            rd.FamilyID = FromBase64String(familyid);
            rd.FINYRData = FromBase64String(Finy);
            rd.Subcategory = FromBase64String(sub_category);
            rd.ClientID = FromBase64String(client_id);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            string subcategory = FromBase64String(sub_category);
            if (subcategory == "Direct Equity")
            {
                List<psp_rpt_performance_holding_direct_equity_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_direct_equity_report>>(data1);

                try
                {
                    psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                        Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                        document.Open();
                        // document.Add(new Paragraph("Hello World"));
                        //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p.Alignment = 1;
                        //document.Add(p);
                        //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p1.Alignment = 1;
                        //document.Add(p1);
                        //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p2.Alignment = 1;
                        //document.Add(p2);
                        //Paragraph pe = new Paragraph("   ");
                        //document.Add(pe);
                        // string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                        //string imageURL = Path.Combine("~/images/logo.jpeg");
                        string imageURL = _config.GetValue<string>("ImagePath");
                        PdfPTable TableHeader = new PdfPTable(1);
                        TableHeader.WidthPercentage = 100;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        //Resize image depend upon your need
                        jpg.ScaleToFit(200f, 120f);
                        //Give space before image
                        jpg.SpacingBefore = 50f;
                        //Give some space after the image
                        jpg.SpacingAfter = 5f;
                        jpg.Alignment = Element.ALIGN_LEFT;


                        Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p.Alignment = 1;
                        p.Alignment = Element.ALIGN_RIGHT;
                        PdfPCell cell_000 = new PdfPCell();
                        cell_000.AddElement(p);

                        Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p1.Alignment = 1;
                        p1.Alignment = Element.ALIGN_RIGHT;
                        cell_000.Border = 0;
                        cell_000.AddElement(p1);
                        Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p2.Alignment = 1;
                        p2.Alignment = Element.ALIGN_RIGHT;
                        cell_000.AddElement(p2);
                        cell_000.AddElement(jpg);
                        TableHeader.AddCell(cell_000);
                        document.Add(TableHeader);
                        //document.Add(p1);
                        // document.Add(p2);

                        //document.Add(jpg);
                        Paragraph pe = new Paragraph("   ");
                        document.Add(pe);

                        Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        pFin.Alignment = 1;
                        pFin.Alignment = Element.ALIGN_LEFT;
                        document.Add(pFin);


                        Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        pFam.Alignment = 1;
                        pFam.Alignment = Element.ALIGN_LEFT;
                        document.Add(pFam);

                        Paragraph pheader = new Paragraph("   ");
                        document.Add(pheader);
                        PdfPTable Table = new PdfPTable(6);
                        Table.WidthPercentage = 100;

                        //PdfPTable Tableheader = new PdfPTable(1);
                        //Tableheader.WidthPercentage = 100;
                        //PdfPCell cell_header = new PdfPCell();
                        //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                        //Tableheader.AddCell(cell_header);
                        //document.Add(Tableheader);
                        Paragraph pc0 = new Paragraph("Client", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc0.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_0 = new PdfPCell();
                        cell_0.AddElement(pc0);
                        cell_0.Padding = 5;
                        Table.AddCell(cell_0);
                        Paragraph pc1 = new Paragraph("Scrip", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc1.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_1 = new PdfPCell();
                        cell_1.AddElement(pc1);
                        cell_1.Padding = 5;
                        Table.AddCell(cell_1);
                        Paragraph pc2 = new Paragraph("ISIN", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc2.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_2 = new PdfPCell();
                        cell_2.AddElement(pc2);
                        cell_2.Padding = 5;
                        Table.AddCell(cell_2);
                        Paragraph pc3 = new Paragraph("Quantity", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc3.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_3 = new PdfPCell();
                        cell_3.AddElement(pc3);
                        cell_3.Padding = 5;
                        Table.AddCell(cell_3);
                        Paragraph pc4 = new Paragraph("Holding cost", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc4.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_4 = new PdfPCell();
                        cell_4.AddElement(pc4);
                        cell_4.Padding = 5;
                        Table.AddCell(cell_4);
                        Paragraph pc5 = new Paragraph("Current Market Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc5.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_5 = new PdfPCell();
                        cell_5.AddElement(pc5);
                        cell_5.Padding = 5;
                        Table.AddCell(cell_5);



                        document.Add(Table);

                        PdfPTable Table1 = new PdfPTable(6);
                        Table1.WidthPercentage = 100;
                        Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));

                        foreach (var screen in psprptperformanceholdingdirectequity)
                        {
                            // Table1.AddCell(new PdfPCell(new Phrase(screen.main_client_name, font)), PdfPCell.ALIGN_CENTER);
                            Phrase phrase = new Phrase(new Phrase(screen.main_client_name, font));
                            PdfPCell cell00 = new PdfPCell(phrase);
                            cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell00);
                            Phrase phrase1 = new Phrase(new Phrase(screen.scrip_name, font));
                            PdfPCell cell01 = new PdfPCell(phrase1);
                            cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell01);
                            Phrase phrase2 = new Phrase(new Phrase(screen.isin, font));
                            PdfPCell cell02 = new PdfPCell(phrase2);
                            cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell02);
                            Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.Quantity), font));
                            PdfPCell cell03 = new PdfPCell(phrase3);
                            cell03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell03);
                            Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.holding_per), font));
                            PdfPCell cell04 = new PdfPCell(phrase4);
                            cell04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell04);
                            Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.Market_Value), font));
                            PdfPCell cell05 = new PdfPCell(phrase5);
                            cell05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell05);
                            // Table1.AddCell(new PdfPCell(new Phrase(screen.scrip_name, font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(screen.isin, font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Quantity), font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.holding_per), font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Market_Value), font)));
                            //Table1.AddCell(getCell(Convert.ToString(screen.main_client_name), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.isin), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.Quantity), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.holding_per), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.Market_Value), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(screen.main_client_name);
                            //Table1.AddCell(screen.scrip_name);
                            //Table1.AddCell(screen.isin);
                            //Table1.AddCell(Convert.ToString(screen.Quantity));
                            //Table1.AddCell(Convert.ToString(screen.holding_per));
                            //Table1.AddCell(Convert.ToString(screen.Market_Value)); 


                        }
                        document.Add(Table1);
                        Paragraph ps = new Paragraph("   ");
                        Paragraph ps1 = new Paragraph("   ");
                        document.Add(ps);
                        document.Add(ps1);
                        Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                        pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                        document.Add(pfoot);
                        //PdfPTable Tablefooter = new PdfPTable(1);
                        //Tablefooter.WidthPercentage = 100;
                        //PdfPCell cell_footer = new PdfPCell();
                        //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                        //Tablefooter.AddCell(cell_footer);
                        //document.Add(Tablefooter);
                        document.Close();
                        writer.Close();


                        var content = memoryStream.ToArray();
                        return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "" + rd.ClientID + "_PerformanceReport_DirectEquity_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"

              );
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if(subcategory== "Debt MF")
            {
                List<psp_rpt_performance_holding_debt_mf_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_debt_mf_report>>(data1);

                try
                {
                    psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                        Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                        document.Open();
                        // document.Add(new Paragraph("Hello World"));
                        //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p.Alignment = 1;
                        //document.Add(p);
                        //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p1.Alignment = 1;
                        //document.Add(p1);
                        //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p2.Alignment = 1;
                        //document.Add(p2);
                        //Paragraph pe = new Paragraph("   ");
                        //document.Add(pe);
                        // string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                        //string imageURL = Path.Combine("~/images/logo.jpeg");
                        string imageURL = _config.GetValue<string>("ImagePath");
                        PdfPTable TableHeader = new PdfPTable(1);
                        TableHeader.WidthPercentage = 100;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        //Resize image depend upon your need
                        jpg.ScaleToFit(200f, 120f);
                        //Give space before image
                        jpg.SpacingBefore = 50f;
                        //Give some space after the image
                        jpg.SpacingAfter = 5f;
                        jpg.Alignment = Element.ALIGN_LEFT;


                        Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p.Alignment = 1;
                        p.Alignment = Element.ALIGN_RIGHT;
                        PdfPCell cell_000 = new PdfPCell();
                        cell_000.AddElement(p);

                        Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p1.Alignment = 1;
                        p1.Alignment = Element.ALIGN_RIGHT;
                        cell_000.Border = 0;
                        cell_000.AddElement(p1);
                        Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p2.Alignment = 1;
                        p2.Alignment = Element.ALIGN_RIGHT;
                        cell_000.AddElement(p2);
                        cell_000.AddElement(jpg);
                        TableHeader.AddCell(cell_000);
                        document.Add(TableHeader);
                        //document.Add(p1);
                        // document.Add(p2);

                        //document.Add(jpg);
                        Paragraph pe = new Paragraph("   ");
                        document.Add(pe);

                        Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        pFin.Alignment = 1;
                        pFin.Alignment = Element.ALIGN_LEFT;
                        document.Add(pFin);


                        Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        pFam.Alignment = 1;
                        pFam.Alignment = Element.ALIGN_LEFT;
                        document.Add(pFam);

                        Paragraph pheader = new Paragraph("   ");
                        document.Add(pheader);
                        PdfPTable Table = new PdfPTable(6);
                        Table.WidthPercentage = 100;

                        //PdfPTable Tableheader = new PdfPTable(1);
                        //Tableheader.WidthPercentage = 100;
                        //PdfPCell cell_header = new PdfPCell();
                        //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                        //Tableheader.AddCell(cell_header);
                        //document.Add(Tableheader);
                        Paragraph pc0 = new Paragraph("Client", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc0.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_0 = new PdfPCell();
                        cell_0.AddElement(pc0);
                        cell_0.Padding = 5;
                        Table.AddCell(cell_0);
                        Paragraph pc1 = new Paragraph("Scrip", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc1.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_1 = new PdfPCell();
                        cell_1.AddElement(pc1);
                        cell_1.Padding = 5;
                        Table.AddCell(cell_1);
                        Paragraph pc2 = new Paragraph("Date_of_purchase", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc2.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_2 = new PdfPCell();
                        cell_2.AddElement(pc2);
                        cell_2.Padding = 5;
                        Table.AddCell(cell_2);
                        Paragraph pc3 = new Paragraph("Quantity", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc3.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_3 = new PdfPCell();
                        cell_3.AddElement(pc3);
                        cell_3.Padding = 5;
                        Table.AddCell(cell_3);
                        Paragraph pc4 = new Paragraph("Holding cost", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc4.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_4 = new PdfPCell();
                        cell_4.AddElement(pc4);
                        cell_4.Padding = 5;
                        Table.AddCell(cell_4);
                        Paragraph pc5 = new Paragraph("Current Market Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc5.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_5 = new PdfPCell();
                        cell_5.AddElement(pc5);
                        cell_5.Padding = 5;
                        Table.AddCell(cell_5);
                      


                        document.Add(Table);

                        PdfPTable Table1 = new PdfPTable(6);
                        Table1.WidthPercentage = 100;
                        Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));

                        foreach (var screen in psprptperformanceholdingdirectequity)
                        {
                            // Table1.AddCell(new PdfPCell(new Phrase(screen.main_client_name, font)), PdfPCell.ALIGN_CENTER);
                            Phrase phrase = new Phrase(new Phrase(screen.main_client_name, font));
                            PdfPCell cell00 = new PdfPCell(phrase);
                            cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell00);
                            Phrase phrase1 = new Phrase(new Phrase(screen.scrip_name, font));
                            PdfPCell cell01 = new PdfPCell(phrase1);
                            cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell01);
                            Phrase phrase2 = new Phrase(new Phrase(screen.date_of_purchase, font));
                            PdfPCell cell02 = new PdfPCell(phrase2);
                            cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell02);
                            Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.Quantity), font));
                            PdfPCell cell03 = new PdfPCell(phrase3);
                            cell03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell03);
                            Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.holding_per), font));
                            PdfPCell cell04 = new PdfPCell(phrase4);
                            cell04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell04);
                            Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.Market_Value), font));
                            PdfPCell cell05 = new PdfPCell(phrase5);
                            cell05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell05);
                            // Table1.AddCell(new PdfPCell(new Phrase(screen.scrip_name, font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(screen.isin, font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Quantity), font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.holding_per), font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Market_Value), font)));
                            //Table1.AddCell(getCell(Convert.ToString(screen.main_client_name), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.isin), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.Quantity), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.holding_per), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.Market_Value), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(screen.main_client_name);
                            //Table1.AddCell(screen.scrip_name);
                            //Table1.AddCell(screen.isin);
                            //Table1.AddCell(Convert.ToString(screen.Quantity));
                            //Table1.AddCell(Convert.ToString(screen.holding_per));
                            //Table1.AddCell(Convert.ToString(screen.Market_Value)); 


                        }
                        document.Add(Table1);
                        Paragraph ps = new Paragraph("   ");
                        Paragraph ps1 = new Paragraph("   ");
                        document.Add(ps);
                        document.Add(ps1);
                        Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                        pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                        document.Add(pfoot);
                        //PdfPTable Tablefooter = new PdfPTable(1);
                        //Tablefooter.WidthPercentage = 100;
                        //PdfPCell cell_footer = new PdfPCell();
                        //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                        //Tablefooter.AddCell(cell_footer);
                        //document.Add(Tablefooter);
                        document.Close();
                        writer.Close();


                        var content = memoryStream.ToArray();
                        return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "" + rd.ClientID + "_PerformanceReport_DebtMF_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"

              );
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (subcategory == "Bonds")
            {
                List<psp_rpt_performance_holding_bonds_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_bonds_report>>(data1);

                try
                {
                    psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                        Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                        document.Open();
                        // document.Add(new Paragraph("Hello World"));
                        //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p.Alignment = 1;
                        //document.Add(p);
                        //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p1.Alignment = 1;
                        //document.Add(p1);
                        //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p2.Alignment = 1;
                        //document.Add(p2);
                        //Paragraph pe = new Paragraph("   ");
                        //document.Add(pe);
                        // string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                        //string imageURL = Path.Combine("~/images/logo.jpeg");
                        string imageURL = _config.GetValue<string>("ImagePath");
                        PdfPTable TableHeader = new PdfPTable(1);
                        TableHeader.WidthPercentage = 100;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        //Resize image depend upon your need
                        jpg.ScaleToFit(200f, 120f);
                        //Give space before image
                        jpg.SpacingBefore = 50f;
                        //Give some space after the image
                        jpg.SpacingAfter = 5f;
                        jpg.Alignment = Element.ALIGN_LEFT;


                        Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p.Alignment = 1;
                        p.Alignment = Element.ALIGN_RIGHT;
                        PdfPCell cell_000 = new PdfPCell();
                        cell_000.AddElement(p);

                        Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p1.Alignment = 1;
                        p1.Alignment = Element.ALIGN_RIGHT;
                        cell_000.Border = 0;
                        cell_000.AddElement(p1);
                        Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p2.Alignment = 1;
                        p2.Alignment = Element.ALIGN_RIGHT;
                        cell_000.AddElement(p2);
                        cell_000.AddElement(jpg);
                        TableHeader.AddCell(cell_000);
                        document.Add(TableHeader);
                        //document.Add(p1);
                        // document.Add(p2);

                        //document.Add(jpg);
                        Paragraph pe = new Paragraph("   ");
                        document.Add(pe);

                        Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        pFin.Alignment = 1;
                        pFin.Alignment = Element.ALIGN_LEFT;
                        document.Add(pFin);


                        Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        pFam.Alignment = 1;
                        pFam.Alignment = Element.ALIGN_LEFT;
                        document.Add(pFam);

                        Paragraph pheader = new Paragraph("   ");
                        document.Add(pheader);
                        PdfPTable Table = new PdfPTable(6);
                        Table.WidthPercentage = 100;

                        //PdfPTable Tableheader = new PdfPTable(1);
                        //Tableheader.WidthPercentage = 100;
                        //PdfPCell cell_header = new PdfPCell();
                        //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                        //Tableheader.AddCell(cell_header);
                        //document.Add(Tableheader);
                        Paragraph pc0 = new Paragraph("Client", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc0.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_0 = new PdfPCell();
                        cell_0.AddElement(pc0);
                        cell_0.Padding = 5;
                        Table.AddCell(cell_0);
                        Paragraph pc1 = new Paragraph("Scrip", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc1.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_1 = new PdfPCell();
                        cell_1.AddElement(pc1);
                        cell_1.Padding = 5;
                        Table.AddCell(cell_1);
                        Paragraph pc2 = new Paragraph("Category", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc2.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_2 = new PdfPCell();
                        cell_2.AddElement(pc2);
                        cell_2.Padding = 5;
                        Table.AddCell(cell_2);
                        Paragraph pc3 = new Paragraph("Sub Category", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc3.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_3 = new PdfPCell();
                        cell_3.AddElement(pc3);
                        cell_3.Padding = 5;
                        Table.AddCell(cell_3);
                        Paragraph pc4 = new Paragraph("Date_of_purchase", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc4.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_4 = new PdfPCell();
                        cell_4.AddElement(pc4);
                        cell_4.Padding = 5;
                        Table.AddCell(cell_4);
                        Paragraph pc5 = new Paragraph("Quantity", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc5.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_5 = new PdfPCell();
                        cell_5.AddElement(pc5);
                        cell_5.Padding = 5;
                        Table.AddCell(cell_5);



                        document.Add(Table);

                        PdfPTable Table1 = new PdfPTable(6);
                        Table1.WidthPercentage = 100;
                        Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));

                        foreach (var screen in psprptperformanceholdingdirectequity)
                        {
                            // Table1.AddCell(new PdfPCell(new Phrase(screen.main_client_name, font)), PdfPCell.ALIGN_CENTER);
                            Phrase phrase = new Phrase(new Phrase(screen.main_client_name, font));
                            PdfPCell cell00 = new PdfPCell(phrase);
                            cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell00);
                            Phrase phrase1 = new Phrase(new Phrase(screen.scrip_name, font));
                            PdfPCell cell01 = new PdfPCell(phrase1);
                            cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell01);
                            Phrase phrase2 = new Phrase(new Phrase(screen.category, font));
                            PdfPCell cell02 = new PdfPCell(phrase2);
                            cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell02);
                            Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.sub_category), font));
                            PdfPCell cell03 = new PdfPCell(phrase3);
                            cell03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell03);
                            Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.date_of_purchase), font));
                            PdfPCell cell04 = new PdfPCell(phrase4);
                            cell04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell04);
                            Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.quantity), font));
                            PdfPCell cell05 = new PdfPCell(phrase5);
                            cell05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell05);
                            // Table1.AddCell(new PdfPCell(new Phrase(screen.scrip_name, font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(screen.isin, font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Quantity), font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.holding_per), font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Market_Value), font)));
                            //Table1.AddCell(getCell(Convert.ToString(screen.main_client_name), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.isin), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.Quantity), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.holding_per), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.Market_Value), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(screen.main_client_name);
                            //Table1.AddCell(screen.scrip_name);
                            //Table1.AddCell(screen.isin);
                            //Table1.AddCell(Convert.ToString(screen.Quantity));
                            //Table1.AddCell(Convert.ToString(screen.holding_per));
                            //Table1.AddCell(Convert.ToString(screen.Market_Value)); 


                        }
                        document.Add(Table1);
                        Paragraph ps = new Paragraph("   ");
                        Paragraph ps1 = new Paragraph("   ");
                        document.Add(ps);
                        document.Add(ps1);
                        Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                        pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                        document.Add(pfoot);
                        //PdfPTable Tablefooter = new PdfPTable(1);
                        //Tablefooter.WidthPercentage = 100;
                        //PdfPCell cell_footer = new PdfPCell();
                        //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                        //Tablefooter.AddCell(cell_footer);
                        //document.Add(Tablefooter);
                        document.Close();
                        writer.Close();


                        var content = memoryStream.ToArray();
                        return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "" + rd.ClientID + "_PerformanceReport_Bonds_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"

              );
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (subcategory == "Equity PMS")
            {
                List<psp_rpt_performance_holding_equity_pms_report_new_format> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_equity_pms_report_new_format>>(data1);

                try
                {
                    psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                        Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                        document.Open();
                        // document.Add(new Paragraph("Hello World"));
                        //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p.Alignment = 1;
                        //document.Add(p);
                        //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p1.Alignment = 1;
                        //document.Add(p1);
                        //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p2.Alignment = 1;
                        //document.Add(p2);
                        //Paragraph pe = new Paragraph("   ");
                        //document.Add(pe);
                        // string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                        //string imageURL = Path.Combine("~/images/logo.jpeg");
                        string imageURL = _config.GetValue<string>("ImagePath");
                        PdfPTable TableHeader = new PdfPTable(1);
                        TableHeader.WidthPercentage = 100;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        //Resize image depend upon your need
                        jpg.ScaleToFit(200f, 120f);
                        //Give space before image
                        jpg.SpacingBefore = 50f;
                        //Give some space after the image
                        jpg.SpacingAfter = 5f;
                        jpg.Alignment = Element.ALIGN_LEFT;


                        Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p.Alignment = 1;
                        p.Alignment = Element.ALIGN_RIGHT;
                        PdfPCell cell_000 = new PdfPCell();
                        cell_000.AddElement(p);

                        Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p1.Alignment = 1;
                        p1.Alignment = Element.ALIGN_RIGHT;
                        cell_000.Border = 0;
                        cell_000.AddElement(p1);
                        Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p2.Alignment = 1;
                        p2.Alignment = Element.ALIGN_RIGHT;
                        cell_000.AddElement(p2);
                        cell_000.AddElement(jpg);
                        TableHeader.AddCell(cell_000);
                        document.Add(TableHeader);
                        //document.Add(p1);
                        // document.Add(p2);

                        //document.Add(jpg);
                        Paragraph pe = new Paragraph("   ");
                        document.Add(pe);

                        Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        pFin.Alignment = 1;
                        pFin.Alignment = Element.ALIGN_LEFT;
                        document.Add(pFin);


                        Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        pFam.Alignment = 1;
                        pFam.Alignment = Element.ALIGN_LEFT;
                        document.Add(pFam);

                        Paragraph pheader = new Paragraph("   ");
                        document.Add(pheader);
                        PdfPTable Table = new PdfPTable(6);
                        Table.WidthPercentage = 100;

                        //PdfPTable Tableheader = new PdfPTable(1);
                        //Tableheader.WidthPercentage = 100;
                        //PdfPCell cell_header = new PdfPCell();
                        //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                        //Tableheader.AddCell(cell_header);
                        //document.Add(Tableheader);
                        Paragraph pc0 = new Paragraph("Client", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc0.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_0 = new PdfPCell();
                        cell_0.AddElement(pc0);
                        cell_0.Padding = 5;
                        Table.AddCell(cell_0);
                        Paragraph pc1 = new Paragraph("Scrip", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc1.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_1 = new PdfPCell();
                        cell_1.AddElement(pc1);
                        cell_1.Padding = 5;
                        Table.AddCell(cell_1);
                        Paragraph pc2 = new Paragraph("ISIN", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc2.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_2 = new PdfPCell();
                        cell_2.AddElement(pc2);
                        cell_2.Padding = 5;
                        Table.AddCell(cell_2);
                        Paragraph pc3 = new Paragraph("Quantity", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc3.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_3 = new PdfPCell();
                        cell_3.AddElement(pc3);
                        cell_3.Padding = 5;
                        Table.AddCell(cell_3);
                        Paragraph pc4 = new Paragraph("Holding cost", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc4.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_4 = new PdfPCell();
                        cell_4.AddElement(pc4);
                        cell_4.Padding = 5;
                        Table.AddCell(cell_4);
                        Paragraph pc5 = new Paragraph("Current Market Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc5.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_5 = new PdfPCell();
                        cell_5.AddElement(pc5);
                        cell_5.Padding = 5;
                        Table.AddCell(cell_5);



                        document.Add(Table);

                        PdfPTable Table1 = new PdfPTable(6);
                        Table1.WidthPercentage = 100;
                        Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));

                        foreach (var screen in psprptperformanceholdingdirectequity)
                        {
                            // Table1.AddCell(new PdfPCell(new Phrase(screen.main_client_name, font)), PdfPCell.ALIGN_CENTER);
                            Phrase phrase = new Phrase(new Phrase(screen.main_client_name, font));
                            PdfPCell cell00 = new PdfPCell(phrase);
                            cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell00);
                            Phrase phrase1 = new Phrase(new Phrase(screen.scrip_name, font));
                            PdfPCell cell01 = new PdfPCell(phrase1);
                            cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell01);
                            Phrase phrase2 = new Phrase(new Phrase(screen.isin, font));
                            PdfPCell cell02 = new PdfPCell(phrase2);
                            cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell02);
                            Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.Quantity), font));
                            PdfPCell cell03 = new PdfPCell(phrase3);
                            cell03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell03);
                            Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.holding_per), font));
                            PdfPCell cell04 = new PdfPCell(phrase4);
                            cell04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell04);
                            Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.Market_Value), font));
                            PdfPCell cell05 = new PdfPCell(phrase5);
                            cell05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell05);
                            // Table1.AddCell(new PdfPCell(new Phrase(screen.scrip_name, font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(screen.isin, font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Quantity), font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.holding_per), font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Market_Value), font)));
                            //Table1.AddCell(getCell(Convert.ToString(screen.main_client_name), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.isin), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.Quantity), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.holding_per), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.Market_Value), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(screen.main_client_name);
                            //Table1.AddCell(screen.scrip_name);
                            //Table1.AddCell(screen.isin);
                            //Table1.AddCell(Convert.ToString(screen.Quantity));
                            //Table1.AddCell(Convert.ToString(screen.holding_per));
                            //Table1.AddCell(Convert.ToString(screen.Market_Value)); 


                        }
                        document.Add(Table1);
                        Paragraph ps = new Paragraph("   ");
                        Paragraph ps1 = new Paragraph("   ");
                        document.Add(ps);
                        document.Add(ps1);
                        Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                        pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                        document.Add(pfoot);
                        //PdfPTable Tablefooter = new PdfPTable(1);
                        //Tablefooter.WidthPercentage = 100;
                        //PdfPCell cell_footer = new PdfPCell();
                        //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                        //Tablefooter.AddCell(cell_footer);
                        //document.Add(Tablefooter);
                        document.Close();
                        writer.Close();


                        var content = memoryStream.ToArray();
                        return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "" + rd.ClientID + "_PerformanceReport_EquityPMS_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"

              );
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (subcategory == "Equity MF")
            {
                List<psp_rpt_performance_holding_equity_mf_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_equity_mf_report>>(data1);

                try
                {
                    psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                        Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                        document.Open();
                        // document.Add(new Paragraph("Hello World"));
                        //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p.Alignment = 1;
                        //document.Add(p);
                        //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p1.Alignment = 1;
                        //document.Add(p1);
                        //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        //p2.Alignment = 1;
                        //document.Add(p2);
                        //Paragraph pe = new Paragraph("   ");
                        //document.Add(pe);
                        // string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                        //string imageURL = Path.Combine("~/images/logo.jpeg");
                        string imageURL = _config.GetValue<string>("ImagePath");
                        PdfPTable TableHeader = new PdfPTable(1);
                        TableHeader.WidthPercentage = 100;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        //Resize image depend upon your need
                        jpg.ScaleToFit(200f, 120f);
                        //Give space before image
                        jpg.SpacingBefore = 50f;
                        //Give some space after the image
                        jpg.SpacingAfter = 5f;
                        jpg.Alignment = Element.ALIGN_LEFT;


                        Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p.Alignment = 1;
                        p.Alignment = Element.ALIGN_RIGHT;
                        PdfPCell cell_000 = new PdfPCell();
                        cell_000.AddElement(p);

                        Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p1.Alignment = 1;
                        p1.Alignment = Element.ALIGN_RIGHT;
                        cell_000.Border = 0;
                        cell_000.AddElement(p1);
                        Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        p2.Alignment = 1;
                        p2.Alignment = Element.ALIGN_RIGHT;
                        cell_000.AddElement(p2);
                        cell_000.AddElement(jpg);
                        TableHeader.AddCell(cell_000);
                        document.Add(TableHeader);
                        //document.Add(p1);
                        // document.Add(p2);

                        //document.Add(jpg);
                        Paragraph pe = new Paragraph("   ");
                        document.Add(pe);

                        Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        pFin.Alignment = 1;
                        pFin.Alignment = Element.ALIGN_LEFT;
                        document.Add(pFin);


                        Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                        pFam.Alignment = 1;
                        pFam.Alignment = Element.ALIGN_LEFT;
                        document.Add(pFam);

                        Paragraph pheader = new Paragraph("   ");
                        document.Add(pheader);
                        PdfPTable Table = new PdfPTable(6);
                        Table.WidthPercentage = 100;

                        //PdfPTable Tableheader = new PdfPTable(1);
                        //Tableheader.WidthPercentage = 100;
                        //PdfPCell cell_header = new PdfPCell();
                        //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                        //Tableheader.AddCell(cell_header);
                        //document.Add(Tableheader);
                        Paragraph pc0 = new Paragraph("Client", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc0.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_0 = new PdfPCell();
                        cell_0.AddElement(pc0);
                        cell_0.Padding = 5;
                        Table.AddCell(cell_0);
                        Paragraph pc1 = new Paragraph("Scrip", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc1.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_1 = new PdfPCell();
                        cell_1.AddElement(pc1);
                        cell_1.Padding = 5;
                        Table.AddCell(cell_1);
                        Paragraph pc2 = new Paragraph("Fund Style", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc2.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_2 = new PdfPCell();
                        cell_2.AddElement(pc2);
                        cell_2.Padding = 5;
                        Table.AddCell(cell_2);
                        Paragraph pc3 = new Paragraph("Quantity", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc3.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_3 = new PdfPCell();
                        cell_3.AddElement(pc3);
                        cell_3.Padding = 5;
                        Table.AddCell(cell_3);
                        Paragraph pc4 = new Paragraph("Holding cost", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc4.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_4 = new PdfPCell();
                        cell_4.AddElement(pc4);
                        cell_4.Padding = 5;
                        Table.AddCell(cell_4);
                        Paragraph pc5 = new Paragraph("Current Market Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                        pc5.Alignment = Element.ALIGN_CENTER;
                        PdfPCell cell_5 = new PdfPCell();
                        cell_5.AddElement(pc5);
                        cell_5.Padding = 5;
                        Table.AddCell(cell_5);



                        document.Add(Table);

                        PdfPTable Table1 = new PdfPTable(6);
                        Table1.WidthPercentage = 100;
                        Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));

                        foreach (var screen in psprptperformanceholdingdirectequity)
                        {
                            // Table1.AddCell(new PdfPCell(new Phrase(screen.main_client_name, font)), PdfPCell.ALIGN_CENTER);
                            Phrase phrase = new Phrase(new Phrase(screen.main_client_name, font));
                            PdfPCell cell00 = new PdfPCell(phrase);
                            cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell00);
                            Phrase phrase1 = new Phrase(new Phrase(screen.scrip_name, font));
                            PdfPCell cell01 = new PdfPCell(phrase1);
                            cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell01);
                            Phrase phrase2 = new Phrase(new Phrase(screen.fund_style, font));
                            PdfPCell cell02 = new PdfPCell(phrase2);
                            cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell02);
                            Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.Quantity), font));
                            PdfPCell cell03 = new PdfPCell(phrase3);
                            cell03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell03);
                            Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.holding_per), font));
                            PdfPCell cell04 = new PdfPCell(phrase4);
                            cell04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell04);
                            Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.Market_Value), font));
                            PdfPCell cell05 = new PdfPCell(phrase5);
                            cell05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            Table1.AddCell(cell05);
                            // Table1.AddCell(new PdfPCell(new Phrase(screen.scrip_name, font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(screen.isin, font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Quantity), font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.holding_per), font)));
                            //Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Market_Value), font)));
                            //Table1.AddCell(getCell(Convert.ToString(screen.main_client_name), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.isin), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.Quantity), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.holding_per), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(getCell(Convert.ToString(screen.Market_Value), PdfPCell.ALIGN_CENTER));
                            //Table1.AddCell(screen.main_client_name);
                            //Table1.AddCell(screen.scrip_name);
                            //Table1.AddCell(screen.isin);
                            //Table1.AddCell(Convert.ToString(screen.Quantity));
                            //Table1.AddCell(Convert.ToString(screen.holding_per));
                            //Table1.AddCell(Convert.ToString(screen.Market_Value)); 


                        }
                        document.Add(Table1);
                        Paragraph ps = new Paragraph("   ");
                        Paragraph ps1 = new Paragraph("   ");
                        document.Add(ps);
                        document.Add(ps1);
                        Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                        pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                        document.Add(pfoot);
                        //PdfPTable Tablefooter = new PdfPTable(1);
                        //Tablefooter.WidthPercentage = 100;
                        //PdfPCell cell_footer = new PdfPCell();
                        //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                        //Tablefooter.AddCell(cell_footer);
                        //document.Add(Tablefooter);
                        document.Close();
                        writer.Close();


                        var content = memoryStream.ToArray();
                        return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "" + rd.ClientID + "_PerformanceReport_EquityMF_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"

              );
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return null;
        }

        public IActionResult DownloadPerformancePdf(string FINYR, string Family,string FinyearValue,string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingreportDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_report rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_report>(dtoq);
            rd.FINYR = FromBase64String(FINYR);
            rd.Family = FromBase64String(Family);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_performance_holding_report> psprptperformanceholding = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_report>>(data1);


            //object groups = new List<psp_rpt_performance_holding_report>();
            //for (var i = 0; i < psprptperformanceholding.Count; i++)
            //{
            //    var groupName = psprptperformanceholding[i].main_client_name_header;
            //    if (groupName != "All Clients")
            //    {
            //        if (!groups[groupName])
            //        {
            //            groups[groupName] = [];
            //        }
            //        groups[groupName].push(data[i]);
            //    }
            //}


            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();



                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                   // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();
                    string imageURL = _config.GetValue<string>("ImagePath");// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    PdfPTable TableHeader = new PdfPTable(2);
                    TableHeader.WidthPercentage = 100;                    
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    //jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    //jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;
                    
                    
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));                                      
                    //p.Alignment = 1;
                    //p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(jpg);
                    cell_000.Border = 0;
                    cell_000.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell_000.VerticalAlignment   = Element.ALIGN_TOP;
                    


                    PdfPCell cell_001 = new PdfPCell();
                    //cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_001.Border = 0;
                    cell_001.AddElement(p1);

                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;

                    cell_001.AddElement(p2);
                    cell_001.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell_001.VerticalAlignment = Element.ALIGN_TOP;

                    TableHeader.AddCell(cell_000);
                    TableHeader.AddCell(cell_001);
                    
                    document.Add(TableHeader);

                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- "+ FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);


                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);


                    //Table Head
                    PdfPTable Table = new PdfPTable(15);
                    Table.WidthPercentage = 100;

                    Font headerFont = new Font(FontFactory.GetFont("Roboto, sans-serif", 8, Font.BOLD, GrayColor.BLACK));

                    //Paragraph pc0 = new Paragraph("Family Name", headerFont);
                    //pc0.Alignment = Element.ALIGN_CENTER;
                    //PdfPCell cell_0 = new PdfPCell();
                    //cell_0.AddElement(pc0);
                    //cell_0.Padding = 5;
                    //Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Client Name", headerFont);
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Sub category", headerFont);
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Contribution", headerFont);
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Holding cost", headerFont);
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Current Market Value", headerFont);
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);
                    Paragraph pc6 = new Paragraph("% Holding", headerFont);
                    pc6.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_6 = new PdfPCell();
                    cell_6.AddElement(pc6);
                    cell_6.Padding = 5;
                    Table.AddCell(cell_6);
                    Paragraph pc7 = new Paragraph("Unrealised Profit(Short Term)", headerFont);
                    pc7.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_7 = new PdfPCell();
                    cell_7.AddElement(pc7);
                    cell_7.Padding = 5;
                    Table.AddCell(cell_7);
                    Paragraph pc8 = new Paragraph("Unrealised Profit(Long Term)", headerFont);
                    pc8.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_8 = new PdfPCell();
                    cell_8.AddElement(pc8);
                    cell_8.Padding = 5;                    
                    Table.AddCell(cell_8);
                    Paragraph pc9 = new Paragraph("Realised Profit(Short Term)", headerFont);
                    pc9.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_9 = new PdfPCell();
                    cell_9.AddElement(pc9);
                    cell_9.Padding = 5;
                    Table.AddCell(cell_9);
                    Paragraph pc10 = new Paragraph("Realised Profit(Long Term)", headerFont);
                    pc10.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_10 = new PdfPCell();
                    cell_10.AddElement(pc10);
                    cell_10.Padding = 5;
                    Table.AddCell(cell_10);
                    Paragraph pc11 = new Paragraph("Total Profit(Short Term)", headerFont);
                    pc11.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_11 = new PdfPCell();
                    cell_11.AddElement(pc11);
                    cell_11.Padding = 5;
                    Table.AddCell(cell_11);
                    Paragraph pc12 = new Paragraph("Total Profit(Long Term)", headerFont);
                    pc12.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_12 = new PdfPCell();
                    cell_12.AddElement(pc12);
                    cell_12.Padding = 5;
                    Table.AddCell(cell_12);
                    Paragraph pc13 = new Paragraph("Dividend/Interest", headerFont);
                    pc13.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_13 = new PdfPCell();
                    cell_13.AddElement(pc13);
                    cell_13.Padding = 5;
                    Table.AddCell(cell_13);
                    Paragraph pc14 = new Paragraph("ABS %", headerFont);
                    pc14.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_14 = new PdfPCell();
                    cell_14.AddElement(pc14);
                    cell_14.Padding = 5;
                    Table.AddCell(cell_14);
                    Paragraph pc15 = new Paragraph("XXIR %", headerFont);
                    pc15.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_15 = new PdfPCell();
                    cell_15.AddElement(pc15);
                    cell_15.Padding = 5;
                    Table.AddCell(cell_15);


                    document.Add(Table);

                    //Table data
                    PdfPTable Table1 = new PdfPTable(15);
                    Table1.WidthPercentage = 100;
                    Table1.PaddingTop = 10;
                    // Table1.TotalHeight = 12;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 8, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in psprptperformanceholding)
                    {
                        //Phrase phrase = new Phrase(new Phrase(screen.family_name, font));
                        //PdfPCell cell00 = new PdfPCell(phrase);
                        //cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        //Table1.AddCell(cell00);
                        //Phrase phrase1 = new Phrase(new Phrase(screen.main_client_name, font));
                        //PdfPCell cell01 = new PdfPCell(phrase1);
                        //cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        //Table1.AddCell(cell01);
                        //Table1.AddCell(new PdfPCell(new Phrase(screen.family_name, font)));
                        Table1.AddCell(new PdfPCell(new Phrase(screen.main_client_name, font)));
                        Table1.AddCell(new PdfPCell(new Phrase(screen.sub_category, font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.contribution), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.hld_cost), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.Market_Value), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.holding_per), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.srt_unreal_profit), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.long_unreal_profit), font)));

                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.srt_real_profit), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.long_real_profit), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.srt_ttl_gain), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.lng_ttl_gain), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.dividend), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.return_abs), font)));
                        Table1.AddCell(new PdfPCell(new Phrase(Convert.ToString(screen.return_xirr), font)));

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: ""+ rd.Family + "_Preformance_"+DateTime.Now.ToString("dd-MMM-yyy")+".pdf"
              ); 
                }


            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult PerformanceDividendpopupExcel(string client_id, string finyr, string family_id)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            rd.finyr = Convert.ToInt32(FromBase64String(finyr));
            rd.family_id = Convert.ToInt32(FromBase64String(family_id));
            rd.main_client_id = FromBase64String(client_id);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);
            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {

                    IXLWorksheet worksheet = workbook.Worksheets.Add("Performance Dividend");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Client";
                    worksheet.Cell(4, 2).Value = "Date";
                    worksheet.Cell(4, 3).Value = "ISIN";
                    worksheet.Cell(4, 4).Value = "Scrip";
                    worksheet.Cell(4, 5).Value = "Amount";

                    for (int index = 1; index <= dividend.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        dividend[index - 1].client_name;
                        worksheet.Cell(index + 4, 2).Value =
                        dividend[index - 1].dividend_date;
                        worksheet.Cell(index + 4, 3).Value =
                        dividend[index - 1].ISIN;
                        worksheet.Cell(index + 4, 4).Value =
                       dividend[index - 1].scrip_name;
                        worksheet.Cell(index + 4, 5).Value =
                       dividend[index - 1].value;

                    }
                    int hr = dividend.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.main_client_id + "_PerformanceReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult PerformanceDividendpopupPdf(string client_id, string finyr, string family_id,string FinyearValue, string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            rd.finyr = Convert.ToInt32(FromBase64String(finyr));
            rd.family_id = Convert.ToInt32(FromBase64String(family_id));
            rd.main_client_id = FromBase64String(client_id);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);


                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(5);
                    Table.WidthPercentage = 100;

                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Client", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5; 
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("ISIN", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Scrip", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);


                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(5);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in dividend)
                    {
                        Phrase phrase = new Phrase(new Phrase(screen.client_name, font));
                        PdfPCell cell00 = new PdfPCell(phrase);
                        cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell00);

                        Phrase phrase1 = new Phrase(new Phrase(Convert.ToString(screen.dividend_date), font));
                        PdfPCell cell01 = new PdfPCell(phrase1);
                        cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell01);

                        Phrase phrase2 = new Phrase(new Phrase(Convert.ToString(screen.ISIN), font));
                        PdfPCell cell02 = new PdfPCell(phrase2);
                        cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell02);

                        Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.scrip_name), font));
                        PdfPCell cell03 = new PdfPCell(phrase3);
                        cell03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell03);

                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.value), font));
                        PdfPCell cell04 = new PdfPCell(phrase4);
                        cell04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell04);
                        //Table1.AddCell(getCell(Convert.ToString(screen.client_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.dividend_date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ISIN), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.value), PdfPCell.ALIGN_CENTER));

                        //Table1.AddCell(Convert.ToString(screen.client_name));
                        //Table1.AddCell(Convert.ToString(screen.dividend_date));
                        //Table1.AddCell(Convert.ToString(screen.ISIN));
                        //Table1.AddCell(Convert.ToString(screen.scrip_name));
                        //Table1.AddCell(Convert.ToString(screen.value));

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          fileDownloadName: "" + rd.main_client_id + "_PerformanceReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadSIPDetailsExcel()
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/SIPDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            SIPDetail rd = JsonConvert.DeserializeObject<SIPDetail>(dtoq);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<SIPDetail> sip = JsonConvert.DeserializeObject<List<SIPDetail>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {
                    
                    IXLWorksheet worksheet = workbook.Worksheets.Add("SIP");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;

                    worksheet.Cell(4, 1).Value = "Family Name";
                    worksheet.Cell(4, 2).Value = "Client Name";
                    worksheet.Cell(4, 3).Value = "Email Id";
                    worksheet.Cell(4, 4).Value = "Folio No";
                    worksheet.Cell(4, 5).Value = "Start Date";
                    worksheet.Cell(4, 6).Value = "Frequency";
                    worksheet.Cell(4, 7).Value = "Scheme Name";
                    worksheet.Cell(4, 8).Value = "Bank_Name"; 
                    for (int index = 1; index <= sip.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        sip[index - 1].Login_Name;
                        worksheet.Cell(index + 4, 2).Value =
                        sip[index - 1].main_client_name;
                        worksheet.Cell(index + 4, 3).Value =
                        sip[index - 1].Email_Id;

                        worksheet.Cell(index + 4, 4).Value =
                       sip[index - 1].Folio_no;
                        worksheet.Cell(index + 4, 5).Value =
                        sip[index - 1].Start_Date;
                        worksheet.Cell(index + 4, 6).Value =
                        sip[index - 1].Frequency;

                        worksheet.Cell(index + 4, 7).Value =
                       sip[index - 1].Scheme_Name;
                        worksheet.Cell(index + 4, 8).Value =
                        sip[index - 1].Bank_Name; 
                    }
                    int hr = sip.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          fileDownloadName: "SIPReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
        ); ;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadSIPDetailsPdf()
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/SIPDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            SIPDetail rd = JsonConvert.DeserializeObject<SIPDetail>(dtoq);

            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<SIPDetail> sip = JsonConvert.DeserializeObject<List<SIPDetail>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(8);
                    Table.WidthPercentage = 100;

                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Family Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Client Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Email Id", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Folio No", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Start Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Frequency", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);
                    Paragraph pc6 = new Paragraph("Scheme Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc6.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_6 = new PdfPCell();
                    cell_6.AddElement(pc6);
                    cell_6.Padding = 5;
                    Table.AddCell(cell_6);
                    Paragraph pc7 = new Paragraph("Bank_Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc7.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_7 = new PdfPCell();
                    cell_7.AddElement(pc7);
                    cell_7.Padding = 5;
                    Table.AddCell(cell_7);


                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(8);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in sip)
                    {
                        Phrase phrase = new Phrase(new Phrase(screen.Login_Name, font));
                        PdfPCell cell00 = new PdfPCell(phrase);
                        cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell00);
                        Phrase phrase1 = new Phrase(new Phrase(screen.main_client_name, font));
                        PdfPCell cell01 = new PdfPCell(phrase1);
                        cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell01);
                        Phrase phrase2 = new Phrase(new Phrase(screen.Email_Id, font));
                        PdfPCell cell02 = new PdfPCell(phrase2);
                        cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell02);
                        Phrase phrase3 = new Phrase(new Phrase(screen.Folio_no, font));
                        PdfPCell cell03 = new PdfPCell(phrase3);
                        cell03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell03);
                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.Start_Date), font));
                        PdfPCell cell04 = new PdfPCell(phrase4);
                        cell04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell04);
                        Phrase phrase5 = new Phrase(new Phrase(screen.Frequency, font));
                        PdfPCell cell05 = new PdfPCell(phrase5);
                        cell05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell05);
                        Phrase phrase6 = new Phrase(new Phrase(screen.Scheme_Name, font));
                        PdfPCell cell06 = new PdfPCell(phrase6);
                        cell06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell06);
                        Phrase phrase7 = new Phrase(new Phrase(screen.Bank_Name, font));
                        PdfPCell cell07 = new PdfPCell(phrase7);
                        cell07.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell07);
                        //Table1.AddCell(getCell(Convert.ToString(screen.Login_Name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.main_client_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.Email_Id), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.Folio_no), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.Start_Date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.Frequency), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.Scheme_Name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.Bank_Name), PdfPCell.ALIGN_CENTER));

                        //Table1.AddCell(Convert.ToString(screen.Login_Name));
                        //Table1.AddCell(Convert.ToString(screen.main_client_name));
                        //Table1.AddCell(Convert.ToString(screen.Email_Id));
                        //Table1.AddCell(Convert.ToString(screen.Folio_no));
                        //Table1.AddCell(Convert.ToString(screen.Start_Date));
                        //Table1.AddCell(Convert.ToString(screen.Frequency));
                        //Table1.AddCell(Convert.ToString(screen.Scheme_Name));
                        //Table1.AddCell(Convert.ToString(screen.Bank_Name));
                      
                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "SIPReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }

              
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadExpiringSIPDetailsExcel()
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/SIPDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            SIPDetail rd = JsonConvert.DeserializeObject<SIPDetail>(dtoq);
            //  rd.finyr = Int32.Parse(rpt.yearID);
            //  rd.family_id = Int32.Parse(rpt.familyListID);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<SIPDetail> sip = JsonConvert.DeserializeObject<List<SIPDetail>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {

                    IXLWorksheet worksheet = workbook.Worksheets.Add("SIP");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;

                    worksheet.Cell(4, 1).Value = "Family";
                    worksheet.Cell(4, 2).Value = "Email Id";
                    worksheet.Cell(4, 3).Value = "Folio No";
                    worksheet.Cell(4, 4).Value = "Start Date";
                    worksheet.Cell(4, 5).Value = "Frequency";
                    worksheet.Cell(4, 6).Value = "Account No";
                    worksheet.Cell(4, 7).Value = "in days";
                    
                    for (int index = 1; index <= sip.Count; index++)
                    {
                        if (!"0".Equals(sip[index - 1].terminating))
                        {
                            worksheet.Cell(index + 4, 1).Value =
                        sip[index - 1].Login_Name;
                            worksheet.Cell(index + 4, 2).Value =
                            sip[index - 1].Email_Id;
                            worksheet.Cell(index + 4, 3).Value =
                            sip[index - 1].Folio_no;

                            worksheet.Cell(index + 4, 4).Value =
                           sip[index - 1].Start_Date;
                            worksheet.Cell(index + 4, 5).Value =
                            sip[index - 1].Frequency;
                            worksheet.Cell(index + 4, 6).Value =
                            sip[index - 1].Ac_No;

                            worksheet.Cell(index + 4, 7).Value =
                           sip[index - 1].End_Date;
                        }
                    }
                    int hr = sip.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          fileDownloadName: "SIPReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
        ); ;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadExpiringSIPDetailsPdf()
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/SIPDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            SIPDetail rd = JsonConvert.DeserializeObject<SIPDetail>(dtoq);
            //  rd.finyr = Int32.Parse(rpt.yearID);
            //  rd.family_id = Int32.Parse(rpt.familyListID);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<SIPDetail> sip = JsonConvert.DeserializeObject<List<SIPDetail>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(jpg);
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                   
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(7);
                    Table.WidthPercentage = 100;

                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Family Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Email Id", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Folio No", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Start Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Frequency", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Account No", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);
                    Paragraph pc6 = new Paragraph("Expiring", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc6.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_6 = new PdfPCell();
                    cell_6.AddElement(pc6);
                    cell_6.Padding = 5;
                    Table.AddCell(cell_6);
                    


                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(7);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in sip)
                    {
                        if(!"0".Equals(screen.terminating)) { 
                        Phrase phrase = new Phrase(new Phrase(screen.Login_Name, font));
                        PdfPCell cell00 = new PdfPCell(phrase);
                        cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell00);
                        Phrase phrase1 = new Phrase(new Phrase(screen.Email_Id, font));
                        PdfPCell cell01 = new PdfPCell(phrase1);
                        cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell01);
                        Phrase phrase2 = new Phrase(new Phrase(screen.Folio_no, font));
                        PdfPCell cell02 = new PdfPCell(phrase2);
                        cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell02);
                        Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.Start_Date), font));
                        PdfPCell cell03 = new PdfPCell(phrase3);
                        cell03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell03);
                        Phrase phrase4 = new Phrase(new Phrase(screen.Frequency, font));
                        PdfPCell cell04 = new PdfPCell(phrase4);
                        cell04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell04);
                        Phrase phrase5 = new Phrase(new Phrase(screen.Ac_No, font));
                        PdfPCell cell05 = new PdfPCell(phrase5);
                        cell05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell05);
                        Phrase phrase6 = new Phrase(new Phrase(Convert.ToString(screen.End_Date), font));
                        PdfPCell cell06 = new PdfPCell(phrase6);
                        cell06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell06);
                        }

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "SIPReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }


            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public IActionResult DownloadDividendDetailsExcel(string finYear)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            rd.finyr = Convert.ToInt32(FromBase64String(finYear));
            rd.family_id = 0;  //Int32.Parse(rpt.familyListID);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend>  dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {
                   
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Dividend");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Family Name";
                    worksheet.Cell(4, 2).Value = "Client Name";
                    worksheet.Cell(4, 3).Value = "Sub category";
                    worksheet.Cell(4, 4).Value = "category";
                    worksheet.Cell(4, 5).Value = "dividend Date";
                    worksheet.Cell(4, 6).Value = "ISIN";
                    worksheet.Cell(4, 7).Value = "Scheme Name";
                    worksheet.Cell(4, 8).Value = "value";
                    for (int index = 1; index <= dividend.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        dividend[index - 1].family_name;
                        worksheet.Cell(index + 4, 2).Value =
                        dividend[index - 1].client_name;
                        worksheet.Cell(index + 4, 3).Value =
                        dividend[index - 1].sub_category;

                        worksheet.Cell(index + 4, 4).Value =
                       dividend[index - 1].category;
                        worksheet.Cell(index + 4, 5).Value =
                        dividend[index - 1].dividend_date;
                        worksheet.Cell(index + 4, 6).Value =
                        dividend[index - 1].ISIN;

                        worksheet.Cell(index + 4, 7).Value =
                       dividend[index - 1].scrip_name;
                        worksheet.Cell(index + 4, 8).Value =
                        dividend[index - 1].value;
                    }
                    int hr = dividend.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.finyr + "_DividendReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadDividendDetailsPdf(string finYear,string FinyearValue)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            rd.finyr = Convert.ToInt32(FromBase64String(finYear));
            rd.family_id = 0;  //Int32.Parse(rpt.familyListID);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);


                   

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(8);
                    Table.WidthPercentage = 100;

                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Family Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Client Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Sub category", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Category", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Dividend Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("ISIN", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);
                    Paragraph pc6 = new Paragraph("Scheme Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc6.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_6 = new PdfPCell();
                    cell_6.AddElement(pc6);
                    cell_6.Padding = 5;
                    Table.AddCell(cell_6);
                    Paragraph pc7 = new Paragraph("value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc7.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_7 = new PdfPCell();
                    cell_7.AddElement(pc7);
                    cell_7.Padding = 5;
                    Table.AddCell(cell_7);


                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(8);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in dividend)
                    {
                        Phrase phrase = new Phrase(new Phrase(screen.family_name, font));
                        PdfPCell cell00 = new PdfPCell(phrase);
                        cell00.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell00);

                        Phrase phrase1 = new Phrase(new Phrase(screen.client_name, font));
                        PdfPCell cell01 = new PdfPCell(phrase1);
                        cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell01);

                        Phrase phrase2 = new Phrase(new Phrase(screen.sub_category, font));
                        PdfPCell cell02 = new PdfPCell(phrase2);
                        cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell02);

                        Phrase phrase3 = new Phrase(new Phrase(screen.category, font));
                        PdfPCell cell03 = new PdfPCell(phrase3);
                        cell03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell03);

                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.dividend_date), font));
                        PdfPCell cell04 = new PdfPCell(phrase4);
                        cell04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell04);

                        Phrase phrase5 = new Phrase(new Phrase(screen.ISIN, font));
                        PdfPCell cell05 = new PdfPCell(phrase5);
                        cell05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell05);

                        Phrase phrase6 = new Phrase(new Phrase(screen.scrip_name, font));
                        PdfPCell cell06 = new PdfPCell(phrase6);
                        cell06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell06);
                        
                        Phrase phrase7 = new Phrase(new Phrase(Convert.ToString(screen.value), font));
                        PdfPCell cell07 = new PdfPCell(phrase7);
                        cell07.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell07);

                        //Table1.AddCell(getCell(Convert.ToString(screen.family_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.client_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.sub_category), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.category), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.dividend_date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ISIN), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.value), PdfPCell.ALIGN_CENTER));

                        //Table1.AddCell(Convert.ToString(screen.family_name));
                        //Table1.AddCell(Convert.ToString(screen.client_name));
                        //Table1.AddCell(Convert.ToString(screen.sub_category));
                        //Table1.AddCell(Convert.ToString(screen.category));
                        //Table1.AddCell(Convert.ToString(screen.dividend_date));
                        //Table1.AddCell(Convert.ToString(screen.ISIN));
                        //Table1.AddCell(Convert.ToString(screen.scrip_name));
                        //Table1.AddCell(Convert.ToString(screen.value));

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                   // document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "" + rd.finyr + "_DividendReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
               
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadDividendDetailsPopupExcel(string finYear)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            rd.finyr = Convert.ToInt32(FromBase64String(finYear));
            rd.family_id = 0;// Int32.Parse(rpt.familyListID);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {

                    IXLWorksheet worksheet = workbook.Worksheets.Add("Dividend");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Family Name";
                    worksheet.Cell(4, 2).Value = "Client Name";                   
                    worksheet.Cell(4, 3).Value = "Dividend Date";
                    worksheet.Cell(4, 4).Value = "ISIN";
                    worksheet.Cell(4, 5).Value = "Scheme Name";
                    worksheet.Cell(4, 6).Value = "Amount";
                    for (int index = 1; index <= dividend.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        dividend[index - 1].family_name;
                        worksheet.Cell(index + 4, 2).Value =
                        dividend[index - 1].client_name;
                        worksheet.Cell(index + 4, 3).Value =
                        dividend[index - 1].dividend_date;
                        worksheet.Cell(index + 4, 4).Value =
                        dividend[index - 1].ISIN;
                        worksheet.Cell(index + 4, 5).Value =
                       dividend[index - 1].scrip_name;
                        worksheet.Cell(index + 4, 6).Value =
                        dividend[index - 1].value;
                    }
                    int hr = dividend.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.finyr + "_DividendReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadDividendDetailsPopupPdf(string finYear,string FinyearValue)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            rd.finyr = Convert.ToInt32(FromBase64String(finYear));
            rd.family_id = 0;// Int32.Parse(rpt.familyListID);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);


                    
                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(6);
                    Table.WidthPercentage = 100;
                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Family Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Client Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Dividend Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("ISIN", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Scheme Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);


                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(6);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in dividend)
                    {
                        Phrase phrase0 = new Phrase(new Phrase(screen.family_name, font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);

                        Phrase phrase1 = new Phrase(new Phrase(screen.category, font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);

                        Phrase phrase2 = new Phrase(new Phrase(Convert.ToString(screen.dividend_date), font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);

                        Phrase phrase3 = new Phrase(new Phrase(screen.ISIN, font));
                        PdfPCell cell3 = new PdfPCell(phrase3);
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell3);

                        Phrase phrase4 = new Phrase(new Phrase(screen.scrip_name, font));
                        PdfPCell cell4 = new PdfPCell(phrase4);
                        cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell4);

                        Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.value), font));
                        PdfPCell cell5 = new PdfPCell(phrase5);
                        cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell5);

                        //Table1.AddCell(getCell(Convert.ToString(screen.family_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.category), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.dividend_date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ISIN), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.value), PdfPCell.ALIGN_CENTER));

                        //Table1.AddCell(Convert.ToString(screen.family_name));
                        //Table1.AddCell(Convert.ToString(screen.category));
                        //Table1.AddCell(Convert.ToString(screen.dividend_date));
                        //Table1.AddCell(Convert.ToString(screen.ISIN));
                        //Table1.AddCell(Convert.ToString(screen.scrip_name));
                        //Table1.AddCell(Convert.ToString(screen.value));

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "" + rd.finyr + "_DividendReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }

            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadIncomeDetailsExcel(string finYear,string Family)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/IncomeDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            IncomeStatement rd = JsonConvert.DeserializeObject<IncomeStatement>(dtoq);
            rd.family_id = Convert.ToInt32(FromBase64String(Family));
            rd.year = Convert.ToInt32(FromBase64String(finYear));
            rd.rpt_period = 1;
            rd.rpt_period_value = Convert.ToInt32(FromBase64String(finYear));
            // rd.main_client_id = rpt.main_client_id;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<IncomeStatement> income = JsonConvert.DeserializeObject<List<IncomeStatement>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Income");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Family ID";
                    worksheet.Cell(4, 2).Value = "Client Name";
                    worksheet.Cell(4, 3).Value = "Sub category";
                    worksheet.Cell(4, 4).Value = "category";
                    worksheet.Cell(4, 5).Value = "dividend";
                    worksheet.Cell(4, 6).Value = "Interest";
                    worksheet.Cell(4, 7).Value = "Realised (Profit & Loss)";
                    worksheet.Cell(4, 8).Value = "Taxable (Profit & Loss)";
                    for (int index = 1; index <= income.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        income[index - 1].family_id;
                        worksheet.Cell(index + 4, 2).Value =
                        income[index - 1].client_name;
                        worksheet.Cell(index + 4, 3).Value =
                        income[index - 1].sub_category;

                        worksheet.Cell(index + 4, 4).Value =
                       income[index - 1].category;
                        worksheet.Cell(index + 4, 5).Value =
                        income[index - 1].dividend;
                        worksheet.Cell(index + 4, 6).Value =
                        income[index - 1].int_pnl;

                        worksheet.Cell(index + 4, 7).Value =
                       income[index - 1].total_pnl;
                        worksheet.Cell(index + 4, 8).Value =
                        income[index - 1].total_pnl_gf;
                    }
                    int hr = income.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.family_id + "_IncomeReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadIncomeDetailsPdf(string finYear, string Family,string FinyearValue,string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/IncomeDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            IncomeStatement rd = JsonConvert.DeserializeObject<IncomeStatement>(dtoq);
            rd.family_id = Convert.ToInt32(FromBase64String(Family));
            rd.year = Convert.ToInt32(FromBase64String(finYear));
            rd.rpt_period = 1;
            rd.rpt_period_value = Convert.ToInt32(FromBase64String(finYear));
            // rd.main_client_id = rpt.main_client_id;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<IncomeStatement> income = JsonConvert.DeserializeObject<List<IncomeStatement>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);


                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(8);
                    Table.WidthPercentage = 100;
                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Family ID", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Client Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Sub category", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("category", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("dividend", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Interest", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);
                    Paragraph pc6 = new Paragraph("Realised (Profit & Loss", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc6.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_6 = new PdfPCell();
                    cell_6.AddElement(pc6);
                    cell_6.Padding = 5;
                    Table.AddCell(cell_6);
                    Paragraph pc7 = new Paragraph("Taxable (Profit & Loss", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc7.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_7 = new PdfPCell();
                    cell_7.AddElement(pc7);
                    cell_7.Padding = 5;
                    Table.AddCell(cell_7);



                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(8);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in income)
                    {
                        Phrase phrase0 = new Phrase(new Phrase(Convert.ToString(screen.family_id), font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);
                        Phrase phrase1 = new Phrase(new Phrase(screen.client_name, font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);
                        Phrase phrase2 = new Phrase(new Phrase(screen.sub_category, font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);
                        Phrase phrase3 = new Phrase(new Phrase(screen.category, font));
                        PdfPCell cell3 = new PdfPCell(phrase3);
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell3);
                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.dividend), font));
                        PdfPCell cell4 = new PdfPCell(phrase4);
                        cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell4);
                        Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.int_pnl), font));
                        PdfPCell cell5 = new PdfPCell(phrase5);
                        cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell5);
                        Phrase phrase6 = new Phrase(new Phrase(Convert.ToString(screen.total_pnl), font));
                        PdfPCell cell6 = new PdfPCell(phrase6);
                        cell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell6);
                        Phrase phrase7 = new Phrase(new Phrase(Convert.ToString(screen.total_pnl_gf), font));
                        PdfPCell cell7 = new PdfPCell(phrase7);
                        cell7.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell7);


                        //Table1.AddCell(getCell(Convert.ToString(screen.family_id), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.client_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.sub_category), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.category), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.dividend), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.int_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.total_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.total_pnl_gf), PdfPCell.ALIGN_CENTER));

                        //Table1.AddCell(Convert.ToString(screen.family_id));
                        //Table1.AddCell(Convert.ToString(screen.client_name));
                        //Table1.AddCell(Convert.ToString(screen.sub_category));
                        //Table1.AddCell(Convert.ToString(screen.category));
                        //Table1.AddCell(Convert.ToString(screen.dividend));
                        //Table1.AddCell(Convert.ToString(screen.int_pnl));
                        //Table1.AddCell(Convert.ToString(screen.total_pnl));
                        //Table1.AddCell(Convert.ToString(screen.total_pnl_gf));

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "" + rd.family_id + "_IncomeReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }

               
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadXIRRDetailsExcel(string finYear)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/PSPDSPEQUITYDEALERTRACKTDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Equitydealertracksheet rd = JsonConvert.DeserializeObject<Equitydealertracksheet>(dtoq);
            rd.Fin_yr = Convert.ToInt32(FromBase64String(finYear));
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Equitydealertracksheet> Equitydealertracksheet = JsonConvert.DeserializeObject<List<Equitydealertracksheet>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Equity");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;

                    worksheet.Cell(4, 1).Value = "Client Name";
                    worksheet.Cell(4, 2).Value = "Opening Value";
                    worksheet.Cell(4, 3).Value = "Inflow Value";
                    worksheet.Cell(4, 4).Value = "Outflow  Value";
                    worksheet.Cell(4, 5).Value = "Ledger";
                    worksheet.Cell(4, 6).Value = "Stock";
                    worksheet.Cell(4, 7).Value = "Liquid Bees";
                    worksheet.Cell(4, 8).Value = "Value";
                    for (int index = 1; index <= Equitydealertracksheet.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        Equitydealertracksheet[index - 1].client_name;
                        worksheet.Cell(index + 4, 2).Value =
                        Equitydealertracksheet[index - 1].op_valuation_amt;
                        worksheet.Cell(index + 4, 3).Value =
                        Equitydealertracksheet[index - 1].inflow_amt;

                        worksheet.Cell(index + 4, 4).Value =
                       Equitydealertracksheet[index - 1].outflow_amt;
                        worksheet.Cell(index + 4, 5).Value =
                        Equitydealertracksheet[index - 1].cl_ledger;
                        worksheet.Cell(index + 4, 6).Value =
                        Equitydealertracksheet[index - 1].stk_percent;

                        worksheet.Cell(index + 4, 7).Value =
                       Equitydealertracksheet[index - 1].Liquid_bees_amt;
                        worksheet.Cell(index + 4, 8).Value =
                        Equitydealertracksheet[index - 1].show_value;
                    }
                    int hr = Equitydealertracksheet.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.Fin_yr + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadXIRRDetailsPdf(string finYear,string FinyearValue,string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/PSPDSPEQUITYDEALERTRACKTDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Equitydealertracksheet rd = JsonConvert.DeserializeObject<Equitydealertracksheet>(dtoq);
            rd.Fin_yr = Convert.ToInt32(FromBase64String(finYear));
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Equitydealertracksheet> Equitydealertracksheet = JsonConvert.DeserializeObject<List<Equitydealertracksheet>>(data1);
            try
            {
               
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                   
                    PdfPTable Table = new PdfPTable(8);
                    Table.WidthPercentage = 100;
                   
                    PdfPTable Tableheader = new PdfPTable(1);
                    Tableheader.WidthPercentage = 100;

                    //Paragraph p = new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //Paragraph p = new Paragraph(header.Hdr_name  + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);                   
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);
                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    Paragraph pc0 = new Paragraph("Client Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Opening Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Inflow Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Outflow  Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Ledger", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Stock", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);
                    Paragraph pc6 = new Paragraph("Liquid Bees", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc6.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_6 = new PdfPCell();
                    cell_6.AddElement(pc6);
                    cell_6.Padding = 5;
                    Table.AddCell(cell_6);
                    Paragraph pc7 = new Paragraph("Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc7.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_7 = new PdfPCell();
                    cell_7.AddElement(pc7);
                    cell_7.Padding = 5;
                    Table.AddCell(cell_7);




                    // PdfPCell cell_0 = new PdfPCell();
                    // cell_0.AddElement(new Paragraph("Client Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK)));
                    //cell_0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    // cell_0.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    // Table.AddCell(cell_0);


                    //PdfPCell cell_0 = new PdfPCell(new iTextSharp.text.Phrase("Client Name"), FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE };
                    //table.AddCell(c2);

                    //PdfPCell cell_1 = new PdfPCell();
                    //cell_1.AddElement(new Paragraph("Opening Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK)));
                    //Table.AddCell(cell_1);

                    //PdfPCell cell_2 = new PdfPCell();
                    //cell_2.AddElement(new Paragraph("Inflow Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK)));
                    //Table.AddCell(cell_2);

                    //PdfPCell cell_3 = new PdfPCell();
                    //cell_3.AddElement(new Paragraph("Outflow  Value", FontFactory.GetFont("Roboto, sans-serif",13, Font.BOLD, GrayColor.BLACK)));
                    //Table.AddCell(cell_3);

                    //PdfPCell cell_4 = new PdfPCell();
                    //cell_4.AddElement(new Paragraph("Ledger", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK)));
                    //Table.AddCell(cell_4);

                    //PdfPCell cell_5 = new PdfPCell();
                    //cell_5.AddElement(new Paragraph("Stock", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK)));
                    //Table.AddCell(cell_5);

                    //PdfPCell cell_6 = new PdfPCell();
                    //cell_6.AddElement(new Paragraph("Liquid Bees", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK)));
                    //Table.AddCell(cell_6);

                    //PdfPCell cell_7 = new PdfPCell();
                    //cell_7.HorizontalAlignment = Element.ALIGN_CENTER;
                    //cell_7.AddElement(new Paragraph("Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK)));
                  
                    //Table.AddCell(cell_7);

                 

                    document.Add(Table);
                    PdfPTable Table1 = new PdfPTable(8);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in Equitydealertracksheet)
                    {
                        Phrase phrase0 = new Phrase(new Phrase(screen.client_name, font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);
                        Phrase phrase1 = new Phrase(new Phrase(Convert.ToString(screen.op_valuation_amt), font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);
                        Phrase phrase2 = new Phrase(new Phrase(Convert.ToString(screen.inflow_amt), font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);
                        Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.outflow_amt), font));
                        PdfPCell cell3 = new PdfPCell(phrase3);
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell3);
                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.cl_ledger), font));
                        PdfPCell cell4 = new PdfPCell(phrase4);
                        cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell4);
                        Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.stk_percent), font));
                        PdfPCell cell5 = new PdfPCell(phrase5);
                        cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell5);
                        Phrase phrase6 = new Phrase(new Phrase(Convert.ToString(screen.Liquid_bees_amt), font));
                        PdfPCell cell6 = new PdfPCell(phrase6);
                        cell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell6);
                        Phrase phrase7 = new Phrase(new Phrase(Convert.ToString(screen.show_value), font));
                        PdfPCell cell7 = new PdfPCell(phrase7);
                        cell7.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell7);

                        //Phrase phrase = new Phrase();
                        //phrase.Add(new Chunk(screen.client_name, new Font(Font.FontFamily.HELVETICA, 13, Font.NORMAL)));
                        //Table1.AddCell(phrase);
                        // Table1.AddCell(Convert.ToString(screen.client_name));
                        // Table1.AddCell(Convert.ToString(screen.op_valuation_amt));
                        //Table1.AddCell(getCell(Convert.ToString(screen.op_valuation_amt), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.inflow_amt), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.outflow_amt), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.cl_ledger), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.stk_percent), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.Liquid_bees_amt), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.show_value), PdfPCell.ALIGN_CENTER));


                        //Table1.AddCell(Convert.ToString(screen.inflow_amt));
                        //Table1.AddCell(Convert.ToString(screen.outflow_amt));
                        //Table1.AddCell(Convert.ToString(screen.cl_ledger));
                        //Table1.AddCell(Convert.ToString(screen.stk_percent));
                        //Table1.AddCell(Convert.ToString(screen.Liquid_bees_amt));
                        //Table1.AddCell(Convert.ToString(screen.show_value));
                      
                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);


                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //cell_footer.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    
                    document.Close();
                    writer.Close();

                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "" + rd.Fin_yr + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }


            }

            catch (Exception ex)
            {

            }
return null;
        }

        public PdfPCell getCell(String text, int alignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text));
            cell.Padding=0;
            cell.HorizontalAlignment = alignment;
            //  cell.setBorder(PdfPCell.NO_BORDER);
            
            return cell;
        }

        public IActionResult DownloadXIRRPopupExcel(string ClientID)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/EquityClientFyFactorDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientFyFactor rd = JsonConvert.DeserializeObject<EquityClientFyFactor>(dtoq);
            rd.account_code = FromBase64String(ClientID);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<EquityClientFyFactor> equityClientflow = JsonConvert.DeserializeObject<List<EquityClientFyFactor>>(data1);
            psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


            string url1 = _config.GetValue<string>("APIKey") + "Reports/EquityClientFlowDetails";
            string dtoq1 = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientFlow rd1 = JsonConvert.DeserializeObject<EquityClientFlow>(dtoq1);
            rd1.account_code = ClientID;
            //rd1.flow_type = 0;
            rd1.summary = 0;
            string serializeProfile1 = JsonConvert.SerializeObject(rd1);
            string data2 = EncryptionDecryption.Encrypt(serializeProfile1);
            EncryptData Endata1 = new EncryptData();
            Endata1.EncryptObject = data2;
            string data3 = HttpCall.HttpPostMethod(url1, Endata1);
            EquityClientFlow equityClientflow1 = JsonConvert.DeserializeObject<EquityClientFlow>(data3);
            //  List<EquityClientFlow> equityClientflow1 = JsonConvert.DeserializeObject<List<EquityClientFlow>>(data3);


            string url_3 = _config.GetValue<string>("APIKey") + "Reports/EquityClientDetails";
            string dtoq_3 = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientSummary rd_3 = JsonConvert.DeserializeObject<EquityClientSummary>(dtoq_3);
            rd_3.account_code = ClientID;
            string serializeProfile_3 = JsonConvert.SerializeObject(rd_3);
            string data_3 = EncryptionDecryption.Encrypt(serializeProfile_3);
            EncryptData Endata_3 = new EncryptData();
            Endata_3.EncryptObject = data_3;
            string data1_3 = HttpCall.HttpPostMethod(url_3, Endata_3);
            List<EquityClientSummary> equityClientSummary = JsonConvert.DeserializeObject<List<EquityClientSummary>>(data1_3);





            try
            {
                //psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Performance Holding");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name;
                    worksheet.Cell(2, 3).Value = header.hdr_address;
                    worksheet.Cell(3, 3).Value = header.hdr_contact + "\n " + header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Family";
                    worksheet.Cell(4, 2).Value = "Client (Code)";
                    worksheet.Cell(4, 3).Value = "Acc Opening Date";
                    worksheet.Cell(5, 1).Value = equityClientflow1.Login_Name;
                    worksheet.Cell(5, 2).Value = equityClientflow1.client_name;
                    worksheet.Cell(5, 3).Value = equityClientflow1.ac_open_date;
                    worksheet.Cell(6, 1).Value = "Portfolio Activity";
                    worksheet.Cell(6, 2).Value = "Amount";
                    worksheet.Cell(7, 1).Value = "Opening Stock Valuation";
                    worksheet.Cell(7, 2).Value = equityClientflow1.opening_stock;
                    worksheet.Cell(8, 1).Value = "Opening Ledger Balance";
                    worksheet.Cell(8, 2).Value = equityClientflow1.opening_ledger;
                    worksheet.Cell(9, 1).Value = "Inflow Amount";
                    worksheet.Cell(9, 2).Value = equityClientflow1.inflow_amount;
                    worksheet.Cell(10, 1).Value = "Outflow Amount";
                    worksheet.Cell(10, 2).Value = equityClientflow1.outflow_amount;
                    worksheet.Cell(11, 1).Value = "Closing Ledger Balance";
                    worksheet.Cell(11, 2).Value = equityClientflow1.closing_ledger;
                    worksheet.Cell(12, 1).Value = "Current Valuation";
                    worksheet.Cell(12, 2).Value = equityClientflow1.current_value;
                    worksheet.Cell(6, 7).Value = "Sector";
                    worksheet.Cell(6, 8).Value = "Amount";
                    worksheet.Cell(6, 9).Value = "Holding %";
                    for (int index = 1; index <= equityClientSummary.Count; index++)
                    {
                        worksheet.Cell(index + 6, 7).Value =
                        equityClientSummary[index - 1].scrip_industry;

                        worksheet.Cell(index + 6, 8).Value =
                      equityClientSummary[index - 1].amount;


                        worksheet.Cell(index + 6, 9).Value =
                      equityClientSummary[index - 1].amount / equityClientSummary[index - 1].net_amount;

                    }


                    //worksheet.Cell(2, 3).Value = header.hdr_contact;
                    //worksheet.Cell(3, 3).Value = header.hdr_email;
                    //worksheet.Cell(4, 1).Value = "Family Name";
                    //worksheet.Cell(4, 2).Value = "Client Name";
                    //worksheet.Cell(4, 3).Value = "Sub category";
                    //worksheet.Cell(4, 4).Value = "contribution";
                    //worksheet.Cell(4, 5).Value = "Holding cost ";
                    //worksheet.Cell(4, 6).Value = "Current Market Value";
                    //worksheet.Cell(4, 7).Value = "% Holding";
                    //worksheet.Cell(4, 8).Value = "Unrealised Profit(Long Term)";
                    //worksheet.Cell(4, 9).Value = "Realised Profit(Short Term)";
                    worksheet.Cell(13, 1).Value = "F.Y.";
                    worksheet.Cell(13, 2).Value = "Dividend";
                    worksheet.Cell(13, 3).Value = "Intraday";
                    worksheet.Cell(13, 4).Value = "Short Term";
                    worksheet.Cell(13, 5).Value = "Long Term";
                    worksheet.Cell(13, 6).Value = "ABS";
                    worksheet.Cell(13, 7).Value = "XIRR";
                    worksheet.Cell(13, 8).Value = "NIFTY";
                    worksheet.Cell(13, 9).Value = "MIDCAP";
                    for (int index = 1; index <= equityClientflow.Count; index++)
                    {
                        worksheet.Cell(index + 13, 1).Value =
                        equityClientflow[index - 1].fin_year;

                        worksheet.Cell(index + 13, 2).Value =
                        equityClientflow[index - 1].dividend;

                        worksheet.Cell(index + 13, 3).Value =
                        equityClientflow[index - 1].dividend;

                        worksheet.Cell(index + 13, 4).Value =
                       equityClientflow[index - 1].int_real_profit;

                        worksheet.Cell(index + 13, 5).Value =
                        equityClientflow[index - 1].srt_real_profit;

                        worksheet.Cell(index + 13, 6).Value =
                        equityClientflow[index - 1].long_real_profit;

                        worksheet.Cell(index + 13, 7).Value =
                       equityClientflow[index - 1].abs_ret;

                        worksheet.Cell(index + 13, 8).Value =
                        equityClientflow[index - 1].index_xirr;

                        worksheet.Cell(index + 13, 9).Value =
                        equityClientflow[index - 1].midcap_xirr;
                    }
                    //int hr = equityClientflow.Count;
                    //hr += 5;
                    //worksheet.Cell(hr, 3).Value = header.ftr2;


                    int hr = equityClientflow.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);


                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "" + rd.fin_year + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult DownloadXIRRPopupPDF(string ClientID,string FinyearValue,string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/EquityClientFyFactorDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientFyFactor rd = JsonConvert.DeserializeObject<EquityClientFyFactor>(dtoq);
            rd.account_code = FromBase64String(ClientID);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<EquityClientFyFactor> equityClientflow = JsonConvert.DeserializeObject<List<EquityClientFyFactor>>(data1);
            psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();

            string url1 = _config.GetValue<string>("APIKey") + "Reports/EquityClientFlowDetails";
            string dtoq1 = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientFlow rd1 = JsonConvert.DeserializeObject<EquityClientFlow>(dtoq1);
            rd1.account_code = FromBase64String(ClientID);
            //rd1.flow_type = 0;
            rd1.summary = 0;
            string serializeProfile1 = JsonConvert.SerializeObject(rd1);
            string data2 = EncryptionDecryption.Encrypt(serializeProfile1);
            EncryptData Endata1 = new EncryptData();
            Endata1.EncryptObject = data2;
            string data3 = HttpCall.HttpPostMethod(url1, Endata1);
            EquityClientFlow equityClientflow1 = JsonConvert.DeserializeObject<EquityClientFlow>(data3);
            //  List<EquityClientFlow> equityClientflow1 = JsonConvert.DeserializeObject<List<EquityClientFlow>>(data3);

            string url_3 = _config.GetValue<string>("APIKey") + "Reports/EquityClientDetails";
            string dtoq_3 = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientSummary rd_3 = JsonConvert.DeserializeObject<EquityClientSummary>(dtoq_3);
            rd_3.account_code = FromBase64String(ClientID);
            string serializeProfile_3 = JsonConvert.SerializeObject(rd_3);
            string data_3 = EncryptionDecryption.Encrypt(serializeProfile_3);
            EncryptData Endata_3 = new EncryptData();
            Endata_3.EncryptObject = data_3;
            string data1_3 = HttpCall.HttpPostMethod(url_3, Endata_3);
            List<EquityClientSummary> equityClientSummary = JsonConvert.DeserializeObject<List<EquityClientSummary>>(data1_3);

            try
            {
                psp_dsp_report_page_setup headers = pspdspreportpagesetupDetails();
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();



                    PdfPTable Table = new PdfPTable(3);
                    Table.WidthPercentage = 100;
                    PdfPTable Tableheader = new PdfPTable(1);
                    Tableheader.WidthPercentage = 100;
                    //Paragraph p = new Paragraph(headers.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(headers.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(headers.hdr_contact + "\n" + headers.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);

                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(headers.Hdr_name + headers.hdr_address + "\n" + headers.hdr_contact + "\n" + headers.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);
                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    Paragraph pc0 = new Paragraph("Family", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc01= new Paragraph("Client (Code)", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc01.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_01 = new PdfPCell();
                    cell_01.AddElement(pc01);
                    cell_01.Padding = 5;
                    Table.AddCell(cell_01);
                    Paragraph pc02 = new Paragraph("Acc Opening Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc02.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_02 = new PdfPCell();
                    cell_02.AddElement(pc02);
                    cell_02.Padding = 5;
                    Table.AddCell(cell_02);                
                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(3);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    //  PdfPCell cell_header1 = new PdfPCell();
                    Phrase phrase0 = new Phrase(new Phrase(equityClientflow1.Login_Name, font));
                    PdfPCell cell0 = new PdfPCell(phrase0);
                    cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    Table1.AddCell(cell0);
                    Phrase phrase01 = new Phrase(new Phrase(equityClientflow1.client_name, font));
                    PdfPCell cell01 = new PdfPCell(phrase01);
                    cell01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    Table1.AddCell(cell01);
                    Phrase phrase02 = new Phrase(new Phrase(equityClientflow1.ac_open_date, font));
                    PdfPCell cell02 = new PdfPCell(phrase02);
                    cell02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    Table1.AddCell(cell02);


                    //Paragraph pc3 = new Paragraph(equityClientflow1.Login_Name, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    //pc3.Alignment = Element.ALIGN_CENTER;
                    //PdfPCell cell_01 = new PdfPCell();
                    //cell_01.AddElement(pc3);
                    //Table1.AddCell(cell_01);
                    //Paragraph pc4 = new Paragraph(equityClientflow1.client_name, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    //pc4.Alignment = Element.ALIGN_CENTER;
                    //PdfPCell cell_02 = new PdfPCell();
                    //cell_02.AddElement(pc4);
                    //Table1.AddCell(cell_02);
                    //Paragraph pc5 = new Paragraph(equityClientflow1.ac_open_date, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    //pc5.Alignment = Element.ALIGN_CENTER;
                    //PdfPCell cell_03 = new PdfPCell();
                    //cell_03.AddElement(pc5);
                    //Table1.AddCell(cell_03);
                    document.Add(Table1);
                    Paragraph p3 = new Paragraph("   ");
                    Paragraph p4 = new Paragraph("   ");
                    document.Add(p3);
                    document.Add(p4);
                    
                    PdfPTable Table4 = new PdfPTable(6);
                    Table4.WidthPercentage = 100;
                    
                    PdfPTable Table111 = new PdfPTable(6);
                    Table111.WidthPercentage = 100;
                    Paragraph pc_11 = new Paragraph("Opening Stock Valuation", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_11.Alignment = Element.ALIGN_CENTER;                    
                    PdfPCell cell_11 = new PdfPCell();
                    cell_11.AddElement(pc_11);
                    cell_11.Padding = 5;
                    Table4.AddCell(cell_11);
                    Paragraph pc_12 = new Paragraph("Opening Ledger Balance", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_12.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_12 = new PdfPCell();
                    cell_12.AddElement(pc_12);
                    cell_12.Padding = 5;
                    Table4.AddCell(cell_12);
                    Paragraph pc_13 = new Paragraph("Inflow Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_13.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_13 = new PdfPCell();
                    cell_13.AddElement(pc_13);
                    cell_13.Padding = 5;
                    Table4.AddCell(cell_13);
                    Paragraph pc_14 = new Paragraph("Outflow Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_14.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_14 = new PdfPCell();
                    cell_14.AddElement(pc_14);
                    cell_14.Padding = 5;
                    Table4.AddCell(cell_14);
                    Paragraph pc_15 = new Paragraph("Closing Ledger Balance", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_15.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_15 = new PdfPCell();
                    cell_15.AddElement(pc_15);
                    cell_15.Padding = 5;
                    Table4.AddCell(cell_15);
                    Paragraph pc_16 = new Paragraph("Closing Ledger Balance", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_16.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_16 = new PdfPCell();
                    cell_16.AddElement(pc_16);
                    cell_16.Padding = 5;
                    Table4.AddCell(cell_16);
                    document.Add(Table4);

                    //  PdfPCell cell_header1 = new PdfPCell();

                    Phrase phrase11 = new Phrase(new Phrase(Convert.ToString(equityClientflow1.opening_stock), font));
                    PdfPCell cell11 = new PdfPCell(phrase11);
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    Table111.AddCell(cell11);
                    Phrase phrase12 = new Phrase(new Phrase(Convert.ToString(equityClientflow1.opening_ledger), font));
                    PdfPCell cell12 = new PdfPCell(phrase12);
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    Table111.AddCell(cell12);
                    Phrase phrase13 = new Phrase(new Phrase(Convert.ToString(equityClientflow1.inflow_amount), font));
                    PdfPCell cell13 = new PdfPCell(phrase13);
                    cell13.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    Table111.AddCell(cell13);
                    Phrase phrase14 = new Phrase(new Phrase(Convert.ToString(equityClientflow1.outflow_amount), font));
                    PdfPCell cell14 = new PdfPCell(phrase14);
                    cell14.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    Table111.AddCell(cell14);
                    Phrase phrase15 = new Phrase(new Phrase(Convert.ToString(equityClientflow1.closing_ledger), font));
                    PdfPCell cell15 = new PdfPCell(phrase15);
                    cell15.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    Table111.AddCell(cell15);
                    Phrase phrase16 = new Phrase(new Phrase(Convert.ToString(equityClientflow1.current_value), font));
                    PdfPCell cell16 = new PdfPCell(phrase16);
                    cell16.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    Table111.AddCell(cell16);

                    //Paragraph pc61 = new Paragraph(equityClientflow1.opening_stock.ToString(),FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    //pc61.Alignment = Element.ALIGN_CENTER;
                    //PdfPCell cell_61 = new PdfPCell();
                    //cell_61.AddElement(pc61);
                    //Table111.AddCell(cell_61);
                    //Paragraph pc62 = new Paragraph(equityClientflow1.opening_ledger.ToString(), FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    //pc62.Alignment = Element.ALIGN_CENTER;
                    //PdfPCell cell_62 = new PdfPCell();
                    //cell_62.AddElement(pc62);
                    //Table111.AddCell(cell_62);
                    //Paragraph pc63 = new Paragraph(equityClientflow1.inflow_amount.ToString(), FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    //pc63.Alignment = Element.ALIGN_CENTER;
                    //PdfPCell cell_63 = new PdfPCell();
                    //cell_63.AddElement(pc63);
                    //Table111.AddCell(cell_63);
                    //Paragraph pc64 = new Paragraph(equityClientflow1.outflow_amount.ToString(), FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    //pc64.Alignment = Element.ALIGN_CENTER;
                    //PdfPCell cell_64 = new PdfPCell();
                    //cell_64.AddElement(pc64);
                    //Table111.AddCell(cell_64);
                    //Paragraph pc65 = new Paragraph(equityClientflow1.closing_ledger.ToString(), FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    //pc65.Alignment = Element.ALIGN_CENTER;
                    //PdfPCell cell_65 = new PdfPCell();
                    //cell_65.AddElement(pc65);
                    //Table111.AddCell(cell_65);
                    //Paragraph pc66 = new Paragraph(equityClientflow1.current_value.ToString(), FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    //pc66.Alignment = Element.ALIGN_CENTER;
                    //PdfPCell cell_66 = new PdfPCell();
                    //cell_66.AddElement(pc66);
                    //Table111.AddCell(cell_66);

                    document.Add(Table111);
                    Paragraph p5 = new Paragraph("   ");
                    Paragraph p6 = new Paragraph("   ");
                    document.Add(p5);
                    document.Add(p6);
                    //PdfPCell cell_41 = new PdfPCell();
                    //cell_41.AddElement(new Paragraph("Opening Stock Valuation:-" + " " + equityClientflow1.opening_stock, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK)));
                    //cell_41.AddElement(new Paragraph("Opening Ledger Balance:-" + " " + equityClientflow1.opening_ledger, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK)));
                    //cell_41.AddElement(new Paragraph("Inflow Amount:-" + " " + equityClientflow1.inflow_amount, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK)));
                    //cell_41.AddElement(new Paragraph("Outflow Amount:-" + " " + equityClientflow1.outflow_amount, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK)));
                    //cell_41.AddElement(new Paragraph("Closing Ledger Balance:-" + " " + equityClientflow1.closing_ledger, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK)));
                    //cell_41.AddElement(new Paragraph("Current Valuation:-" + " " + equityClientflow1.current_value, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK)));
                    //Table4.AddCell(cell_41);

                    //PdfPCell cell_42 = new PdfPCell();

                    //Table4.AddCell(cell_42);

                    //PdfPCell cell_43 = new PdfPCell();

                    //Table4.AddCell(cell_43);

                    //PdfPCell cell_44 = new PdfPCell();

                    //Table4.AddCell(cell_44);

                    //PdfPCell cell_45 = new PdfPCell();

                    //Table4.AddCell(cell_45);

                    //PdfPCell cell_46 = new PdfPCell();

                    //Table4.AddCell(cell_46);


                    PdfPTable Table21 = new PdfPTable(3);
                    Table21.WidthPercentage = 100;

                    Paragraph pc_21 = new Paragraph("Sector", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_21.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_21 = new PdfPCell();
                    cell_21.AddElement(pc_21);
                    cell_21.Padding = 5;
                    Table21.AddCell(cell_21);
                    
                    Paragraph pc_22 = new Paragraph("Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_22.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_22 = new PdfPCell();
                    cell_22.AddElement(pc_22);
                    cell_22.Padding = 5;
                    Table21.AddCell(cell_22);

                    Paragraph pc_23 = new Paragraph("Holding %", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_23.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_23 = new PdfPCell();
                    cell_23.AddElement(pc_23);
                    cell_23.Padding = 5;
                    Table21.AddCell(cell_23);
                    document.Add(Table21);
                    PdfPTable Table2 = new PdfPTable(3);
                    Table2.WidthPercentage = 100;
                    foreach (var screen in equityClientSummary)
                    {
                        Phrase phrase21 = new Phrase(new Phrase(screen.scrip_industry, font));
                        PdfPCell cell21 = new PdfPCell(phrase21);
                        cell21.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table2.AddCell(cell21);
                        Phrase phrase22 = new Phrase(new Phrase(Convert.ToString(screen.amount), font));
                        PdfPCell cell22 = new PdfPCell(phrase22);
                        cell22.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table2.AddCell(cell22);
                        Phrase phrase23 = new Phrase(new Phrase(Convert.ToString(screen.amount), font));
                        PdfPCell cell23 = new PdfPCell(phrase23);
                        cell23.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table2.AddCell(cell23);

                        //Table2.AddCell(Convert.ToString(screen.scrip_industry));
                        //Table2.AddCell(Convert.ToString(screen.amount));
                        //Table2.AddCell(Convert.ToString(screen.amount/screen.net_amount));
                        //Table2.AddCell(getCell(Convert.ToString(screen.scrip_industry), PdfPCell.ALIGN_CENTER));
                        //Table2.AddCell(getCell(Convert.ToString(screen.amount), PdfPCell.ALIGN_CENTER));
                        //Table2.AddCell(getCell(Convert.ToString(screen.amount / screen.net_amount), PdfPCell.ALIGN_CENTER));
                    }
                    document.Add(Table2);
                    Paragraph p7 = new Paragraph("   ");
                    Paragraph p8 = new Paragraph("   ");
                    document.Add(p7);
                    document.Add(p8);
                    PdfPTable Table31 = new PdfPTable(9);
                    Table31.WidthPercentage = 100;
                    Paragraph pc_31 = new Paragraph("F.Y.", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_31.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_31 = new PdfPCell();
                    cell_31.AddElement(pc_31);
                    cell_31.Padding = 5;
                    Table31.AddCell(cell_31);
                    Paragraph pc_32 = new Paragraph("Dividend", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_32.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_32 = new PdfPCell();
                    cell_32.AddElement(pc_32);
                    cell_32.Padding = 5;
                    Table31.AddCell(cell_32);
                    Paragraph pc_33 = new Paragraph("Intraday", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_33.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_33 = new PdfPCell();
                    cell_33.AddElement(pc_33);
                    cell_33.Padding = 5;
                    Table31.AddCell(cell_33);
                    Paragraph pc_34 = new Paragraph("Short Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_34.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_34 = new PdfPCell();
                    cell_34.AddElement(pc_34);
                    cell_34.Padding = 5;
                    Table31.AddCell(cell_34);
                    Paragraph pc_35 = new Paragraph("Long Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_35.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_35 = new PdfPCell();
                    cell_35.AddElement(pc_35);
                    cell_35.Padding = 5;
                    Table31.AddCell(cell_35);
                    Paragraph pc_36 = new Paragraph("ABS", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_36.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_36 = new PdfPCell();
                    cell_36.AddElement(pc_36);
                    cell_36.Padding = 5;
                    Table31.AddCell(cell_36);
                    Paragraph pc_37 = new Paragraph("XIRR", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_37.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_37 = new PdfPCell();
                    cell_37.AddElement(pc_37);
                    cell_37.Padding = 5;
                    Table31.AddCell(cell_37);
                    Paragraph pc_38 = new Paragraph("NIFTY", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_38.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_38 = new PdfPCell();
                    cell_38.AddElement(pc_38);
                    cell_38.Padding = 5;
                    Table31.AddCell(cell_38);
                    Paragraph pc_39 = new Paragraph("MIDCAP", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc_39.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_39 = new PdfPCell();
                    cell_39.AddElement(pc_39);
                    cell_39.Padding = 5;
                    Table31.AddCell(cell_39);
                    document.Add(Table31);
                    PdfPTable Table3 = new PdfPTable(9);
                    Table3.WidthPercentage = 100;
                    foreach (var screen in equityClientflow)
                    {
                        Phrase phrase31 = new Phrase(new Phrase(screen.fin_year, font));
                        PdfPCell cell31 = new PdfPCell(phrase31);
                        cell31.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table3.AddCell(cell31);
                        Phrase phrase32 = new Phrase(new Phrase(Convert.ToString(screen.dividend), font));
                        PdfPCell cell32 = new PdfPCell(phrase32);
                        cell32.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table3.AddCell(cell32);
                        Phrase phrase33 = new Phrase(new Phrase(Convert.ToString(screen.dividend), font));
                        PdfPCell cell33 = new PdfPCell(phrase33);
                        cell33.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table3.AddCell(cell33);
                        Phrase phrase34 = new Phrase(new Phrase(Convert.ToString(screen.int_real_profit), font));
                        PdfPCell cell34 = new PdfPCell(phrase34);
                        cell34.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table3.AddCell(cell34);
                        Phrase phrase35 = new Phrase(new Phrase(Convert.ToString(screen.srt_real_profit), font));
                        PdfPCell cell35 = new PdfPCell(phrase35);
                        cell35.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table3.AddCell(cell35);
                        Phrase phrase36 = new Phrase(new Phrase(Convert.ToString(screen.long_real_profit), font));
                        PdfPCell cell36 = new PdfPCell(phrase36);
                        cell36.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table3.AddCell(cell36);
                        Phrase phrase37 = new Phrase(new Phrase(Convert.ToString(screen.abs_ret), font));
                        PdfPCell cell37 = new PdfPCell(phrase37);
                        cell37.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table3.AddCell(cell37);
                        Phrase phrase38 = new Phrase(new Phrase(Convert.ToString(screen.index_xirr), font));
                        PdfPCell cell38 = new PdfPCell(phrase38);
                        cell38.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table3.AddCell(cell38);
                        Phrase phrase39 = new Phrase(new Phrase(Convert.ToString(screen.midcap_xirr), font));
                        PdfPCell cell39 = new PdfPCell(phrase39);
                        cell39.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table3.AddCell(cell39);

                        //Table3.AddCell(getCell(Convert.ToString(screen.fin_year), PdfPCell.ALIGN_CENTER));
                        //Table3.AddCell(getCell(Convert.ToString(screen.dividend), PdfPCell.ALIGN_CENTER));
                        //Table3.AddCell(getCell(Convert.ToString(screen.dividend), PdfPCell.ALIGN_CENTER));
                        //Table3.AddCell(getCell(Convert.ToString(screen.int_real_profit), PdfPCell.ALIGN_CENTER));
                        //Table3.AddCell(getCell(Convert.ToString(screen.srt_real_profit), PdfPCell.ALIGN_CENTER));
                        //Table3.AddCell(getCell(Convert.ToString(screen.long_real_profit), PdfPCell.ALIGN_CENTER));
                        //Table3.AddCell(getCell(Convert.ToString(screen.abs_ret), PdfPCell.ALIGN_CENTER));
                        //Table3.AddCell(getCell(Convert.ToString(screen.index_xirr), PdfPCell.ALIGN_CENTER));
                        //Table3.AddCell(getCell(Convert.ToString(screen.midcap_xirr), PdfPCell.ALIGN_CENTER));
                        //Table3.AddCell(Convert.ToString(screen.fin_year));
                        //Table3.AddCell(Convert.ToString(screen.dividend));
                        //Table3.AddCell(Convert.ToString(screen.dividend));
                        //Table3.AddCell(Convert.ToString(screen.int_real_profit));
                        //Table3.AddCell(Convert.ToString(screen.srt_real_profit));
                        //Table3.AddCell(Convert.ToString(screen.long_real_profit));
                        //Table3.AddCell(Convert.ToString(screen.abs_ret));
                        //Table3.AddCell(Convert.ToString(screen.index_xirr));
                        //Table3.AddCell(Convert.ToString(screen.midcap_xirr));
                    }

                    document.Add(Table3);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);

                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);

                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "" + rd.account_code + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }


            }

            catch (Exception ex)
            {

            }




            return null;
        }

        public IActionResult psprptclientperformancecheckDetailsExcel(string FINYR,string Client, string Scrip_Code, string subcategory, string account_code, string asset_code)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptclientperformancecheckDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_client_performance_check rd = JsonConvert.DeserializeObject<psp_rpt_client_performance_check>(dtoq);
            rd.FINYR = FromBase64String(FINYR);
            rd.Client = FromBase64String(Client);
            rd.Scrip_Code = FromBase64String(Scrip_Code);
            rd.subcategory = FromBase64String(subcategory);
            rd.asset_code = FromBase64String(asset_code);
            rd.account_code = FromBase64String(account_code);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_client_performance_check> psprptclientperformancecheck = JsonConvert.DeserializeObject<List<psp_rpt_client_performance_check>>(data1);
            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {

                    IXLWorksheet worksheet = workbook.Worksheets.Add("performance");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Buy Rate";
                    worksheet.Cell(4, 2).Value = "Cmp";
                    worksheet.Cell(4, 3).Value = "Dividend";
                    worksheet.Cell(4, 4).Value = "Unrealised Profit Short Term";
                    worksheet.Cell(4, 5).Value = "Unrealised Profit Long Term";
                    worksheet.Cell(4, 6).Value = "Realised Profit Short Term";
                    worksheet.Cell(4, 7).Value = "Realised Profit Long Term";
                    worksheet.Cell(4, 8).Value = "Total Profit Short Term";
                    worksheet.Cell(4, 9).Value = "Total Profit Long Term";
                    worksheet.Cell(4, 10).Value = "% Returns ABS %";
                    worksheet.Cell(4, 11).Value = "% Returns XXIR %";
                    for (int index = 1; index <= psprptclientperformancecheck.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        psprptclientperformancecheck[index - 1].buy_sell_trn_rate;
                        worksheet.Cell(index + 4, 2).Value =
                        psprptclientperformancecheck[index - 1].cmp;
                        worksheet.Cell(index + 4, 3).Value =
                        psprptclientperformancecheck[index - 1].dividend;

                        worksheet.Cell(index + 4, 4).Value =
                       psprptclientperformancecheck[index - 1].ust_pnl;
                        worksheet.Cell(index + 4, 5).Value =
                        psprptclientperformancecheck[index - 1].ult_pnl;
                        worksheet.Cell(index + 4, 6).Value =
                        psprptclientperformancecheck[index - 1].rst_pnl;

                        worksheet.Cell(index + 4, 7).Value =
                       psprptclientperformancecheck[index - 1].rlt_pnl;
                        worksheet.Cell(index + 4, 8).Value =
                        psprptclientperformancecheck[index - 1].ttl_srt_gain;
                        worksheet.Cell(index + 4, 9).Value =
                       psprptclientperformancecheck[index - 1].ttl_long_gain;
                        worksheet.Cell(index + 4, 10).Value =
                       psprptclientperformancecheck[index - 1].absolute;
                        worksheet.Cell(index + 4, 11).Value =
                       psprptclientperformancecheck[index - 1].xirr;
                    }
                    int hr = psprptclientperformancecheck.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.account_code + "_PerformanceReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult psprptclientperformancecheckDetailsPdf(string FINYR, string Client, string Scrip_Code, string subcategory, string account_code, string asset_code,string FinyearValue,string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptclientperformancecheckDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_client_performance_check rd = JsonConvert.DeserializeObject<psp_rpt_client_performance_check>(dtoq);
            rd.FINYR = FromBase64String(FINYR);
            rd.Client = FromBase64String(Client);
            rd.Scrip_Code = FromBase64String(Scrip_Code);
            rd.subcategory = FromBase64String(subcategory);
            rd.asset_code = FromBase64String(asset_code);
            rd.account_code = FromBase64String(account_code);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_client_performance_check> psprptclientperformancecheck = JsonConvert.DeserializeObject<List<psp_rpt_client_performance_check>>(data1);
            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    // string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);
                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);


                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(11);
                    Table.WidthPercentage = 100;

                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Buy/Sell Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Buy/Sell Quantity", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Buy Rate", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Cmp", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Dividend", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Unrealised Profit Short Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);
                    Paragraph pc6 = new Paragraph("Unrealised Profit Long Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc6.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_6 = new PdfPCell();
                    cell_6.AddElement(pc6);
                    cell_6.Padding = 5;
                    Table.AddCell(cell_6);
                    Paragraph pc7 = new Paragraph("Realised Profit Short Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc7.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_7 = new PdfPCell();
                    cell_7.AddElement(pc7);
                    cell_7.Padding = 5;
                    Table.AddCell(cell_7);
                    Paragraph pc8 = new Paragraph("Realised Profit Long Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc8.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_8 = new PdfPCell();
                    cell_8.AddElement(pc8);
                    cell_8.Padding = 5;
                    Table.AddCell(cell_8);
                    Paragraph pc9 = new Paragraph("Total Profit Short Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc9.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_9 = new PdfPCell();
                    cell_9.AddElement(pc9);
                    cell_9.Padding = 5;
                    Table.AddCell(cell_9);
                    Paragraph pc10 = new Paragraph("Total Profit Long Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc10.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_10 = new PdfPCell();
                    cell_10.AddElement(pc10);
                    cell_10.Padding = 5;
                    Table.AddCell(cell_10);
                    Paragraph pc11 = new Paragraph("% Returns ABS %", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc11.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_11 = new PdfPCell();
                    cell_11.AddElement(pc11);
                    cell_11.Padding = 5;
                    Table.AddCell(cell_11);
                    Paragraph pc12 = new Paragraph("% Returns XXIR %", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc12.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_12 = new PdfPCell();
                    cell_12.AddElement(pc12);
                    cell_12.Padding = 5;
                    Table.AddCell(cell_12);

                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(11);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in psprptclientperformancecheck)
                    {
                        Phrase phrase0 = new Phrase(new Phrase(screen.buy_sell_trn_date, font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);
                        Phrase phrase1 = new Phrase(new Phrase(Convert.ToString(screen.buy_sell_trn_qty), font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);
                        Phrase phrase2 = new Phrase(new Phrase(Convert.ToString(screen.buy_sell_trn_rate), font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);
                        Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.cmp), font));
                        PdfPCell cell3 = new PdfPCell(phrase3);
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell3);
                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.dividend), font));
                        PdfPCell cell4 = new PdfPCell(phrase4);
                        cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell4);
                        Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.ust_pnl), font));
                        PdfPCell cell5 = new PdfPCell(phrase5);
                        cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell5);
                        Phrase phrase6 = new Phrase(new Phrase(Convert.ToString(screen.ult_pnl), font));
                        PdfPCell cell6 = new PdfPCell(phrase6);
                        cell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell6);
                        Phrase phrase7 = new Phrase(new Phrase(Convert.ToString(screen.rst_pnl), font));
                        PdfPCell cell7 = new PdfPCell(phrase7);
                        cell7.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell7);
                        Phrase phrase8 = new Phrase(new Phrase(Convert.ToString(screen.rlt_pnl), font));
                        PdfPCell cell8 = new PdfPCell(phrase8);
                        cell8.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell8);
                        Phrase phrase9 = new Phrase(new Phrase(Convert.ToString(screen.ttl_srt_gain), font));
                        PdfPCell cell9 = new PdfPCell(phrase9);
                        cell9.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell9);
                        Phrase phrase10 = new Phrase(new Phrase(Convert.ToString(screen.ttl_long_gain), font));
                        PdfPCell cell10 = new PdfPCell(phrase10);
                        cell10.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell10);
                        Phrase phrase11 = new Phrase(new Phrase(Convert.ToString(screen.absolute), font));
                        PdfPCell cell11 = new PdfPCell(phrase11);
                        cell11.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell11);
                        Phrase phrase12 = new Phrase(new Phrase(Convert.ToString(screen.xirr), font));
                        PdfPCell cell12 = new PdfPCell(phrase12);
                        cell12.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell12);


                        //Table1.AddCell(getCell(Convert.ToString(screen.buy_sell_trn_date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.buy_sell_trn_qty), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.buy_sell_trn_rate), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.cmp), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.dividend), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ust_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ult_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.rst_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.rlt_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ttl_srt_gain), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ttl_long_gain), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.absolute), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.xirr), PdfPCell.ALIGN_CENTER));

                        //Table1.AddCell(Convert.ToString(screen.buy_sell_trn_rate));
                        //Table1.AddCell(Convert.ToString(screen.cmp));
                        //Table1.AddCell(Convert.ToString(screen.dividend));
                        //Table1.AddCell(Convert.ToString(screen.ust_pnl));
                        //Table1.AddCell(Convert.ToString(screen.ult_pnl));
                        //Table1.AddCell(Convert.ToString(screen.rst_pnl));
                        //Table1.AddCell(Convert.ToString(screen.rlt_pnl));
                        //Table1.AddCell(Convert.ToString(screen.ttl_srt_gain));
                        //Table1.AddCell(Convert.ToString(screen.ttl_long_gain));
                        //Table1.AddCell(Convert.ToString(screen.absolute));
                        //Table1.AddCell(Convert.ToString(screen.xirr));

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          fileDownloadName: "" + rd.account_code + "_PerformanceReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult PspDspOutFlowDetailsExcel(string account_code, string flow)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/PspDspOutFlowDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            PSPDSPOUTFLOW rd = JsonConvert.DeserializeObject<PSPDSPOUTFLOW>(dtoq);
            rd.flow_type = FromBase64String(flow);
            rd.summary = "1";
            rd.account_code = FromBase64String(account_code);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<PSPDSPOUTFLOW> pdoutflow = JsonConvert.DeserializeObject<List<PSPDSPOUTFLOW>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {

                    IXLWorksheet worksheet = workbook.Worksheets.Add("XIRR");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Trans Date";
                    worksheet.Cell(4, 2).Value = "Remarks";
                    worksheet.Cell(4, 3).Value = "Amount";
                   
                    for (int index = 1; index <= pdoutflow.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        pdoutflow[index - 1].trans_date;
                        worksheet.Cell(index + 4, 2).Value =
                        pdoutflow[index - 1].remarks;
                        worksheet.Cell(index + 4, 3).Value =
                        pdoutflow[index - 1].amount;
 
                    }
                    int hr = pdoutflow.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.account_code + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult PspDspOutFlowDetailsPdf1(string account_code, string flow,string FinyearValue,string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/PspDspOutFlowDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            PSPDSPOUTFLOW rd = JsonConvert.DeserializeObject<PSPDSPOUTFLOW>(dtoq);
            rd.flow_type = FromBase64String(flow);
            rd.summary = "1";
            rd.account_code = FromBase64String(account_code);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<PSPDSPOUTFLOW> pdoutflow = JsonConvert.DeserializeObject<List<PSPDSPOUTFLOW>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));


                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);
                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(3);
                    Table.WidthPercentage = 100;
                    Paragraph pc0 = new Paragraph("Trans Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Remarks", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);

                    //PdfPCell cell_0 = new PdfPCell();
                    //cell_0.AddElement(new Paragraph("Trans Date", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Table.AddCell(cell_0);

                    //PdfPCell cell_1 = new PdfPCell();
                    //cell_1.AddElement(new Paragraph("Remarks", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Table.AddCell(cell_1);

                    //PdfPCell cell_2 = new PdfPCell();
                    //cell_2.AddElement(new Paragraph("Amount", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Table.AddCell(cell_2);



                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(3);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in pdoutflow)
                    {
                        Phrase phrase0 = new Phrase(new Phrase(screen.trans_date, font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);
                        Phrase phrase1 = new Phrase(new Phrase(Convert.ToString(screen.remarks), font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);
                        Phrase phrase2 = new Phrase(new Phrase(Convert.ToString(screen.amount), font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);

                        //Table1.AddCell(getCell(Convert.ToString(screen.trans_date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.remarks), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.amount), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(Convert.ToString(screen.trans_date));
                        //Table1.AddCell(Convert.ToString(screen.remarks));
                        //Table1.AddCell(Convert.ToString(screen.amount));
                       
                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);

                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          fileDownloadName: "" + rd.account_code + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult XIRRclientValuationflowExcel(string account_code, string client_id)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/EquityClientHoldingDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientHolding rd = JsonConvert.DeserializeObject<EquityClientHolding>(dtoq);
            rd.account_code = FromBase64String(account_code);
            rd.holding_type = "2";
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<EquityClientHolding> equityClientholding = JsonConvert.DeserializeObject<List<EquityClientHolding>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {

                    IXLWorksheet worksheet = workbook.Worksheets.Add("XIRR");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Scrip Name";
                    worksheet.Cell(4, 2).Value = "Holding Qty";
                    worksheet.Cell(4, 3).Value = "DP Holding Qty";
                    worksheet.Cell(4, 4).Value = "Holding Rate";
                    worksheet.Cell(4, 5).Value = "Holding cost";
                    worksheet.Cell(4, 6).Value = "Holding MKT Rate";
                    worksheet.Cell(4, 7).Value = "Market Value";

                    for (int index = 1; index <= equityClientholding.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        equityClientholding[index - 1].scrip_name;
                        worksheet.Cell(index + 4, 2).Value =
                        equityClientholding[index - 1].holding_qty;
                        worksheet.Cell(index + 4, 3).Value =
                        equityClientholding[index - 1].dp_holding_qty;
                        worksheet.Cell(index + 4, 4).Value =
                        equityClientholding[index - 1].holding_rate;
                        worksheet.Cell(index + 4, 5).Value =
                        equityClientholding[index - 1].holding_cost;
                        worksheet.Cell(index + 4, 6).Value =
                        equityClientholding[index - 1].holding_mkt_rate;
                        worksheet.Cell(index + 4, 7).Value =
                        equityClientholding[index - 1].market_value;

                    }
                    int hr = equityClientholding.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.account_code + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult XIRRclientValuationflowPdf(string account_code, string client_id,string FinyearValue, string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/EquityClientHoldingDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientHolding rd = JsonConvert.DeserializeObject<EquityClientHolding>(dtoq);
            rd.account_code = FromBase64String(account_code);
            rd.holding_type = "2";
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<EquityClientHolding> equityClientholding = JsonConvert.DeserializeObject<List<EquityClientHolding>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    // string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);
                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(7);
                    Table.WidthPercentage = 100;

                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Scrip Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Holding Qty", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("DP Holding Qty", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Holding Rate", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Holding cost", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Holding MKT Rate", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);
                    Paragraph pc6 = new Paragraph("Market Value", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc6.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_6 = new PdfPCell();
                    cell_6.AddElement(pc6);
                    cell_6.Padding = 5;
                    Table.AddCell(cell_6);
                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(7);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in equityClientholding)
                    {
                        Phrase phrase0 = new Phrase(new Phrase(screen.scrip_name, font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);
                        Phrase phrase1 = new Phrase(new Phrase(Convert.ToString(screen.holding_qty), font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);
                        Phrase phrase2 = new Phrase(new Phrase(Convert.ToString(screen.dp_holding_qty), font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);
                        Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.holding_rate), font));
                        PdfPCell cell3 = new PdfPCell(phrase3);
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell3);
                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.holding_cost), font));
                        PdfPCell cell4 = new PdfPCell(phrase4);
                        cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell4);
                        Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.holding_mkt_rate), font));
                        PdfPCell cell5 = new PdfPCell(phrase5);
                        cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell5);
                        Phrase phrase6 = new Phrase(new Phrase(Convert.ToString(screen.market_value), font));
                        PdfPCell cell6 = new PdfPCell(phrase6);
                        cell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell6);


                        //Table1.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.holding_qty), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.dp_holding_qty), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.holding_rate), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.holding_cost), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.holding_mkt_rate), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.market_value), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(Convert.ToString(screen.scrip_name));
                        //Table1.AddCell(Convert.ToString(screen.holding_qty));
                        //Table1.AddCell(Convert.ToString(screen.dp_holding_qty));
                        //Table1.AddCell(Convert.ToString(screen.holding_rate));
                        //Table1.AddCell(Convert.ToString(screen.holding_cost));
                        //Table1.AddCell(Convert.ToString(screen.holding_mkt_rate));
                        //Table1.AddCell(Convert.ToString(screen.market_value));

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          fileDownloadName: "" + rd.account_code + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult XIRRDividendinoutflowExcel(string client_id, string fy,string family_id)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            rd.finyr = Convert.ToInt32(FromBase64String(fy));
            rd.family_id = Convert.ToInt32(FromBase64String(family_id));
            rd.main_client_id = FromBase64String(client_id);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {

                    IXLWorksheet worksheet = workbook.Worksheets.Add("XIRR");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Client";
                    worksheet.Cell(4, 2).Value = "Date";
                    worksheet.Cell(4, 3).Value = "ISIN";
                    worksheet.Cell(4, 4).Value = "Scrip";
                    worksheet.Cell(4, 5).Value = "Amount";

                    for (int index = 1; index <= dividend.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        dividend[index - 1].client_name;
                        worksheet.Cell(index + 4, 2).Value =
                        dividend[index - 1].dividend_date;
                        worksheet.Cell(index + 4, 3).Value =
                        dividend[index - 1].ISIN;
                        worksheet.Cell(index + 4, 4).Value =
                        dividend[index - 1].scrip_name;
                        worksheet.Cell(index + 4, 5).Value =
                        dividend[index - 1].value;
                    }
                    int hr = dividend.Count;
                    hr += 7;

                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.main_client_id + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult XIRRDividendinoutflowPdf(string client_id, string fy, string family_id,string FinyearValue,string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            rd.finyr = Convert.ToInt32(FromBase64String(fy));
            rd.family_id = Convert.ToInt32(FromBase64String(family_id));
            rd.main_client_id = FromBase64String(client_id);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);


                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(5);
                    Table.WidthPercentage = 100;

                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Client", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("ISIN", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Scrip", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(5);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in dividend)
                    {
                        Phrase phrase0 = new Phrase(new Phrase(screen.client_name, font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);
                        Phrase phrase1 = new Phrase(new Phrase(Convert.ToString(screen.dividend_date), font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);
                        Phrase phrase2 = new Phrase(new Phrase(screen.ISIN, font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);
                        Phrase phrase3 = new Phrase(new Phrase(screen.scrip_name, font));
                        PdfPCell cell3 = new PdfPCell(phrase3);
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell3);
                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.value), font));
                        PdfPCell cell4 = new PdfPCell(phrase4);
                        cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell4);

                        //Table1.AddCell(getCell(Convert.ToString(screen.client_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.dividend_date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ISIN), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.scrip_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.value), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(Convert.ToString(screen.client_name));
                        //Table1.AddCell(Convert.ToString(screen.dividend_date));
                        //Table1.AddCell(Convert.ToString(screen.ISIN));
                        //Table1.AddCell(Convert.ToString(screen.scrip_name));
                        //Table1.AddCell(Convert.ToString(screen.value));
                       

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          fileDownloadName: "" + rd.main_client_id + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult XIRRInoutflowAmountExcel(string account_code, string trans_date, string flow)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspinflowoutflowDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_inflow_outflow_details rd = JsonConvert.DeserializeObject<psp_dsp_inflow_outflow_details>(dtoq);
           // rd.trans_date1 = Convert.ToDateTime(rpt.trans_date);
            rd.trans_date = FromBase64String(trans_date);
            rd.flow_type = FromBase64String(flow);
            rd.account_code = FromBase64String(account_code);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_inflow_outflow_details> pspdspinflowoutflow = JsonConvert.DeserializeObject<List<psp_dsp_inflow_outflow_details>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {

                    IXLWorksheet worksheet = workbook.Worksheets.Add("XIRR");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Trans Date";
                    worksheet.Cell(4, 2).Value = "Scrip Name";
                    worksheet.Cell(4, 3).Value = "Quantity";
                    worksheet.Cell(4, 4).Value = "Rate";
                    worksheet.Cell(4, 5).Value = "Amount";

                    for (int index = 1; index <= pspdspinflowoutflow.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        pspdspinflowoutflow[index - 1].trans_date;
                        worksheet.Cell(index + 4, 2).Value =
                        pspdspinflowoutflow[index - 1].script_name;
                        worksheet.Cell(index + 4, 3).Value =
                        pspdspinflowoutflow[index - 1].trn_qty;
                        worksheet.Cell(index + 4, 4).Value =
                        pspdspinflowoutflow[index - 1].trn_rate;
                        worksheet.Cell(index + 4, 5).Value =
                        pspdspinflowoutflow[index - 1].amount;
                    }
                    int hr = pspdspinflowoutflow.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.account_code + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult XIRRInoutflowAmountPdf(string account_code, string trans_date, string flow,string FinyearValue,string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspinflowoutflowDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_inflow_outflow_details rd = JsonConvert.DeserializeObject<psp_dsp_inflow_outflow_details>(dtoq);
            //rd.trans_date1 = Convert.ToDateTime(rpt.trans_date);
            rd.trans_date = FromBase64String(trans_date);
            rd.flow_type = FromBase64String(flow);
            rd.account_code = FromBase64String(account_code);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_inflow_outflow_details> pspdspinflowoutflow = JsonConvert.DeserializeObject<List<psp_dsp_inflow_outflow_details>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));



                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    // string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);


                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(5);
                    Table.WidthPercentage = 100;

                    Paragraph pc0 = new Paragraph("Trans Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Scrip Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Quantity", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Rate", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(5);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in pspdspinflowoutflow)
                    {

                        Phrase phrase0 = new Phrase(new Phrase(screen.trans_date, font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);
                        Phrase phrase1 = new Phrase(new Phrase(screen.script_name, font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);
                        Phrase phrase2 = new Phrase(new Phrase(Convert.ToString(screen.trn_qty), font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);
                        Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.trn_rate), font));
                        PdfPCell cell3 = new PdfPCell(phrase3);
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell3);
                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.amount), font));
                        PdfPCell cell4 = new PdfPCell(phrase4);
                        cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell4);

                        //Table1.AddCell(getCell(Convert.ToString(screen.trans_date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.script_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.trn_qty), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.trn_rate), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.amount), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(Convert.ToString(screen.trans_date));
                        //Table1.AddCell(Convert.ToString(screen.script_name));
                        //Table1.AddCell(Convert.ToString(screen.trn_qty));
                        //Table1.AddCell(Convert.ToString(screen.trn_rate));
                        //Table1.AddCell(Convert.ToString(screen.amount));

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          fileDownloadName: "" + rd.account_code + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult XIRRValuationHoldingExcel(string client_Code, string script_code)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspcurrentholdingdrilldownDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_current_holding_drill_down rd = JsonConvert.DeserializeObject<psp_dsp_current_holding_drill_down>(dtoq);
            rd.client_Code = FromBase64String(client_Code);
            rd.script_code = FromBase64String(script_code);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_current_holding_drill_down> pspDspCurrentHoldingDrillDowns = JsonConvert.DeserializeObject<List<psp_dsp_current_holding_drill_down>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {

                    IXLWorksheet worksheet = workbook.Worksheets.Add("XIRR");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Scrip Name";
                    worksheet.Cell(4, 2).Value = "Trade Date";
                    worksheet.Cell(4, 3).Value = "Quantity";
                    worksheet.Cell(4, 4).Value = "Trade Rate";
                    worksheet.Cell(4, 5).Value = "Short Unread Profit";
                    worksheet.Cell(4, 6).Value = "Long Unread Profit";

                    for (int index = 1; index <= pspDspCurrentHoldingDrillDowns.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        pspDspCurrentHoldingDrillDowns[index - 1].script_name;
                        worksheet.Cell(index + 4, 2).Value =
                        pspDspCurrentHoldingDrillDowns[index - 1].tr_date;
                        worksheet.Cell(index + 4, 3).Value =
                        pspDspCurrentHoldingDrillDowns[index - 1].cr_qty;
                        worksheet.Cell(index + 4, 4).Value =
                        pspDspCurrentHoldingDrillDowns[index - 1].tr_rate;
                        worksheet.Cell(index + 4, 5).Value =
                        pspDspCurrentHoldingDrillDowns[index - 1].srt_unreal_profit;
                        worksheet.Cell(index + 4, 6).Value =
                        pspDspCurrentHoldingDrillDowns[index - 1].long_unreal_profit;

                    }
                    int hr = pspDspCurrentHoldingDrillDowns.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.client_Code + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult XIRRValuationHoldingPdf(string client_Code, string script_code,string FinyearValue,string FamilyName)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspcurrentholdingdrilldownDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_current_holding_drill_down rd = JsonConvert.DeserializeObject<psp_dsp_current_holding_drill_down>(dtoq);
            rd.client_Code = FromBase64String(client_Code);
            rd.script_code = FromBase64String(script_code);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_current_holding_drill_down> pspDspCurrentHoldingDrillDowns = JsonConvert.DeserializeObject<List<psp_dsp_current_holding_drill_down>>(data1);

            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);


                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(6);
                    Table.WidthPercentage = 100;

                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Scrip Name", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Trade Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Quantity", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Trade Rate", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Short Unread Profit", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Long Unread Profit", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);

                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(6);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in pspDspCurrentHoldingDrillDowns)
                    {
                        Phrase phrase0 = new Phrase(new Phrase(screen.script_name, font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);
                        Phrase phrase1 = new Phrase(new Phrase(screen.tr_date, font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);
                        Phrase phrase2 = new Phrase(new Phrase(Convert.ToString(screen.cr_qty), font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);
                        Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.tr_rate), font));
                        PdfPCell cell3 = new PdfPCell(phrase3);
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell3);
                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.srt_unreal_profit), font));
                        PdfPCell cell4 = new PdfPCell(phrase4);
                        cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell4);
                        Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.long_unreal_profit), font));
                        PdfPCell cell5 = new PdfPCell(phrase5);
                        cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell5);

                        //Table1.AddCell(getCell(Convert.ToString(screen.script_name), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.tr_date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.cr_qty), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.tr_rate), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.srt_unreal_profit), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.long_unreal_profit), PdfPCell.ALIGN_CENTER));

                        //Table1.AddCell(Convert.ToString(screen.script_name));
                        //Table1.AddCell(Convert.ToString(screen.tr_date));
                        //Table1.AddCell(Convert.ToString(screen.cr_qty));
                        //Table1.AddCell(Convert.ToString(screen.tr_rate));
                        //Table1.AddCell(Convert.ToString(screen.srt_unreal_profit));
                        //Table1.AddCell(Convert.ToString(screen.long_unreal_profit));
                       

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          fileDownloadName: "" + rd.client_Code + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult PspDspOutFlowDetailsPdf(string FINYR, string Client, string Scrip_Code, string subcategory, string account_code, string asset_code)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptclientperformancecheckDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_client_performance_check rd = JsonConvert.DeserializeObject<psp_rpt_client_performance_check>(dtoq);
            rd.FINYR = FromBase64String(FINYR);
            rd.Client = FromBase64String(Client);
            rd.Scrip_Code = FromBase64String(Scrip_Code);
            rd.subcategory = FromBase64String(subcategory);
            rd.asset_code = FromBase64String(asset_code);
            rd.account_code = FromBase64String(account_code);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_client_performance_check> psprptclientperformancecheck = JsonConvert.DeserializeObject<List<psp_rpt_client_performance_check>>(data1);
            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                   
                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(11);
                    Table.WidthPercentage = 100;

                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Buy Rate", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Cmp", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Dividend", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Unrealised Profit Short Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Unrealised Profit Long Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Realised Profit Short Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);
                    Paragraph pc6 = new Paragraph("Realised Profit Long Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc6.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_6 = new PdfPCell();
                    cell_6.AddElement(pc6);
                    cell_6.Padding = 5;
                    Table.AddCell(cell_6);
                    Paragraph pc7 = new Paragraph("Total Profit Short Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc7.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_7 = new PdfPCell();
                    cell_7.AddElement(pc7);
                    cell_7.Padding = 5;
                    Table.AddCell(cell_7);
                    Paragraph pc8 = new Paragraph("Total Profit Long Term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc8.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_8 = new PdfPCell();
                    cell_8.AddElement(pc8);
                    cell_8.Padding = 5;
                    Table.AddCell(cell_8);
                    Paragraph pc9 = new Paragraph("% Returns ABS %", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc9.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_9 = new PdfPCell();
                    cell_9.AddElement(pc9);
                    cell_9.Padding = 5;
                    Table.AddCell(cell_9);
                    Paragraph pc10 = new Paragraph("% Returns XXIR %", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc10.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_10 = new PdfPCell();
                    cell_10.AddElement(pc10);
                    cell_10.Padding = 5;
                    Table.AddCell(cell_10);

                    document.Add(Table);

                    PdfPTable Table1 = new PdfPTable(11);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in psprptclientperformancecheck)
                    {
                        Phrase phrase0 = new Phrase(new Phrase(Convert.ToString(screen.buy_sell_trn_rate), font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);
                        Phrase phrase1 = new Phrase(new Phrase(Convert.ToString(screen.cmp), font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);
                        Phrase phrase2 = new Phrase(new Phrase(Convert.ToString(screen.dividend), font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);
                        Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.ust_pnl), font));
                        PdfPCell cell3 = new PdfPCell(phrase3);
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell3);
                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.ult_pnl), font));
                        PdfPCell cell4 = new PdfPCell(phrase4);
                        cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell4);
                        Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.rst_pnl), font));
                        PdfPCell cell5 = new PdfPCell(phrase5);
                        cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell5);
                        Phrase phrase6 = new Phrase(new Phrase(Convert.ToString(screen.rlt_pnl), font));
                        PdfPCell cell6 = new PdfPCell(phrase6);
                        cell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell6);
                        Phrase phrase7 = new Phrase(new Phrase(Convert.ToString(screen.ttl_srt_gain), font));
                        PdfPCell cell7 = new PdfPCell(phrase7);
                        cell7.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell7);
                        Phrase phrase8 = new Phrase(new Phrase(Convert.ToString(screen.ttl_long_gain), font));
                        PdfPCell cell8 = new PdfPCell(phrase8);
                        cell8.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell8);
                        Phrase phrase9 = new Phrase(new Phrase(Convert.ToString(screen.absolute), font));
                        PdfPCell cell9 = new PdfPCell(phrase9);
                        cell9.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell9);
                        Phrase phrase10 = new Phrase(new Phrase(Convert.ToString(screen.xirr), font));
                        PdfPCell cell10 = new PdfPCell(phrase10);
                        cell10.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell10);

                        //Table1.AddCell(getCell(Convert.ToString(screen.buy_sell_trn_rate), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.cmp), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.dividend), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ust_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ult_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.rst_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.rlt_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ttl_srt_gain), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.ttl_long_gain), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.absolute), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.xirr), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(Convert.ToString(screen.buy_sell_trn_rate));
                        //Table1.AddCell(Convert.ToString(screen.cmp));
                        //Table1.AddCell(Convert.ToString(screen.dividend));
                        //Table1.AddCell(Convert.ToString(screen.ust_pnl));
                        //Table1.AddCell(Convert.ToString(screen.ult_pnl));
                        //Table1.AddCell(Convert.ToString(screen.rst_pnl));
                        //Table1.AddCell(Convert.ToString(screen.rlt_pnl));
                        //Table1.AddCell(Convert.ToString(screen.ttl_srt_gain));
                        //Table1.AddCell(Convert.ToString(screen.ttl_long_gain));
                        //Table1.AddCell(Convert.ToString(screen.absolute));
                        //Table1.AddCell(Convert.ToString(screen.xirr));

                    }
                    document.Add(Table1);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          fileDownloadName: "" + rd.client_code + "_XIRRReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult RealisedDetailsExcel(string FINYEAR, string ClientID, string Subcategory, string Rtpperiod , string Rptperiod_value)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/RealisedDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            RealisedGainLoss rd = JsonConvert.DeserializeObject<RealisedGainLoss>(dtoq);
            rd.FINYR = Convert.ToInt32(FromBase64String(FINYEAR));
            rd.sub_category = FromBase64String(Subcategory);
            rd.Client = FromBase64String(ClientID);
            rd.rpt_period = Convert.ToInt32(FromBase64String(Rtpperiod));
            rd.rpt_period_value = FromBase64String(Rptperiod_value);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<RealisedGainLoss> realisedgainloss = JsonConvert.DeserializeObject<List<RealisedGainLoss>>(data1);
            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();
                using (var workbook = new XLWorkbook())
                {

                    IXLWorksheet worksheet = workbook.Worksheets.Add("RealisedDetails");
                    worksheet.AddPicture(_config.GetValue<string>("ImagePath"));
                    worksheet.Cell(1, 3).Value = header.Hdr_name + "\n " + header.hdr_address;
                    worksheet.Cell(2, 3).Value = header.hdr_contact;
                    worksheet.Cell(3, 3).Value = header.hdr_email;
                    worksheet.Cell(4, 1).Value = "Qty";
                    worksheet.Cell(4, 2).Value = "Date";
                    worksheet.Cell(4, 3).Value = "Rate";
                    worksheet.Cell(4, 4).Value = "Amount";
                    worksheet.Cell(4, 5).Value = "Date";
                    worksheet.Cell(4, 6).Value = "Rate";
                    worksheet.Cell(4, 7).Value = "Amount";
                    worksheet.Cell(4, 8).Value = "Intraday";
                    worksheet.Cell(4, 9).Value = "Short term";
                    worksheet.Cell(4, 10).Value = "Long term";
                    worksheet.Cell(4, 11).Value = "Total";

                    for (int index = 1; index <= realisedgainloss.Count; index++)
                    {
                        worksheet.Cell(index + 4, 1).Value =
                        realisedgainloss[index - 1].buy_trn_qty;
                        worksheet.Cell(index + 4, 2).Value =
                        realisedgainloss[index - 1].buy_trn_date;
                        worksheet.Cell(index + 4, 3).Value =
                        realisedgainloss[index - 1].buy_trn_rate;
                        worksheet.Cell(index + 4, 4).Value =
                        realisedgainloss[index - 1].buy_trn_rate;
                        worksheet.Cell(index + 4, 5).Value =
                        realisedgainloss[index - 1].sell_trn_date;
                        worksheet.Cell(index + 4, 6).Value =
                        realisedgainloss[index - 1].sell_trn_rate;
                        worksheet.Cell(index + 4, 7).Value =
                        realisedgainloss[index - 1].sell_trn_rate;
                        worksheet.Cell(index + 4, 8).Value =
                        realisedgainloss[index - 1].it_pnl_gf;
                        worksheet.Cell(index + 4, 9).Value =
                        realisedgainloss[index - 1].st_pnl;
                        worksheet.Cell(index + 4, 10).Value =
                        realisedgainloss[index - 1].it_pnl;
                        worksheet.Cell(index + 4, 11).Value =
                        realisedgainloss[index - 1].int_pnl;

                    }
                    int hr = realisedgainloss.Count;
                    hr += 7;
                    worksheet.Cell(hr, 1).Value = header.ftr1;
                    worksheet.Cell(hr, 1).Style.Alignment.SetWrapText(false);

                    worksheet.Cell(hr + 1, 1).Value = header.ftr2.Trim();
                    worksheet.Cell(hr + 1, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 2, 1).Value = header.ftr3.Trim();
                    worksheet.Cell(hr + 2, 1).Style.Alignment.SetWrapText(false);


                    worksheet.Cell(hr + 3, 1).Value = header.ftr4.Trim();
                    worksheet.Cell(hr + 3, 1).Style.Alignment.SetWrapText(false);

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // return File(content, contentType, fileName);

                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileDownloadName: "" + rd.client_code + "_IncomeReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".xlsx"
          );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public IActionResult RealisedDetailsPdf(string FINYEAR, string ClientID, string Subcategory, string Rtpperiod, string Rptperiod_value,string FinyearValue,string FamilyName)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/RealisedDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            RealisedGainLoss rd = JsonConvert.DeserializeObject<RealisedGainLoss>(dtoq);
            rd.FINYR = Convert.ToInt32(FromBase64String(FINYEAR));
            rd.sub_category = FromBase64String(Subcategory);
            rd.Client = FromBase64String(ClientID);
            rd.rpt_period = Convert.ToInt32(FromBase64String(Rtpperiod));
            rd.rpt_period_value = FromBase64String(Rptperiod_value);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<RealisedGainLoss> realisedgainloss = JsonConvert.DeserializeObject<List<RealisedGainLoss>>(data1);
            try
            {
                psp_dsp_report_page_setup header = pspdspreportpagesetupDetails();


                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    // Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 15, 10);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();
                    // document.Add(new Paragraph("Hello World"));
                    //Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p.Alignment = 1;
                    //document.Add(p);
                    //Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p1.Alignment = 1;
                    //document.Add(p1);
                    //Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    //p2.Alignment = 1;
                    //document.Add(p2);
                    //Paragraph pe = new Paragraph("   ");
                    //document.Add(pe);
                    // string imageURL = @"D:\Dasharath\Pie\PieReports\PieReports\wwwroot\images\logo.jpeg";// Path.Combine(_env.WebRootFileProvider.GetFileInfo("/images/logo.jpeg").PhysicalPath);
                    //string imageURL = Path.Combine("~/images/logo.jpeg");
                    string imageURL = _config.GetValue<string>("ImagePath");
                    PdfPTable TableHeader = new PdfPTable(1);
                    TableHeader.WidthPercentage = 100;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(200f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 50f;
                    //Give some space after the image
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_LEFT;


                    Paragraph p = new Paragraph(header.Hdr_name + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p.Alignment = 1;
                    p.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cell_000 = new PdfPCell();
                    cell_000.AddElement(p);

                    Paragraph p1 = new Paragraph(header.hdr_address + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p1.Alignment = 1;
                    p1.Alignment = Element.ALIGN_RIGHT;
                    cell_000.Border = 0;
                    cell_000.AddElement(p1);
                    Paragraph p2 = new Paragraph(header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    p2.Alignment = 1;
                    p2.Alignment = Element.ALIGN_RIGHT;
                    cell_000.AddElement(p2);
                    cell_000.AddElement(jpg);
                    TableHeader.AddCell(cell_000);
                    document.Add(TableHeader);
                    //document.Add(p1);
                    // document.Add(p2);

                    //document.Add(jpg);
                    Paragraph pe = new Paragraph("   ");
                    document.Add(pe);

                    Paragraph pFin = new Paragraph("FINYEAR :- " + FinyearValue + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFin.Alignment = 1;
                    pFin.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFin);


                    Paragraph pFam = new Paragraph("Family Name :- " + FamilyName + "\n", FontFactory.GetFont("Roboto, sans-serif", 14, Font.BOLD, GrayColor.BLACK));
                    pFam.Alignment = 1;
                    pFam.Alignment = Element.ALIGN_LEFT;
                    document.Add(pFam);

                    Paragraph pheader = new Paragraph("   ");
                    document.Add(pheader);
                    PdfPTable Table = new PdfPTable(11);
                    Table.WidthPercentage = 100;

                    //PdfPTable Tableheader = new PdfPTable(1);
                    //Tableheader.WidthPercentage = 100;
                    //PdfPCell cell_header = new PdfPCell();
                    //cell_header.AddElement(new Paragraph(header.Hdr_name + header.hdr_address + "\n" + header.hdr_contact + "\n" + header.hdr_email + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tableheader.AddCell(cell_header);
                    //document.Add(Tableheader);
                    Paragraph pc0 = new Paragraph("Qty", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc0.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_0 = new PdfPCell();
                    cell_0.AddElement(pc0);
                    cell_0.Padding = 5;
                    Table.AddCell(cell_0);
                    Paragraph pc1 = new Paragraph("Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc1.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_1 = new PdfPCell();
                    cell_1.AddElement(pc1);
                    cell_1.Padding = 5;
                    Table.AddCell(cell_1);
                    Paragraph pc2 = new Paragraph("Rate", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc2.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_2 = new PdfPCell();
                    cell_2.AddElement(pc2);
                    cell_2.Padding = 5;
                    Table.AddCell(cell_2);
                    Paragraph pc3 = new Paragraph("Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc3.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_3 = new PdfPCell();
                    cell_3.AddElement(pc3);
                    cell_3.Padding = 5;
                    Table.AddCell(cell_3);
                    Paragraph pc4 = new Paragraph("Date", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc4.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_4 = new PdfPCell();
                    cell_4.AddElement(pc4);
                    cell_4.Padding = 5;
                    Table.AddCell(cell_4);
                    Paragraph pc5 = new Paragraph("Rate", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc5.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_5 = new PdfPCell();
                    cell_5.AddElement(pc5);
                    cell_5.Padding = 5;
                    Table.AddCell(cell_5);
                    Paragraph pc6 = new Paragraph("Amount", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc6.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_6 = new PdfPCell();
                    cell_6.AddElement(pc6);
                    cell_6.Padding = 5;
                    Table.AddCell(cell_6);
                    Paragraph pc7 = new Paragraph("Intraday", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc7.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_7 = new PdfPCell();
                    cell_7.AddElement(pc7);
                    cell_7.Padding = 5;
                    Table.AddCell(cell_7);
                    Paragraph pc8 = new Paragraph("Short term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc8.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_8 = new PdfPCell();
                    cell_8.AddElement(pc8);
                    cell_8.Padding = 5;
                    Table.AddCell(cell_8);
                    Paragraph pc9 = new Paragraph("Long term", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc9.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_9 = new PdfPCell();
                    cell_9.AddElement(pc9);
                    cell_9.Padding = 5;
                    Table.AddCell(cell_9);
                    Paragraph pc10 = new Paragraph("Total", FontFactory.GetFont("Roboto, sans-serif", 13, Font.BOLD, GrayColor.BLACK));
                    pc10.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cell_10 = new PdfPCell();
                    cell_10.AddElement(pc10);
                    cell_10.Padding = 5;
                    Table.AddCell(cell_10);

                    document.Add(Table);
                    PdfPTable Table1 = new PdfPTable(11);
                    Table1.WidthPercentage = 100;
                    Font font = new Font(FontFactory.GetFont("Roboto, sans-serif", 11, Font.NORMAL, GrayColor.BLACK));
                    foreach (var screen in realisedgainloss)
                    {
                        Phrase phrase0 = new Phrase(new Phrase(Convert.ToString(screen.buy_trn_qty), font));
                        PdfPCell cell0 = new PdfPCell(phrase0);
                        cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell0);
                        Phrase phrase1 = new Phrase(new Phrase(Convert.ToString(screen.buy_trn_date), font));
                        PdfPCell cell1 = new PdfPCell(phrase1);
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell1);
                        Phrase phrase2 = new Phrase(new Phrase(Convert.ToString(screen.buy_trn_rate), font));
                        PdfPCell cell2 = new PdfPCell(phrase2);
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell2);
                        Phrase phrase3 = new Phrase(new Phrase(Convert.ToString(screen.buy_trn_rate), font));
                        PdfPCell cell3 = new PdfPCell(phrase3);
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell3);
                        Phrase phrase4 = new Phrase(new Phrase(Convert.ToString(screen.sell_trn_date), font));
                        PdfPCell cell4 = new PdfPCell(phrase4);
                        cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell4);
                        Phrase phrase5 = new Phrase(new Phrase(Convert.ToString(screen.sell_trn_rate), font));
                        PdfPCell cell5 = new PdfPCell(phrase5);
                        cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell5);
                        Phrase phrase6 = new Phrase(new Phrase(Convert.ToString(screen.sell_trn_rate), font));
                        PdfPCell cell6 = new PdfPCell(phrase6);
                        cell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell6);
                        Phrase phrase7 = new Phrase(new Phrase(Convert.ToString(screen.it_pnl_gf), font));
                        PdfPCell cell7 = new PdfPCell(phrase7);
                        cell7.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell7);
                        Phrase phrase8 = new Phrase(new Phrase(Convert.ToString(screen.st_pnl), font));
                        PdfPCell cell8 = new PdfPCell(phrase8);
                        cell8.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell8);
                        Phrase phrase9 = new Phrase(new Phrase(Convert.ToString(screen.it_pnl), font));
                        PdfPCell cell9 = new PdfPCell(phrase9);
                        cell9.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell9);
                        Phrase phrase10 = new Phrase(new Phrase(Convert.ToString(screen.int_pnl), font));
                        PdfPCell cell10 = new PdfPCell(phrase10);
                        cell10.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        Table1.AddCell(cell10);


                        //Table1.AddCell(getCell(Convert.ToString(screen.buy_trn_qty), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.buy_trn_date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.buy_trn_rate), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.buy_trn_rate), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.sell_trn_date), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.sell_trn_rate), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.sell_trn_rate), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.it_pnl_gf), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.st_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.it_pnl), PdfPCell.ALIGN_CENTER));
                        //Table1.AddCell(getCell(Convert.ToString(screen.int_pnl), PdfPCell.ALIGN_CENTER));

                        //Table1.AddCell(Convert.ToString(screen.buy_trn_qty));
                        //Table1.AddCell(Convert.ToString(screen.buy_trn_date));
                        //Table1.AddCell(Convert.ToString(screen.buy_trn_rate));
                        //Table1.AddCell(Convert.ToString(screen.buy_trn_rate));
                        //Table1.AddCell(Convert.ToString(screen.sell_trn_date));
                        //Table1.AddCell(Convert.ToString(screen.sell_trn_rate));
                        //Table1.AddCell(Convert.ToString(screen.sell_trn_rate));
                        //Table1.AddCell(Convert.ToString(screen.it_pnl_gf));
                        //Table1.AddCell(Convert.ToString(screen.st_pnl));
                        //Table1.AddCell(Convert.ToString(screen.it_pnl));
                        //Table1.AddCell(Convert.ToString(screen.int_pnl));

                    }
                    document.Add(Table1);
                    Paragraph ps = new Paragraph("   ");
                    Paragraph ps1 = new Paragraph("   ");
                    document.Add(ps);
                    document.Add(ps1);
                    Paragraph pfoot = new Paragraph(header.ftr1 + header.ftr2 + header.ftr3 + header.ftr4, FontFactory.GetFont("Roboto, sans-serif", 13, Font.NORMAL, GrayColor.BLACK));
                    pfoot.Alignment = Element.ALIGN_JUSTIFIED;
                    document.Add(pfoot);
                    //PdfPTable Tablefooter = new PdfPTable(1);
                    //Tablefooter.WidthPercentage = 100;
                    //PdfPCell cell_footer = new PdfPCell();
                    //cell_footer.AddElement(new Paragraph(header.ftr2, FontFactory.GetFont("Arial", 10, Font.NORMAL, GrayColor.BLACK)));
                    //Tablefooter.AddCell(cell_footer);
                    //document.Add(Tablefooter);
                    document.Close();
                    writer.Close();


                    var content = memoryStream.ToArray();
                    return File(fileContents: content, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
           fileDownloadName: "" + rd.client_code + "_IncomeReport_" + DateTime.Now.ToString("dd-MMM-yyy") + ".pdf"
              );
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [HttpPost]
        public psp_dsp_report_page_setup pspdspreportpagesetupDetails()
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspreportpagesetupDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_report_page_setup rd = JsonConvert.DeserializeObject<psp_dsp_report_page_setup>(dtoq);

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_report_page_setup pspdspreportpagesetup = JsonConvert.DeserializeObject<psp_dsp_report_page_setup>(data1);
            return pspdspreportpagesetup;
        }
    }
    }
