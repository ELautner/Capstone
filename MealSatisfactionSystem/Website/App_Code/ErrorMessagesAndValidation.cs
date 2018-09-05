using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/* 
CREATED:    C. Stanhope		MAR 24 2018

ErrorMessagesAndValidation
Used to access methods for validation and creating error messages.

FIELDS:
None

METHODS:
public ErrorMessagesAndValidation() - constructor, currently does nothing other than create an instance of ErrorMessagesAndValidation
public string ErrorList(List<string> errorList) - takes a list of field errors and turns them into a list that can be displayed as a user message
public string List(List<string> strings) - takes a list of strings and turns them into a grammatically correct list in English
*/
public class ErrorMessagesAndValidation
{
    /* 
    CREATED:    C. Stanhope         MAR 24 2018

    ErrorMessagesAndValidation()
    The ErrorMessagesAndValidation constructor. Creates an instance of ErrorMessagesAndValidation.

    PARAMETERS: 	
    None

    RETURNS: 
    None

    ODEV METHOD CALLS:
    None
    */
    public ErrorMessagesAndValidation()
    {
        
    }

    /* 
    CREATED:    C. Stanhope         MAR 24 2018

    ErrorList()
    Takes a list of field errors and turns them into a list that can be displayed as a user message. Accounts for English grammar and creates a list of errors accordingly.

    PARAMETERS: 	
    List<string> errorList - the list of fields that are incorrect and should appear in the error list

    RETURNS: 
    string - the message to be output to the user

    ODEV METHOD CALLS:
    None
    */
    public string ErrorList(List<string> errorList)
    {
        string message;

        if (errorList.Count() == 1) // if only one error in list
        {
            message = "Please provide input for the " + errorList[0] + " field in the form.";
        }
        else if (errorList.Count == 2) // if two errors in list
        {
            message = "Please provide input for the " + errorList[0] + " and " + errorList[1] + " fields in the form.";
        }
        else // more than two errors
        {
            string errorListMessage = "";

            for (int i = 0; i < errorList.Count(); i++)
            {
                if (i == errorList.Count() - 1) // if last error in list
                {
                    errorListMessage += "and " + errorList[i];
                }
                else
                {
                    errorListMessage += errorList[i] + ", ";
                }
            }

            message = "Please provide input for the " + errorListMessage + " fields in the form.";
        }

        return message;
    }

    /* 
    CREATED:    C. Stanhope         APR 15 2018

    List()
    Takes a list of strings and turns them into a list that can be displayed as a user message. Accounts for English grammar and creates a list of strings accordingly.

    PARAMETERS: 	
    List<string> strings - the list of strings that need to be turned into a grammatical list

    RETURNS: 
    string - the strings in a proper sentence list

    ODEV METHOD CALLS:
    None
    */
    public string List(List<string> strings)
    {
        string message = "";

        if (strings.Count() == 1) // if only one error in list
        {
            message = strings[0];
        }
        else if (strings.Count == 2) // if two errors in list
        {
            message = strings[0] + " and " + strings[1];
        }
        else // more than two errors
        {
            for (int i = 0; i < strings.Count(); i++)
            {
                if (i == strings.Count() - 1) // if last error in list
                {
                    message += "and " + strings[i];
                }
                else
                {
                    message += strings[i] + ", ";
                }
            }
        }

        return message;
    }
}