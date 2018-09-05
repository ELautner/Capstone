using System;
using System.Collections.Generic;
using System.ComponentModel;
using MSS.Data;
using System.Data.Entity;
using MSS.System.DAL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSS.Data.Entities;

namespace MSS.System.BLL
{
    /*
    AUTHOR: Patrick Chavez
    DATE CREATED: MARCH 4 2018

    ManagementAccountController
    The ManagementAccountController class purpose is to use methods to insert, update, delete or retrieve data from the Management Account table in the MSS database.

    METHODS:
    public void AddManagementAccount(managementaccount tempManagementAccount) 
    public void DeleteManagementAccount(managementaccount tempManagementAccount)
    public managementaccount GetManagementAccountDetails(int managementAccountID) 
    public List<managementaccount> GetManagementAccounts()
    public Boolean LoginManagement(string attemptedUserName, string attemptedUserPassword)
    public void UpdateManagementAccount(managementaccount updatedManagementAccount)

    */

    [DataObject]
    class ManagementAccountController
    {
        /*
        AUTHOR: Patrick Chavez
        DATE CREATEED: MARCH 4 2018
        
        AddManagementAccount()
        This method adds a new management account to the database.

        PARAMETERS:
        managementaccount tempManagementAccount - The management account object that contains the new details for the new account.

        RETURNS: void, N/A

        METHOD CALLS: N/A 

        */
        // ManagementAccount needs a POCO
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddManagementAccount(managementaccount tempManagementAccount)
        {
            using (var context = new MSSContext())
            {
                managementaccount newManagementAccount = new managementaccount();
                newManagementAccount.firstname = tempManagementAccount.firstname;
                newManagementAccount.lastname = tempManagementAccount.lastname;
                newManagementAccount.email = tempManagementAccount.email;

                //Need to check if a name already exists by counting how many of the same names are in the database
                int numOfNames = 0;
                numOfNames = (from ma in context.managementaccounts
                              where ma.firstname == tempManagementAccount.firstname &&
                              ma.lastname == tempManagementAccount.lastname
                              select ma).Count();
                
                if (numOfNames == 0 )
                {
                    newManagementAccount.username = (tempManagementAccount.firstname + tempManagementAccount.lastname).ToLower();
                }
                else
                {
                    // if there is a least one other person with the same name, the UserName is set with the value of numOfNames for the UserName to be Unique
                    newManagementAccount.username = (tempManagementAccount.firstname + tempManagementAccount.lastname + numOfNames).ToLower();
                }
                newManagementAccount.userpassword = "Defaultpass1"; // unsure how the password is going to be generated
                newManagementAccount.activeyn = true;
                newManagementAccount.authorizationlevelid = tempManagementAccount.authorizationlevelid;

                context.managementaccounts.Add(newManagementAccount);
                context.SaveChanges();
            }
        }

        /*
        AUTHOR: Patrick Chavez
        DATE CREATEED: MARCH 4 2018

        DeleteManagementAccount()
        This method sets a management account inactive. I.e the management account activeyn field set to false.

        PARAMETERS:
        managementaccount tempManagementAccount - The management account that will be set to inactive

        RETURNS: void, N/A

        METHOD CALLS: N/A 

        */
        // ManagementAccount needs a POCO
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void DeleteManagementAccount(managementaccount tempManagementAccount)
        {
            using (var context = new MSSContext())
            {
                var managementaccount = context.managementaccounts.Find(tempManagementAccount.managementaccountid);
                managementaccount.activeyn = false;
                var existingManagementAccount = context.Entry(managementaccount);
                existingManagementAccount.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /*
        AUTHOR: Patrick Chavez
        DATE CREATEED: MARCH 4 2018

        GetManagementAccountDetails()
        This method retrieves a single management account by using managementaccountid field.

        PARAMETERS:
        managementaccount managementAccountID - The ID of the management account being retrieved

        RETURNS: managementaccount - A single management account with all of the account details

        METHOD CALLS: N/A 

        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public managementaccount GetManagementAccountDetails(int managementAccountID)  
        {
            using (var context = new MSSContext())
            {
                var managementaccount = context.managementaccounts.Find(managementAccountID);
                return managementaccount;
            }
        }

        /*
        AUTHOR: Patrick Chavez
        DATE CREATEED: MARCH 4 2018

        GetManagementAccounts()
        This method retrieves all management accounts in the MSS database.

        PARAMETERS: N/A

        RETURNS: List<managementaccount> - A list of all management accounts

        METHOD CALLS: N/A 

        */
        // Unsure if all of the managementaccounts or ones specific to a care site
        // Also, should this retrieve all accounts or only accounts that are active
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<managementaccount> GetManagementAccounts() 
        {
            using (var context = new MSSContext())
            {
                return context.managementaccounts.ToList();
            }
        }

        /*
        AUTHOR: Patrick Chavez
        DATE CREATEED: MARCH 4 2018

        LoginManagement()
        This method checks if a UserName and UserPassword is correctly matched with the same values as a record in the MSS database.
        Also checks if the management account is active.

        PARAMETERS:
        string attemptedUserName - The UserName that will be checked in the database
        string attemptedUserPassword - The UserPassword that will be check in the database

        RETURNS: successfulLogin - A boolean that determines if the management acount is in the database. 
                                   If the boolean is true, the management account is in the database otherwise the boolean is false.

        METHOD CALLS: N/A 

        */
        // Unsure if need this method
        // Unsure what the return value should be after succesfful/unsucessful login
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Boolean LoginManagement(string attemptedUserName, string attemptedUserPassword)
        {
            Boolean succesfulLogin = false;

            using (var context = new MSSContext())
            {
                managementaccount managementAccountExists = (from ma in context.managementaccounts
                                                             where ma.username == attemptedUserName &&
                                                             ma.userpassword == attemptedUserPassword &&
                                                             ma.activeyn == true
                                                             select ma).FirstOrDefault();
                if (managementAccountExists == null)
                {
                    throw new Exception("Invalid Username or Password. Please contact your System Administrator for further assistance");
                }
                else
                {
                    succesfulLogin = true;
                }
            }

            return succesfulLogin;
        }

        /*
        AUTHOR: Patrick Chavez
        DATE CREATEED: MARCH 4 2018

        UpdateManagementAccount()
        This method updates a single management account's password, authorizatrionlevel and caresite.

        PARAMETERS:
        managementaccount updatedManagementAccount - The management account that is going to be updated.

        RETURNS: void, N/A

        METHOD CALLS: N/A 

        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateManagementAccount(managementaccount updatedManagementAccount)
        {
            using (var context = new MSSContext())
            {
                var managementaccount = context.managementaccounts.Find(updatedManagementAccount.managementaccountid);
                managementaccount.userpassword = updatedManagementAccount.userpassword;
                managementaccount.authorizationlevelid = updatedManagementAccount.authorizationlevelid;
                managementaccount.caresites = updatedManagementAccount.caresites;
                var existingManagementAccount = context.Entry(managementaccount);
                existingManagementAccount.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
