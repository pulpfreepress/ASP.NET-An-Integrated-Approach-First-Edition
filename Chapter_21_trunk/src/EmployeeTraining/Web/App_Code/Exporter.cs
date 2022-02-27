using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Web.App_Code {
    public class Exporter {


        #region Constants

        public enum FileType { Excel, Word };
        private const String RESPONSE_CONTENT_DISPOSITION = "Content-Disposition";
        private const String RESPONSE_FILENAME = "attachment;filename=";
        private const String YYYYMMDDHHMM_FORMAT = "yyyyMMddHHmm";
        private const String EXCEL = ".xls";
        private const String WORD = ".doc";
        private const String EXCEL_CONTENT_TYPE = "application/vnd.xls";
        private const String WORD_CONTENT_TYPE = "application/msword.doc";
        #endregion


        #region Private Fields
        private DateTime _timeStamp = DateTime.Now;
        private Dictionary<FileType, String> _fileExtensions;
        private Dictionary<FileType, String> _httpContentTypes;
        #endregion

        #region Properties


        /// <summary>
        /// Defaults to "Report".  The name of the file containing the exported data.
        /// </summary>
        private String _exportFileName = "Report";
        public String ExportFileName {
            get { return _exportFileName; }
            set { _exportFileName = value; }
        }

        /// <summary>
        /// Defaults to YYMMDDHHMM.  The Format for the date appended to the ReportName.
        /// </summary>
        private String _timeStampFormat = YYYYMMDDHHMM_FORMAT;
        public String TimeStampFormat {
            get { return _timeStampFormat; }
            set { _timeStampFormat = value; }
        }

        /// <summary>
        /// Defaults to Excel.  The type of file to be exported.
        /// </summary>
        private FileType _typeOfFile = FileType.Excel;   //Excel is the Default
        public FileType TypeOfFile {
            get { return _typeOfFile; }
            set { _typeOfFile = value; }
        }

        #endregion

        #region Constructors

        public Exporter():this(FileType.Excel) { }

        public Exporter(FileType filetype) {
            TypeOfFile = filetype;
            _fileExtensions = new Dictionary<FileType, String>();
            _fileExtensions.Add(FileType.Excel, EXCEL);
            _fileExtensions.Add(FileType.Word, WORD);

            _httpContentTypes = new Dictionary<FileType, String>();
            _httpContentTypes.Add(FileType.Excel, EXCEL_CONTENT_TYPE);
            _httpContentTypes.Add(FileType.Word, WORD_CONTENT_TYPE);
        }

        #endregion

        public void ExportReportData(GridView gridView, String exportFileName) {

            String timeStamp = DateTime.Now.ToString(TimeStampFormat);
            HttpResponse response = gridView.Page.Response;
            response.Clear();
            response.AddHeader(RESPONSE_CONTENT_DISPOSITION,
                                RESPONSE_FILENAME +
                                exportFileName +
                                timeStamp +
                                _fileExtensions[TypeOfFile]);
            response.Charset = "";
            response.ContentType = _httpContentTypes[TypeOfFile];

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            gridView.AllowPaging = false;
            gridView.AllowSorting = false;

            gridView.DataBind();
            gridView.RenderControl(htmlWrite);
            response.Write(stringWrite.ToString());
            response.End();
        }
    }
}