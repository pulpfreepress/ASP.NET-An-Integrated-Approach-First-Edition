using System;
using System.Web.Security;
using System.Web.Configuration;


public class HashGenerator {

   public static void Main(string[] args){
     
	 
	 try{
	    string username = args[0];
		string password = args[1];
		string hashed_login = FormsAuthentication.HashPasswordForStoringInConfigFile(username + password, FormsAuthPasswordFormat.MD5.ToString());
		
		Console.WriteLine(hashed_login);
	 
	 
	 }catch(Exception){
	   Console.WriteLine("Usage: HashGenerator username password");
	   
	 
	 }
   
   
   }

}