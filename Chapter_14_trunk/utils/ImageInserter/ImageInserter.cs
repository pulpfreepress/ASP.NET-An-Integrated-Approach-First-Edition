using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Data.Common;

using System.Data.Sql;
using System.Data.SqlClient;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


public class ImageInserter {

   public static void Main(String[] args){
   
      const String PICTURE = "@picture";  
     	
      const string UPDATE_PICTURE =
	   "UPDATE tbl_Employee " +
	   "SET Picture = " + PICTURE + " " +
	   "WHERE EmployeeID = 1";
	   
	   Image 			picture = null;
	   MemoryStream		ms		= null; 
		
	   
   
       if(args.Length > 0){
	     // Attempt to load image provided
         picture = new Bitmap(args[0]);
	   } else {
	     // If no image provided, load ReferenceImage.tif
		 picture = new Bitmap("ReferenceImage.tif");
		}
		
		try{
		ms = new MemoryStream();
		picture.Save(ms, ImageFormat.Tiff);
		byte[] byte_array = ms.ToArray();
		//create a connection to the database and insert image into first record
		
		Database db = DatabaseFactory.CreateDatabase();
		DbCommand command = db.GetSqlStringCommand(UPDATE_PICTURE);
		db.AddInParameter(command, PICTURE, DbType.Binary, byte_array);
		db.ExecuteNonQuery(command);
		Console.WriteLine("Image updated for 1st record in the database successfully!");
		}catch(Exception e){
		  Console.WriteLine(e);
		}
		
    } // end Main()
} // emd ImageInserter class