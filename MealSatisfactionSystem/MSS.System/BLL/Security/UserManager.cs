using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MSS.Data.Entities.Security;
using MSS.System.DAL;
using MSS.System.DAL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;
using System.ComponentModel;
using MSS.Data.DTOs;
using MSS.Data.DTOs.Security;

namespace MSS.System.BLL.Security
{
    /* 
    CREATED:    P. Chavez         MAR 16 2018

    UserManager
    The UserManager class reads, creates and modifies data for the AspNetUsers table.
    This UserManager class inherits the UserManager class from Microsoft Asp.Net Identity.

    FIELDS:
    private const string STR_DEFAULT_FIRSTNAME - the default first name of the WebMaster
    private const string STR_DEFAULT_LASTNAME - the default last name of the WebMaster
    private const string STR_DEFAULT_PASSWORD - the default password of the WebMaster

    METHODS:
    public void AddAdministratorAccount() - adds an account named Administrator Account that has Administrator authorization 
    public void AddAccountUser(string firstName, string lastName, string email, string authLevelRole, int careSiteId, string password) - adds a new account and assigns the account to an authorization level role in the MSS database
    public string GenerateNewPassword() - creates a random 8 digit password containing numbers, uppercase and lowercase letters
    private string GenerateUserName(string firstName, string lastName) - creates a unique username based on first name, last name and the number of same names in the database
    public List<AspNetUserDTO> ListApplicationUsers(string searchResults) - this method returns a list of users that contain the given username
    public void ModifyAccount (string userName, string firstName, string lastName, string email, int careSiteId, string oldRole, string newRole) - this method changes the selected account's first name, last name, email, care site ID, and role 
    */
    [DataObject]
    public class UserManager : UserManager<ApplicationUser, string>
    {
        #region AdministratorAccount Constants
        private const string STR_DEFAULT_FIRSTNAME = "Administrator";
        private const string STR_DEFAULT_LASTNAME = "Account";
        private const string STR_DEFAULT_PASSWORD = "Password1";
        #endregion

        public UserManager()
            : base(new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(new ApplicationDbContext()))
        {
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        /* 
        CREATED:    P. Chavez         MAR 22 2018

        AddAdministratorAccount()
        This method adds an account named Administrator Account that has Administrator authorization to the AspNetUsers table if is is not already there. 
        This method is called when the project is executed after a build/rebuild of the project (Global.asax).

        PARAMETERS:     
        None

        RETURNS: 
        void

        ODEV METHOD CALLS: 
        None
        */
        public void AddAdministratorAccount()
        {
            if (!Users.Any(user => user.UserName.Equals((STR_DEFAULT_FIRSTNAME + STR_DEFAULT_LASTNAME).ToLower())))
            {
                var newWebMaster = new ApplicationUser
                {
                    UserName = string.Format("{0}{1}", STR_DEFAULT_FIRSTNAME, STR_DEFAULT_LASTNAME).ToLower(),
                    Email = string.Format("{0}.{1}@email.com", STR_DEFAULT_FIRSTNAME, STR_DEFAULT_LASTNAME).ToLower(),
                    EmailConfirmed = true,
                    firstname = STR_DEFAULT_FIRSTNAME,
                    lastname = STR_DEFAULT_LASTNAME,
                    activeyn = true
                };

                this.Create(newWebMaster, STR_DEFAULT_PASSWORD);

                this.AddToRole(newWebMaster.Id, AuthorizationLevelRoles.Administrator);
            }
        }

        /* 
        CREATED:    P. Chavez         MAR 23 2018

        AddAccountUser()
        This method adds a new account and assigns the account to an authorization level role in the MSS database.

        PARAMETERS:     
        string firstName - the first name of the account
        string lastName - the last name of the account
        string email - the account's email
        string authLevelRole - the assigned authorization level role given to the account
        int careSiteId - the care site ID the account is associated with
        string password - the password for the account

        RETURNS: 
        ApplicationUser - returns the new account 

        ODEV METHOD CALLS: 
        GenerateUserName()
        */
        public ApplicationUser AddAccountUser(string firstName, string lastName, string email, string authLevelRole, int careSiteId, string password)
        {
            string newUserName = GenerateUserName(firstName, lastName);

            var newAccountUser = new ApplicationUser()
            {
                UserName = newUserName,
                firstname = firstName,
                lastname = lastName,
                Email = email,
                EmailConfirmed = true,
                activeyn = true
            };

            if (careSiteId != 0)
            {
                newAccountUser.caresiteid = careSiteId;
            }

            this.Create(newAccountUser, password);

            this.AddToRole(newAccountUser.Id, authLevelRole);

            return newAccountUser;
        }


        /* 
        CREATED:    P. Chavez         MAR 23 2018

        GenerateNewPassword()
        This method creates a random 8 character password containing numbers, uppercase and lowercase letters.

        PARAMETERS:     
        None

        RETURNS: 
        string - the new password

        ODEV METHOD CALLS: 
        None
        */
        public string GenerateNewPassword()
        {
            Random rnd = new Random();
            string newPassword = Membership.GeneratePassword(8, 0);
            newPassword = Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => rnd.Next(0,10).ToString());

            string firstChar = newPassword.Substring(0, 1).ToUpper();

            // If its a number, replace it with capital A
            if (Regex.IsMatch(firstChar, @"\d"))
            {
                firstChar = "A";
            }

            newPassword = newPassword.Remove(0, 1).Insert(0, firstChar);
            return newPassword;
        }

        /* 
        CREATED:    P. Chavez         MAR 23 2018

        GenerateUserName()
        This method creates a unique username based on first name, last name and the number of same names in the MSS database.

        PARAMETERS:     
        string firstName - the first name in the username
        string lastName - the last name in the username

        RETURNS: 
        string - the new username

        ODEV METHOD CALLS: 
        None
        */
        private string GenerateUserName(string firstName, string lastName)
        {
            // Remove whitespaces from name for the username
            firstName = firstName.Replace(" ", string.Empty);
            lastName = lastName.Replace(" ", string.Empty);
            // Check if username is taken
            var existingUserNames = from user in Users.ToList()
                                    where user.UserName.Contains(firstName.ToLower() + lastName.ToLower())
                                    select user;

            int numberOfSameNames = existingUserNames.Count();
            string newUserName;
            
            if (numberOfSameNames > 0)
            {
                newUserName = string.Format("{0}{1}{2}", firstName, lastName, numberOfSameNames + 1).ToLower();
            }
            else
            {
                newUserName = string.Format("{0}{1}{2}", firstName, lastName, 1).ToLower();
            }
            return newUserName;
        }

        /*
        CREATED:    E.Lautner MAR 27 2018

        ListApplicationUsers()
        This method returns all users that start with the string given by the admin.

        PARAMETERS:     
        string searchResults - the specified string given by the user

        RETURNS: 
        List<ApplicationUserDTO> - a list of users

        ODEV METHOD CALLS: 
        None
        */

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ApplicationUserDTO> ListApplicationUsers(string searchResults)
        {

            if (searchResults == null)
            {
                searchResults = "";
            }

            var rm = new RoleManager();
            List<ApplicationUserDTO> results = new List<ApplicationUserDTO>();
            var tempresults = from x in Users.ToList()
                              where (x.UserName.Contains(searchResults) || (x.firstname + " " + x.lastname).Contains(searchResults))  && x.activeyn == true && x.UserName != ("administratoraccount")
                              orderby x.UserName
                              select new ApplicationUserDTO
                              {
                                  firstname = x.firstname,
                                  lastname = x.lastname,
                                  activeyn = x.activeyn,
                                  username = x.UserName
                              };
            return tempresults.ToList();
            
        }


        /*
        CREATED:    E.Lautner    APR 1 2018

        ModifyAccount()
        This method updates the account with the information given.

        PARAMETERS:     
        string userName - the username of the account being modified
        string firstName - the new first name of the account
        string lastName - the new last name of the account
        string email - the new email of the account
        int careSiteId - the new care site ID of the account
        string oldRole - the old role of the account
        string newRole - the new role of the account

        RETURNS: 
        None

        ODEV METHOD CALLS: 
        GenerateUserName()
        */

        public string ModifyAccount (string userName, string firstName, string lastName, string email, int careSiteId, string oldRole, string newRole)
        {
            

            UserManager userManager = new UserManager();
            var selectedUser = userManager.FindByName(userName);

            if (careSiteId == 0)
            {
                selectedUser.caresiteid = null;
            }
             else
            {
                selectedUser.caresiteid = careSiteId;
            }
            if (selectedUser.firstname == firstName && selectedUser.lastname == lastName)
            {
                selectedUser.UserName = userName;
            }
            else
            {
                string newUserName = userManager.GenerateUserName(firstName, lastName);
                selectedUser.UserName = newUserName;
            }
      
            selectedUser.firstname = firstName;
            selectedUser.lastname = lastName;
            selectedUser.Email = email;

            userManager.RemoveFromRole(selectedUser.Id, oldRole);
            userManager.AddToRole(selectedUser.Id, newRole);

            userManager.Update(selectedUser);
            return selectedUser.UserName;    

        }
    }
}